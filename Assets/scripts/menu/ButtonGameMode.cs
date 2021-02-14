using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ButtonGameMode : BarnBehaviour
{


    //##################################################################################################
    // VARS

    /// <summary>
    /// The Game.Mode that is associated with a particular button instance.
    /// </summary>
    public Game.Mode _gameMode;


    //##################################################################################################
    // METHODS

    void OnClick()
    {
        GameManager.Instance.StartNewGame(_gameMode);
    }


    //##################################################################################################
    // GETTERS & SETTERS

}