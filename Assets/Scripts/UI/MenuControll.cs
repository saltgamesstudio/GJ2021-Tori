using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuControll : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private bool menuState;
    [SerializeField] private Animator Overlay;
    [SerializeField] private GameObject Play;
    [SerializeField] private GameObject Exit;
    [SerializeField] private GameObject StageButton;

    private Button btnPlay,btnExit;
    void Start()
    {
        btnPlay = Play.GetComponent<Button>();
        btnExit = Exit.GetComponent<Button>();
    }

    public void PlayBtn()
    {
        Overlay.SetTrigger("OpenOverlay");
        menuState = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(StageButton);
    }


    // Update is called once per frame
    void Update()
    {

        if (menuState)
        {
            btnPlay.enabled = false;
            btnExit.enabled = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Overlay.SetTrigger("CloseOverlay");
                menuState = false;
                btnPlay.enabled = true;
                btnExit.enabled = true;
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(Play);
            }
        }





    }
}
