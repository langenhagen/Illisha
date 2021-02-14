using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class DBConnectorTest : BarnBehaviour
{

    public DBConnector dbc;

	// Use this for initialization
	void Start () 
    {
        Log("RUNNING  " + System.DateTime.Now);

        dbc = new SQLiteConnector("Illy.db");
	}
	
	// Update is called once per frame
	void Update () {


	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 50), "Start test"))
        {
            Test();
        }
    }


    public void Test()
    {

        /* Dictionary<int, string> players = dbc.Players();
         foreach (var p in players)
         {
             Log(p.Key + " : " + p.Value);
         }

         Log("/////////////////////////////////");

         dbc.DeletePlayer(4);

         players = dbc.Players();
         foreach (var p in players)
         {
             Log(p.Key + " : " + p.Value);
         }


         Log("/////////////////////////////////");/**/

        /*dbc.NewGame(1, Language.Russian, Language.French, Game.Mode.PingPong);

        Log("/////////////////////////////////");/**/
        /*
        var games = dbc.Games(1);
        Game game = null;
        foreach (Game g in games)
        {
            Log("id: " + g.Id + " achievements: " + g.Achievements + " CannonSpeed: " + g.CannonSpeed + " GameMode: " + g.GameMode + 
                " GameStarted: " + g.GameStarted + " GameStopped: " + g.GameStopped + " From: " + g.LanguageFrom + " To: " + g.LanguageTo + 
                " NumHits: " + g.NumHits + " NumPossibleHits: " + g.NumPossibleHits + " NumShots: " + g.NumShots + " NumWords: " + g.NumWords +
                " Score: " + g.Score + " Vocabs: " + g.Vocabs);
            game = g;
        }


        Log("/////////////////////////////////");/**/
        /*
        dbc.LoadVocabs(game);

        foreach( var v in game.Vocabs)
        {
            Log(v);
        }

        Log("/////////////////////////////////");/**/

        /*dbc.DeleteGame(2);
        
        Log("/////////////////////////////////");/**/
        /*
        games = dbc.Games(1);
        foreach (Game g in games)
        {
            Log("id: " + g.Id + " achievements: " + g.Achievements + " CannonSpeed: " + g.CannonSpeed + " GameMode: " + g.GameMode +
                " GameStarted: " + g.GameStarted + " GameStopped: " + g.GameStopped + " From: " + g.LanguageFrom + " To: " + g.LanguageTo +
                " NumHits: " + g.NumHits + " NumPossibleHits: " + g.NumPossibleHits + " NumShots: " + g.NumShots + " NumWords: " + g.NumWords +
                " Score: " + g.Score + " WordStats: " + g.WordStats);
        }

        Log("/////////////////////////////////"); /**/
        /*
        game.CannonSpeed = 50;
        game.GameStopped = System.DateTime.Now;
        game.NumHits = 99;
        game.NumPossibleHits = 100;
        game.NumShots = 200;
        game.NumWords = 10;
        game.Score = 1234;

        Log("/////////////////////////////////"); /**/

        /*
        var vocab = dbc.GetWord(Language.German, Language.English, 1);
        Log(vocab.Item1 + " : " + vocab.Item2);

        Log("/////////////////////////////////"); /**/


        /*dbc.SaveGame(game);
        
        Log("/////////////////////////////////"); /**/

        /*IllishaOptions opts = new IllishaOptions( -1, 0.5f);

        dbc.SaveOptions(1, opts);

        Log("/////////////////////////////////"); /**/

        /*
        var dict = dbc.AllWords(Language.English, Language.German);
        foreach (var v in dict)
            Log(v.ToString());

        Log("/////////////////////////////////"); /**/

        dbc.closeDB();
        
    }

   
}
