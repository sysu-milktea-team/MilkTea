using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerSystemController : MonoBehaviour {
    private float beginTime;
    // Use this for initialization
    private int childcnt = 0;
    private int nowRunCusId = -1;
    private int createdCustomerCnt = 0;
    private int successCustomerCnt = 0;
    private bool isRun = false;

    private GameObject currentCustomer;

    public GameObject water;
    public GameObject[] _customers;
    private int targetCustomerNumber = 3;

    private int _mksize = -1;


    public int getSuccessedCustomers() {
        return successCustomerCnt;
    }

	void Start () {
        currentCustomer = null;
	}
	
    public int getTargetCustomers() {
        return targetCustomerNumber;
    }



	// Update is called once per frame
	void Update () {


        int totalNum = amount.teaNum + amount.milkNum;

        if (totalNum == 0)
        {
            water.SetActive(false);
        }
        else
        {
            water.SetActive(true);
        } 



        // check customers

        if (isRun && currentCustomer && currentCustomer.GetComponent<customerController>().isEnd() == true)
        { // 客人计时结束
            submit(water.GetComponent<WaterController>().getMilkTea());
            //servedCustomer(); // 客人计时结束，自动把当前已制作的奶茶提交给他
            //currentCustomer.GetComponent<customerController>().destroy();
            //this.gameObject.GetComponent<Transform>()
        }

		
	}

    public bool allServed() {
 
        return successCustomerCnt >=  targetCustomerNumber;
    }

    public void setDifficulty(int val) {
        _mksize = val;

        targetCustomerNumber = 3 + val * 2; // temporary
    }
   

    public void setTarget(int t)
    {
        targetCustomerNumber = t;
    }


    public void run() {
        //Debug.Log("> Customer system is running. ");
        //Invoke("checkPossibleCustomer", 0.0f);


        //InvokeRepeating("checkPossibleCustomer", 0, 2.0f); // 每隔一段时间查找一次有没有客人还没开始计时
        this.gameObject.SetActive(true);
        beginTime = Time.time;
        isRun = true;
        successCustomerCnt = 0;

        if (currentCustomer)
        {
            currentCustomer.GetComponent<customerController>().kill();
            currentCustomer.GetComponent<customerController>().destroy();

        }
        currentCustomer = null;


        createCustomer();


        water.GetComponent<WaterController>().reset();
        
    }
    public void end() {
        //Debug.Log("> Customer system ended. ");
        //for (int i = 0; i < createdCustomerCnt - successCustomerCnt; i++){ 
        //    customerController cus = this.transform.GetChild(i).gameObject.GetComponent<customerController>();

        //    if (cus == null) continue;
        //    cus.kill();
        //    //cus.destroy();
        //}
        if (currentCustomer) {
            currentCustomer.GetComponent<customerController>().kill();
            currentCustomer.GetComponent<customerController>().destroy();
        }
        createdCustomerCnt = 0;
        successCustomerCnt = 0;
        isRun = false;


        water.GetComponent<WaterController>().reset();
 
    }

    private void servedCustomer() {
        Debug.LogFormat("> End Customer {0}", nowRunCusId);
        //outOfTime[nowRunCusId] = true;
        //nowRunCusId = -1;
        //checkPossibleCustomer();
        if (currentCustomer) {
            currentCustomer.GetComponent<customerController>().kill();
            currentCustomer.GetComponent<customerController>().destroy();
        }
        currentCustomer = null;


        //if (successCustomerCnt < targetCustomerNumber)
        {
            createCustomer();
        }

    }

    public void submit(MilkTea mk) {
        bool isServed = false;
        Debug.LogFormat("> CUSTOMER_SYSTEM: SUBMIT, current =  {0} ", currentCustomer != null);

        if (currentCustomer != null) {
            isServed = currentCustomer.GetComponent<customerController>().serveBy(mk);
            if (isServed)
            {

                successCustomerCnt++;
                //counter.GetComponent<CounterController>().addScore(); 
                Debug.LogFormat("> CUSTOMER_SYSTEM: SERVED, already serve {0} ", successCustomerCnt);
                // 客人得到正确奶茶时消失


                servedCustomer();

            }
            else
            {
                // 如果提交的奶茶不符合当前客人的要求
                if (currentCustomer.GetComponent<customerController>().isEnd() == true)
                {
                    servedCustomer();
                    // 客人没有得到正确的奶茶但计时结束时消失

                }

            }
        }




        water.GetComponent<WaterController>().reset();
    }

    private void createCustomer() {

        if (!isRun) return;

        int ptr = Mathf.FloorToInt(_customers.Length * Random.Range(0.0f, 0.99f));
        GameObject _customer = _customers[ptr];

        GameObject clone = Instantiate(_customer, _customer.transform.position, _customer.transform.rotation);

        GameObject container = this.gameObject;

        clone.transform.parent = container.transform;
        clone.transform.localScale = _customer.transform.localScale;

        clone.SetActive(true);
        clone.GetComponent<customerController>().setDifficulty(_mksize);
        clone.GetComponent<customerController>().run();
        currentCustomer = clone;
        Debug.LogFormat("> CUS_SYS container = {0}", container != null);
        Debug.LogFormat("> CUS_SYS: Creating customer.  current = {0}", currentCustomer != null);

        createdCustomerCnt++;

    }

    // --------------------------------------------------------------------------
    /*
    private void checkPossibleCustomer()
    {  // unused
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
                    servedCustomer();
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
    */
	


}
