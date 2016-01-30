using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using Character;

public class Carac : MonoBehaviour {

    public Color color;
    private Dictionary<int, Image[]> _panel = new Dictionary<int, Image[]>();
    private enum CARAC { DAMAGE, HP, SPEED, CANDIES, SPECIAL };
	// Use this for initialization
	void Awake () {
        _panel.Add((int)CARAC.DAMAGE,   transform.GetChild(1).GetChild((int)CARAC.DAMAGE).GetChild(0).GetComponentsInChildren<Image>());
        _panel.Add((int)CARAC.HP,       transform.GetChild(1).GetChild((int)CARAC.HP).GetChild(0).GetComponentsInChildren<Image>());
        _panel.Add((int)CARAC.SPEED,    transform.GetChild(1).GetChild((int)CARAC.SPEED).GetChild(0).GetComponentsInChildren<Image>());
        _panel.Add((int)CARAC.CANDIES,  transform.GetChild(1).GetChild((int)CARAC.CANDIES).GetChild(0).GetComponentsInChildren<Image>());
        _panel.Add((int)CARAC.SPECIAL,  transform.GetChild(1).GetChild((int)CARAC.SPECIAL).GetChild(0).GetComponentsInChildren<Image>());
    }

    public void setObject(Part part, string name)
    {
        if (part != null)
        {
            int power;
            power = Mathf.Clamp(part.damage, 0, 5) + 1;
            setField((int)CARAC.DAMAGE, power);

            power = Mathf.Clamp(part.hp, 0, 5) + 1;
            setField((int)CARAC.HP, power);

            power = Mathf.Clamp(part.speed, 0, 5) + 1;
            setField((int)CARAC.SPEED, power);

            power = Mathf.Clamp(part.candies, 0, 5) + 1;
            setField((int)CARAC.CANDIES, power);

            power = Mathf.Clamp(part.special, 0, 5) + 1;
            setField((int)CARAC.SPECIAL, power);
        }
        transform.GetChild(0).GetComponent<Text>().text = name;
    }
    private void setField(int carac, int power)
    {
        for (int i = 1; i < 6; i++)
        {
            if (i < power)
                _panel[carac][i].color = color;
            else
                _panel[carac][i].color = Color.white;
        }
    }
}
