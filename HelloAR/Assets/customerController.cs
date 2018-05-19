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
	// Use this for initialization
	void Start () {

                progress.GetComponent<MeshRenderer>().material.SetFloat("_Cutoff", 1); 
	}
	
	// Update is called once per frame
	void Update () {
        if (isRun) {
            lifestate = (Time.time - beginTime) / waitTime;
            //Debug.Log(lifestate);
            if (isEnd()) {
                kill();

                this.gameObject.SetActive(false); // 只有当计时结束时才消失
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

        // 计时中途被打断时，客人模型不消失
    }
    public bool isEnd() {
        return lifestate >= 1.0;
    }
    public bool isRunning() {
        return isRun;
    }

    public void run() {
        //Debug.Log(" > Customer Running.");
        isRun = true;
        beginTime = Time.time;
        progress.gameObject.SetActive(true);
        xlen = progress.GetComponent<Transform>().localScale.x;
    }
}
