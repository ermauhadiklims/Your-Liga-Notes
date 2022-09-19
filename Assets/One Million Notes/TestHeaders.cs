using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TestHeaders : MonoBehaviour
{
    public Text text;
    public InputField input;


    void Start(){

        DateTime time = DateTime.Now;
        long timeTicks = time.Ticks;
        // Debug.Log("Simple date = " + time);
        // Debug.Log("In tacts =" + timeInt);
        // Debug.Log("Converted = " + new DateTime(timeInt));

        Debug.Log(Application.persistentDataPath);


    }
  
}
