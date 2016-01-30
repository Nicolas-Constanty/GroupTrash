using UnityEngine;
using System.Collections;
using ItemEvents;

public class MapGen : MonoBehaviour, IActivatableGameObject
{
    public int density = 5;
    public GameObject obstacle;
    public float spacement_x = 15f;
    public float spacement_y = 10f;
    private GameObject[] generated;

	// Use this for initialization
	void Start ()
    {
        generated = new GameObject[density];
        for (int i = 0; i < density; ++i)
            generated[i] = null;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    private void GenerateMap()
    {
        for (int i = 0; i < density; ++i)
        {
            if (generated[i] != null)
                Destroy(generated[i].gameObject);
            generated[i] = Instantiate(obstacle, new Vector2(transform.position.x + Random.value * spacement_x, transform.position.y + Random.value * spacement_y), Quaternion.identity) as GameObject;
        }
    }

    public void Activate()
    {
        GenerateMap();
    }

    public void Desactivate()
    {
        GenerateMap();
    }
}
