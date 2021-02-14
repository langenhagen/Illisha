using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;


// XXX optimize
public class SQLiteConnector : DBConnector
{
    private SQLiteDB _db = null;

    //##################################################################################################
    // CONSTRUCTOR

    /// <summary>
    /// Constructs the Connector and opens the database, i.e. calls openDB().
    /// </summary>
    /// <param name="dbFileName">The filename of the db, e.g. "Foo.db".</param>
    public SQLiteConnector( string dbFileName)
    {
        _db = new SQLiteDB();

        string dbPath = Application.persistentDataPath + "/" + dbFileName;

        if (!File.Exists(dbPath))
        {
            Logger.Log("Database \"" + dbPath + "\" doesn't exist and will now be created.");

            // first time application starts
            createDBAtPersistentPath(dbFileName /*simple fname*/);
        }

        openDB(dbPath);
    }

    //##################################################################################################
    // METHODS

    public void openDB(string dbPath)
    {
        if (File.Exists(dbPath))
        {
            // *** dtabase exists ***
            try
            {
                _db.Open(dbPath);
            }
            catch (Exception e)
            {
                Logger.Log(e.Message + "\n" + e.StackTrace + "\n" + 
                           "On WebPlayer it must give an exception, it's normal.", Logger.Exception);
            }
        }
        else
        {
            Logger.Log("Database " + dbPath + " doesn't exist!", Logger.Exception);
            throw (new System.Exception("Database " + dbPath + " doesn't exist!"));
        }
    }

    public void closeDB()
    {
        _db.Close();
    }

    public Pair<string, string> Word(Language from, Language to, int matchID)
    {
        Pair<string, string> ret = new Pair<string,string>();

        string tableFrom  = TableLanguageString(from);
        string tableTo    = TableLanguageString(to);
        string tableMatch = TableMatchString(from, to);

        string query = "SELECT " + tableFrom + ".phrase AS f, " + tableTo + ".phrase AS t " +
                       "FROM " + tableMatch + " " +
                       "JOIN " + tableFrom + " ON " + tableMatch + ".f = " + tableFrom + ".id " +
                       "JOIN " + tableTo   + " ON " + tableMatch + ".t = " + tableTo   + ".id " +
                       "WHERE " + tableMatch + ".id = " + matchID;


        SQLiteQuery q = new SQLiteQuery(_db, query);
        q.Step();

        ret.Item1 = q.GetString("f");
        ret.Item2 = q.GetString("t");

        q.Release();

        return ret;
    }

    /// <summary>
    /// Returns a List of all players.
    /// The players are retrieved with their options but without their games.
    /// If you want to load a player with his/her preloaded games.
    /// </summary>
    /// <returns>A List of all players.</returns>
    public List<Player> Players()
    {
        List<Player> ret = new List<Player>();
        
        string query = "SELECT * FROM players";
        SQLiteQuery q = new SQLiteQuery(_db, query);


        while (q.Step())
        {
            int id = q.GetInteger("id");
            string name = q.GetString("name");
            IllishaOptions options = new IllishaOptions();
            options.VolumeFx    = (float)q.GetDouble("volume_fx");
            options.VolumeMusic = (float)q.GetDouble("volume_music");

            Player player = new Player(id, name, options);

            ret.Add(player);
        }

        q.Release();

        return ret;
    }

    public List<Pair<string,int>> Highscore()
    {
        List<Pair<string, int>> ret = new List<Pair<string, int>>();

        string query = "SELECT players.name AS name, games.score AS score " +
                        "FROM games JOIN players ON players.id = games.player " +
                        "WHERE games.score > 0 " +
                        "ORDER BY score DESC LIMIT 10";

        SQLiteQuery q = new SQLiteQuery(_db, query);

        while (q.Step())
        {
            string name = q.GetString("name");
            int score   = q.GetInteger("score");
 
            ret.Add( new Pair<string,int>( name, score));
        }

        return ret;
    }

