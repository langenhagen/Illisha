using UnityEngine;
using System.Collections.Generic;

public class SetupOptionsMenu : BarnBehaviour
{

    public UIToggle _toggleMusic;
    public UIToggle _toggleFx;

    public UISlider _sliderMusic;
    public UISlider _sliderFx;

    //##################################################################################################
    // METHODS


    void OnEnable()
    {
        IllishaOptions options = GameManager.Player.Options;
        
        _toggleMusic.value = options.VolumeMusic != 0;
        _toggleFx.value    = options.VolumeFx != 0;

        _sliderMusic.value = options.VolumeMusic;
        _sliderFx.value    = options.VolumeFx;
    }
}
