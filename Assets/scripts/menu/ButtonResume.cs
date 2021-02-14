using UnityEngine;
using System.Collections;

public class ButtonResume : BarnBehaviour {

    void OnClick()
    {
        GameManager.UI.PauseMenu.Hide();
        Time.timeScale = 1;
    }
}
