using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour {


    private bool isBegin = false;
    private float state = 0;
    private bool isFinish = false;
    private bool isFound = false;
    private float selfCounter = 0;

	public Image progress;
	private float waitTime = 5.0f;
    public Button button;
    public Button next;
    public Button replay;
    public Button submit;
    public Button redo;
    public Button exit;

    public GameObject counter;

    public Slider difficulty;
    public GameObject difficultyGroup;


    public GameObject customerGroup;  
    public GameObject water;
    public Text gameOver;


    public GameObject GameWin;
    public GameObject RoundWin;
    public GameObject Win;

    public Text stageHint;



    public Text target;
    public Text scoreHint;


    public GameObject TeaButton;
    public GameObject MilkButton;
    public GameObject itemGroup;

    private int currentScore = 0;
    private int totalScore = 0;

    public int stages = -1;
    private int stageptr = 0;


    private float[] waitTimeSetting = { 10.0f, 25.0f, 30.0f, 30.0f, 30.0f };
    private int[] targetCustomerNumberSetting = { 1, 3, 5, 6, 7 };

	// Use this for initialization
	void Start () {
		button.onClick.AddListener(onClicked);
        replay.onClick.AddListener(onReplay);
        submit.onClick.AddListener(onSubmit);
        exit.onClick.AddListener(onExit);
        next.onClick.AddListener(onNextStage);

	}
	
	// Update is called once per frame
	void Update () {
        counter.GetComponent<Text>().text = "当前局得分: " + currentScore.ToString();

        bool begins = isBegin && isFound;
        TeaButton.SetActive(begins);
        MilkButton.SetActive(begins);
        //Debug.LogFormat("> TIME-BAR: begins = {0}", begins);

        stageHint.text = (stageptr+1).ToString();

        if (begins) {
            target.GetComponent<Text>().text = customerGroup.GetComponent<CustomerSystemController>().getTargetCustomers().ToString();

            selfCounter +=  Time.deltaTime;
            state = (selfCounter - 0.0f) / waitTime;
            //Debug.LogFormat("selfCounter={0},  state={1}", selfCounter, state);
			progress.fillAmount = 1-state;

            currentScore = customerGroup.GetComponent<CustomerSystemController>().getSuccessedCustomers();


            if (progress.fillAmount <= 0.005) {
                // 认为已经结束
                //Debug.LogFormat("gameover  - {0}", gameOver.gameObject);
                //Debug.LogFormat("gameover text  - {0}", gameOver.GetComponent<Text>().gameObject);
                if (customerGroup.GetComponent<CustomerSystemController>().allServed() == true)
                {
                    //Debug.Log("> TIME_BAR: GAME WIN.");
                    // 在一局游戏结束之前全部服务成功
                    // 显示胜利提示信息
                    if (stageptr+1 == stages) {
                        showGameWin();
                    } else {
                        showRoundWin();

                    }
                } else {

                    showGameOver();

                }
                isBegin = false;
                isFinish = true;


               
            }




		}
        if (isFinish) {
            customerGroup.GetComponent<CustomerSystemController>().end();
            submit.GetComponent<Button>().gameObject.SetActive(false);
            redo.GetComponent<Button>().gameObject.SetActive(false);
        }
	}

    private void showGameOver() {

        gameOver.GetComponent<Text>().gameObject.SetActive(true);
        gameOver.gameObject.SetActive(true);
        replay.GetComponent<Button>().gameObject.SetActive(true);

    }

    private void disableGameOver() {

        gameOver.GetComponent<Text>().gameObject.SetActive(false);
        gameOver.gameObject.SetActive(false);
    }

    private void showRoundWin() {
        Win.SetActive(true);
        totalScore += currentScore;
        RoundWin.SetActive(true);
        GameWin.SetActive(false);
        scoreHint.gameObject.SetActive(true);
        scoreHint.text = totalScore.ToString();
        next.GetComponent<Button>().gameObject.SetActive(true);

    }

    private void showGameWin() {

        Win.SetActive(true);
        totalScore += currentScore;

        GameWin.SetActive(true);
        RoundWin.SetActive(false);

        scoreHint.gameObject.SetActive(true);
        scoreHint.text = totalScore.ToString();


        Debug.LogFormat("> TIMEBAR  next button = {0}", next.GetComponent<Button>().gameObject != null);

        next.GetComponent<Button>().gameObject.SetActive(false);
        replay.GetComponent<Button>().gameObject.SetActive(true); 



    }

    void gameRestart() {

        isFinish = false;
        customerGroup.GetComponent<CustomerSystemController>().setDifficulty(Mathf.FloorToInt(difficulty.value)); // set once
        difficultyGroup.SetActive(false);
        stageptr = -1;
        totalScore = 0;

        replay.GetComponent<Button>().gameObject.SetActive(false); 


        float baseWaitTime = 8.0f;
        waitTime = baseWaitTime;

        RoundStart();


    }

	void onClicked() {
        Debug.Log("> GAME Start Clicked");

        gameRestart();

	}

    void onNextStage()
    {
        this.waitTime -= 1.0f; // debugging
        RoundStart();
        next.GetComponent<Button>().gameObject.SetActive(false);
    }

    void RoundStart() {
        isBegin = true;
        isFinish = false;
        isFound = true;
        selfCounter = 0.0f;
        stageptr++;

        button.GetComponent<Button>().gameObject.SetActive(false);
        next.GetComponent<Button>().gameObject.SetActive(false);

        disableGameOver();

        submit.GetComponent<Button>().gameObject.SetActive(true);
        redo.GetComponent<Button>().gameObject.SetActive(true);

        //submit.gameObject.SetActive(true);
        //redo.gameObject.SetActive(true);

        //Debug.LogFormat("submit.gameObject = {0}, redo.gameObject = {1}", submit.gameObject != null, redo.gameObject != null);
        //Debug.LogFormat("submit.GetComponent<Button>().gameObject = {0}, redo.GetComponent<Button>().gameObject = {1}",
                        //submit.GetComponent<Button>().gameObject != null, redo.GetComponent<Button>().gameObject!=null);

        itemGroup.SetActive(true);


        int targetNumber = targetCustomerNumberSetting[stageptr]; // debugging 
        waitTime = waitTimeSetting[stageptr];
        customerGroup.GetComponent<CustomerSystemController>().setTarget(targetNumber); // TO DO
        customerGroup.GetComponent<CustomerSystemController>().run();

        Win.SetActive(false);
        Debug.Log("> Round Start");



    }
	void onExit(){
        isBegin = false;
        isFinish = false;
        difficultyGroup.SetActive(true);
        itemGroup.SetActive(false);

        totalScore = 0;

        stageptr = -1;


        button.GetComponent<Button>().gameObject.SetActive(true);
        submit.GetComponent<Button>().gameObject.SetActive(false);
        redo.GetComponent<Button>().gameObject.SetActive(false);
        next.GetComponent<Button>().gameObject.SetActive(false);
        replay.GetComponent<Button>().gameObject.SetActive(false);

        customerGroup.GetComponent<CustomerSystemController>().end();

        Win.SetActive(false);


        disableGameOver();
         


        state = 0;

    }

    void onReplay() {
        Debug.Log("> Game Restart. ");

        gameRestart();

         

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
