using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class mainmn : MonoBehaviour
{


    
    public void start_button()
    {

        SceneManager.LoadScene(1);
    }

    public void exit_button()
    {

        Application.Quit();

    }
    public void exithowtoplay()
    {

        gameObject.SetActive(false);
    }
    public void howtoplaybutton()
    {
        gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
