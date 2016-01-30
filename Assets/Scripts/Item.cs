using UnityEngine;
using Character;

public class Item : MonoBehaviour {

    public Part _part;
    public Part Part
    {
        get { return _part; }
        set {   _part.damage = value.damage;
                _part.hp = value.hp;
                _part.candies = value.candies;
                _part.special = value.special;
                _part.speed = value.speed;
             _part.sprite = value.sprite;
        }
    }

}
