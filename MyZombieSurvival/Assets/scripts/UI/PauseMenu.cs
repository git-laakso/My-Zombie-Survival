using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Luodaan pausemenu arvoksi false. Vertailuu käytetään booleania tässä tapauksessa.
    //Peliobjekti canvas joka haluamme näyttävän.
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;

    //Aloitamme normaalista ajan standardista 1.
    void Start()
    {
        Time.timeScale = 1f;
    }

    //Luetaan käyttäjän näppäintä P, jota painettuna käsitellään resume ja pause funktiota.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //Jis peli on pause, siirry resume funktioon. Meidän bool arvo on normaalisti false, eli siirrymme näppäintä p painamalla pause funktioon. Jos haluamme pois pausesta, valitsemme resume joka funktiossa poistaa hidastuksen ja hiiren.
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Asetamme aktiiviseksi pausemenu UI näkymäm. Hidastamme globaalia aikaa ja asetamme gameISPause arvoksi tosi. Lisäksi hiiri tulee näkymiin, jotta voidaan valita kahdesta nappulasta.
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.2f;
        gameIsPaused = true;
        Cursor.visible = true;
    }

    //Ladataan funktolla scene.
    public void LoadMenu()
    {
        Debug.Log("Loading menu");
        SceneManager.LoadScene("Menu");
    }

    //Poistutaan funktiolla pelistä. Build valmis komento.
    public void QuitMenu()
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}

