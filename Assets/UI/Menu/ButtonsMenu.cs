using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsMenu : MonoBehaviour
{
    [SerializeField] private int _numberNextScene;
    public void NextScene()
    {
        SceneManager.LoadScene(_numberNextScene);
    }

    public void ExiteTheGame()
    {
        Application.Quit();    
    }
}