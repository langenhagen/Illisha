using UnityEngine;
using System.Collections;

public class ButtonOptionsOk : BarnBehaviour
{
    public UIToggle _toggleMusic;
    public UIToggle _toggleFx;

    public UISlider _sliderMusic;
    public UISlider _sliderFx;

    //##################################################################################################
    // METHODS

    void OnClick()
    {
        IllishaOptions options = new IllishaOptions();

        if (_toggleFx.value)
            options.VolumeFx = _sliderFx.value;
        else
            options.VolumeFx = 0;

        if (_toggleMusic.value)
            options.VolumeMusic = _sliderMusic.value;
        else
            options.VolumeMusic = 0;
        

        GameManager.Instance.SaveOptions(options);
    }

    //##################################################################################################
    // GETTERS & SETTERS
    

}