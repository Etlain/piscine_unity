using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerUI : MonoBehaviour
{
    public GameObject gameManager;
    public Text       lifeText;
    public Text       energyText;

    private gameManager gameManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<gameManager>();
        lifeText.text = gameManagerScript.playerHp.ToString();
        energyText.text = gameManagerScript.playerEnergy.ToString();
        Debug.Log("game manager "+gameManagerScript.playerHp);
        Debug.Log("game manager "+gameManagerScript.playerEnergy);

    }

    // Update is called once per frame
    void Update()
    {
        energyText.text = gameManagerScript.playerEnergy.ToString();
        //Debug.Log("game manager "+gameManagerScript.playerEnergy);

    }

    void OnEnable()
    {
        if (gameManager)
            gameManager.GetComponent<gameManager>().loseLifeEvent += printLifeUI;
    }

    void printLifeUI()
    {
        lifeText.text = gameManagerScript.playerHp.ToString();
    }

    void OnDisable()
    {
        if (gameManager)
            gameManager.GetComponent<gameManager>().loseLifeEvent -= printLifeUI;
    }

    public void downEnergy(int energyCostTower)
    {
        if (canDownEnergy(energyCostTower))
            gameManagerScript.playerEnergy -= energyCostTower;
    }

    public bool canDownEnergy(int energyCostTower)
    {
        if (gameManagerScript.playerEnergy >= energyCostTower)
            return (true);
        return (false);
    }
}
