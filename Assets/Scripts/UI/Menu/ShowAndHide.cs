using UnityEngine;

public class ShowAndHide : MonoBehaviour
{
    // This Script is for hiding and showing an object.

    public void Hide(GameObject _gameObject)
    {
        _gameObject.SetActive(false);
    }

    public void Show(GameObject _gameObject)
    {
        _gameObject.SetActive(true);
    }
}
