using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onClickCreateItem : MonoBehaviour {

    public GameObject Prefab;
    public int RayDistance = 10;
    private Vector3 Point;
    public LayerMask Whatever;

    // Use this for initialization
    void Start () {
		
	}

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Make Whatever a Raycast layer or if you don't want it just exclude it
            if (Physics.Raycast(ray, out hit, Whatever.value))
            {
                Point = hit.point;
                Instantiate(Prefab, Point, Quaternion.identity);
            }
        }
    }
}
