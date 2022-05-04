using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderMainMenu : MonoBehaviour
{
    Slider slider;
    public void SetVolume(float volume)
    {
        Utils.SharedValues.SetVolume(volume);
    }

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        slider.value = Utils.SharedValues.Volume;
    }
}
