
/// <summary>
/// Storage class for Options in the game.
/// </summary>
public class IllishaOptions
{

	private float _volumeFx;
	private float _volumeMusic;

	//##################################################################################################
	// CONSTRUCTOR

	public IllishaOptions ()
	{
        _volumeFx = 0.8f;
        _volumeMusic = 0.8f;
	}

    public IllishaOptions ( float volumeFx, float volumeMusic)
    {
        VolumeFx = volumeFx;
        VolumeMusic = volumeMusic; 
    }


	//##################################################################################################
	// METHODS

	//##################################################################################################
	// GETTER & SETTER

	public float VolumeFx {
		get {
			return _volumeFx;
		}
		set {
            _volumeFx = System.Math.Max( 0, System.Math.Min (1, value));
		}
	}

	public float VolumeMusic {
		get {
			return _volumeMusic;
		}
		set {
            _volumeMusic = System.Math.Max( 0, System.Math.Min (1, value));
		}
	}
}

