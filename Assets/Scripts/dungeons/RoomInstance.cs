using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInstance : MonoBehaviour {
    #region variable declaration
    public Texture2D tex;
	[HideInInspector]
	public Vector2 gridPos;
	public int type; // 0: normal, 1: enter
	[HideInInspector]
	public bool doorTop, doorBot, doorLeft, doorRight;
	[SerializeField]
	GameObject doorU, doorD, doorL, doorR, doorWallU, doorWallD, doorWallL, doorWallR;
	[SerializeField]
	ColorToGameObject[] mappings;
	float tileSize = 1;
	Vector2 roomSizeInTiles = new Vector2(15,9);
    #endregion
    public void Setup(Texture2D _tex, Vector2 _gridPos, int _type, bool _doorTop, bool _doorBot, bool _doorLeft, bool _doorRight){
		tex = _tex;
		gridPos = _gridPos;
		type = _type;
		doorTop = _doorTop;
		doorBot = _doorBot;
		doorLeft = _doorLeft;
		doorRight = _doorRight;
		MakeDoors();
		GenerateRoomTiles();
	}
	void MakeDoors(){
		//top door, get position then spawn
		Vector3 spawnPos = transform.position + Vector3.up*(roomSizeInTiles.y/4) - Vector3.up*((tileSize/4)-2) - Vector3.left*(tileSize-1);
		PlaceDoor(spawnPos, doorTop, doorU,doorWallU);
		//bottom door
		spawnPos = transform.position + Vector3.down*(roomSizeInTiles.y/4) - Vector3.down*((tileSize/4-2)) -Vector3.left*(tileSize-1);
		PlaceDoor(spawnPos, doorBot, doorD,doorWallD);
		//right door
		spawnPos = transform.position + Vector3.right*(roomSizeInTiles.x-8) - Vector3.up*(tileSize-1);
		PlaceDoor(spawnPos, doorRight, doorR,doorWallR);
		//left door
		spawnPos = transform.position + Vector3.left*(roomSizeInTiles.x-8)- Vector3.up*(tileSize-1);
		PlaceDoor(spawnPos, doorLeft, doorL,doorWallL);
	}
	void PlaceDoor(Vector3 spawnPos, bool door, GameObject doorSpawn, GameObject doorWallSpawn){
		// check whether its a door or wall, then spawn
		if (door){
			Instantiate(doorSpawn, spawnPos, Quaternion.identity).transform.parent = transform;
		}else{
			Instantiate(doorWallSpawn, spawnPos, Quaternion.identity).transform.parent = transform;
		}
	}
	void GenerateRoomTiles(){
		//loop through every pixel of the texture
		for(int x = 0; x < tex.width; x++){
			for (int y = 0; y < tex.height; y++){
				GenerateTile(x,y);
			}
		}
	}
	void GenerateTile(int x, int y){
		Color pixelColor = tex.GetPixel(x,y);
		//skip clear spaces in texture
		if (pixelColor.a == 0){
			return;
		}
		//find the color to math the pixel
		foreach (ColorToGameObject mapping in mappings){
            Vector4 map = new Vector4(Mathf.RoundToInt(mapping.color.a), Mathf.RoundToInt(mapping.color.r*1000), Mathf.RoundToInt(mapping.color.g * 1000), Mathf.RoundToInt(mapping.color.b * 1000));
            Vector4 pixel = new Vector4(Mathf.RoundToInt(pixelColor.a), Mathf.RoundToInt(pixelColor.r*1000), Mathf.RoundToInt(pixelColor.g * 1000), Mathf.RoundToInt(pixelColor.b * 1000));
            if (map == pixel)
            {
				Vector3 spawnPos = positionFromTileGrid(x,y);
				Instantiate(mapping.prefab, spawnPos, Quaternion.identity).transform.parent = this.transform;
			}
		}
	}
	public Vector3 positionFromTileGrid(int x, int y)
    {
		Vector3 ret;
		//find difference between the corner of the texture and the center of this object
		Vector3 offset = new Vector3((-roomSizeInTiles.x + 8)*tileSize, (roomSizeInTiles.y/4)*tileSize - (tileSize/4)+2, 0);
		//find scaled up position at the offset
		ret = new Vector3(tileSize * (float) x, -tileSize * (float) y, 0) + offset + transform.position;
		return ret;
	}
}
