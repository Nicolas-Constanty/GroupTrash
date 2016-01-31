using UnityEngine;
using System.Collections;

public class MeteorIce : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player1") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Player2") ||
            collision.gameObject.layer == LayerMask.NameToLayer("doll"))
            StartCoroutine(freezePlayer(collision.gameObject.GetComponent<Item>()));
        Destroy(gameObject);
    }

    IEnumerator freezePlayer(Item partItem)
    {
        partItem.Part.speed -= 1;
        yield return new WaitForSeconds(2);
        partItem.Part.speed = 1;
    }
}
