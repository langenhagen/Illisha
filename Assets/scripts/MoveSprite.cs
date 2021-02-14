using UnityEngine;
using System.Collections;

public class MoveSprite : MonoBehaviour {

    // since movesprite orientates its speed on the ground plane, connect it here
    public Transform _groundPlane;


    public float _speed = 0;


    private float _planeSpeed;


    //##################################################################################################
    // METHODS

	// Use this for initialization
	void Start () 
    {
        _planeSpeed =
            _groundPlane.GetComponent<ScrollTexture>().Speed *
            _groundPlane.localScale.y / _groundPlane.renderer.material.mainTextureScale.y;
	}
	
	// Update is called once per frame
	void Update ()
    {

        float speed = (ScrollTexture.GlobalSpeed * _planeSpeed + _speed) * Time.deltaTime ;

        // move sprite
        transform.Translate(Vector3.down  * Mathf.Cos(ScrollTexture.GlobalAngle) * speed, Space.World);
        transform.Translate(Vector3.right * Mathf.Sin(ScrollTexture.GlobalAngle) * speed, Space.World);
	}

    //##################################################################################################
    // GETTERS & SETTERS

    public float    Speed           {  get { return _speed;         }
                                       set { _speed = value ;       }}


    public Transform GroundPlane    { get { return _groundPlane;    }
                                      set { _groundPlane = value;   }}
}
