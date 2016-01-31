using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Character;

public class PlayerSelect : MonoBehaviour {

    private string[] NAME = { "Head", "Left Arm", "Body", "Right Arm", "Left Leg", "Right Leg" };
    [Range(1, 2)]
    public int control = 1;
    public Carac CaracsPerso;
    public BottomPanel panel;

	public GameObject[] buttons;
	private int buttonIdx = 0;
    public int Index
    {
        get { return buttonIdx; }
        set { buttonIdx = value; }
    }
   

	private bool isMoving = false;

	void Start ()
	{
		// Selectionne la tête par défaut
		buttons[buttonIdx].GetComponent<Image>().color = Color.red;
        CaracsPerso.setObject(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetPlayerPart(control, (int)PART.HEAD), NAME[0]);
	}
	
	void Update ()
	{
		MoveCursor ();
	}

	void MoveCursor()
	{
		float verticalP1 = Input.GetAxis ("Vertical" + control.ToString());
		//float horizontalP1 = Input.GetAxisRaw ("Horizontal" + control.ToString());
		//Debug.Log("Vertical = " + verticalP1);

		if (verticalP1 != 0 && !isMoving)
		{
			Debug.Log ("Switching");
            StartCoroutine(waitV());
			buttons[buttonIdx].GetComponent<Image>().color = Color.white;
			if (verticalP1 < 0)
				++buttonIdx;
			else
				--buttonIdx;
			buttonIdx = (buttonIdx < 0) ? buttons.Length - 1 : (buttonIdx > buttons.Length - 1) ? 0 : buttonIdx;
			buttons[buttonIdx].GetComponent<Image>().color = Color.red;
            CaracsPerso.setObject(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetPlayerPart(control, buttonIdx), NAME[buttonIdx]);
        }
	}

    IEnumerator waitV()
    {
        isMoving = true;
        yield return new WaitForSeconds(0.15f);
        isMoving = false;
    }
}
