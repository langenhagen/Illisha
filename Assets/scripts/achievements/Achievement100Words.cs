using UnityEngine;
using System.Collections.Generic;

using ProgressList = System.Collections.Generic.List<ProgressElement>;

public class Achievement100Words : Achievement 
{
    public Achievement100Words()
    {
        Name            = "100 words";
        Explanation     = "You shot hundred words!";
        Qualifications  = "Shoot 100 words.";
        ImageName       = "Achievement2"; // TODO
    }


    public override ProgressList Progress()
    {
        ProgressList ret = new ProgressList();

        ret.Add(new ProgressElement("Shoot 100 words", GameManager.Game.NumWords, 100));

        return ret;
    }
}
