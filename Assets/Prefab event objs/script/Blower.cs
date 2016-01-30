using UnityEngine;
using System.Collections;
using ItemEvents;

public class Blower : MonoBehaviour, IActivatableGameObject
{
    private BoxCollider2D windArea;
    public float windVelocity = 10f;

	// Use this for initialization
	void Start ()
    {
        windArea = GetComponent<BoxCollider2D>();
        windArea.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void Activate()
    {
        windArea.enabled = true;
        //Debug.Log("On active le tout");
    }

    public void Desactivate()
    {
        windArea.enabled = false;
        //Debug.Log("On désactive le tout");
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<Rigidbody2D>().velocity += Vector2.right * windVelocity;
    }
}
