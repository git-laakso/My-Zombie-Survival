using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myEffects : MonoBehaviour
{
    public PauseMenu mypause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            if (PauseMenu.gameIsPaused)
            {
                mypause.Resume();
                
            }
            else
            {
                mypause.Pause();
                

            }
        }
    }
}
