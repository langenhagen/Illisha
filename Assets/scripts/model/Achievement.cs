using System.Collections.Generic;


using UnityEngine;


/// <summary>
/// Represents achievements in the Illisha game.
/// </summary>
public abstract class Achievement
{
    public bool         IsAchieved      { get; private set; }

    public string       Name            { get; protected set; }
    public string       Explanation     { get; protected set; }
    public string       Qualifications  { get; protected set; }
    public string       ImageName       { get; protected set; }


    //##################################################################################################
    // CONSTRUCTOR

    public Achievement()
    {
        IsAchieved = CalculateIsAchieved();
        if (IsAchieved)
            Apply();
    }


	//##################################################################################################
	// METHODS

    public abstract List<ProgressElement> Progress();


    public bool IsNewlyAchieved()
    {
        bool ret;

        if (IsAchieved == false && CalculateIsAchieved())
        {
            IsAchieved = true;
            ret = true;
        }
        else
        {
            ret = false;
        }

        return ret;
    }


    /// <summary>
    /// Applies the effects of a particular achievement.
    /// </summary>
    public virtual void Apply() {}


    //##################################################################################################
    // HELPERS

    private bool CalculateIsAchieved()
    {
        return Progress().TrueForAll(p => p.CurrentValue >= p.TargetValue);
    }
}

