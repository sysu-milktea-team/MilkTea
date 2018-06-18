using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSystemController : MonoBehaviour {
    private float beginTime;
    // Use this for initialization
    private bool[] outOfTime;
    private int childcnt = 0;
    private int nowRunCusId = 0;
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
        Debug.Log("> Customer system: child cnt : ");
        Debug.Log(childcnt);


	}
	
	// Update is called once per frame
	void Update () {
        //if (isRun) {
        //    checkPossibleCustomer();

        //}
		
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
                if (cus == null) continue;
                if (cus.isEnd() == true)
                { // 客人计时结束
                    outOfTime[i] = true;
                    nowRunCusId = -1; // 表示当前没有在计时的客人
                    Debug.LogFormat("> End Customer {0}", i);
                    runnedCustomerCnt++;
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
                    this.transform.GetChild(i).gameObject.GetComponent<customerController>().run();
                    nowRunCusId = i;
                    break;  
                } 

            }
            i++;
        }
    }
    public void run() {
        Debug.Log("> Customer system is running. ");
        InvokeRepeating("checkPossibleCustomer", 0, 2.0f); // 每隔一段时间查找一次有没有客人还没开始计时
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

    public void submit(MilkTea mk) {
        bool isServed = this.transform.GetChild(nowRunCusId).gameObject.GetComponent<customerController>().serveBy(mk);
        if (isServed) {
            
        } else {
            
        }
    }
	


}
