using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneButton : MonoBehaviour
{
    // this is a simple script that switches to the scenes with the same name as "sceneName" when the button is pressed.

    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
