using UnityEngine;
using System.Collections;

public class CameraFlight : BarnBehaviour {


    public CannonMovement _cannonMovement;

    public float _rollDirectionAndFrequency = 0.75f;

    public float _multiplierAngle = 5;
    public float _multiplierFoV = 2;


    public  float _enableDuration        = 5;
    private float _rollingSinceSeconds   = 0;

    

    private Camera _camera;

    private float _internalTimeAngle;


    //##################################################################################################
    // METHODS

	// Use this for initialization
	void Start ()
    {
        _camera = GetComponent<Camera>();
        
        _internalTimeAngle = Time.time;
	}

    
	
	// Update is called once per frame
	void Update ()
    {

        float deltaTime = Time.deltaTime;


        // speed getting and setting

        float speed = _cannonMovement.Speed;
        ScrollTexture.GlobalSpeed = speed;


        // FoV calculation & adjustment
 
        float targetFoV     = 60 + speed * _multiplierFoV;

        if (targetFoV != _camera.fieldOfView)
        {
            _camera.fieldOfView = Util.Interpolate(deltaTime, _camera.fieldOfView, targetFoV, 60, 120);

            GameManager.Character.EyeMovement.TargetPupilSizePercent  = (targetFoV - 50) / 120;
        }
        

        // camera roll
        if (IsRolling())
        {

            float curve = _rollingSinceSeconds < _enableDuration / 2 ? _rollingSinceSeconds : _enableDuration - _rollingSinceSeconds;

            _internalTimeAngle   += deltaTime * curve * _rollDirectionAndFrequency;
            _rollingSinceSeconds += deltaTime;


            Vector3 camRotation = transform.rotation.eulerAngles;

            camRotation.y = (Mathf.Sin(_internalTimeAngle)) * _multiplierAngle * speed;
            transform.eulerAngles = camRotation;

            ScrollTexture.GlobalAngle = Mathf.Deg2Rad * camRotation.y * 0.004f;
        }
	}


    public void Roll()
    {
        _rollingSinceSeconds = 0;

        _rollDirectionAndFrequency *= Random.value > 0.5 ?  1 : -1;
    }

    public bool IsRolling()
    {
        return _rollingSinceSeconds < _enableDuration;
    }

}
