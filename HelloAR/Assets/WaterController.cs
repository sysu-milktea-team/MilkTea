using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class MilkTea {
    
    public MilkTea(){
        
    }

    public void addMilk() { amount.milkNum++;  }
    public void addTea() { amount.teaNum++; }
    public void reset() {
        amount.milkNum = 0;
        amount.teaNum = 0;
    }
    public void randomReset(int total){
        int seed = Mathf.FloorToInt(total * Random.Range(0.0f, 1.0f));
        for (int i = 0; i < 7; i++)
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

    public int getMilk() { return amount.milkNum; }
    public int getTea() { return amount.teaNum; }
        


}

public class WaterController : MonoBehaviour {


    private MilkTea mk = new MilkTea();

    public GameObject TeaText;
    public GameObject MilkText;
    public GameObject water;

	// Use this for initialization
    void Start () { 
        mk.reset();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.LogFormat("udpate water {0}", Time.time);
        MilkText.GetComponent<Text>().text = "Milk: " +  mk.getMilk().ToString();
        TeaText.GetComponent<Text>().text = "Tea: " +  mk.getTea().ToString();
	}

    public MilkTea getMilkTea() {
        
        return mk;
    }

	public void reset()
	{
        water.SetActive(false);
        mk.reset();
	}
}
