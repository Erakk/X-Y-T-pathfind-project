using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEditor.Events;

public class ClickableTile : MonoBehaviour {

	public int tileX;
	public int tileY;
	public bool isWalkable = true;
	Buttons buttons;
	Tilemap tilemap;
	GameObject go;
	MoveAI moveAI;

	void Start()
	{
		buttons = GameObject.Find ("Canvas").GetComponent<Buttons>();
		tilemap = Tilemap.tilemap;
	}


	void OnMouseUp ()
	{
//		Debug.Log (tileX + " " + tileY);
		switch (buttons.ChangeSelection) {
		case "target":
			go = (GameObject)Instantiate (tilemap.targetAI, new Vector2 (tileX, tileY), Quaternion.identity);
			tilemap.mai.target.x = tileX;
			tilemap.mai.target.y = tileY;

			buttons.ChangeSelection = "empty";
			break;
		case "wall":
			isWalkable = false;
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = tilemap.wallSprite;
//			go = (GameObject)Instantiate (tilemap.wall, new Vector2(tileX*2,tileY*2),Quaternion.identity);
			break;
		case "floor":
			isWalkable = true;
			this.gameObject.GetComponent<SpriteRenderer> ().sprite = tilemap.floorSprite;

//			go = (GameObject)Instantiate (tilemap.floor, new Vector2(tileX*2,tileY*2),Quaternion.identity);
			break;
		case "AI":
			go = (GameObject)Instantiate (tilemap.AI, new Vector2 (tileX, tileY), Quaternion.identity);
			go.tag = "AI";
			MoveAI mai = go.AddComponent<MoveAI> ();

			mai.start.x = tileX;
			mai.start.y = tileY;
			tilemap.mai = mai;
			buttons.ChangeSelection = "target";
			break;
		
		default:
			break;
		}
	}

}
