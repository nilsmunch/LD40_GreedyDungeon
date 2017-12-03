using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firepit : MonoBehaviour
{
    SpriteRenderer mySprite;
    float runcycle = 0f;

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") {
            PlayerCharacterWalking.playerToonScript.TakeDamage();
                return; }

        if (other.gameObject.tag == "Badguys")
        {
            SoundeffectsManager.PlayEffect("gargoyle_death");
            if (other.GetComponent<Gargoyle>() != null)
            {
                other.gameObject.GetComponent<Gargoyle>().Death();
            }
            if (other.GetComponent<GoblinSpearthrower>() != null)
            {
                other.gameObject.GetComponent<GoblinSpearthrower>().Death();
            }
            if (other.GetComponent<BarrelDoodad>() != null)
            {
                other.gameObject.GetComponent<BarrelDoodad>().Death();
            }
            return;
        }
    }

    private void FixedUpdate()
    {
        runcycle += Time.fixedDeltaTime * 4;
        mySprite.flipX = (Mathf.FloorToInt(runcycle) == 0);

        if (runcycle >= 2) runcycle -= 2;
    }
}
