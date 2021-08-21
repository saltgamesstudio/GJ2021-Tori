using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool pauseState;
    [SerializeField] private Animator Overlay;
    void Start()
    {

    }

    public void PauseBtn()
    {
        Overlay.SetTrigger("StartPause");
        pauseState = true;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (pauseState)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Overlay.SetTrigger("StopPause");
                pauseState = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Overlay.SetTrigger("StartPause");
                pauseState = true;
            }
        }




    }
}
