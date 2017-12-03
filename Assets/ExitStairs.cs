using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitStairs : MonoBehaviour {

    static public bool gamePaused = false;
    public GameObject bgm;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            gamePaused = true;
            SceneManager.LoadSceneAsync("GameOver",LoadSceneMode.Additive);

            GameObject.Destroy(bgm);

            //   print("Game over");
        }
    }
}
