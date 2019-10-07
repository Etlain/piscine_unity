using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public UnitsManager     unitsManager;
    public GameObject       unit;
    public int              spawnTimeUnits = 10;
    public Transform        spawnLocationUnits;
    public List<Buildings>  buildings = new List<Buildings>();





    private float           nextSpawnTimeUnits = 0;
    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("test", 2.0f, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawnTimeUnits)
        {
            nextSpawnTimeUnits = Time.time + spawnTimeUnits;
            spawnUnits();
        }
    }


    void spawnUnits()
    {

        GameObject tmp = Instantiate(unit, spawnLocationUnits.position, spawnLocationUnits.rotation);
        unitsManager.addUnit(tmp);
        Debug.Log("Spawn");
    }
}
