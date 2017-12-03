using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gargoyle : SceneActor
{
    private Rigidbody2D rb2d;

    public Sprite sleeping;
    public Sprite hunting;

    Vector2 direction;
    SpriteRenderer mySprite;

    float attackCooldown = 0f;

    float Charge = 0f;

    bool charging;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        Charge = 0f;
        addToActorList();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!charging) return;
        if (collision.gameObject.GetComponent<Gargoyle>() != null)
        {
            SoundeffectsManager.PlayEffect("gargoyle_death");
            collision.gameObject.GetComponent<Gargoyle>().Death();
            Death();
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!mySprite.isVisible) return;
        if (PlayerCharacterWalking.playerToon == null) return;
        if (ExitStairs.gamePaused) return;
        if (dead) return;
        if (attackCooldown > 0f)
        {
            attackCooldown -= Time.fixedDeltaTime;
            return;
        }

        if (charging) {

            Charge -= Time.deltaTime;

            if (Charge >= 0.6f)
            {
                mySprite.color = (mySprite.color == Color.red ? Color.white : Color.red);
                return;
            }

            float monsterWalkSpeed = 7.5f + (0.3f * PlayerCharacterWalking.hateDistance);

            //Vector2 velocity = new Vector2(moveHorizontal, moveVertical) * 5f;
            rb2d.MovePosition(rb2d.position + direction * Time.fixedDeltaTime * monsterWalkSpeed);
            mySprite.flipX = (direction.x > 0);


            Vector2 goodguyNearby = PlayerCharacterWalking.playerToon.transform.position - transform.position;
            if (goodguyNearby.magnitude < 0.8f)
            {
                AttackPlayer();
                Charge = 0;
                return;
            }

            if (Charge <= 0)
            {
                charging = false;
                gameObject.layer = LayerMask.NameToLayer("Movable");
            }
            mySprite.sprite = hunting;
            return;
        }
        mySprite.sprite = sleeping;

        Vector2 goodguyThisWay = PlayerCharacterWalking.playerToon.transform.position - transform.position;

        if (goodguyThisWay.magnitude > PlayerCharacterWalking.hateDistance)
        {
            if (Charge > 0) Charge -= Time.deltaTime * 0.15f;
            mySprite.color = new Color(1f, 1f - Charge, 1f - Charge);
            mySprite.sprite = sleeping;
            return;
        }

        Charge += Time.deltaTime * 0.2f;

        mySprite.color = new Color(1f,1f - Charge, 1f - Charge);

        if (Charge >= 1)
        {
            SoundeffectsManager.PlayEffect("gargoyle_dash");
            var distance = goodguyThisWay.magnitude;
            direction = goodguyThisWay / distance;
            charging = true;
            gameObject.layer = LayerMask.NameToLayer("Evil");
        }

        return;

        mySprite.sprite = hunting;


        
    }

    void AttackPlayer()
    {
        if (!PlayerCharacterWalking.playerToonScript.TakeDamage()) return;
        float damageForce = 400f;
        attackCooldown = .5f;
        rb2d.AddForce(direction * damageForce * -1);
        //rb2d.MovePosition(direction * damageForce * -1);
        PlayerCharacterWalking.playerToon.GetComponent<Rigidbody2D>().AddForce(direction * damageForce);
    }
}
