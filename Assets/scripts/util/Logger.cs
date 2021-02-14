using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Siple logging functionality for you.
/// </summary>
public static class Logger
{
    // The possible types of log entries
    public enum Type
    {
        Log,
        Warning,
        Error,
        Exception
    }

    // Constancs representing the enum Type values for your convenience
    public const Type Std       = Type.Log;
    public const Type Warning   = Type.Warning;
    public const Type Error     = Type.Error;
    public const Type Exception = Type.Exception;


    //##################################################################################################
    // STATIC VARS

    private static System.IO.StreamWriter __file = null;

    //##################################################################################################
    // METHODS


    /// <summary>
    /// Logs a message.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    /// <param name="logType">The Type to be used, specifies the behaviour or the outline of that log entry.</param>
    /// <param name="printStackTrace">Specifies, whether to print a stack trace or not.</param>
    public static void Log(string message, Type logType, bool printStackTrace)
    {
        //
        // If you want to change the logging attitude for the whole project, 
        // change it here by commenting in/out your preferred method.
        // I know it could be more elegant but ...its fast and versatile!
        //

        string stackTraceString = "";
        if (printStackTrace)
        {
            StackFrame[] stackFrames = new StackTrace().GetFrames(); ;

            foreach (StackFrame stackFrame in stackFrames)
            {
                System.Reflection.MethodBase method = stackFrame.GetMethod();
                stackTraceString += "\n   (at  " + method.DeclaringType.Name + "." + method.Name + ")";
            };
        }


        // Log to Text File
        if (__file == null)
            __file = new System.IO.StreamWriter(Application.persistentDataPath + "/" + "Illy.log", true);

        __file.WriteLine(DateTime.Now.ToSQLiteString() + " " + logType + ": " + message + stackTraceString);
        __file.Flush();
        //__file.Close(); // XXX


        // Log to Editor Console
        //print(Util.String(DateTime.Now) + " " + logType + ": " + message);
    }

    /// <summary>
    /// Logs a message.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    /// <param name="logType">The Type to be used, specifies the behaviour or the outline of that log entry.</param>
    public static void Log(string message, Type logType)
    {
        Log(message, logType, true);
    }

    /// <summary>
    /// Logs a message under logging mode.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public static void Log(string message)
    {
        Log(message, Type.Log, false);
    }

    /// <summary>
    /// Logs an object in logging mode.
    /// </summary>
    /// <param name="obj">The the object to be logged.</param>
    public static void Log(object obj)
    {
        Log(obj.ToString(), Type.Log, false);
    }

}