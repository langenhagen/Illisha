using UnityEngine;
using System.Collections;

public class SetupPlayerMenu : BarnBehaviour {

    public UILabel _lblPlayerName;

    public UILabel _lblInfo;

    public UIGrid _gridGames;

    public UIButton _scrollButtonPrefab;

    //##################################################################################################
    // METHODS


    // XXX fastify
    void OnEnable()
    {
        Player player = GameManager.Player;
        
        _lblPlayerName.text = player.Name;

        int lettersTyped = 0;
        int wordsTyped   = 0;

        // delete all currently inhibited buttons
        foreach(Transform btn in _gridGames.transform)
            Destroy(btn.gameObject);


        // update the lettersTyped, wordsTyped and create fresh buttons for every game
        foreach (Game game in player.Games)
        {
            lettersTyped += game.NumHits;
            wordsTyped   += game.NumWords;

            UIButton button = Instantiate(_scrollButtonPrefab) as UIButton;
            button.GetComponentInChildren<UILabel>().text = game.GameMode + "\n" + 
                                                            game.DateTimeStarted.ToString() + " - " + game.DateTimeStopped;


            button.transform.parent     = _gridGames.transform;
            button.transform.localScale = Vector3.one;
            

            // create a Script for a gameand switching the menu / back into game scene.
            ButtonLoadGame script = button.gameObject.AddComponent<ButtonLoadGame>();
            script.Game = game;
        }

        _gridGames.repositionNow = true;
        
        _lblInfo.text = lettersTyped + " Letters typed\n" +
                        wordsTyped   + " Words typed";
    }

}

