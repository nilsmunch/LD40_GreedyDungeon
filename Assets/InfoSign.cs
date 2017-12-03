using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoSign : MonoBehaviour {
    public string signSays;
    public Text signspace;
    static InfoSign showingSign;
    SpriteRenderer render;

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate ()
    {
        if (!render.isVisible) return;
        if (signspace == null) return;
        if (PlayerCharacterWalking.playerToon == null) return;
        Vector2 goodguyThisWay = PlayerCharacterWalking.playerToon.transform.position - transform.position;
        if (goodguyThisWay.magnitude < 2)
        {
            signspace.text = signSays;
            foreach (Text tx in signspace.GetComponentsInChildren<Text>()) {
                tx.text = signSays;
            }
            showingSign = this;
            return;
        }
        if (showingSign == this) showingSign = null;
        if (showingSign != null) return;

        signspace.text = "";
        foreach (Text tx in signspace.GetComponentsInChildren<Text>())
        {
            tx.text = "";
        }
    }
}
