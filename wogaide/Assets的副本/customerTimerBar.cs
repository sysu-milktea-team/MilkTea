using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customerTimerBar : MonoBehaviour
{

    public Image progress;
    public float waitTime = 5.0f;
    public Button button;
    private bool isClicked = false;
    public Text gameOver;
    public GameObject customerGroup;

    // Use this for initialization
    void Start()
    {
        button.onClick.AddListener(onClicked);
    }

    // Update is called once per frame
    void Update()
    {
        if (isClicked)
        {
            progress.fillAmount -= 1.0f / waitTime * Time.deltaTime;
            if (progress.fillAmount <= 0.05)
            {
                gameOver.GetComponent<Text>().gameObject.SetActive(true);
                customerGroup.GetComponent<CustomerSystemController>().end();
            }
        }
    }

    void onClicked()
    {
        Debug.Log("Clicked");
        isClicked = true;
        button.GetComponent<Button>().gameObject.SetActive(false);
        customerGroup.GetComponent<CustomerSystemController>().run();
    }
}
