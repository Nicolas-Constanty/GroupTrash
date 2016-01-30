using UnityEngine;
using Character;

public class Item : MonoBehaviour {

    private Part _part;
    public Part Part
    {
        get { return _part; }
        set { _part = value; }
    }

}
