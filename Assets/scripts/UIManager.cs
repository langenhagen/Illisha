using UnityEngine;
using System.Collections;

public class UIManager : BarnBehaviour
{
    private MainMenu  _mainMenu     = null;
    private PauseMenu _pauseMenu    = null;

    private Ingame2DUI _ingameUI    = null;

    private GameObject _currentMenu = null;


    //##################################################################################################
    // METHODS

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void InitMainMenu()
    {
        Screen.showCursor = true;

        MainMenu = GetComponent<MainMenu>();
    }

    public void InitIngame(GameObject ingame)
    {
        _pauseMenu = ingame.GetComponent<PauseMenu>();
        _ingameUI  = ingame.GetComponent<Ingame2DUI>();
        
    }


    public void MoveToMenu(GameObject menuToGoTo)
    {
        if (menuToGoTo == null)
            Log("Menu to go to is not set!", Logger.Error);

        /// XXX fancify
        NGUITools.SetActive(_currentMenu, false);
        NGUITools.SetActive(menuToGoTo,   true);
        _currentMenu = menuToGoTo;
    }

    //##################################################################################################
    // GETTERS & SETTERS

    public GameObject CurrentMenu { get { return _currentMenu;  }
                                    set { _currentMenu = value; }}


    public MainMenu MainMenu      { get { return _mainMenu;     }
                                    set { _mainMenu = value;    }}

    public PauseMenu PauseMenu    { get { return _pauseMenu;    }
                                    set { _pauseMenu = value;   }}


    public Ingame2DUI IngameUI    { get { return _ingameUI;     }
                                    set { _ingameUI = value;    }}


}
