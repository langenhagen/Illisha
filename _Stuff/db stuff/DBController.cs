using UnityEngine;
using System;
using Mono.Data.Sqlite;


public class DBController : MonoBehaviour {
    
    protected SqliteConnection dbconn;
    
    void Awake ()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer) {
            dbconn = new SqliteConnection("URI=file:" +Application.dataPath + "/../Data.db");
        } else {
            dbconn = new SqliteConnection("URI=file:" +Application.dataPath + "/Data.db");

        }
        
        dbconn.Open();

        SqliteCommand cmd;

        try {
            cmd = new SqliteCommand("SELECT firstname, lastname FROM adressbook", dbconn);
            SqliteDataReader reader = cmd.ExecuteReader();
            
            if (reader.HasRows) {
                while(reader.Read()) {
                    string FirstName = reader.GetString (0);
                    string LastName = reader.GetString (1);
                    
                    Debug.Log ( FirstName + LastName );
                }
            }

            cmd.Dispose();
            cmd = null;
            
            reader.Close();
            reader = null;
        } catch {
            Debug.Log("Error reading DB");
        }


        dbconn.Close();
        dbconn = null;
        
        
    }
    
}