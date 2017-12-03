using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : SceneActor {

    Sprite dull;
    public Sprite awoken;
    public static int StoredGold;

    public bool activated;

    private void Start()
    {
        StoredGold = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (activated) return;
        if (collision.gameObject.GetComponent<PlayerCharacterWalking>() != null)
        {
            SoundeffectsManager.PlayEffect("lifeup");
            activated = true;
            GetComponent<SpriteRenderer>().sprite = awoken;
            collision.gameObject.GetComponent<PlayerCharacterWalking>().originPosition = collision.gameObject.transform.position;
            StoredGold = PlayerCharacterWalking.coins;
        }
    }
}
