using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreManager : MonoBehaviour {

    public Text coinScore;

	// Use this for initialization
	void Start () {
        coinScore.text = PlayerCharacterWalking.coins.ToString() + " coins out of 26";
    }

    public void Replay() {
        SceneManager.LoadScene("Game");
    }

    public void Lore()
    {
        SceneManager.LoadScene("Background");
    }

    public void Quit()
    {
        Application.Quit();
    }



    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("Lobby");
    }
}
