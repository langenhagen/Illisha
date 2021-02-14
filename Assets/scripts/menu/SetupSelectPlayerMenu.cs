using UnityEngine;
using System.Collections.Generic;

public class SetupSelectPlayerMenu : BarnBehaviour
{

    public UIGrid _gridPlayers;

    public UIButton _scrollButtonPrefab;

    //##################################################################################################
    // METHODS


    void OnEnable()
    {
        List<Player> players = GameManager.Players;

        foreach(Transform btn in _gridPlayers.transform)
            Destroy(btn.gameObject);

        foreach (Player player in players)
        {
            UIButton button = Instantiate(_scrollButtonPrefab) as UIButton;
            button.GetComponentInChildren<UILabel>().text = player.Name;

            button.transform.parent = _gridPlayers.transform;
            button.transform.localScale = Vector3.one;

            // create a Script for selecting a player and switching the menu.
            ButtonPlayer script = button.gameObject.AddComponent<ButtonPlayer>() ;
            script.Player = player;
        }

        _gridPlayers.repositionNow = true;
    }
}
