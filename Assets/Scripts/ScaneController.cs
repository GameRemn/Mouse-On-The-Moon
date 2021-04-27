using UnityEngine;
using UnityEngine.SceneManagement;

public class ScaneController : MonoBehaviour
{
    public void ToScane(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
}
