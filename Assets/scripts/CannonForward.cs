using UnityEngine;
using System.Collections;

// TODO: implement
public class CannonForward : BarnBehaviour
{

    public enum SpeedFunction
    {
        Constant,
        Fastron
    }

    //##################################################################################################
    // VARS

    public float _distanceToWord = 20;

    public float _speed = 100;

    public SpeedFunction _speedFunction = SpeedFunction.Constant;


    //##################################################################################################
    // METHODS

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void MoveTo()
    {
        Vector3 targetPosition = GameManager.Session.Word.Glyphs[0].transform.position;
        targetPosition.y -= _distanceToWord;

        switch (_speedFunction)
        {
        case SpeedFunction.Constant:
            MoveToConstantTempo (targetPosition);
            break;
        case SpeedFunction.Fastron:
            Log("Case 2"); // TODO
            break;
        }
    }

    private void MoveToConstantTempo( Vector3 target)
    {
        // TODO
    }
}
