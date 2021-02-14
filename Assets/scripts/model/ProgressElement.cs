using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



/// <summary>
/// One element for indicating the progress & state of achievements.
/// E.g: name: "Bananas eaten" // current: 10 // target: 20.
/// </summary>
public class ProgressElement
{
    public string Key           { get; set; }
    public float  CurrentValue  { get; set; }
    public float  TargetValue   { get; set; }
   
    //##################################################################################################
    // CONSTRUCTOR

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="key">The key name.</param>
    /// <param name="currentValue">The current value.</param>
    /// <param name="targetValue">The target value.</param>
    public ProgressElement( string key, float currentValue, float targetValue)
    {
        Key = key;
        CurrentValue = currentValue;
        TargetValue = targetValue;
    }

    //##################################################################################################
    // METHODS

    /// <summary>
    /// The ratio between current value and target value, 
    /// CurrentValue/TargetValue.
    /// </summary>
    /// <returns>A floating point number.</returns>
    public float ProgressRatio()
    {
        return CurrentValue/TargetValue;
    }

    /// <summary>
    /// Specifies, whether the current value equals or is bigger than the target value.
    /// </summary>
    /// <returns>A floating point number.</returns>
    public bool IsTargetReached()
    {
        return CurrentValue >= TargetValue;
    }
}