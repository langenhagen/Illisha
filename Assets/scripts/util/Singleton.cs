/// <summary>
/// Singleton C# implementation
/// </summary>
// TODO not safe bc there is a Std constructor. Please use Factory pattern and consult eg.
// http://csharpindepth.com/articles/general/singleton.aspx
using System;

public class Singleton<T> where T: new()
{
    protected static T instance;
    
    public static T Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}