using UnityEngine;

using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Superimposing godlike omnipresent GameManagerClass
/// </summary>
public class GameManager : UnitySingleton<GameManager>
{
    // the languages that are fixed for the app.
    private readonly Language _fromLanguage = Language.German;
    private readonly Language _toLanguage   = Language.English;

    public        GameState             GameState     { get; set; }
                                                
    public static DBConnector           DB            { get; set; }
    public static Session               Session       { get; set; }
    public static UIManager             UI            { get; set; }
    public static PointPowerScript      PPScript      { get; set; }
    public static List<Player>          Players       { get; set; }   // List of all Players.
    public static Character   Character     { get; set; }

    public static DoodadCreator    DoodadCreator { get; set; }

    private Game    _game   = null;     // The current game. (couldn't be static -- dunno why)
    private Player  _player = null;     // The current player.


    //##################################################################################################
    // METHODS

    void Awake()
    {
        DontDestroyOnLoad(this);

        DB      = new SQLiteConnector("Illy.db");
        Players = DB.Players();

        UI = gameObject.AddComponent<UIManager>();
        DontDestroyOnLoad(UI);

#if UNITY_EDITOR
        if (EditorApplication.currentScene == "Assets/MainMenu.unity")
        {
            // Do Main Menu stuff

            InitMainMenu();
        }
        else
        {
            // Do ingame stuff
            InitIngame();
        }
#else
        InitMainMenu();
#endif

    }

    private void InitMainMenu()
    {
        GameState = GameState.MainMenu;
        
        Players   = DB.Players();
        UI.InitMainMenu();
    }

    private void InitIngame()
    {

        Log("", Logger.Type.Error);
        GameState = GameState.Ingame;

        Screen.showCursor = false;

        if (Game == null && _player == null)
        {
            print("not initialized. using first player and his first game!");

            _player = DB.Players()[0];
            DB.LoadGames(_player);
            Game = _player.Games[0];
        }

        Game.LoadVocabs();
        Game.AchievementController.InitAchievements();
        

        GameObject ingame = FindGameObjectWithTagSafe("Ingame");

        Session       = ingame.GetComponent<Session>();
        PPScript      = ingame.GetComponent<PointPowerScript>();
        DoodadCreator = ingame.GetComponent<DoodadCreator>();
        Character     = ingame.GetComponent<Character>();

        UI.InitIngame(ingame);
        Session.Init();
    }

    void OnLevelWasLoaded(int level)
    {
        if( level == 0)
        {
            // main menu
            InitMainMenu();
        }
        else if (level == 1)
        {
            // ingame
            InitIngame();
            
        }
    }


    public void QuitSession()
    {
        // XXX do something bout that fancify
        GameState = GameState.MainMenuPlayer;

        Application.LoadLevel(0);
    }

    public void QuitApplication()
    {
        // XXX fancify

        Application.Quit();
    }

    /// <summary>
    /// Creates a new player, puts it into the players list and makes it the current player.
    /// </summary>
    /// <param name="name">The name of the new player.</param>
    /// <returns>The Player object.</returns>
    public void CreateNewPlayerAndMoveToPlayerMenu(string name)
    {
        if (name.Length > 0)
        {
            Log("Creating Player named '" + name + "'...");

            _player = DB.NewPlayer(name);
            Players.Add(_player);

            UI.MoveToMenu( UI.MainMenu.MnuPlayer);
        }
    }

    public void SaveOptions(IllishaOptions options)
    {
        _player.Options = options;
        DB.SaveOptions(_player.Id, options);
    }


    public void StartNewGame( Game.Mode gameMode)
    {
        Game = DB.NewGame(_player, _fromLanguage, _toLanguage, gameMode);

        LoadSession();
    }

    public void LoadSession()
    {
        FadeOut fadeOut = GetComponent<FadeOut>();
        fadeOut.StartFade();
        Invoke("StartSession", fadeOut.FadeDurationInSeconds);
    }

    private void StartSession()
    {
        Application.LoadLevel(1);
    }

    /// <summary>
    /// Retrieves the highscore.
    /// </summary>
    /// <returns>The hiscore, meaning the names of the players and their scores on single games.</returns>
    public List<Pair<string, int>> Highscore()
    {
        return DB.Highscore();
    }



    private void SaveGame()
    {
        Log("Saving Game... ");
        Game.DateTimeStopped = System.DateTime.Now;
        DB.SaveGame(Game);
    }

    //##################################################################################################
    // GETTERS & SETTERS

    public static Player Player {   get { return GameManager.Instance._player;      }
                                    set {   GameManager gm = GameManager.Instance;
                                            gm._player = value;
                                            GameManager.DB.LoadGames(gm._player);   }}


    public static Game   Game   {   get { return GameManager.Instance._game;        }
                                    set { GameManager.Instance._game = value;       }}


}
