using UnityEngine;
using System.Collections;

public class HitMeLight : MonoBehaviour {

    public int maxHit = 5;
    private int hit = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("doll") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude < 10)
            return;
        if (hit < maxHit)
            ++hit;
        else
            gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
    }

    public bool enoughtHitted()
    {
        return (hit >= maxHit);
    }
}
