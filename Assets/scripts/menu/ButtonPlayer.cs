using UnityEngine;
using System.Collections;

public class ButtonPlayer : BarnBehaviour
{
    private Player _player = null;

    
    //##################################################################################################
    // METHODS

    void OnClick()
    {
        if (_player == null)
            Logger.Log("Player member variable is null!", Logger.Error);

        
        GameManager.Player = Player;
        GameManager.UI.MoveToMenu( GameManager.UI.MainMenu.MnuPlayer);

    }

    //##################################################################################################
    // GETTERS & SETTERS

    public Player Player
    {
        get
        {
            if( _player == null)
                Logger.Log("Player element you requested is null!", Logger.Warning);
            return _player;
        }
        set { _player = value; }
    }
}