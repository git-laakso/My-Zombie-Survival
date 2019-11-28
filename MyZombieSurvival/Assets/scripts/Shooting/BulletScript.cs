using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //Määritetään AudioSource, luodin nopeus ja itse luoti peliobjektina, joka raahataan inspektorista.
    public AudioSource playClip;
    float bulletSpeed = 1100f;
    public GameObject bullet;
    //Määritetään fire rate ja ns "Cooldown" jotta asetta ei saada laukeamaan hiirtä rämpyttämällä sarjatulena.
    public float fireRate = 0.5f;
    private float CD = 0.0f;
    
    void Update()
    {
        //Luetaan hiiren vasenta näppäintä ja verrataan and portilla globaalia aikaa ja kierrätetään CD ja fire funktio ennen uuden aloittamista.
        if (Input.GetMouseButtonDown(0) && Time.time > CD)
        {
            Fire();
        }

    }

    void Fire()
    {
        //Käytetään audioraita, joka vastaa aseen laukaisu ääntä.
        playClip.Play();
        //Tehdään kopio luodista ja asetetaan tyypiksi peliobjekti. Käytetään  fysiikkamoottoria ja käsketään menemään eteenpäin asetetusta objektista.
        GameObject tempBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        //Asetetaan rigidbody. Käytetään painovoimaa addForce:lla.
        Rigidbody tempRigidBodyBullet = tempBullet.GetComponent<Rigidbody>();
        tempRigidBodyBullet.AddForce(tempRigidBodyBullet.transform.forward * bulletSpeed);
        //Tuhotaan luoti.
        Destroy(tempBullet, 0.5f);
        //Viivästytetään uudelleen ampumista jota verrataan hiiren painalluksen yhteydessä rivillä 18.
        CD = Time.time + fireRate;
    }
}