    public Player NewPlayer(string name)
    {
        string query = "INSERT INTO players (name) VALUES ('" + name + "')";
        SQLiteQuery q = new SQLiteQuery(_db, query);
        q.Step();
        q.Release();

        query = " SELECT * FROM players WHERE name = '" + name + "'";
        q = new SQLiteQuery(_db, query);

        int id = -1;

        while (q.Step())
        {
            int current_id = q.GetInteger("id");
            if (id < current_id)
                id = current_id;
        }
        q.Release();

        return new Player(id, name);
    }

    public void DeletePlayer(int playerID)
    {
        // XXX via triggers, it's may bee faster

        string query = "DELETE FROM players WHERE id = " + playerID;
        SQLiteQuery q = new SQLiteQuery(_db, query);
        q.Step();
        q.Release();

        query = "SELECT id FROM games where player = " + playerID + ";";

        while (q.Step())
        {
            DeleteGame( q.GetInteger("id"));
        }
    }

    /// <summary>
    /// Retrieves a specific game with the specified id.
    /// </summary>
    /// <param name="id">The id of the game.</param>
    /// <returns>The game with a specified id.</returns>
    public Game Game(int id)
    {
        Game ret;

        string query = "SELECT * FROM games WHERE id = " + id + ";";
        SQLiteQuery q = new SQLiteQuery(_db, query);
        q.Step();
        
        ret = new Game(q.GetInteger("id"),
                       q.GetString("lng_from").ToLanguage(),
                       q.GetString("lng_to").ToLanguage(),
                       q.GetString("game_mode").ToMode());

        ret.DateTimeStarted = q.GetString("datetime_started").ToDateTime();
        ret.DateTimeStopped = q.GetString("datetime_stopped").ToDateTime();

        ret.NumShots        = q.GetInteger("num_shots");
        ret.NumHits         = q.GetInteger("num_hits");
        ret.NumWords        = q.GetInteger("num_words");
        ret.NumPossibleHits = q.GetInteger("num_possible_hits");

        ret.CannonSpeed = (float)q.GetDouble("cannon_speed");

        ret.Score = q.GetInteger("score");

        return ret;
    }

    /// <summary>
    /// Retrieves all games of a player and loads them into the player-object. 
    /// However, it doesn't load them completely as it doesn't load 
    /// the word_stats_game_[id] tables and therefore doesn't 
    /// load the games' Vocab-dictionaries.
    /// </summary>
    /// <param name="player">The player.</param>
    public void LoadGames( Player player)
    {
        List<Game> games = new List<Game>();

        string query = "SELECT * FROM games WHERE player = " + player.Id + " ORDER BY datetime_stopped DESC ;";
        SQLiteQuery q = new SQLiteQuery(_db, query);

        while (q.Step())
        {    
            Game game = new Game(q.GetInteger("id"),
                                 q.GetString("lng_from").ToLanguage(),
                                 q.GetString("lng_to"  ).ToLanguage(),
                                 q.GetString("game_mode").ToMode() );

            game.DateTimeStarted = q.GetString("datetime_started").ToDateTime();
            game.DateTimeStopped = q.GetString("datetime_stopped").ToDateTime();

            game.NumShots        = q.GetInteger("num_shots");
            game.NumHits         = q.GetInteger("num_hits");
            game.NumWords        = q.GetInteger("num_words");
            game.NumPossibleHits = q.GetInteger("num_possible_hits");

            game.CannonSpeed = (float)q.GetDouble("cannon_speed");

            game.Score = q.GetInteger("score");


            games.Add(game);
        }
        q.Release();

        player.Games = games;
    }

