using UnityEngine;
using System.Collections.Generic;

public interface DBConnector
{
    void openDB(string filename);

    void closeDB();

    Pair<string, string> Word(Language from, Language to, int matchID);

    /// <summary>
    /// Returns a List of all players.
    /// The players are retrieved with their options but without their games.
    /// If you want to load a player with his/her preloaded games call LoadGames().
    /// </summary>
    /// <returns>A List of all players.</returns>
    List<Player> Players();

    List<Pair<string, int>> Highscore();

    Player NewPlayer(string name);

    void DeletePlayer(int playerID);


    /// <summary>
    /// Retrieves a specific game with the specified id.
    /// </summary>
    /// <param name="id">The id of the game.</param>
    /// <returns>The game with a specified id.</returns>
    Game Game(int id);

    /// <summary>
    /// Retrieves all games of a player and loads them into the player-object. 
    /// However, it doesn't load them completely as it doesn't load 
    /// the word_stats_game_[id] tables and therefore doesn't 
    /// load the games' Vocab-dictionaries.
    /// </summary>
    /// <param name="player">The player.</param>
    void LoadGames(Player player);


    /// <summary>
    /// Adds the whole Vocabs for to a given Game object.
    /// </summary>
    /// <param name="game">The Game to which the vocabulary shall be added.</param>
    void LoadVocabs(Game game);


    /// <summary>
    /// Creates a new game in the db and returns it. Beware, this Game will be without vocabs.
    /// If you want to load a game's Vocabs, call LoadVocabs().
    /// It puts the game into the player's list of games.
    /// </summary>
    /// <param name="playerID">The player's id,</param>
    /// <param name="from">The native language.</param>
    /// <param name="to">The language to learn.</param>
    /// <param name="mode">The mode to play the game.</param>
    /// <returns>A game.</returns>
    Game NewGame(Player player, Language from, Language to, Game.Mode mode);

    void DeleteGame(int gameID);

    void SaveOptions(int playerID, IllishaOptions options);


    /// <summary>
    /// Saves the given game.
    /// Beware, this shall not update the game.DateTimeStopped Property automatically.
    /// If you want to set it right, set it by yourself.
    /// </summary>
    /// <param name="game">The game to be saved.</param>
    void SaveGame(Game game);
}