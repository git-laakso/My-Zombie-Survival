using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Käytetään User Interface osiota.
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    //Käytetään ääniraitaa audiosourcena.
    public AudioSource PlayerDied;
    //Määritetään GameOver UI ikkuna.
    public GameObject GameOver;
    //Määritetään pelaajan liikkumisnopeus. Optimaalinen on 5.
    public float movementSpeed = 5.0f;
    //Määritetään pystysuora ja vaaka.
    private float vertMovement;
    private float horizMovement;

    //Määritetään elämäpisteet ja paljonko otetaan vastaan damagea. Olen käyttänyt 25 muissa damage osioissa myöskin.
    public int health = 100;
    public int damage = 25;

    //Otetaan ns elämäbaari käyttöön ja luetaan sitä 100 pisteestä lähtien. vähennetään 25 per vihollisen osuma. Luotu UI Sliderilla ja saadaan näkymään aktiivisena peliscenessä setActive komennolla.
    public GameObject ActivateSlider;
    public Slider  remainingHP;
    
    void Start()
    {
        //Lukitaan hiiri ja määritetään näkymättömäksi.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //Otetaan elämäbaari käyttöön.
        ActivateSlider.SetActive(true);
        
    }
    void Update()
    {
        //Luetaan käyttäjän syötettä Input.GetAxis() funktiolla. Kerrotaan käyttäjän liikkumisella mikä on 5 ja käytetään globaalia aikaa kerrotaan muuttuvalla ajalla liikkuessa.
        vertMovement = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        horizMovement = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        //Luodaan ns liikkuminen 3D maailmassa käyttäen vaakaa ja pystysuoraa liikkumistapaa.
        transform.Translate(horizMovement, 0, vertMovement);

        //Luetaan näppäintä "p" jolloin hiiri tulee näkymään jälleen ja lukitus poistuu. Tämän olisi voinut tehdä UI scriptiin.
        if (Input.GetKeyDown(KeyCode.P))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //Luetaan onko aloitettu kosketus vihollisen kanssa, jonka tagi on enemy.
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //Logitetaan jäljellä oleva elämä ja määritetään elämäpisteet. Tässä osiossa saadaan elämäbaari value:lla lukemaan elämäpisteitä.
            Debug.Log("Player: Remaining hp:" + health);
            remainingHP.value = health;
            health -= damage;
            
            //Jos pelaaja tippuu alle nollan elämäpisteen, tuhoa pelaaja objekti. Käynnistä GameOver UI näkymä aktiivisena. Poistetaan hiiren lukitus ja soitetaan kuolit audioraita.
            if (health < 0 )
            {
                Destroy(gameObject);
                GameOver.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                PlayerDied.Play();
            }
        }
    }
}

