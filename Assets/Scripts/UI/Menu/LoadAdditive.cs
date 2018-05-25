using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditive : MonoBehaviour
{
    // Script to add a scene to the current scene.

    // This function takes a string to find the right scene to load.
    void LoadInNextScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
    }
}
