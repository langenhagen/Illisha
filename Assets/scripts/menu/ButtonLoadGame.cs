using UnityEngine;
using System.Collections;

public class ButtonLoadGame : BarnBehaviour
{
    private Game _game;

    //##################################################################################################
    // METHODS


    void OnClick()
    {
        GameManager.Game = Game;
        GameManager.Instance.LoadSession();
    }


    //##################################################################################################
    // GETTERS & SETTERS

    public Game Game
    {
        get
        {
            if (_game== null)
                Logger.Log("Game element you requested is null!", Logger.Warning);
            return _game;
        }
        set { _game = value; }
    }

}