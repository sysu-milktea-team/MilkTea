using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMilk : MonoBehaviour
{

    public GameObject water;

    public Material milk;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

     void OnMouseDown()
    {

        if (water.active == false)
            water.active = true;
            water.GetComponent<MeshRenderer>().material = milk;
    }
}
