using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    private Animator deathanim;
    private bool isDead;

    private void Start()
    {
        deathanim = GetComponent<Animator>();
    }
    public void PlayDeathScreen()
    {
        deathanim.SetTrigger("StartAnim");
        isDead = true;
    }

    private void Update()
    {
        if (isDead)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MainMenu");
                isDead = false;
            }
        }
        
    }

}