    /// <summary>
    /// Adds the whole Vocabs for to a given Game object.
    /// </summary>
    /// <param name="game">The Game to which the vocabulary shall be added.</param>
    public void LoadVocabs( Game game)
    {
        string tableFrom  = TableLanguageString(game.LanguageFrom);
        string tableTo    = TableLanguageString(game.LanguageTo);
        string tableMatch = TableMatchString(game.LanguageFrom, game.LanguageTo);
        string tableStats = TableStatsString( game.Id);

        string query = "SELECT " + tableMatch + ".id AS match_id, " + tableFrom + ".phrase AS f, " + tableTo + ".phrase AS t, " + 
                            tableTo + ".commonness AS commonness, " + tableStats + ".time_for_phrase AS time_for_phrase, " + tableStats + ".count AS count " +
                        "FROM " + tableMatch + " " +
                        "LEFT OUTER JOIN " + tableStats + " ON " + tableStats + ".match_id = " + tableMatch + ".id " +
                        "JOIN " + tableFrom + " ON " + tableFrom + ".id = " + tableMatch + ".f " +
                        "JOIN " + tableTo + " ON " + tableTo + ".id = " + tableMatch + ".t;";

        SQLiteQuery q = new SQLiteQuery(_db, query);

        while (q.Step())
        {
            float timeForPhrase = 0;
            int count = 0;

            if (!q.IsNULL("time_for_phrase"))
            {
                timeForPhrase = (float)q.GetDouble("time_for_phrase");
                count = q.GetInteger("count");
            }
            
            Vocab vocab = new Vocab( q.GetString("f"), q.GetString("t"), (float)q.GetDouble("commonness"), timeForPhrase, count);
            game.Vocabs.Add( q.GetInteger("match_id"), vocab);
        }

        q.Release();
    }

    /// <summary>
    /// Creates a new game in the db and returns it. Beware, this Game will be without vocabs.
    /// If you want to load a game's Vocabs, call LoadVocabs().
    /// It puts the game into the player's list of games.
    /// </summary>
    /// <param name="playerID">The player's id,</param>
    /// <param name="from">The native language.</param>
    /// <param name="to">The language to learn.</param>
    /// <param name="mode">The mode to play the game.</param>
    /// <returns>A game.</returns>
    public Game NewGame(Player player, Language from, Language to, Game.Mode mode)
    {
        Game ret = new Game(-1, from, to, mode);

        string f = from.Shortcut();
        string t = to.Shortcut();

        string datetimeStarted = ret.DateTimeStarted.ToSQLiteString();
        string datetimeStopped = ret.DateTimeStopped.ToSQLiteString();


        string query = "INSERT INTO games (player, game_mode, lng_from, lng_to, datetime_started, datetime_stopped, " +
                                          "num_shots, num_hits, num_words, num_possible_hits, cannon_speed, score) " +
                       "VALUES (" + player.Id + ", '" + mode + "', '" + f + "', '" + t + "', '" + datetimeStarted + "', '" + datetimeStopped + "', " +
                                ret.NumShots + ", " + ret.NumHits + ", " + ret.NumWords + ", " + ret.NumPossibleHits + ", " + ret.CannonSpeed + ", " + ret.Score + ");";

        SQLiteQuery q = new SQLiteQuery(_db, query);
        q.Step();
        q.Release();
        
        
        // get id
        query = "SELECT max(id) as id FROM games";
        q = new SQLiteQuery(_db, query);
        q.Step();
        
        ret.Id = q.GetInteger("id");
        q.Release();

        // create empty word_stats_game_[id] table
        query = "CREATE TABLE word_stats_game_" + ret.Id + " (" +
                    "match_id        INTEGER PRIMARY KEY, " +
                    "count           INTEGER DEFAULT ( 0 ), " +
                    "time_for_phrase REAL    DEFAULT ( 0 ));";

        q = new SQLiteQuery(_db, query);
        q.Step();
        q.Release();

        player.Games.Add(ret);

        return ret;
    }

    public void DeleteGame(int gameID)
    {
        string query = "DELETE FROM games WHERE id = " + gameID + ";";
        SQLiteQuery q = new SQLiteQuery(_db, query);
        q.Step();
        q.Release();

        query = "DROP TABLE word_stats_game_" + gameID + ";";
        q = new SQLiteQuery(_db, query);
        q.Step();
        q.Release();
    }


    public void SaveOptions(int playerID, IllishaOptions options)
    {
        string query = "UPDATE players SET " +
                           "volume_fx= "    + options.VolumeFx + ", " +
                           "volume_music= " + options.VolumeMusic + " " +
                       "WHERE id = " + playerID + ";";

        SQLiteQuery q = new SQLiteQuery(_db, query);
        q.Step();
        q.Release();
    }


