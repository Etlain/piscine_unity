using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject       menu;
    public GameObject       confirm;
    //private gameManager gameManager;
    private bool            isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = new gameManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
                pause(true);
            else
                pause(false);
        }
    }

    void        pause(bool isPause)
    {
        gameManager.gm.pause(isPause);
        isPaused = isPause;
        if (isPause)
            menu.SetActive(true);
        else
        {
            menu.SetActive(false);
            confirm.SetActive(false);
        }
    }

    public void quitButton()
    {
        confirm.SetActive(true);
    }

    public void yesQuitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void noQuitButton()
    {
        confirm.SetActive(false);
    }

    public void resumeButton()
    {
        pause(false);
    }
}
