using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime=1f;
    [SerializeField] private GameObject PauseUI;
    public void PindahScene(string scene)
    {
        var ui = Instantiate(PauseUI, new Vector3(0, 0, 0), Quaternion.identity);
        DontDestroyOnLoad(ui);
        StartCoroutine(LoadScene(scene));
    }

    IEnumerator LoadScene(string m_scene)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(m_scene);
    }


}
