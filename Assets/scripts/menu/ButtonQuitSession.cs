using UnityEngine;
using System.Collections;

public class ButtonQuitSession : BarnBehaviour
{

    public void OnClick()
    {
        GameManager.Instance.QuitSession();
    }
}
