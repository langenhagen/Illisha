using UnityEngine;
using System.Collections;

/// <summary>
/// Just plug it to your superslider, the rest happens by itself.
/// </summary>
public class SliderMuteCheckBox : BarnBehaviour
{
    private UIToggle _toggleCheckBox;

    private UISlider _sliderCheckBox;

    //##################################################################################################
    // METHODS

    void Start() 
    {
        _toggleCheckBox = GetComponentInChildren<UIToggle>();
        _sliderCheckBox = GetComponentInChildren<UISlider>();

        EventDelegate ed = new EventDelegate(this, "ExecuteOnSlider");
        _sliderCheckBox.onChange.Add(ed);

        ed = new EventDelegate(this, "ExecuteOnCheck");
        _toggleCheckBox.onChange.Add(ed);
    }


    private void ExecuteOnSlider()
    {
        if (_sliderCheckBox.value > 0)
        {
            _toggleCheckBox.value = true;
        }
        else
        {
            _toggleCheckBox.value = false;
        }
    }


    public void ExecuteOnCheck()
    {
        if (!_toggleCheckBox.value)
        {
            _sliderCheckBox.value = 0;
        }
    }


    //##################################################################################################
    // GETTERS & SETTERS


}