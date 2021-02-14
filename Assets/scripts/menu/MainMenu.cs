using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : UnitySingleton<MainMenu>
{
    public GameObject _mnuFirstPlayer;
    public GameObject _mnuHighScores;
    public GameObject _mnuNewGame;
    public GameObject _mnuOptions;
    public GameObject _mnuPlayer;
    public GameObject _mnuSelectPlayer;
    public GameObject _mnuStatistics;


    //##################################################################################################
    // METHODS

    void Start()
    {
        bool isFirstPlayer = GameManager.Players.Count == 0 ? true : false;

        GameManager.UI.CurrentMenu = isFirstPlayer ? _mnuFirstPlayer : _mnuSelectPlayer;

        NGUITools.SetActive(_mnuFirstPlayer , isFirstPlayer);
        NGUITools.SetActive(_mnuHighScores  , false);
        NGUITools.SetActive(_mnuNewGame     , false);
        NGUITools.SetActive(_mnuOptions     , false);
        NGUITools.SetActive(_mnuPlayer      , false);
        NGUITools.SetActive(_mnuSelectPlayer, !isFirstPlayer);
        NGUITools.SetActive(_mnuStatistics  , false);
    }

    //##################################################################################################
    // HELPERS

    
    //##################################################################################################
    // GETTERS & SETTERS

    public GameObject MnuPlayer
    {
        get { return _mnuPlayer; }
        set { _mnuPlayer = value; }
    }
}
