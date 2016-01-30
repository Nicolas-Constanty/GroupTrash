using UnityEngine;
using System.Collections;

public class MeteorIce : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Geler la poupée correspondante
        if (collision.gameObject.layer != LayerMask.NameToLayer("projectile"))
            Destroy(gameObject);
    }
}
