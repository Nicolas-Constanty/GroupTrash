using UnityEngine;
using Character;

public class Item : MonoBehaviour {

    private Part _part = new Part();
    public Part Part
    {
        get { return _part; }
        set {   _part.damage = value.damage;
                _part.hp = value.hp;
                _part.candies = value.candies;
                _part.special = value.special;
                _part.speed = value.speed;
                _part.sprite = value.sprite;
                _part.spriteParts = value.spriteParts;
        }
    }

}
