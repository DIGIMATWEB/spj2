using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float myCountDown;
    public float myCountDown2;
    public GameObject Message;
    public GameObject Loadingobj;
    public GameObject loadingCanvas;
    public GameObject ProductCanvas;
    public GameObject slideCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Message.SetActive(false);
        Loadingobj.SetActive(false);
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        //  
        
    }

    public void ActiveUI()
    {

        Message.SetActive(true);
        Loadingobj.SetActive(true);
        StartCoroutine(CountDown2());
    }
    public void ActiveUI2()
    {
      
        loadingCanvas.SetActive(false);
       /* slideCanvas.SetActive(true);
        ProductCanvas.SetActive(true);*/
        StopAllCoroutines();
    }
    public IEnumerator CountDown()
    {
        while (true)
        {
            //myCountDown -= Time.deltaTime; 
            //Debug.Log(myCountDown + "= timer");
            if (myCountDown <= 0)
            {
                /*Debug.Log("show text");
                myCountDown = 5;*/
                ActiveUI();

            }
            myCountDown -= 1;
            yield return new WaitForSeconds(1f);
           

        }
    }
    public IEnumerator CountDown2()
    {
        while (true)
        {
            //myCountDown -= Time.deltaTime; 
            Debug.Log(myCountDown2 + "= timer2");
            if (myCountDown2 <= 0)
            {
                /*Debug.Log("show text");
                myCountDown = 5;*/
                ActiveUI2();

            }
            myCountDown2 -= 1;
            yield return new WaitForSeconds(2f);


        }
    }
}