    /// <summary>
    /// Saves the given game.
    /// Beware, this doesn't update the game.DateTimeStopped Property automatically.
    /// If you want to set it right, set it by yourself.
    /// </summary>
    /// <param name="game">The game to be saved.</param>
    public void SaveGame(Game game)
    {
        // update games row
        string query = "UPDATE games SET " +
                           "cannon_speed= "      + game.CannonSpeed + ", " +
                           "datetime_stopped= '" + game.DateTimeStopped.ToSQLiteString() + "', " +
                           "num_hits= "          + game.NumHits + ", " +
                           "num_possible_hits= " + game.NumPossibleHits + ", " +
                           "num_shots= "         + game.NumShots + ", " +
                           "num_words= "         + game.NumWords + ", " +
                           "score= "             + game.Score + " " +
                       "WHERE id = " + game.Id + ";";


        SQLiteQuery q = new SQLiteQuery(_db, query);
        q.Step();
        q.Release();

        // delete content of word_stats_game_[id]
        query = "DELETE FROM word_stats_game_" + game.Id + ";";
        q = new SQLiteQuery(_db, query);
        q.Step();
        q.Release();


        // create single rows for insert into word_stats_game_[id]
        string rowString = "";

        Dictionary<int, Vocab> vocabs = GameManager.Game.Vocabs;
        foreach( KeyValuePair<int,Vocab> pair in vocabs)
        {
            if (pair.Value.Count > 0)
            {
                rowString += "( " + pair.Key + ", " + pair.Value.Count + ", " + pair.Value.TimeForPhrase + "),";
            }   
        }

        if (rowString.Length > 0)
        {
            rowString = rowString.Substring(0, rowString.Length - 1);

            // and insert
            query = "INSERT INTO word_stats_game_" + game.Id + "VALUES " + rowString + ";";
            q = new SQLiteQuery(_db, query);
            q.Step();
            q.Release();
        }
    }


    //##################################################################################################
    // HELPERS


    // lets copy prebuild database from StreamingAssets and load store to persistancePath with Test2
    public void createDBAtPersistentPath(string filename /*simple fname*/)
    {
        // grab bytes from prebuilt database
        byte[] bytes = null;

        string persistentPathFileName = Application.persistentDataPath + "/" + filename;

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        string dbpath = "file://" + Application.streamingAssetsPath + "/" + filename;
        WWW www = new WWW(dbpath);
        //yield return www;
        bytes = www.bytes;

#elif UNITY_WEBPLAYER
        string dbpath = "StreamingAssets/" + filename;
        WWW www = new WWW(dbpath);
        //yield return www;
        bytes = www.bytes;

#elif UNITY_IPHONE
        string dbpath = Application.dataPath + "/Raw/" + filename;					
        try
        {
            using ( FileStream fileStream = new FileStream(dbpath, FileMode.Open, FileAccess.Read, FileShare.Read) )
            {
                bytes = new byte[fileStream.Length];
                fileStream.Read(bytes,0,(int)fileStream.Length);
            }
        }
        catch (Exception e)
        {
            Logger.Log(e.Message + "\n" + e.StackTrace);
        }

#elif UNITY_ANDROID
        string dbpath = Application.streamingAssetsPath + "/" + filename;
        WWW www = new WWW(dbpath);
        //yield return www;
        bytes = www.bytes;

#endif

        if (bytes != null)
        {
            try
            {
                // copy database to real file into cache folder
                using (FileStream fileStream = new FileStream(persistentPathFileName, FileMode.Create, FileAccess.Write))
                {
                    fileStream.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message + "\n" + e.StackTrace);
            }
        }
    }


    private string TableLanguageString( Language language)
    {
        return "lng_" + language.Shortcut(); ;
    }

    private string TableMatchString( Language from, Language to)
    {
        return "match_" + from.Shortcut() + "_" + to.Shortcut();
    }

    private string TableStatsString( int gameID)
    {
        return "word_stats_game_" + gameID;
    }
}