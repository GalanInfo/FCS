using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonSystem : MonoBehaviour
{
    //FIELDS
    //Person txt UI
    public Text txt_Person;
    //default Person value
    public int defaultPerson;
    //current Person value
    public int Person;


    //METHODS
    //Init (set the default values)
    public void Init()
    {
        Person = defaultPerson;
        UpdateUI();
    }
    //Gain Person (input of value)
    public void Gain(int val)
    {
        Person += val;
        UpdateUI();
    }
    //Use Person (input of value)
    public bool Use(int val)
    {
        if (EnoughPerson(val))
        {
            Person -= val;
            UpdateUI();
            return true;
        }
        else
        {
            return false;
        }
    }
    //Check availability of Person
    public bool EnoughPerson(int val)
    {
        //Check if the val is equal or more than Person
        if (val <= Person)
            return true;
        else
            return false;
    }
    //Update txt ui
    void UpdateUI()
    {
        txt_Person.text = Person.ToString();
    }
}
