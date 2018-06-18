using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkTea {
    private int milk = 0;
    private int tea = 0;
    MilkTea(){}

    public void addMilk() { milk++;  }
    public void addTea() { tea++; }
    public void reset() {
        milk = 0;
        tea = 0;
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

    public int getMilk() { return milk; }
    public int getTea() { return tea; }
        


}

public class WaterController : MonoBehaviour {


    private MilkTea mk;

	// Use this for initialization
	void Start () {
        mk.reset();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public MilkTea getMilkTea() {
        return mk;
    }
}
