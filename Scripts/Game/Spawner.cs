using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    //list of towers (prefabs) that will instantiate
    public List<GameObject> towersPrefabs;
    //Transform of the spawning towers (Root Object)
    public Transform spawnTowerRoot;
    //list of towers (UI)
    public List<Image> towersUI;
    //id of tower to spawn
    int spawnID=-1;
    //SpawnPoints Tilemap
    public Tilemap spawnTilemap;
    //Tile position
    private Vector3Int tilePos;


    [SerializeField] private GameObject menuPausa;

    [SerializeField] private GameObject menuFin;
    private int cantidadRestaurantes = 1;

    private Vector3 cafePosicion = new Vector3(5,-1,0);
    private int precioCafe = 1;
    private int cantidadCafe = 2;

    private Vector3 colaPosicion = new Vector3(5, -2, 0);
    private int precioCola = 2;
    private int cantidadCola = 1;

    private Vector3 icePosicion = new Vector3(5, -3, 0);
    private int precioIce = 5;
    private int cantidadIce = 1;

    private int cantidadPerson = 2; //cantidad de personas que se suman por cada casa puesta

    private int cantidadCasaCafe = 20;
    private int cantidadCasaCola = 10;
    private int cantidadCasaIce = 5;
    private int cantidadCasaPerson = 12;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CompraCafeColaIce();
        }
        if (CanSpawn())
            DetectSpawnPoint();
    }

    void CompraCafeColaIce()
    {
        //get the world space postion of the mouse
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //get the position of the cell in the tilemap
        var cellPosDefault = spawnTilemap.WorldToCell(mousePos);

        print(cellPosDefault);

        if (cellPosDefault == cafePosicion)
        {
            if (GameManager.instance.currency.EnoughCurrency(precioCafe)) //si el dinero es => al precio cafe
            {
                GameManager.instance.currency.Use(precioCafe); //se resta precio cafe al dinero
                GameManager.instance.cafe.Gain(cantidadCafe); // se añaden 2 cafe
            }
            //print("cafecito parce");
        }
        else if (cellPosDefault == colaPosicion)
        {
            if (GameManager.instance.currency.EnoughCurrency(precioCola))
            {
                GameManager.instance.currency.Use(precioCola);
                GameManager.instance.cola.Gain(cantidadCola); 
            }
            //print("COLA LIGHT");
        } else if (cellPosDefault == icePosicion)
        {
            if (GameManager.instance.currency.EnoughCurrency(precioIce))
            {
                GameManager.instance.currency.Use(precioIce);
                GameManager.instance.ice.Gain(cantidadIce);
            }
            //print("HELADITO PA");
        }

    }

    bool CanSpawn()
    {
        if (spawnID == -1)
            return false;
        else
            return true;
    }

    void DetectSpawnPoint()
    {
        //Detect when mouse is clicked (first touch clicked)
        if(Input.GetMouseButtonDown(0))
        {
            //get the world space postion of the mouse
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //get the position of the cell in the tilemap
            var cellPosDefault = spawnTilemap.WorldToCell(mousePos);
            //get the center position of the cell
            var cellPosCentered = spawnTilemap.GetCellCenterWorld(cellPosDefault);
            //check if we can spawn in that cell (collider)

            if(spawnTilemap.GetColliderType(cellPosDefault)== Tile.ColliderType.Sprite)
            {
                int towerCost = TowerCost(spawnID);

                //Check if currency is enough to spawn
                if(GameManager.instance.currency.EnoughCurrency(towerCost))
                {
                    if (spawnID != 2)
                    {
                        if (spawnID == 1)
                        {
                            GameManager.instance.person.Gain(cantidadPerson); //al poner casa se suman X personas
                            print("CASA PUESTA");
                        } else
                        {
                            print("VENDEDOR PUESTO");
                        }

                        //Use the amount of cost from the currency available
                        GameManager.instance.currency.Use(towerCost);
                        //Spawn the tower
                        SpawnTower(cellPosCentered, cellPosDefault);

                        //Disable the collider
                        spawnTilemap.SetColliderType(cellPosDefault, Tile.ColliderType.None);
                    }
                    else
                    {
                        if (GameManager.instance.cafe.EnoughCafe(cantidadCasaCafe) && GameManager.instance.cola.EnoughCola(cantidadCasaCola)
                            && GameManager.instance.ice.EnoughIce(cantidadCasaIce) && GameManager.instance.person.EnoughPerson(cantidadCasaPerson))
                        {
                            GameManager.instance.cafe.Use(cantidadCasaCafe); // se restan X cantidad de cafe cola ice
                            GameManager.instance.cola.Use(cantidadCasaCola);
                            GameManager.instance.ice.Use(cantidadCasaIce);
                            GameManager.instance.person.Use(cantidadCasaPerson);

                            SpawnTower(cellPosCentered, cellPosDefault);
                            spawnTilemap.SetColliderType(cellPosDefault, Tile.ColliderType.None);
                            print("HAMBUERGESERIA PUESTA");

                            cantidadRestaurantes++;

                            if (cantidadRestaurantes == 3)
                            {
                                Time.timeScale = 0f;
                                menuPausa.SetActive(true);
                                menuFin.SetActive(true);
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("Not Enough Currency");
                }                               
            }                                  
        }
    }

    public int TowerCost(int id)
    {
        switch(id)
        {
            case 0: return towersPrefabs[id].GetComponent<Tower_Pink>().cost;
            case 1: return towersPrefabs[id].GetComponent<Tower_Mask>().cost;
            case 2: return towersPrefabs[id].GetComponent<Tower_Ninja>().cost;  
            default:return -1;
        }
    }

    void SpawnTower(Vector3 position, Vector3Int cellPosition)
    {
        GameObject tower = Instantiate(towersPrefabs[spawnID],spawnTowerRoot);
        tower.transform.position = position;
        tower.GetComponent<Tower>().Init(cellPosition);

        DeselectTowers();
    }

    public void RevertCellState(Vector3Int pos)
    {
        spawnTilemap.SetColliderType(pos, Tile.ColliderType.Sprite);
    }

    public void SelectTower(int id)
    {
        DeselectTowers();
        //Set the spawnID
        spawnID = id;
        //Highlight the tower
        towersUI[spawnID].color = Color.white;        
    }

    public void DeselectTowers()
    {
        spawnID = -1;
        foreach(var t in towersUI)
        {
            t.color = new Color(0.5f, 0.5f, 0.5f);
        }
    }    


    
}
