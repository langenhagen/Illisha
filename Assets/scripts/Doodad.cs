using UnityEngine;
using System.Collections;

public class Doodad : BarnBehaviour {


    public float _minInstantiationHeight = 0;
    public float _maxInstantiationHeight = 0;

    public float _minSpeed = 0;
    public float _maxSpeed = 0;

    
	// Use this for initialization
	void Start ()
    {
        GameManager.DoodadCreator.NumDoodads++;

        GetComponent<MoveSprite>().Speed = Random.Range( _minSpeed, _maxSpeed);
        GetComponent<RotateToTarget>().Target = Camera.main.transform;

        Vector3 position = transform.position;

        if (Random.value > 0.5f)
            position.x = Random.Range(10, 100);
        else
            position.x = Random.Range(-100, -10);

        position.y = 350;
        position.z = Random.Range(_minInstantiationHeight, _maxInstantiationHeight);
        transform.position = position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        // destroy if out of view
        if (transform.position.y < Camera.main.transform.position.y)
        {
            GameManager.DoodadCreator.NumDoodads--;
            Destroy(gameObject);
        }
	}



    //##################################################################################################
    // GETTERS & SETTERS


    public float MinInstantiationHeight
    {
        get { return _minInstantiationHeight; }
        set { _minInstantiationHeight = value; }
    }

    public float MaxInstantiationHeight
    {
        get { return _maxInstantiationHeight; }
        set { _maxInstantiationHeight = value; }
    }
}
