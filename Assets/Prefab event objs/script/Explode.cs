using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour
{
    private CircleCollider2D explodeCollider;
    public float explosion_radius = 0f;

	// Use this for initialization
	void Start ()
    {
        explodeCollider = GetComponent<CircleCollider2D>();
        explodeCollider.radius = 0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("doll"))
        {
            Vector2 explodeDir = (collision.transform.position - transform.position) * explosion_radius;

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = explodeDir;
            Destroy(gameObject);
        }
    }
}
