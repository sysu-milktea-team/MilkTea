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
    private float beginTime;
    private float state = 0;
    private bool isFinish = false;
    private bool isFound = false;
    private float selfBegin = 0;
    private float selfCounter = 0;
    public GameObject customerGroup;  
    public GameObject water;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(onClicked);
        replay.onClick.AddListener(onReplay);

        submit.onClick.AddListener(onSubmit);
	}
	
	// Update is called once per frame
	void Update () {
        if (isBegin && isFound) {
            selfCounter +=  Time.deltaTime;
            state = (selfCounter - 0) / waitTime;
            //Debug.LogFormat("selfCounter={0},  state={1}", selfCounter, state);
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
            selfBegin = 0;
            selfCounter = 0;
        }
	}

	void onClicked() {
		Debug.Log("Clicked");
        beginTime = Time.time; 
        isBegin = true;
        selfCounter = 0.0f;
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

    public void onLostTarget() {
        this.isFound = false;

    }

    public void onFoundTarget() {
        if (this.isBegin == true) {
            this.isFound = true; 
        }
    }

    void onSubmit() {
        Debug.Log("> Submit. ");

        customerGroup.GetComponent<CustomerSystemController>().submit(water.GetComponent<WaterController>().getMilkTea());
        
    }
}
