using UnityEngine;
using System.Collections;

public class FoVBla : BarnBehaviour
{


    Camera _cam;

    bool up = true;

    // Use this for initialization
    void Start()
    {

        _cam = gameObject.GetComponent<Camera>();


    }

    //##################################################################################################
    // METHODS

    // Update is called once per frame
    void Update()
    {
        if(up)
         _cam.fieldOfView += Time.deltaTime;
        else
            _cam.fieldOfView -= Time.deltaTime;

        if (_cam.fieldOfView > 178)
            up = false;
        else if (_cam.fieldOfView < 2)
            up = true;
    }


   
}
