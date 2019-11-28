using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLookScript : MonoBehaviour
{
    //Serialisoidaan private field osioita.
    [SerializeField]
    public float sensitivity = 5.0f;
    [SerializeField]
    public float smoothing = 2.0f;
    //Peliobjekti character, johon "slottiin" raahataan inspektorissa pelaaja.
    public GameObject character;
    //Luetaan vector 2 arvon kasvamista.
    private Vector2 mouseLook;
    //Tehdään hiiren liikkumisesta "sulavampaa".
    private Vector2 smoothV;

    //Käynnistyksen yhteydessä luetaan pelaaj objektia ja sen child objekteja.
    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    void Update()
    {
        //md = mouse delta
        //En osaa oikein kuvailla alla olevaa tapahtumaa, mutta linkistä saa hyvän tuntuman hiiren akselin ja sen positionin vertailuun pelaajan liikuttaessa hiirtä.
        //https://docs.unity3d.com/ScriptReference/Input.GetAxis.html

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        //Kahden float arvon lopputulos. "Interpolaatio".
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        //Lisätään nousevasti sulavuutta.
        mouseLook += smoothV;

        //Vector3 oikea on X akseli. Käytetään Quaternion:ia hiiren rotaatiossa ja verrataan akseleita vectoreihin.
        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}