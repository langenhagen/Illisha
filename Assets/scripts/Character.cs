using UnityEngine;
using System.Collections;

public class Character : BarnBehaviour {

    public EyeMovement _eyeMovement;
    

    //##################################################################################################
    // METHODS

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //##################################################################################################
    // GETTERS & SETTERS


    public EyeMovement EyeMovement
    {
        get { return _eyeMovement; }
        set { _eyeMovement = value; }
    }

}
