using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountKR : MonoBehaviour
{
    //Lasketaan tappoja
    //Määritetään audioraita viimeiselle vihollisaallolle, jolloin lopetetaan pelimusiikki.
    //Määritetään voitto UI näkymä ja gameWon audioraidan. Myöskin määritetään vihollisaallot jota laitetaan aktiiviseksi scriptillä edetessä peliä.
    public AudioSource BosswaveAudio;
    public AudioSource stopMainMusic;
    public AudioSource AudioGameWon;
    public GameObject gameWonPanel;
    public GameObject Wave2;
    public GameObject Wave3;
    //Aloitamme 0 taposta, jota päivitetään enemyAI scriptistä kutsumalla tämän scriptin UpdateKC() funktiota.
    public int EnemyKC = 0;

    public void UpdateKC()
    {
        //Päivitetään yhdestä taposta yhdellä pisteellä kill countteria.
        EnemyKC++;
        //Logitetaan montakoa vihollista on tuhottu, aina kun vihollinen tuhotaan.
        Debug.Log("ENEMIES SLAIN: " + EnemyKC);

        //Tehdään logiikka, joka vastaa tuhottuja vihollisia.
        if (EnemyKC == 4)
        {
            //Neljän ns tapon jälkeen alkaa seuraava aalto, joka on normaalisti fasle. (set Active hoitaa uuden vihollisaallon)
            Wave2.gameObject.SetActive(true);
            Debug.Log("Wave 2 triggered");
            
            //Toistetaan sama 12 vihollisen kanssa. Tämä on kolmas ja viimeinen aalto. Lopetetaan pelimusiikin soitto ja aloitetaan "boss" adioraita ja tässä aallossa yksi vahvempi vihollinen.
        } else if (EnemyKC == 12)
        {
            Wave3.gameObject.SetActive(true);
            Debug.Log("Wave 3 triggered");
            BosswaveAudio.Play();
            stopMainMusic.Stop();

            //Vihollisia on yhteensä 17 eli 16 normaalia +1 boss. Kun kaikki on kukistettu, voittaa pelin ja aloitetaan voittomusiikki, sekä käynnistetään YOU WON UI näkymä.
        } else if (EnemyKC == 17)
        {
            Debug.Log("YOU BEAT THE GAME!");
            Cursor.lockState = CursorLockMode.None;
            gameWonPanel.gameObject.SetActive(true);
            BosswaveAudio.Stop();
            stopMainMusic.Stop();
            AudioGameWon.Play();
        }
    }
}
