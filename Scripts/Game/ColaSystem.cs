using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColaSystem : MonoBehaviour
{
    //FIELDS
    //Cola txt UI
    public Text txt_Cola;
    //default Cola value
    public int defaultCola;
    //current Cola value
    public int Cola;


    //METHODS
    //Init (set the default values)
    public void Init()
    {
        Cola = defaultCola;
        UpdateUI();
    }
    //Gain Cola (input of value)
    public void Gain(int val)
    {
        Cola += val;
        UpdateUI();
    }
    //Use Cola (input of value)
    public bool Use(int val)
    {
        if (EnoughCola(val))
        {
            Cola -= val;
            UpdateUI();
            return true;
        }
        else
        {
            return false;
        }
    }
    //Check availability of Cola
    public bool EnoughCola(int val)
    {
        //Check if the val is equal or more than Cola
        if (val <= Cola)
            return true;
        else
            return false;
    }
    //Update txt ui
    void UpdateUI()
    {
        txt_Cola.text = Cola.ToString();
    }
}
