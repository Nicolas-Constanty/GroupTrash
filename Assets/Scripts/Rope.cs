using UnityEngine;

public class Rope : MonoBehaviour {
    public GameObject target;
    LineRenderer line;

    public void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetPositions(new Vector3[transform.childCount + 1]);
    }

    public void Update()
    {
        
        Vector3[] vecLine = new Vector3[transform.childCount+1];

        for (int i = 0; i < transform.childCount; i++)
        {
            vecLine[i] = transform.GetChild(i).position;
        }

        vecLine[transform.childCount] = target.transform.position;

        line.SetPositions(vecLine);
    }
}
