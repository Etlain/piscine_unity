using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsManager : MonoBehaviour
{
    public UnitsManager     unitsManager;
    public GameObject       unit;
    public float            spawnTimeUnits = 10;
    public float            spawnTimePenalty = 2.5f;
    public Transform        spawnLocationUnits;
    public List<Buildings>  buildings = new List<Buildings>();

    private float           nextSpawnTimeUnits = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        majSpawnTimeAtDestroyBuilding();
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
        Debug.Log("Spawn :"+getRaceName());
    }

    string getRaceName()
    {
        if (unit.tag.IndexOf("Orc") >= 0)
            return "ORC";
        return "HUMAN";
    }

    string getOppositeRaceName()
    {
        if (unit.tag.IndexOf("Orc") >= 0)
            return "HUMAN";
        return "ORC";
    }

    void majSpawnTimeAtDestroyBuilding()
    {
        bool defeat = true;

        for (int i = 0; i < buildings.Count; i++)
        {
            if (!buildings[i])
            {
                buildings.Remove(buildings[i]);
                spawnTimeUnits += spawnTimePenalty;
            }
            else if (buildings[i].name.IndexOf("CityHall") >= 0)
            {
                defeat = false;
            }
        }
        if (defeat)
            launchDefeat();
    }

    void launchDefeat()
    {
        Debug.Log(getOppositeRaceName()+" WIN BECAUSE IS THE BIG BOSS OF THE WOOOOOOOO.....RLD");
        Time.timeScale = 0f;
    }
}
