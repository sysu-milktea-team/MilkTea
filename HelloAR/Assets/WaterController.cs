using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
public class MilkTea {
    // should be singleton.

    private int PEARL = 0, BOBA = 1, MILK = 2;
    private int sidefood = -1;

    public MilkTea(){
        
    }

    public void addMilk() { 
        amount.milkNum++;
        //Debug.LogFormat("> MILK_TEA: ADD MILK TO {0}", amount.milkNum);
    }
    public void addTea() { 
        amount.teaNum++; 
    }
    public void reset() {
        amount.milkNum = 0;
        amount.teaNum = 0;
        sidefood = -1;
    }


    public int getMilk() { return amount.milkNum; }
    public int getTea() { return amount.teaNum; }
    public int getSideFood() { return sidefood; }
    public void setSideFood(int val) { sidefood = val; }


}

public class WaterController : MonoBehaviour {


    private MilkTea mk = new MilkTea();

    public GameObject TeaText;
    public GameObject MilkText;
    public GameObject water;
    public GameObject container;

    public GameObject pearls, boba, milkgai;

    public Button redo;


	// Use this for initialization
    void Start () { 
        mk.reset();
        redo.onClick.AddListener(reset);

	}
	
	// Update is called once per frame
	void Update () {
        //Debug.LogFormat("udpate water {0}", Time.time);
        MilkText.GetComponent<Text>().text = "Milk: " +  mk.getMilk().ToString();
        TeaText.GetComponent<Text>().text = "Tea: " +  mk.getTea().ToString();


        float unit = 0.12f;

        int totalNum = amount.teaNum + amount.milkNum;

        GameObject water = this.gameObject;

        //

        //Vector3 oldpos = water.GetComponent<Transform>().position;
        //Vector3 pos = new Vector3(oldpos.x, oldpos.y, oldpos.z);
        //container.y = totalNum * unit;
        //water.GetComponent<Transform>().position = pos;

        Vector3 oldscal = water.GetComponent<Transform>().localScale;
        Vector3 scal = new Vector3(oldscal.x, oldscal.y, oldscal.z);

        scal.y = totalNum * unit;
        container.GetComponent<Transform>().localScale = scal;

        // update milkgai position
        int MILK = 2;
        if (mk.getSideFood() == MILK) {
            setMilkGai();
        }
	}

    private void setMilkGai() {
        double[] hard_coded_y_values = { 0.374, 0.541, 0.755, 1.018, 1.269, 1.487 };

        Vector3 oldpos = this.gameObject.GetComponent<Transform>().position;
        Vector3 newpos = new Vector3(oldpos.x, oldpos.y, oldpos.z);
        int totalAmount = mk.getTea() + mk.getMilk();

        newpos.y = (float)(hard_coded_y_values[totalAmount] * 8.56f);
        milkgai.GetComponent<Transform>().position = newpos;
        
    }

    public MilkTea getMilkTea() {
        
        return mk;
    }

	public void reset()
	{
        //water.SetActive(false);
        mk.reset();
        pearls.SetActive(false);
        boba.SetActive(false);
        milkgai.SetActive(false);
        setMilkGai();
        //this.gameObject.GetComponent<Transform>().localScale = new Vector3(0.120f,0.0001f,0.120f);
	}

    public void changeSideFood(int val) {
        mk.setSideFood(val);
        // 控制各种配料的显示（只能加一种）
        pearls.SetActive(false);
        boba.SetActive(false);
        milkgai.SetActive(false);
        switch(val) {
            case 0: // pearls
                pearls.SetActive(true);
                break;
            case 1: // boba
                boba.SetActive(true);
                break;
            case 2: // milkgai
                milkgai.SetActive(true);
                break;
        }
    }
}
