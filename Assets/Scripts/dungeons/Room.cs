using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
	public Vector2 gridPos; //position of the room
	public int type;        //type of the room
	public bool doorTop, doorBot, doorLeft, doorRight; //bools to check if doors are in what position
    //constructor to pass position and type values
    public Room(Vector2 _gridPos, int _type)
    {
		gridPos = _gridPos;
		type = _type;
	}
}
