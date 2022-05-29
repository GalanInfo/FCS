using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceSystem : MonoBehaviour
{
    //FIELDS
    //Ice txt UI
    public Text txt_Ice;
    //default Ice value
    public int defaultIce;
    //current Ice value
    public int Ice;


    //METHODS
    //Init (set the default values)
    public void Init()
    {
        Ice = defaultIce;
        UpdateUI();
    }
    //Gain Ice (input of value)
    public void Gain(int val)
    {
        Ice += val;
        UpdateUI();
    }
    //Use Ice (input of value)
    public bool Use(int val)
    {
        if (EnoughIce(val))
        {
            Ice -= val;
            UpdateUI();
            return true;
        }
        else
        {
            return false;
        }
    }
    //Check availability of Ice
    public bool EnoughIce(int val)
    {
        //Check if the val is equal or more than Ice
        if (val <= Ice)
            return true;
        else
            return false;
    }
    //Update txt ui
    void UpdateUI()
    {
        txt_Ice.text = Ice.ToString();
    }
}
