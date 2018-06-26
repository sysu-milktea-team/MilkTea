using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour {


    private bool isBegin = false;
    private float beginTime;
    private float state = 0;
    private bool isFinish = false;
    private bool isFound = false;
    private float selfBegin = 0;
    private float selfCounter = 0;

	public Image progress;
	public float waitTime = 5.0f;
    public Button button;
    public Button replay;
    public Button submit;
    public Button redo;


    public GameObject customerGroup;  
    public GameObject water;
    public Text gameOver;
    public Text win;
    public Text target;
    public GameObject TeaButton;
    public GameObject MilkButton;

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(onClicked);
        replay.onClick.AddListener(onReplay);
        submit.onClick.AddListener(onSubmit);

	}
	
	// Update is called once per frame
	void Update () {

        bool begins = isBegin && isFound;
        TeaButton.SetActive(begins);
        MilkButton.SetActive(begins);
        //Debug.LogFormat("> TIME-BAR: begins = {0}", begins);


        if (begins) {
            target.GetComponent<Text>().text = customerGroup.GetComponent<CustomerSystemController>().targetCustomerNumber.ToString();

            selfCounter +=  Time.deltaTime;
            state = (selfCounter - 0) / waitTime;
            //Debug.LogFormat("selfCounter={0},  state={1}", selfCounter, state);
			progress.fillAmount = 1-state;



            if (progress.fillAmount <= 0.005) {
                // 认为已经结束
                //Debug.LogFormat("gameover  - {0}", gameOver.gameObject);
                //Debug.LogFormat("gameover text  - {0}", gameOver.GetComponent<Text>().gameObject);
                gameOver.GetComponent<Text>().gameObject.SetActive(true);
                gameOver.gameObject.SetActive(true);
                isBegin = false;
                isFinish = true;


                if (customerGroup.GetComponent<CustomerSystemController>().allServed() == true)
                {
                    //Debug.Log("> TIME_BAR: GAME WIN.");
                    // 在一局游戏结束之前全部服务成功
                    // 显示胜利提示信息
                    win.GetComponent<Text>().gameObject.SetActive(true);

                }
            }




		}
        if (isFinish) {
            customerGroup.GetComponent<CustomerSystemController>().end();
            replay.GetComponent<Button>().gameObject.SetActive(true);
            submit.GetComponent<Button>().gameObject.SetActive(false);
            redo.GetComponent<Button>().gameObject.SetActive(false);
            selfBegin = 0;
            selfCounter = 0;
        }
	}

	void onClicked() {
        Debug.Log("> Start Clicked");
        beginTime = Time.time; 
        isBegin = true;
        isFound = true;
        selfCounter = 0.0f;
        button.GetComponent<Button>().gameObject.SetActive(false);
        customerGroup.GetComponent<CustomerSystemController>().run();
        submit.GetComponent<Button>().gameObject.SetActive(true);
        redo.GetComponent<Button>().gameObject.SetActive(true);
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
        redo.GetComponent<Button>().gameObject.SetActive(true);
        win.GetComponent<Text>().gameObject.SetActive(false);
    }

    public void onLostTarget() {
        submit.GetComponent<Button>().gameObject.SetActive(false);
        redo.GetComponent<Button>().gameObject.SetActive(false);
        this.isFound = false;


    }

    public void onFoundTarget() {
        if (this.isBegin == true) {
            this.isFound = true; 
            submit.GetComponent<Button>().gameObject.SetActive(true);
            redo.GetComponent<Button>().gameObject.SetActive(true);
        }
    }

    void onSubmit() {
         

        //Debug.Log("> Submit. ");

        customerGroup.GetComponent<CustomerSystemController>().submit(water.GetComponent<WaterController>().getMilkTea());

        
    }

     
}
