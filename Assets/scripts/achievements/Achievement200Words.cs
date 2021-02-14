using UnityEngine;
using System.Collections.Generic;

using ProgressList = System.Collections.Generic.List<ProgressElement>;

public class Achievement200Words : Achievement 
{
    public Achievement200Words()
    {
        Name            = "200 words";
        Explanation     = "You shot two hundred words!";
        Qualifications  = "Shoot 200 words.";
        ImageName       = "Achievement"; // TODO
    }


    public override ProgressList Progress()
    {
        ProgressList ret = new ProgressList();

        ret.Add(new ProgressElement("Shoot 200 words", GameManager.Game.NumWords, 200));

        return ret;
    }
}