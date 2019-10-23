using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public GameObject   downBarUI;
    public GameObject   towerIcon;
    public Text         damageText;
    public Text         rangeText;
    public Text         energyText;
    public Text         waitText;


    // Start is called before the first frame update
    private ManagerUI   downBarManagerUI;
    private towerScript towerScriptTowerPrefab;
    private ItemUI      itemUITowerIcon;

    private bool        isCanDownEnergy = false;

    void Start()
    {
        downBarManagerUI = downBarUI.GetComponent<ManagerUI>();
        itemUITowerIcon = towerIcon.GetComponent<ItemUI>();
        towerScriptTowerPrefab = itemUITowerIcon.towerPrefab.GetComponent<towerScript>();
        damageText.text = towerScriptTowerPrefab.damage.ToString();
        rangeText.text = towerScriptTowerPrefab.range.ToString();
        energyText.text = towerScriptTowerPrefab.energy.ToString();
        waitText.text = towerScriptTowerPrefab.fireRate.ToString();

        /*if (downBarManagerUI.canDownEnergy(towerScriptTowerPrefab.energy))
            isCanDownEnergy = true;*/

        //Debug.Log("tettttt :"+towerScriptTowerPrefab.damage);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanDownEnergy == false && downBarManagerUI.canDownEnergy(towerScriptTowerPrefab.energy))
        {
            /*if (isCanDownEnergy == false)
            {

            }*/
            // color white
            // enabled script
            towerIcon.GetComponent<SpriteRenderer>().color = Color.white;
            itemUITowerIcon.enabled = true;
            isCanDownEnergy = true;
        }
        else if (isCanDownEnergy == true && downBarManagerUI.canDownEnergy(towerScriptTowerPrefab.energy) == false)
        {
            //color red
            // disabled script
            towerIcon.GetComponent<SpriteRenderer>().color = Color.red;
            itemUITowerIcon.enabled = false;
            isCanDownEnergy = false;
        }
    }
}
