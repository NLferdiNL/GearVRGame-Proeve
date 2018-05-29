using UnityEngine;
using UnityEngine.UI;

public class UpdateVolumeSlider : MonoBehaviour
{
    private Slider slider;

    [SerializeField]
    private AudioVolumeHolder audioVolumeHolder;

    public float tempVolumeHolder;

    [SerializeField]
    private int SliderNumber;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        switch (SliderNumber)
        {
            case 1:
                slider.value = audioVolumeHolder.MusicVolume;
                tempVolumeHolder = audioVolumeHolder.MusicVolume;
                break;
            case 2:
                slider.value = audioVolumeHolder.SfxVolume;
                tempVolumeHolder = audioVolumeHolder.SfxVolume;
                break;

            default:
                Debug.Log("Something is wrong");
                break;
        }
    }
    public void UpdateSlider()
    {
        slider.value = tempVolumeHolder;
    }
}
