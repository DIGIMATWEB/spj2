using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Instrucciones : MonoBehaviour
{
    public Toggle onoff;

    public GameObject instrucciones;
    public GameObject video;

    private bool isOn;

    public void TurnOnOffInstr() 
    {
        isOn = onoff.isOn;

        if (isOn) 
        {
            instrucciones.SetActive(true);
            video.GetComponentInChildren<VideoPlayer>().Stop();
            video.SetActive(false);
            isOn = false;
        }
        else 
        {
            instrucciones.SetActive(false);
            video.SetActive(true);
            video.GetComponentInChildren<VideoPlayer>().Play();
            isOn = true;
        }
    }
}
