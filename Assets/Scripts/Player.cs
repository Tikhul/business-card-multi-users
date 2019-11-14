using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class Player {
    public string Username;
    public string userID;

    public Player() { }

    public Player(string name, string id)
    {
        Username = name;
        userID = id;
    }
}
