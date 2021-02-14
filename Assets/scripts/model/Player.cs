using System;
using System.Collections.Generic;

/// <summary>
/// Model class that describes a player within Illisha.
/// </summary>
public class Player
{
    private int    _id;
    private string _name;

    private IllishaOptions _options;
    private List<Game>     _games;

    //##################################################################################################
    // CONSTRUCTORS


	public Player ( int id, string name)
        : this(id, name, new IllishaOptions(), new List<Game>())
	{}


    public Player(int id, string name, IllishaOptions options)
        : this(id, name, options, new List<Game>())
    {}


    public Player(int id, string name, IllishaOptions options, List<Game> games)
    {
        _id = id;
        _name = name;

        _options = options;
        _games = games;
    }

    //##################################################################################################
    // METHODS

    //##################################################################################################
    // GETTERS & SETTERS

    public int Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public IllishaOptions Options
    {
        get
        {
            return _options;
        }
        set
        {
            _options = value;
        }
    }

    public List<Game> Games
    {
        get
        {
            return _games;
        }
        set
        {
            _games = value;
        }
    }
}