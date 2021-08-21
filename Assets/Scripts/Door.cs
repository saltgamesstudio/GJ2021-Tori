using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Workshop;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private string nextScene;

    private void OnTriggerEnter2D(Collider2D othercollider)
    {
        var player = othercollider.GetComponent<PlayerController>();
        if(player.haveKey == true)
        {
            SceneManager.LoadScene(nextScene);
        }
            
            
    }
}
