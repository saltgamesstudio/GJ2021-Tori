using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool menuState;
    [SerializeField] private bool startState;
    [SerializeField] private Animator StartScreen;
    [SerializeField] private Animator Overlay;
    void Start()
    {

    }

    public void PlayBtn()
    {
        Overlay.SetTrigger("OpenOverlay");
        menuState = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (!startState)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartScreen.SetTrigger("Start");
            }

            if (menuState)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Overlay.SetTrigger("CloseOverlay");
                    menuState = false;
                }
            }
        }
        

        
        
    }
}
