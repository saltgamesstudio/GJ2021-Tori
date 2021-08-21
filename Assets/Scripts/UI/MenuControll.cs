using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool menuState;
    [SerializeField] private Animator OverlayMenu;
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (!menuState)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OverlayMenu.SetTrigger("OpenOverlay");
                menuState = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OverlayMenu.SetTrigger("CloseOverlay");
                menuState = false;
            }
        }
        
    }
}
