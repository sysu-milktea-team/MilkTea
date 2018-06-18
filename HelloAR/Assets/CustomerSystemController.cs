using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSystemController : MonoBehaviour {
    private float beginTime;
    // Use this for initialization
    private bool[] outOfTime;
    private int childcnt = 0;
    private int nowRunCusId = -1;
    private int runnedCustomerCnt = 0;
    private bool isRun = false;

    public GameObject counter;

	void Start () {


        
        int i = 0;

        foreach (Transform child in this.transform)
        {
              //(child.gameObject.GetComponent<customerController>() == null) continue;

            Debug.Log(child.gameObject.name);

            i++;
            childcnt++;
        }

        outOfTime = new bool[childcnt];
        for (int j = 0; j < childcnt; j++) {
            outOfTime[j] = false;
        }
        Debug.LogFormat("> Customer system: child cnt : {0}",childcnt);


	}
	
	// Update is called once per frame
	void Update () {
        //if (isRun) {
        //    checkPossibleCustomer();

        //}

         
		
	}

    public bool allServed() {

        for (int i = 0; i < childcnt; i++)
        {
            if (outOfTime[i] == false){
                return false;
            }

         }
        return true;
    }

    private void checkPossibleCustomer() {
        //Debug.Log("> Check Customer at [" +  Time.time  + "]");

        for (int i = 0; i < childcnt; i++)
        {
            if (outOfTime[i] == false)
            {
                //Debug.Log("get child i  " + this.transform.GetChild(i) == null);
                //if (this.transform.GetChild(i) == null)
                //    Debug.Log("get child i.parent  is null");
                //if (this.transform.GetChild(i).gameObject != null)
                //    Debug.Log(this.transform.GetChild(i).gameObject.name);
                //if (this.transform.GetChild(i).gameObject.GetComponent<customerController>() == null)
                    //Debug.Log("get child i.parent.gameobj.getctrler()  ");


                customerController cus = this.transform.GetChild(i).gameObject.GetComponent<customerController>();

                Debug.LogFormat("> CUSTOMER_SYSTEM: cus == null:{0}, cus.isEnd():{1}, cus.isRunning()={2}, i={3}", cus == null, cus.isEnd(), cus.isRunning(), i);

                if (cus == null) continue;
                if (cus.isEnd() == true)
                { // 客人计时结束
                    serveCustomer();
                    //this.gameObject.GetComponent<Transform>()
                    continue;
                }



                if (cus.isRunning() && cus.isEnd() == false)
                {
                    // 如果有一个正在计时的客人，不启动新的计时
                    break;
                }
                if (cus.isRunning() == false)
                { // 客人的计时还没开始
                    if (nowRunCusId != -1 && nowRunCusId != 0) break; // 说明当前有正在计时的客人
                    Debug.LogFormat("> Run Customer {0} at Time: {1}", i, Time.time);
                    nowRunCusId = i;
                    cus.run();
                    break;  
                } 

            }
            i++;
        }
    }
    public void run() {
        Debug.Log("> Customer system is running. ");
        Invoke("checkPossibleCustomer", 0.0f);
        //InvokeRepeating("checkPossibleCustomer", 0, 2.0f); // 每隔一段时间查找一次有没有客人还没开始计时
        this.gameObject.SetActive(true);
        beginTime = Time.time;
        isRun = true;
        for (int i = 0; i < childcnt; i++)
        {
            outOfTime[i] = false;
            this.transform.GetChild(i).gameObject.SetActive(false);
            customerController cus = this.transform.GetChild(i).gameObject.GetComponent<customerController>();
            if (cus == null) continue;
            cus.restart();

        }
        
    }
    public void end() {
        Debug.Log("> Customer system ended. ");
        CancelInvoke();
        for (int i = 0; i < childcnt; i++){ 
            customerController cus = this.transform.GetChild(i).gameObject.GetComponent<customerController>();
            if (cus == null) continue;
            cus.kill();
        }


    }

    private void serveCustomer() {
        Debug.LogFormat("> End Customer {0}", nowRunCusId);
        outOfTime[nowRunCusId] = true;
        nowRunCusId = -1;
        runnedCustomerCnt++;
        checkPossibleCustomer();
    }

    public void submit(MilkTea mk) {
        bool isServed = false;
        if (nowRunCusId != -1) {
            isServed = this.transform.GetChild(nowRunCusId).gameObject.GetComponent<customerController>().serveBy(mk);
            
        }
        if (isServed) {
            //counter.GetComponent<CounterController>().addScore();
            serveCustomer();
            Debug.LogFormat("> CUSTOMER_SYSTEM: SERVED, already serve {0} ", runnedCustomerCnt);


        } else {
            
        }
    }
	


}
