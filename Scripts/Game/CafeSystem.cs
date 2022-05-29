using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CafeSystem : MonoBehaviour
{
    //FIELDS
    //Cafe txt UI
    public Text txt_Cafe;
    //default Cafe value
    public int defaultCafe;
    //current Cafe value
    public int Cafe;


    //METHODS
    //Init (set the default values)
    public void Init()
    {
        Cafe = defaultCafe;
        UpdateUI();
    }
    //Gain Cafe (input of value)
    public void Gain(int val)
    {
        Cafe += val;
        UpdateUI();
    }
    //Use Cafe (input of value)
    public bool Use(int val)
    {
        if (EnoughCafe(val))
        {
            Cafe -= val;
            UpdateUI();
            return true;
        }
        else
        {
            return false;
        }
    }
    //Check availability of Cafe
    public bool EnoughCafe(int val)
    {
        //Check if the val is equal or more than Cafe
        if (val <= Cafe)
            return true;
        else
            return false;
    }
    //Update txt ui
    void UpdateUI()
    {
        txt_Cafe.text = Cafe.ToString();
    }
}
