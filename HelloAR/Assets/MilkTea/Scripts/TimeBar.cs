using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour {

	public Image progress;
	public float waitTime = 5.0f;
	public Button button;
	private bool isBegin = false;
    public Text gameOver;
    public GameObject customerGroup;
    private float beginTime;
    private float state = 0;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(onClicked);
	}
	
	// Update is called once per frame
	void Update () {
        if (isBegin) {
            state = (Time.time - beginTime) / waitTime;
			progress.fillAmount = 1-state;
            if (progress.fillAmount <= 0.05) {
                Debug.LogFormat("gameover  - {0}", gameOver.gameObject);
                Debug.LogFormat("gameover text  - {0}", gameOver.GetComponent<Text>().gameObject);
                gameOver.GetComponent<Text>().gameObject.SetActive(true);
                gameOver.gameObject.SetActive(true);
                customerGroup.GetComponent<CustomerSystemController>().end();
                isBegin = false;
            }
		}
	}

	void onClicked() {
		Debug.Log("Clicked");
        beginTime = Time.time;
        isBegin = true;	
        button.GetComponent<Button>().gameObject.SetActive(false);
        customerGroup.GetComponent<CustomerSystemController>().run();
	}
}
