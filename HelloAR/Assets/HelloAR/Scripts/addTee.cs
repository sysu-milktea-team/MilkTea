using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addTee : MonoBehaviour {

	public GameObject water;
	public Material tee;
    public Material milktee; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown() {
		if (water.active == false) {
			water.active = true;
			water.GetComponent<MeshRenderer> ().material = tee;
		} else {
			water.GetComponent<MeshRenderer> ().material = milktee;
		}

        //for (int i = 0; i < 25; i++) {
        //    GameObject newBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //    newBall.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        //    newBall.transform.position = new Vector3(-0.1f + i * 0.05f, 10.0f + 2 * i, 0);
        //    newBall.AddComponent<Rigidbody>();

        //    newBall.GetComponent<Rigidbody>().useGravity = true;

        //    //newBall.GetComponent<Rigidbody>().AddForce(new Vector3(0.001f, 0f, 0f));
        //    newBall.GetComponent<MeshRenderer>().material = tee;
         
        //}
       }
}
