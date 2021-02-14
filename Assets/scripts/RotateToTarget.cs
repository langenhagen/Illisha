using UnityEngine;
using System.Collections;

[ExecuteInEditMode()] 
public class RotateToTarget : MonoBehaviour {

    public Transform _target;

    public Vector3 _rotationOffset;

    public bool _rotateX = false;
    public bool _rotateY = false;
    public bool _rotateZ = true;


    //##################################################################################################
    // METHODS
	

	// Update is called once per frame
	void Update ()
    {
        Vector3 difference = transform.position - _target.position;
        difference.Normalize();
        
        float posX = _rotateX ? transform.position.x : _target.position.x;
        float posY = _rotateY ? transform.position.y : _target.position.y;
        float posZ = _rotateZ ? transform.position.z : _target.position.z;


        transform.LookAt(new Vector3(posX, posY, posZ), Vector3.back);
        transform.Rotate(new Vector3(_rotationOffset.x, _rotationOffset.y, _rotationOffset.z));
	}


    //##################################################################################################
    // GETTERS & SETTERS

    public Transform Target
    {
        get { return _target; }
        set { _target = value; }
    }
}
