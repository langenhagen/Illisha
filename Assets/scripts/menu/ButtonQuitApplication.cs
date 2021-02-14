using UnityEngine;
using System.Collections;

public class ButtonQuitApplication : BarnBehaviour
{
    public void OnClick()
	{
        GameManager.Instance.QuitApplication();
	}
}
