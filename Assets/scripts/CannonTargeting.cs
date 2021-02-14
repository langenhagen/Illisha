using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// XXX think about: maybe incorp into CannonMovement
// discourse: leave it here. so u can enable it and disable it when u want.
// interim decision: leave it as a separate class.
// discourse: integrate, that the cannon can make use of the methods, 
//            telling if u are targeting and pressing the right button
//            which leads to immediate movement of the cannon.
public class CannonTargeting : BarnBehaviour
{
	// The Glyph that is currently targeted by the cannon
	private GameObject _currentTargetingGlyph = null;


	//##################################################################################################
	// METHODS

	void Start()
	{}


	void Update ()
	{
        GameObject glyph = AimsAt();

        if (_currentTargetingGlyph != glyph)
        {
            _currentTargetingGlyph = glyph;

            if (glyph != null)
            {
                GameManager.Game.NumPossibleHits++;
                GameManager.Session.PossibleHitsTimestamps.Push(Time.time);
            }
        }
	}


    /// <summary>
    /// Finds the glyph the cannon is pointing to.
    /// </summary>
    /// <returns>The Glyph GameObject the cannon is pointing to or null if no glyph is pointed at.</returns>
	public GameObject AimsAt()
	{
        GameObject ret = null;

		Ray ray = new Ray( transform.position, Vector3.up);
        
        List<GameObject> glyphs = GameManager.Session.Word.Glyphs;

        foreach (GameObject glyph in glyphs)
		{
			if( glyph.collider.bounds.IntersectRay(ray))
			{
				ret = glyph;
				break;
			}
		}
		return ret;
	}
    
}
