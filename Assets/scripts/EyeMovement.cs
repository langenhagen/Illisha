using UnityEngine;
using System.Collections;

public class EyeMovement : BarnBehaviour {

    public Vector2 _eye1Center;
    public Vector2 _eye2Center;

    // max transformational difference from eye center in spacial units.
    public Vector2 _eyeVariance;

    public float _minPupilSize;
    public float _maxPupilSize;

    public float _pupilSizeDeltaMultiplier = 0.7f;


    public float TargetPupilSize { get; set; }


    //##################################################################################################
    // METHODS

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        // look direction
        var Glyphs = GameManager.Session.Word.Glyphs;
        Vector3 lookAtPosition = Glyphs[Glyphs.Count / 2].transform.position;
        


        float directionOffset = Mathf.Min(Mathf.Abs( transform.position.x - lookAtPosition.x) / 40, _eyeVariance.x);

        if (transform.position.x > lookAtPosition.x)
            directionOffset *= -1;

        renderer.material.SetVector( "_eyeCenters", 
                                     new Vector4( _eye1Center.x + directionOffset, _eye1Center.y,
                                                  _eye2Center.x + directionOffset, _eye2Center.y));

        //// pupil size
        float currentPupilSize = renderer.material.GetFloat("_pupil1Size");
        if (currentPupilSize != TargetPupilSize)
        {
            float newPupilSize = Util.Interpolate(Time.deltaTime * _pupilSizeDeltaMultiplier,
                                                    currentPupilSize,
                                                    TargetPupilSize,
                                                    _minPupilSize,
                                                    _maxPupilSize);
            
            renderer.material.SetFloat("_pupil1Size", newPupilSize);
            renderer.material.SetFloat("_pupil2Size", newPupilSize);
        }
        else
        {
            _pupilSizeDeltaMultiplier = 1;
        }
	
	}


    public float TargetPupilSizePercent
    {
        get { return TargetPupilSize / _maxPupilSize - _minPupilSize; }
        set { TargetPupilSize = value * (_maxPupilSize - _minPupilSize) + _minPupilSize; }
    }

    public void Excite()
    {
        _pupilSizeDeltaMultiplier = 0.25f;
        float pupilSize = 0.85f * _maxPupilSize;

        renderer.material.SetFloat("_pupil1Size", pupilSize);
        renderer.material.SetFloat("_pupil2Size", pupilSize);
    }
}
