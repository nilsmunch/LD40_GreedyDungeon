using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : SceneActor
{

    private Rigidbody2D rb2d;

    public Sprite sleeping;
    public Sprite hunting;

    public Transform[] routePoints;

    Transform targetPoint;

    Vector2 direction;
    SpriteRenderer mySprite;

    float attackCooldown = 0f;

    float Charge = 2f;

    float attackRange = 0f;

    bool charging;
    private float ThetaScale = 0.01f;
    private int Size;
    private float Theta = 0f;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        Charge = 0f;
        targetPoint = routePoints[0];
        addToActorList();
    }

    void FixedUpdate()
    {
        if (!mySprite.isVisible) return;
        if (PlayerCharacterWalking.playerToon == null) return;
        if (ExitStairs.gamePaused) return;
        if (dead)
        {
            return;
        }
        if (attackCooldown > 0f)
        {
           // mySprite.sprite = hunting;
            attackCooldown -= Time.fixedDeltaTime;
            return;
        }

        float speedboost = 1f;
        Vector2 goodguyThisWay = PlayerCharacterWalking.playerToon.transform.position - transform.position;

        Vector2 walkThisWay = (Vector2)targetPoint.position - (Vector2)transform.position;
        if (goodguyThisWay.magnitude <= PlayerCharacterWalking.hateDistance)
        {
            walkThisWay = (Vector2)PlayerCharacterWalking.playerToon.transform.position - (Vector2)transform.position;
            if (goodguyThisWay.magnitude < 0.8f)
            {
                AttackPlayer();
                return;
            }
            speedboost = (PlayerCharacterWalking.coins*0.2f) + 1f;
        }

        Vector2 pointDistance = (Vector2)routePoints[0].position - (Vector2)routePoints[1].position;
        if (pointDistance.magnitude < 1.4f)
        {
            foreach (Transform tr in routePoints)
            {
                if (tr.GetComponent<Gargoyle>() != null) tr.GetComponent<Gargoyle>().Death();
                if (tr.GetComponent<BarrelDoodad>() != null) tr.GetComponent<BarrelDoodad>().Death();
            }

            SoundeffectsManager.PlayEffect("gargoyle_death");
            Death();
            return;
        }


        var distance = walkThisWay.magnitude;
        direction = walkThisWay / distance;

        if (distance < 1f) {
            foreach (Transform ral in routePoints)
            {
                if (ral != targetPoint) {
                    targetPoint = ral; break;
            }
            }
        }

        float monsterWalkSpeed = 0.5f + (0.3f * PlayerCharacterWalking.hateDistance) * speedboost;
        rb2d.MovePosition(rb2d.position + direction * Time.fixedDeltaTime * monsterWalkSpeed);
        mySprite.flipX = (direction.x > 0);

        /*

        mySprite.sprite = sleeping;

        attackRange = 1f + Charge * 3f;

        Vector2 goodguyThisWay = PlayerCharacterWalking.playerToon.transform.position - transform.position;

        if (goodguyThisWay.magnitude > PlayerCharacterWalking.hateDistance)
        {
            if (Charge > 0) Charge -= Time.deltaTime * 0.35f;
            mySprite.color = new Color(1f - Charge, 1f - Charge, 1f);
            return;
        }
        var distance = goodguyThisWay.magnitude;
        direction = goodguyThisWay / distance;

        if (goodguyThisWay.magnitude < attackRange)
        {
            AttackPlayer();
            return;
        }

        Charge += Time.deltaTime * 0.2f;
        mySprite.color = new Color(1f - Charge, 1f - Charge, 1f);
        mySprite.flipX = (direction.x > 0);
        */
    }

    void AttackPlayer()
    {
        SoundeffectsManager.PlayEffect("lich_attack");
        attackCooldown = 0.4f;
        if (!PlayerCharacterWalking.playerToonScript.TakeDamage()) return;
        float damageForce = 800f;
        PlayerCharacterWalking.playerToon.GetComponent<Rigidbody2D>().AddForce(direction * damageForce);
    }
}
