using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public Transform Target;
    public Transform Camera;

    public float RotateMod = 1f;

	// Use this for initialization
	void Start () {
        if (Target != null)
        {
            Target.rotation = Quaternion.Euler(0, 0, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (Target != null && Input.GetMouseButton(0))
        {
            //Target.transform.Rotate(Input.GetAxis("Mouse Y"), 0f, 0f);
            //Target.transform.Rotate(0f, -Input.GetAxis("Mouse X") * RotateMod, 0f);
            Camera.transform.RotateAround(Target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * RotateMod);
        }
    }
}
