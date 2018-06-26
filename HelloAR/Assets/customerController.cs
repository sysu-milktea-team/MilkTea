using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CustomerMilkTea
{
    // should be singleton.
    private int PEARL = 0, BOBA = 1, MILK = 2;
    private int sidefood = -1;

    private int milkNum, teaNum;
    public CustomerMilkTea()
    {
        milkNum = 0;
        teaNum = 0;
    }

    public void addMilk() {  milkNum++; }
    public void addTea() {  teaNum++; }
    public void reset()
    {
         milkNum = 0;
         teaNum = 0;
    }
    public void randomReset(int total)
    {
        int seed = Mathf.FloorToInt(total * Random.Range(0.0f, 0.99f));
        sidefood = Mathf.FloorToInt(3 * Random.Range(0.0f, 0.99f));
        for (int i = 0; i < total; i++)
        {
            if (i < seed)
            {
                addMilk();
            }
            else
            {
                addTea();

            }
        }

    }

    public int getMilk() { return  milkNum; }
    public int getTea() { return  teaNum; }
    public int getSideFood() { return sidefood; }



}

public class customerController : MonoBehaviour {
    private bool isRun = false;
    private float beginTime;
    private float lifestate = 0;
    public float waitTime = 3.0f;
    public GameObject progress;
    private float xlen;
    private CustomerMilkTea orderMT = new CustomerMilkTea();
    private bool isServed = false;
    private float punishDelta = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (isRun) {
            lifestate = (Time.time - beginTime) / waitTime + punishDelta;
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
        // 不要在这里控制模型是否消失
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
    private void restart() {

        progress.GetComponent<MeshRenderer>().material.SetFloat("_Cutoff", 1);
        lifestate = 0;
        beginTime = Time.time;
        isRun = false;
        progress.gameObject.SetActive(false);
        //this.gameObject.SetActive(false);
         
        orderMT.randomReset(Mathf.FloorToInt(Random.Range(3.0f, 5.9f)));   // 随机初始化顾客需求，奶和茶加起来是3到5份
        isServed = false;
    }
    public bool serveBy(MilkTea mk) {
        bool success = false;
        bool debug = !true;
        if ( debug || 
            (mk.getMilk() == orderMT.getMilk() && mk.getTea() == orderMT.getTea()
             && mk.getSideFood() == orderMT.getSideFood()) ) {
            // 符合要求
            success = true;
            isServed = true;
            kill();

        }

        if (!success) {
            // 提交的不符合要求

        }



        return debug || success; // production

    }

	public void destroy()
	{
        Destroy(this.gameObject);
	}
}
