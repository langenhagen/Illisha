using UnityEngine;
using System.Collections;

public class InputNewPlayer : BarnBehaviour
{

    //##################################################################################################
    // METHODS

    public void CreateNewPlayer()
    {
        GameManager.Instance.CreateNewPlayerAndMoveToPlayerMenu(GetComponent<UIInput>().value);
    }
}