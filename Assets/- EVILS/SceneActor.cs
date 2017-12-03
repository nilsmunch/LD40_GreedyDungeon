using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneActor : MonoBehaviour {

    static List<SceneActor> all = new List<SceneActor>();
    public Vector2 originPosition;
    public bool dead = false;
    public bool dontRespawnMe = false;

    // Use this for initialization
    void Awake ()
    {
        originPosition = this.transform.position;
    }

    protected void addToActorList() {

        all.Add(this);
    }

    public void resetToNormal()
    {
        if (dontRespawnMe)
        {
            StartCoroutine(clearFromList());
            return;
        }
        transform.position = originPosition;
        dead = false;
        GetComponent<SpriteRenderer>().enabled = true;
        if (GetComponent<CircleCollider2D>() != null) GetComponent<CircleCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator clearFromList()
    {
        yield return new WaitForSeconds(1);
        GameObject.Destroy(this.gameObject);
        all.Remove(this);
    }
	
	// Update is called once per frame
	static public void resetPosition () {
        foreach (SceneActor sc in all) {
            if (sc.gameObject == null) continue;
            sc.resetToNormal();
        }
	}

    public void Death()
    {
        dead = true;
        StartCoroutine(deathAnimation());
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    IEnumerator deathAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
    }
}
