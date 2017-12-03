using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour {
    public int OpensForCoins = 0;
    BoxCollider2D coll;

    public Sprite openGate;
    public Sprite closedGate;

    public SceneActor[] deathlist;

    public Savepoint savepoint;

    SpriteRenderer render;

    bool gateOpen;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        render = GetComponent<SpriteRenderer>();
    }

    bool CheckOpen() {
        if (savepoint != null) return savepoint.activated;


        bool shouldOpen = (OpensForCoins <= PlayerCharacterWalking.coins);
        if (deathlist.Length > 0)
        {
            shouldOpen = false;
            foreach (SceneActor act in deathlist)
            {
                if (!act.dead) return false;
            }
            return true;
        }
        return shouldOpen;
    }

    // Update is called once per frame
    void Update()
    {

        bool shouldOpen = CheckOpen();

        if (shouldOpen)
        {
            if (!gateOpen) {

                if (render.isVisible) SoundeffectsManager.PlayEffect("gateup");

                coll.enabled = false;
                render.sprite = openGate;
                gateOpen = true;
            }
        }
        else
        {
            coll.enabled = true;
            render.sprite = closedGate;
            gateOpen = false;
        }
    }
}
