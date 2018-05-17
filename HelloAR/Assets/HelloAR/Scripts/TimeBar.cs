using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour {

	public Image progress;
	public float waitTime = 30.0f;
	public Button button;
	bool isClicked = false;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(onClicked);
	}
	
	// Update is called once per frame
	void Update () {
		if (isClicked) {
			progress.fillAmount -= 1.0f / waitTime * Time.deltaTime;
		}
	}

	void onClicked() {
		Debug.Log("Clicked");
		isClicked = true;	
		Destroy(button);   //  按键暂时没法消失
	}
}
