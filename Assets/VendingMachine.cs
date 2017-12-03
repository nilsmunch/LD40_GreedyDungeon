using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour {

    public GameObject spawns;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CoinCollected>() != null) {
            collision.gameObject.GetComponent<CoinCollected>().Death() ;
            StartCoroutine(spawnBarrel());
        }
        if (collision.gameObject.GetComponent<Gargoyle>() != null)
        {
            StartCoroutine(spawnBarrel());
        }
    }

    IEnumerator spawnBarrel()
    {
        SoundeffectsManager.PlayEffect("coin");
        yield return new WaitForSeconds(1f);
        SoundeffectsManager.PlayEffect("vending");
        Vector2 spawnpoint = new Vector2(transform.position.x, transform.position.y-2);
        GameObject dropped = GameObject.Instantiate(spawns, spawnpoint, Quaternion.identity, null);
        Rigidbody2D dropflow = dropped.GetComponent<Rigidbody2D>();// dropped.AddComponent<Rigidbody2D>();
        dropflow.AddForce(Vector2.down * 30f);
    }
}
