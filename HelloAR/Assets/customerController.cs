using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customerController : MonoBehaviour {
    private bool isRun = false;
    private float beginTime;
    private float lifestate = 0;
    public float waitTime = 3.0f;
    public GameObject progress;
    private float xlen;
    private MilkTea orderMT = new MilkTea();
    private bool isServed = false;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (isRun) {
            lifestate = (Time.time - beginTime) / waitTime;
            //Debug.Log(lifestate);
            if (isEnd()) {
                kill();
                this.gameObject.SetActive(false); // 客人模型只有当计时结束或成功服务时才消失
            } else {
                progress.GetComponent<MeshRenderer>().material.SetFloat("_Cutoff", 1 - lifestate);  
                //progress.fillAmount = 1 - lifestate;
            }
        }
	}
    public void kill(){

        isRun = false;
        //progress.fillAmount = 0;
        //Debug.Log("> Customer End. ");
        progress.gameObject.SetActive(false);

    }
    public bool isEnd() {
        return isServed || lifestate >= 1.0;
    }
    public bool isRunning() {
        return isRun;
    }

    public void run() {
        //Debug.Log(" > Customer Running.");

        restart();
        isRun = true; 
        beginTime = Time.time;
        progress.gameObject.SetActive(true); // 显示顾客进度条
        this.gameObject.SetActive(true); // 显示顾客
        xlen = progress.GetComponent<Transform>().localScale.x;
    } 
    public void restart() {

        progress.GetComponent<MeshRenderer>().material.SetFloat("_Cutoff", 1);
        lifestate = 0;
        isRun = false;
        progress.gameObject.SetActive(false);
        this.gameObject.SetActive(false); 
        orderMT.randomReset(7);   // 随机初始化顾客需求，奶和茶加起来是7
        isServed = false;
    }
    public bool serveBy(MilkTea mk) {
        bool success = false;
        bool debug = true;
        if ( debug ||  (mk.getMilk() == orderMT.getMilk() && mk.getTea() == orderMT.getTea())) {
            // 符合要求
            success = true;
            isServed = true;
            kill();

            this.gameObject.SetActive(false); // 客人模型只有当计时结束或成功服务时才消失
        }

        if (!success) {
            // 提交的不符合要求
        }



        //return  true; // debug
        return success; // production

    }
}
