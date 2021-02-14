using System;

/// <summary>
/// Class with arbitrary extension methods functions for the Illisha game.
/// </summary>
public static class IllishaExtensionMethods
{
    /// <summary>
    /// Retrieves a shortcutstring representation for a language.
    /// </summary>
    /// <param name="language">A language.</param>
    /// <returns>A string representation of that language.</returns>
    public static string Shortcut(this Language language)
    {
        string ret;

        switch (language)
        {
            case Language.English:
                ret = "en";
                break;
            case Language.French:
                ret = "fr";
                break;
            case Language.German:
                ret = "de";
                break;
            case Language.Russian:
                ret = "ru";
                break;
            default:
                throw (new System.ArgumentException("Language " + language + " not handleable!", language.ToString()));
        }

        return ret;
    }


    public static Language ToLanguage(this string language)
    {
        Language ret;

        if (language == "en")
        {
            ret = Language.English;
        }
        else if (language == "ru")
        {
            ret = Language.Russian;
        }
        else if (language == "de")
        {
            ret = Language.German;
        }
        else if (language == "fr")
        {
            ret = Language.French;
        }
        else
        {
            throw (new System.ArgumentException("Mode " + language + " not handleable!", language));
        }

        return ret;
    }


    public static Game.Mode ToMode(this string mode)
    {
        Game.Mode ret;

        if (mode == "PingPong")
        {
            ret = Game.Mode.PingPong;
        }
        else if (mode == "TypeWriter")
        {
            ret = Game.Mode.TypeWriter;
        }
        else
        {
            throw (new System.ArgumentException("Mode " + mode + " not handleable!", mode));
        }

        return ret;
    }


    /// <summary>
    /// Uses a string of type YYYY-MM-DD hh:mm:ss and generates a DateTime Object with it.
    /// </summary>
    /// <param name="datetime">A String representing a DateTime of format YYYY-MM-DD hh:mm:ss</param>
    /// <returns>A DateTime</returns>
    public static DateTime ToDateTime(this string datetime)
    {
        DateTime ret;

        int year, month, day, hour, minute, second;

        if (int.TryParse(datetime.Substring(0, 4), out year) &&
            int.TryParse(datetime.Substring(5, 2), out month) &&
            int.TryParse(datetime.Substring(8, 2), out day) &&
            int.TryParse(datetime.Substring(11, 2), out hour) &&
            int.TryParse(datetime.Substring(14, 2), out minute) &&
            int.TryParse(datetime.Substring(17, 2), out second))
        {
            ret = new DateTime(year, month, day, hour, minute, second);
        }
        else
        {
            throw (new System.ArgumentException("String " + datetime + " is not of format YYYY-MM-DD hh:mm:ss!", "datetime"));
        }

        return ret;
    }

    /// <summary>
    /// Given a DateTime object, generates a string of format YYYY-MM-DD hh:mm:ss.
    /// </summary>
    /// <param name="dateTime">A DateTime object.</param>
    /// <returns>A string of format YYYY-MM-DD hh:mm:ss.</returns>
    public static string ToSQLiteString( this DateTime dateTime)
    {
        string month = (dateTime.Month < 10 ? "0" : "") + dateTime.Month;
        string day = (dateTime.Day < 10 ? "0" : "") + dateTime.Day;
        string hour = (dateTime.Hour < 10 ? "0" : "") + dateTime.Hour;
        string minute = (dateTime.Minute < 10 ? "0" : "") + dateTime.Minute;
        string second = (dateTime.Second < 10 ? "0" : "") + dateTime.Second;

        return dateTime.Year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
    }
}
