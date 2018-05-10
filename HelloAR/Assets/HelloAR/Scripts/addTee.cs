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
    }
}
