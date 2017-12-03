using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldTracker : MonoBehaviour {
    Text label;
    private void Start()
    {
        label = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update () {
        label.text = PlayerCharacterWalking.coins.ToString();

    }
}
