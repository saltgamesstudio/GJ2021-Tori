using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    public int levelToUnlock = 2;

    public void WinLevel()
    {
        Debug.Log("Level Success");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
}
