using UnityEngine;
using System.Collections;

public class ButtonNewPlayer : BarnBehaviour
{

    public UIInput _txtInput;

	
	//##################################################################################################
	// METHODS


    void OnClick()
    {
        GameManager.Instance.CreateNewPlayerAndMoveToPlayerMenu(_txtInput.value);
    }
}
