using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject popUpIns;
    public GameObject popUpStory;
    public void PlayGame() {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame() {
        Debug.Log("QUIT!!!");
        Application.Quit();
    }

    public void MostrarPopUpIns()
    {
        popUpIns.SetActive(true);
    }

    public void OcultarPopUpIns()
    {
        popUpIns.SetActive(false);
    }

    public void MostrarPopUpStory()
    {
        popUpStory.SetActive(true);
    }

    public void OcultarPopUpStory()
    {
        popUpStory.SetActive(false);
    }
}
