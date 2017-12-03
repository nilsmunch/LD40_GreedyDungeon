using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerHunter : SceneActor
{

    private Rigidbody2D rb2d;

    public Sprite sleeping;
    public Sprite hunting;

    Vector2 direction;
    SpriteRenderer mySprite;

    float attackCooldown = 0f;

    // Use this for initialization
    void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        addToActorList();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!mySprite.isVisible) return;

        if (PlayerCharacterWalking.playerToon == null) return;
        if (ExitStairs.gamePaused) return;
        if (attackCooldown > 0f) {
            attackCooldown -= Time.fixedDeltaTime;
            return;
        }

        Vector2 goodguyThisWay = PlayerCharacterWalking.playerToon.transform.position - transform.position;

        if (goodguyThisWay.magnitude > PlayerCharacterWalking.hateDistance) {
            mySprite.sprite = sleeping;
            return;
        }
        mySprite.sprite = hunting;

        if (goodguyThisWay.magnitude < 0.8f) {
            AttackPlayer();
            return;
        }

        var distance = goodguyThisWay.magnitude;
        direction = goodguyThisWay / distance;


        float monsterWalkSpeed = 0.5f + (0.3f * PlayerCharacterWalking.hateDistance);

        //Vector2 velocity = new Vector2(moveHorizontal, moveVertical) * 5f;
        rb2d.MovePosition(rb2d.position + direction * Time.fixedDeltaTime * monsterWalkSpeed);
        mySprite.flipX = (direction.x > 0);
    }

    void AttackPlayer() {
        if (!PlayerCharacterWalking.playerToonScript.TakeDamage()) return;
        float damageForce = 400f;
        attackCooldown = .5f;
        rb2d.AddForce(direction * damageForce * -1);
        //rb2d.MovePosition(direction * damageForce * -1);
        PlayerCharacterWalking.playerToon.GetComponent<Rigidbody2D>().AddForce(direction* damageForce);
    }
}
