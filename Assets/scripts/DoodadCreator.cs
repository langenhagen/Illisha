using UnityEngine;
using System.Collections;

// TODO everything
public class DoodadCreator : BarnBehaviour {

    public Transform _doodadSat;
    public Transform _doodadIgloo;

    public int _minNumDoodads = 0;
    public int _maxNumDoodads = 5;

    public Transform _groundPlane;

    public int NumDoodads { get; set; }

    //##################################################################################################
    // METHODS

	// Use this for initialization
	void Start ()
    {
	
	}
	
	void Update ()
    {

        if (Random.value > 0.999f && NumDoodads < _maxNumDoodads)
        {
            Transform prefab;

            if (Random.value < 0.5f)
                prefab = _doodadSat;
            else
                prefab = _doodadIgloo;



            Transform doodad = Instantiate(prefab) as Transform;
            doodad.GetComponent<MoveSprite>().GroundPlane = _groundPlane;
        }

	}


}
