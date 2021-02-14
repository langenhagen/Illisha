using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections;
using UnityEngine;

public class AchievementController
{
    public List<Achievement>    Achievements    { get; set; }
    public Queue<Achievement>   NewAchievements { get; set; }     // very new achieved achievements in come here for handling


    //##################################################################################################
    // CONSTRUCTORS

    public AchievementController()
    {
        Achievements = new List<Achievement>();
        NewAchievements = new Queue<Achievement>();
    }

    //##################################################################################################
    // METHODS


    /// <summary>
    /// Registers your achievements ( automatically (; )
    /// </summary>
    public void InitAchievements()
    {
        Assembly mscorlib = typeof(Achievement).Assembly;
        foreach (Type type in mscorlib.GetTypes())
        {
            if (type.IsSubclassOf(typeof(Achievement)) && !type.IsAbstract)
                Achievements.Add(Activator.CreateInstance(type) as Achievement);
        }
    }


    public void HandleNewAchievements()
    {
        bool newAchievementsEmpty = NewAchievements.Count == 0;

        foreach (var achievement in GameManager.Game.AchievementController.Achievements)
        {
            if (achievement.IsNewlyAchieved())
            {
                NewAchievements.Enqueue(achievement);
                achievement.Apply();
            }
        }

        if (newAchievementsEmpty && NewAchievements.Count > 0)
            GameManager.UI.IngameUI.ShowAchievmentPopups(); 
    }


    //##################################################################################################
    // HELPERS


}
