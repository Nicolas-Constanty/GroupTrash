using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour {

	public List<Sprite> sprites;

	private Image img;
	private int idx = 0;

	// Use this for initialization
	void Start ()
	{
		img = GetComponent<Image> ();
		img.sprite = sprites [idx];
	}

	public void NextSprite()
	{
		++idx;
		if (idx >= sprites.Count)
			idx = 0;
		img.sprite = sprites[idx];
	}

	public void PrevSprite()
	{
		--idx;
		if (idx < 0)
			idx = sprites.Count - 1;
		img.sprite = sprites [idx];		
	}

	public void AddSprite(Sprite s)
	{
		sprites.Add (s);
	}
}
