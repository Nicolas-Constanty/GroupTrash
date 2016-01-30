using UnityEngine;
using System.Collections;
using ItemEvents;

public class PushButton : MonoBehaviour {

    public MonoBehaviour linkedObj;
    private bool state = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (linkedObj is IActivatableGameObject && collision.gameObject.layer == LayerMask.NameToLayer("doll"))
        {
            IActivatableGameObject interObj = (IActivatableGameObject)linkedObj;
            if (!state)
                interObj.Activate();
            else
                interObj.Desactivate();
            state = !state;
        }
    }
}
