using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Käytetään Unity moottorista AI osiota
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    //Määritetään Animaattori ja charactercontroller, jota scripti voi käyttää. Käytetään CharControlleria määrittämään pelaajalle damage vihollisen toimesta. Tämä menee rigidbodysta.
    Animator PlayAnimator;
    CharacterController CharControl;
    //Käytetään Nav Mesh Agenttia, jolla vihollinen osaa vaistellä objekteja seuratessaan pelaajaa.
    private NavMeshAgent _nav;
    //Määritetään transform, jossa boss objektin positio.
    private Transform enemyPos;
    //Määritetään elämäpisteet ja paljonko tehdään damagea pelaajaan.
    private int health = 400;
    private int damage = 25;

    public GameObject addKC;
    void Start()
    {
        //Otetaan Nav Mesh, Animator ja character controller komennot käyttöön pelin kynnistyksessä. Asetetaan myös animator tila yhteen, joka aloittaa juoksun. Katso BossAC animator.
        _nav = GetComponent<NavMeshAgent>();
        PlayAnimator = GetComponent<Animator>();
        CharControl = GetComponent<CharacterController>();
        PlayAnimator.SetInteger("run", 1);


        //Etsi peliobjekti, joka on tagattu pelaajaksi.
        enemyPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //Aseta Nav Mesh Agentille destinaatio ja liikuta per frame.
        _nav.SetDestination(enemyPos.position);

    }

    //Luodaan "lyöntietäisyys" ja käynnistetään attack animaatio. Kosketuksen on oltava jatkuvaa, jotta voidaan siirtyä muihin collision osioihin pelaajan liikkuessa pois etäisyydeltä.
    void OnCollisionStay(Collision col)
    {

        if (col.gameObject.name == "Player")
        {
            PlayAnimator.SetInteger("attack", 1);
        }
    }

    //Lyöntietäisyydeltä poistuminen käynnistää attack 0 tilan Animaattorissa, jolloin siirrytään Animator controllerissa takaisin juoksuun.
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            PlayAnimator.SetInteger("attack", 0);
            Debug.Log("Karkuun");
        }
    }

    //Otetaan vastaan damage jos pelaaja ampuu luodilla vihollista ja vähenneään elämäpisteitä. Jos elämäpisteet ovat alle 0, siirrytään enemySlain 1 tilaan Animaattorissa ja siirrytään Die funktioon.
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Bullet(Clone)")
        {
            Debug.Log("Remaining hp:" + health);
            health -= damage;

            if (health < 0)
            {
                PlayAnimator.SetInteger("enemySlain", 1);
                Invoke("Die", 3);
            }
        }
    }

    //Tuhotaan peliobjekti, eli boss vihollinen ja lisätään kill countteriin +1 siirtymällä kyseiseen funktioon.
    public void Die()
    {
        Destroy(gameObject);
        //addKC = GetComponent<CountKR>();
        addKC.GetComponent<CountKR>().UpdateKC();
        //addKC.GetComponent<CountKR>().SpawnWave();





    }
}
