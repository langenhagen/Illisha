using UnityEngine;
using System.Collections.Generic;

using ProgressList = System.Collections.Generic.List<ProgressElement>;

public class Achievement10Words : Achievement 
{
    public Achievement10Words()
    {
        Name            = "First 10 words";
        Explanation     = "You shot your first ten words!";
        Qualifications  = "Shoot 10 words.";
        ImageName       = "Achievement"; // TODO
    }


    public override ProgressList Progress()
    {
        ProgressList ret = new ProgressList();

        ret.Add(new ProgressElement("Shoot 10 words", GameManager.Game.NumWords, 10));

        return ret;
    }
}
