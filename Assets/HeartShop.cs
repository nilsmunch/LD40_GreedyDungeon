using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartShop : SceneActor {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerCharacterWalking.coins -= 5;
            PlayerCharacterWalking.hateDistance -= 5*PlayerCharacterWalking.RadiusPerCoin;
            PlayerCharacterWalking.hitPoints += 1;

            SoundeffectsManager.PlayEffect("lifeup");

            Death();
            return;
        }
    }
}
