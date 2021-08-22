using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator Overlay;
    [SerializeField] private GameObject buttonPause;
    [SerializeField] private Button buttonESC;
    [SerializeField] private Button buttonR;
    [SerializeField] private AudioSource bgm;

    public static bool gameIsPaused;

    void Start()
    {

    }

    public void PauseBtn()
    {
        Overlay.SetTrigger("StartPause");
        gameIsPaused = true;
        Time.timeScale = 0;
        bgm.volume = 0.4f;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        bgm.volume = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            { 
                Overlay.SetTrigger("StopPause");
                gameIsPaused = false;
                buttonESC.enabled = true;
                buttonR.enabled = true;
                EventSystem.current.SetSelectedGameObject(null);
                Time.timeScale = 1;
                bgm.volume = 1f;


            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Overlay.SetTrigger("StartPause");
                gameIsPaused = true;
                buttonESC.enabled = false;
                buttonR.enabled = false;
                EventSystem.current.SetSelectedGameObject(buttonPause);
                Time.timeScale = 0;
                bgm.volume = 0.4f;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }




    }
}
