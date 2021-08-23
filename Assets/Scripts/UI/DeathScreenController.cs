using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenController : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource deathsfx;
    [SerializeField] private Text deathText;
    private Animator deathanim;
    public static bool isDead;
    public static string causeDeath;

    private void Start()
    {
        deathanim = GetComponent<Animator>();
    }
    public void PlayDeathScreen(string deathCause)
    {
        deathsfx.Play();
        deathanim.SetTrigger("StartAnim");
        deathText.text = deathCause;
        isDead = true;
        bgm.Stop();
        Time.timeScale = 0;
        
    }

    private void FixedUpdate()
    {
        if (isDead)
        {
            PlayDeathScreen(causeDeath);
            
        }
        
    }

    private void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                isDead = false;
                causeDeath = null;
                Time.timeScale = 1;
            }

        }
        
    }

}
