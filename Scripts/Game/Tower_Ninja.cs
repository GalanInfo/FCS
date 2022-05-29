using System.Collections;
using UnityEngine;

public class Tower_Ninja : Tower
{
    //FIELDS
    //damage
    public int damage;
    //prefab (shooting item)
    public GameObject prefab_shootItem;
    //shoot interval
    public float interval;


    //METHODS
    //init (start the shooting interval)
    protected override void Start()
    {
        Debug.Log("NINJA");
    }
}
