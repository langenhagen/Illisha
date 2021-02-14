using System;
using System.Collections.Generic;

using UnityEngine;


public class Game
{
    public enum Mode
    {
        TypeWriter,
        PingPong
    }

    //##################################################################################################
    // MEMBER VARS

    public int      Id                                          { get; set; }
    public Mode     GameMode                                    { get; set; }
                                                                
    public Language LanguageFrom                                { get; set; }
    public Language LanguageTo                                  { get; set; }
                                                                
    public DateTime DateTimeStarted                             { get; set; }
    public DateTime DateTimeStopped                             { get; set; }
                                                                
    public float    CannonSpeed                                 { get; set; }
    public int      Score                                       { get; set; }
    public int      NumWords                                    { get; set; }
    public int      NumShots                                    { get; set; }
    public int      NumHits                                     { get; set; }
    public int      NumPossibleHits                             { get; set; }


    public AchievementController AchievementController          { get; private set; }
    
    public Dictionary<int /*match_id*/, Vocab> Vocabs           { get; private set; }


	//##################################################################################################
	// CONSTRUCTORS


    // DONT instantiate this at hand! Look at DBConnector!!!
    public Game( int id, Language from, Language to, Mode mode)
    {
        Id = id;
        GameMode = mode;
        LanguageFrom = from;
        LanguageTo   = to;

        DateTimeStarted = System.DateTime.Now;
        DateTimeStopped = System.DateTime.Now;

        Score           = 0;
        NumShots        = 0;
        NumHits         = 0;
        NumWords        = 0;
        NumPossibleHits = 0;
        CannonSpeed     = 1;


        AchievementController = new AchievementController();

        Vocabs                = new Dictionary<int,Vocab>();
    }


    //##################################################################################################
    // METHODS

    /// <summary>
    /// Loads the vocabs to the internally stored game
    /// </summary>
    public void LoadVocabs()
    {
        Vocabs.Clear();
        GameManager.DB.LoadVocabs(this);
    }

    
	//##################################################################################################
	// GETTERS & SETTERS


}


