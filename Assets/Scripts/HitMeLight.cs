using UnityEngine;
using System.Collections;

public class HitMeLight : MonoBehaviour {

    public int maxHit = 5;
    public GameObject flame;
    public Vector2 flamePos;
    private int hit = 0;
    private bool activated = false;

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
        else if (!activated)
        {
            Instantiate(flame, flamePos, Quaternion.identity);
            activated = true;
        }
    }

    public bool enoughtHitted()
    {
        return (hit >= maxHit);
    }
}
