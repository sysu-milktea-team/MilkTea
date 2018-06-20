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

    private GameObject currentCustomer;

    public GameObject counter;
    public GameObject _customer;
    public int maxCustomer;

	void Start () {




	}
	
	// Update is called once per frame
	void Update () {
        

        counter.GetComponent<Text>().text = "Counter: " + runnedCustomerCnt.ToString();

        // check customers

        if (currentCustomer.GetComponent<customerController>().isEnd() == true)
        { // 客人计时结束
            serveCustomer();
            currentCustomer.GetComponent<customerController>().destroy();
            //this.gameObject.GetComponent<Transform>()
        }

		
	}

    public bool allServed() {
 
        return runnedCustomerCnt >=  maxCustomer;
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
        //Invoke("checkPossibleCustomer", 0.0f);
        Invoke("createCustomer", 0.0f);
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
        runnedCustomerCnt = 0;


    }

    private void serveCustomer() {
        Debug.LogFormat("> End Customer {0}", nowRunCusId);
        //outOfTime[nowRunCusId] = true;
        //nowRunCusId = -1;
        runnedCustomerCnt++;
        //checkPossibleCustomer();
        if (currentCustomer) currentCustomer.GetComponent<customerController>().destroy();
        currentCustomer = null;
        if (runnedCustomerCnt < maxCustomer) {
            createCustomer();
        }
    }

    public void submit(MilkTea mk) {
        bool isServed = false;
        if (currentCustomer != null) {
            isServed = currentCustomer.GetComponent<customerController>().serveBy(mk);
            
        }
        if (isServed) {
            //counter.GetComponent<CounterController>().addScore();
            serveCustomer();
            Debug.LogFormat("> CUSTOMER_SYSTEM: SERVED, already serve {0} ", runnedCustomerCnt);
        } else {
            // 如果提交的奶茶不符合当前客人的要求
        }
    }

    private void createCustomer() {

        GameObject clone = Instantiate(_customer, _customer.transform.position, _customer.transform.rotation);

        GameObject container = this.gameObject;

        clone.transform.parent = container.transform;
        clone.transform.localScale = _customer.transform.localScale;
        clone.GetComponent<customerController>().run();
        currentCustomer = clone;

    }
	


}
