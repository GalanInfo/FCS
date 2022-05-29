using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake() { instance = this; }

    public Spawner spawner;
    public CurrencySystem currency;
    public CafeSystem cafe;
    public ColaSystem cola;
    public IceSystem ice;
    public PersonSystem person;

    void Start()
    {
        GetComponent<CurrencySystem>().Init();
        GetComponent<CafeSystem>().Init();
        GetComponent<ColaSystem>().Init();
        GetComponent<IceSystem>().Init();
        GetComponent<PersonSystem>().Init();
    }
}
