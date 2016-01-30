using UnityEngine;
using System.Collections;
using ItemEvents;

public class MeteorCaster : MonoBehaviour, IActivatableGameObject
{
    public GameObject baseObject;
    public float intensity;
    public float frequency;
    private Vector2 baseDir;
    private bool raining = false;

	// Use this for initialization
	void Start () {
        frequency = 1 / frequency;
	}
	
	// Update is called once per frame
	void Update () {
        baseDir = new Vector2(transform.position.x, transform.position.y - intensity);
	}

    public void Activate()
    {
        raining = true;
        StartCoroutine(RainMeteor());
    }

    public void Desactivate()
    {
        raining = false;
    }

    private IEnumerator RainMeteor()
    {
        while (raining)
        {
            GameObject insted;

            baseDir.x = Random.value * intensity - intensity / 2;
            insted = Instantiate(baseObject, new Vector2(transform.position.x, transform.position.y) - baseDir, Quaternion.identity) as GameObject;
            insted.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, -100));
            yield return new WaitForSeconds(frequency);
        }
    }
}
