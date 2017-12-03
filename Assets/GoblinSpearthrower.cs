using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinSpearthrower : SceneActor
{

    private Rigidbody2D rb2d;

    public Sprite sleeping;
    public Sprite hunting;

    public Transform[] routePoints;
    LineRenderer hateRange;

    Vector2 targetPoint;

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
        hateRange = GetComponentInChildren<LineRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        mySprite = GetComponent<SpriteRenderer>();
        Charge = 0f;
        targetPoint = routePoints[0].position;
        addToActorList();
    }

    void FixedUpdate()
    {
        if (!mySprite.isVisible) return;
        if (PlayerCharacterWalking.playerToon == null) return;
        if (ExitStairs.gamePaused) return;
        if (dead) {

            hateRange.positionCount = 0;
            return; }
        if (attackCooldown > 0f)
        {
            mySprite.sprite = hunting;
            attackCooldown -= Time.fixedDeltaTime;
            return;
        }
        mySprite.sprite = sleeping;

        attackRange = 1f+ Charge * 3f;

            Size = (int)Mathf.FloorToInt(((1f / ThetaScale) + 1f));
            hateRange.positionCount = Size;
            for (int i = 0; i < Size; i++)
            {
                Theta += (2.0f * Mathf.PI * ThetaScale);
                float x = attackRange * Mathf.Cos(Theta);
                float y = attackRange * Mathf.Sin(Theta);
                hateRange.SetPosition(i, new Vector3(x + rb2d.position.x, y + rb2d.position.y, 0));
            }


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

    }

    void AttackPlayer()
    {
        SoundeffectsManager.PlayEffect("lich_attack");
        attackCooldown = 0.4f;
        //Charge = 0f;
        if (!PlayerCharacterWalking.playerToonScript.TakeDamage()) return;
        float damageForce = 800f;
        //Charge = 0f;
        //rb2d.MovePosition(direction * damageForce * -1);
        PlayerCharacterWalking.playerToon.GetComponent<Rigidbody2D>().AddForce(direction * damageForce);
    }
}
