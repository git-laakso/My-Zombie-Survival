using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    //Aloitetaan käynnistyksen yhteydessä hiiren näyttäminen. Main Menu toimii ankkurina, johon pelaajaa ohjataan eri pelin tapahtumista.
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    //Funktio jolla aloitetaan peli "build" järjestyksessä. Katso File -> Build options. Arrayt alkavat nollasta, jossa ensimmäisenä on menu. Funktio siis lataa menusta onClic metodilla peli scenen joka on nappulan takana.
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    //Suljetaan peli. Käytetään Application quittia, koska jätän tämän build valmiiksi. Poista kommentointi jos pelaat vain editorissa.
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
        //emulaattorin quit komento
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    //Määritetty nappula UI takana, josta ladataan scene.
    public void LoadMenu()
    {
        Debug.Log("Loading menu");
        SceneManager.LoadScene("Menu");
    }
}
