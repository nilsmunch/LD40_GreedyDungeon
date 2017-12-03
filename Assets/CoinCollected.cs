using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollected : SceneActor
{
    Rigidbody2D rig;
    CircleCollider2D coll;
    float collectCooldown = 0f;

    private void Start()
    {
        addToActorList();
        collectCooldown = 1f;
        rig = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        coll.isTrigger = (rig.velocity.x == 0 && rig.velocity.y == 0);
        gameObject.layer = (coll.isTrigger ? LayerMask.NameToLayer("Default") : LayerMask.NameToLayer("FlyingCoin"));
        if (collectCooldown > 0) collectCooldown -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (collectCooldown > 0) return;

        if (other.gameObject.tag != "Player") return; 
        StartCoroutine(coinGrab());
    }

    IEnumerator coinGrab() {
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        PlayerCharacterWalking.pickupCoin();
        GetComponent<SpriteRenderer>().enabled = false;
        SoundeffectsManager.PlayEffect("coin");
    }
}
