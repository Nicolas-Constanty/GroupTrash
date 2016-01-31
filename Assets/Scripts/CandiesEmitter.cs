using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CandiesEmitter : MonoBehaviour {
    public int nbCandies = 20;
    public List<Sprite> sprites = new List<Sprite>();

    public GameObject CandyPrefab;
    

    public void EmitCandies()
    {
        StartCoroutine("CandiesPowa");
    }

    IEnumerator CandiesPowa()
    {
        int i = 0;
        while(i < nbCandies)
        {
            GameObject newCandy = Instantiate(CandyPrefab);
            newCandy.transform.position = transform.position;

            newCandy.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
            newCandy.GetComponent<Rigidbody2D>().velocity = transform.transform.up + new Vector3(Random.Range(0,2.1f), Random.Range(0, 2.1f), 0) * Random.Range(0,1.6f);

            i++;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
