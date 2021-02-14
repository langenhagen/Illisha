using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// A custom Superclass for the behaviours with augmented functionality compared to MonoBehaviour.
/// Also contains utility functions that can be handy, but dont confuse them with their original pendants!
/// </summary>
public class BarnBehaviour : MonoBehaviour
{
    
    //##################################################################################################
    // METHODS

    /// <summary>
    /// Logs a message under specified mode with stack trace.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    /// <param name="logType">The Type to be used, specifies the behaviour or the outline of that log entry.</param>
    protected static void Log(string message, Logger.Type logType)
    {
        Logger.Log(message, logType, true);
    }

    /// <summary>
    /// Convenience method using Logger class. Logs a message under logging mode without printing stack trace.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    protected static void Log(string message)
    {
        Logger.Log(message, Logger.Std, false);
    }

    /// <summary>
    /// Convenience method using Logger class. Logs an object in logging mode.
    /// </summary>
    /// <param name="obj">The the object to be logged.</param>
    protected static void Log(object obj)
    {
        Logger.Log(obj.ToString(), Logger.Std);
    }


    /// <summary>
    /// Returns the interface of of Type I if the game object has one attached, null if it doesn't.
    /// Found at http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/
    /// </summary>
    /// <typeparam name="I">An interface class to look for</typeparam>
    /// <returns>An object of tyoe </returns>
    public I GetInterfaceComponent<I>() where I : class
    {
        return GetComponent(typeof(I)) as I;
    }


    /// <summary>
    /// Returns a list of all Interfaces of type I that are attached on active loaded objects.
    /// Please note that this function is very slow. 
    /// It is not recommended to use this function every frame. 
    /// In most cases you can use the singleton pattern instead.
    /// Found at http://devmag.org.za/2012/07/12/50-tips-for-working-with-unity-best-practices/
    /// </summary>
    /// <typeparam name="I">An interface class to look for.</typeparam>
    /// <returns>A list of objects that implement the given Interface.</returns>
    public static List<I> FindObjectsOfInterface<I>() where I : class
    {
        Behaviour[] behaviours = FindObjectsOfType<Behaviour>();
        List<I> list = new List<I>();

        foreach (Behaviour behaviour in behaviours)
        {
            I component = behaviour.GetComponent(typeof(I)) as I;

            if (component != null)
            {
                list.Add(component);
            }
        }

        return list;
    }


    /// <summary>
    /// A logged way of accessing GameObjects with specific tags.
    /// Automatically logs if it finds nothing.
    /// </summary>
    /// <param name="tag">The tag of GameObject to be searched for.</param>
    /// <returns>The GameObject with the specified tag or null in case of not-finding it.</returns>
    public static GameObject FindGameObjectWithTagSafe(string tag)
    {
        GameObject ret = GameObject.FindGameObjectWithTag(tag);

        if (ret == null)
        {
            Logger.Log("Expected to find GameObject with tag \"" + tag + "\" but found none!", Logger.Warning);
        }
        
        return ret;
    }


    /// <summary>
    /// An easy and logged way of accessing Components on objects with specific tags.
    /// Automatically logs if it finds nothing.
    /// </summary>
    /// <typeparam name="T">The component to be searched.</typeparam>
    /// <param name="tag">The tag of GameObject on which to be searched for the component.</param>
    /// <returns>Some component of the specified type parameter or null in case of Error.</returns>
    public static T GetComponentOnObjectWithTagSafe<T>( string tag) where T : Component
    {
        T ret = FindGameObjectWithTagSafe(tag).GetComponent<T>();

        if (ret == null)
        {
            Logger.Log("Expected to find Component of type " + ret.GetType().Name + " on object with tag \"" + tag + "\" but found none!", Logger.Type.Warning);
        }

        return ret;
    }

    /// <summary>
    /// Returns the component of Type type if the game object has one attached, 
    /// null if it doesn't. In addition to the classic GetComponent, this
    /// one also logs an error message.
    /// You can access both builtin components or scripts with this function.
    /// </summary>
    /// <typeparam name="T">The Component type which is to be searched.</typeparam>
    /// <returns>A Component of type T or null.</returns>
    public new T GetComponent<T>() where T : Component
    {
        T component = base.GetComponent<T>();

        if (component == null)
        {
            Logger.Log("Expected to find component of type " + typeof(T) + " but found none!", Logger.Type.Error);
        }

        return component;
    }

    /// <summary>
    /// Returns the component of Type type in the GameObject or any of its children, 
    /// or null if it has no such component attached, using depth first search.
    /// Only active components are returned. In addition to the classic GetComponent, this
    /// one also logs an error message.
    /// </summary>
    /// <typeparam name="T">The Component type which is to be searched.</typeparam>
    /// <returns>A Component of type T or null.</returns>
    public new T GetComponentInChildren<T>() where T : Component
    {
        T component = base.GetComponentInChildren<T>();

        if (component == null)
        {
            Logger.Log("Expected to find component of type " + typeof(T) + " but found none!", Logger.Type.Warning);
        }

        return component;
    }
}
