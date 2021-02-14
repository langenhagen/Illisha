using UnityEngine;
using System.Collections.Generic;

using ProgressList = System.Collections.Generic.List<ProgressElement>;

public class Achievement500Words : Achievement 
{
    public Achievement500Words()
    {
        Name            = "500 words";
        Explanation     = "You shot five hundred words!";
        Qualifications  = "Shoot 500 words.";
        ImageName       = "Achievement"; // TODO    
    }


    public override ProgressList Progress()
    {
        ProgressList ret = new ProgressList();

        ret.Add(new ProgressElement("Shoot 500 words", GameManager.Game.NumWords, 500));

        return ret;
    }
}
