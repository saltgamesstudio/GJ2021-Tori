using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool pauseState;
    [SerializeField] private Animator Overlay;
    [SerializeField] private GameObject buttonPause;
    [SerializeField] private Button buttonESC;
    [SerializeField] private Button buttonR;

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
                buttonESC.enabled = true;
                buttonR.enabled = true;
                EventSystem.current.SetSelectedGameObject(null);

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                Overlay.SetTrigger("StartPause");
                pauseState = true;
                buttonESC.enabled = false;
                buttonR.enabled = false;
                EventSystem.current.SetSelectedGameObject(buttonPause);

            }
        }




    }
}
