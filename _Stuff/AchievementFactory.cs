using System;
using System.Collections.Generic;

using UnityEngine;
using System.Collections;

public class AchievementFactory
{
    private List<Achievement> Achievements { get; private set; }


    //##################################################################################################
    // CONSTRUCTOR

    public AchievementFactory()
    {
        Achievements = new List<Achievement>();
    }


    //##################################################################################################
    // Methods


    public int Register(Achievement achievement)
    {
        for( int i=0; i< Achievements.Count; ++i)
        {
            if (Achievements[i].GetType() == achievement.GetType())
            {
                Logger.Log(" Achievement of type " + achievement.GetType() + " already registered at id " + i + "!", Logger.Error);
                return -1;
            }
        }
        
        // *** achievement not yet registered ***

        Achievements.Add( achievement);

        return Achievements.Count-1;
    }

    public List<Achievement> AchievedAchievements()
    {
        List<Achievement> ret = new List<Achievement>();

        Achievements.ForEach(delegate(Achievement a)
        {
            if (a.IsAchieved())
                ret.Add(a);
        });

        return ret;
    }




}
