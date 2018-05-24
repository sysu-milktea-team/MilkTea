using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour {

	public Image progress;
	public float waitTime = 5.0f;
    public Button button;
    public Button replay;
    public Button submit;
	private bool isBegin = false;
    public Text gameOver;
    public GameObject customerGroup;
    private float beginTime;
    private float state = 0;
    private bool isFinish = false;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(onClicked);
        replay.onClick.AddListener(onReplay);
        submit.onClick.AddListener(onSubmit);
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
                isFinish = true;
            }
		}
        if (isFinish) {
            replay.GetComponent<Button>().gameObject.SetActive(true);
            submit.GetComponent<Button>().gameObject.SetActive(false);
        }
	}

	void onClicked() {
		Debug.Log("Clicked");
        beginTime = Time.time;
        isBegin = true;	
        button.GetComponent<Button>().gameObject.SetActive(false);
        customerGroup.GetComponent<CustomerSystemController>().run();

            submit.GetComponent<Button>().gameObject.SetActive(true);
	}
    void onReplay() {
        Debug.Log("> Game Restart. ");
        state = 0;
        isBegin = true;
        isFinish = false;
        beginTime = Time.time;
        replay.GetComponent<Button>().gameObject.SetActive(false); 

        gameOver.gameObject.SetActive(false);
        gameOver.GetComponent<Text>().gameObject.SetActive(false);
        customerGroup.GetComponent<CustomerSystemController>().run();
        submit.GetComponent<Button>().gameObject.SetActive(true);
    }
    void onSubmit() {
        Debug.Log("> Submit. ");
        //customerGroup.submit(water.milktea);
             
    }
}
