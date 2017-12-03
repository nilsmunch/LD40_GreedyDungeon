using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCharacterWalking : SceneActor
{
    public static float RadiusPerCoin = 0.55f;


    static public PlayerCharacterWalking playerToonScript;
    static public GameObject playerToon;

    public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Phys

    LineRenderer hateRange;
    SpriteRenderer mySprite;

    float runcycle;

    public Sprite standing;
    public Sprite running;
    public Sprite running_alt;

    static public float hateDistance = 1f;

    Vector2 playervelocity;

    float damageCooldown = 0f;

    static public int hitPoints = 3;

    public GameObject coinDropped;

    static public int coins = 0;

    public float ThetaScale = 0.01f;
    private int Size;
    private float Theta = 0f;
    

    static public void pickupCoin() {
        hateDistance += RadiusPerCoin;
        coins += 1;
    }


    public bool TakeDamage() {

        if (damageCooldown > 0f) return false; 
        mySprite.color = Color.red;
        damageCooldown = 0.1f;

        SoundeffectsManager.PlayEffect("slap");


#if UNITY_EDITOR
       // return true;
#endif
        hitPoints -= 1;

        if (hitPoints <= 0) {
            dead = true;
            hateDistance = 0;
            mySprite.color = Color.black;
            StartCoroutine(restartLevel());
            SoundeffectsManager.PlayEffect("death");
        }
        return true;
    }

    IEnumerator restartLevel() {
        yield return new WaitForSeconds(2);
        SceneActor.resetPosition();
        mySprite.color = Color.white;
        hateDistance = 1f;
        hitPoints = 3;
        coins = Savepoint.StoredGold;
    }


    void Start()
    {
        addToActorList();
        Application.targetFrameRate = 60;
        playerToonScript = this;
        playerToon = this.gameObject;
        rb2d = GetComponent<Rigidbody2D>();
        hateRange = GetComponentInChildren<LineRenderer>();
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.color = Color.white;
        hateDistance = 1f;
        hitPoints = 3;
        coins = 0;

        ExitStairs.gamePaused = false;
        dead = false;
    }

    private void Update()
    {
        if (ExitStairs.gamePaused) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Lobby");
        }
            if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space)) {
            if (coins <= 0) return;
            coins--;
            hateDistance -= RadiusPerCoin;
            GameObject dropped = GameObject.Instantiate(coinDropped,transform.position,Quaternion.identity,null);
            Rigidbody2D dropflow = dropped.GetComponent<Rigidbody2D>();// dropped.AddComponent<Rigidbody2D>();
            dropflow.AddForce(playervelocity*30f);
            dropped.GetComponent<CoinCollected>().dontRespawnMe = true;
        }
    }

    void FixedUpdate()
    {
        if (PlayerCharacterWalking.playerToon == null) return;
        if (ExitStairs.gamePaused) return;
        if (dead) return;
        Size = (int)Mathf.FloorToInt(((1f / ThetaScale) + 1f) );
        hateRange.positionCount = Size;
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = hateDistance * Mathf.Cos(Theta);
            float y = hateDistance * Mathf.Sin(Theta);
            hateRange.SetPosition(i, new Vector3(x+ rb2d.position.x, y+ rb2d.position.y, 0));
        }

        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
            return;
        }
        mySprite.color = Color.white;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        if ((moveHorizontal + moveVertical) != 0f)
        {
            mySprite.sprite = (Mathf.FloorToInt(runcycle) == 0 ? running : (Mathf.FloorToInt(runcycle) == 2 ? running_alt : standing));
            runcycle += Time.fixedDeltaTime * 4;
            if (runcycle >= 4) runcycle -= 4;
        }
        else
        {
            runcycle = 0;
            mySprite.sprite = standing;
        }

        if ((moveHorizontal + moveVertical) == 0f) return;
        if ((moveHorizontal) != 0f) mySprite.flipX = (moveHorizontal > 0);

        float speedMod = 1;// 0+Mathf.Pow(0.8f, coins);

        playervelocity = new Vector2(moveHorizontal, moveVertical) * 5f;
        rb2d.MovePosition(rb2d.position + playervelocity * speedMod * Time.fixedDeltaTime);
    }
}
