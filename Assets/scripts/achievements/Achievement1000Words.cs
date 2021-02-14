using UnityEngine;
using System.Collections.Generic;

using ProgressList = System.Collections.Generic.List<ProgressElement>;

public class Achievement1000Words : Achievement 
{
    public Achievement1000Words()
    {
        Name            = "1000 words";
        Explanation     = "You shot hundred words!";
        Qualifications  = "Shoot 1000 words.";
        ImageName       = "Achievement"; // TODO
    }


    public override ProgressList Progress()
    {
        ProgressList ret = new ProgressList();

        ret.Add(new ProgressElement("Shoot 1000 words", GameManager.Game.NumWords, 1000));

        return ret;
    }
}