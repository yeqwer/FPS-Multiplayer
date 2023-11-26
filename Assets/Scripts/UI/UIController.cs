using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void LoadScene(int sceneNumber)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneNumber);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
