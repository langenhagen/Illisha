using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : UnitySingleton<MainMenu>
{
    public GameObject _mnuPause;

    public GameObject _mnuPauseOptions;

    //##################################################################################################
    // METHODS

    void Start()
    {
        NGUITools.SetActive(_mnuPause,        false);
        NGUITools.SetActive(_mnuPauseOptions, false);
    }

    void Update()
    {
        if(Input.GetKeyDown( KeyCode.Escape))
        {
            // on escape button pressed

            if (!_mnuPause.activeSelf && !_mnuPauseOptions.activeSelf)
            {
                // open pause menu
                Show();
                
            }
            else if (_mnuPauseOptions.activeSelf)
            {
                // close pause - options menu
                GameManager.UI.MoveToMenu(_mnuPause);
            }
            else if (_mnuPause.activeSelf)
            {
                // close pause menu
                Hide();
               
            }
        }

    }
 
 
    public void Show()
    {
        Screen.showCursor = true;

        NGUITools.SetActive(_mnuPause, true);
        GameManager.UI.CurrentMenu = _mnuPause;

        // TODO not good managed here
        Time.timeScale = 0.0f;
    }

    public void Hide()
    {
        Screen.showCursor = false;
        NGUITools.SetActive(_mnuPause, false);

        GameManager.UI.CurrentMenu = _mnuPause;

        // TODO not good managed here
        Time.timeScale = 1.0f;
    }


    //##################################################################################################
    // HELPERS




    //##################################################################################################
    // GETTERS & SETTERS

    public GameObject MnuPause
    {
      get { return _mnuPause; }
      set { _mnuPause = value; }
    }
    

    public GameObject MnuPauseOptions
    {
      get { return _mnuPauseOptions; }
      set { _mnuPauseOptions = value; }
    }

}
