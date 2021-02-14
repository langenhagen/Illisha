using UnityEngine;
using System.Collections;

public class ButtonMoveToMenu : BarnBehaviour
{
    public GameObject _menuToGoTo;
    

	//##################################################################################################
	// METHODS


    /// <summary>
    /// Moves from one sprite to another.
    /// </summary>
    /// <param name="destroyMoveInMenuScriptAfterwards">Specifies, if this MoveInMenu Script shall be destroyed after moving.</param>
    public void move( bool destroyMoveInMenuScriptAfterwards) // TODO write me groß
    {
        if (_menuToGoTo == null)
            Log("Member variable _menuToGoTo is null!", Logger.Error);


        GameManager.UI.MoveToMenu(_menuToGoTo);

        if (destroyMoveInMenuScriptAfterwards)
            Destroy(this);
    }


    void OnClick()
	{
        move(false);
	}



    //##################################################################################################
    // GETTERS & SETTERS

    public GameObject MenuToGoTo { get { return _menuToGoTo;  }
                                   set { _menuToGoTo = value; }}
}
