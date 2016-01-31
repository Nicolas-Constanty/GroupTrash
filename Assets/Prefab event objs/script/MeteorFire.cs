using UnityEngine;
using System.Collections;

public class MeteorFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player1") ||
            collision.gameObject.layer == LayerMask.NameToLayer("Player2") ||
            collision.gameObject.layer == LayerMask.NameToLayer("doll"))
            collision.gameObject.GetComponent<Item>().Part.hp -= 1;
        Destroy(gameObject);
    }
}
