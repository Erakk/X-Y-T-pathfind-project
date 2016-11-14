using UnityEngine;
using System.Collections;

public class Tilemap : MonoBehaviour {

	public static Tilemap tilemap;

	public int mapSizeX = 50;
	public int mapSizeY = 50;

	public GameObject floor;
	public GameObject AI;
	public GameObject targetAI;
	public Sprite floorSprite;
	public Sprite wallSprite;

	public MoveAI mai;

	// Use this for initialization
	void Start () 
	{
		tilemap = this;
		GenerateArea ();
		Pathfinding.pathfinding.GeneratePathfindingGraph ();
		PathfindingReverse.pathfinding.GeneratePathfindingGraph ();
	}
	
	void GenerateArea()
	{
		for (int i = 0; i < mapSizeX; i++) {
			for (int j = 0; j < mapSizeY; j++) {
				GameObject go = (GameObject)Instantiate (floor, new Vector3(i,j, 1),Quaternion.identity);

				ClickableTile ct = go.AddComponent<ClickableTile> ();

				ct.tileX = i;
				ct.tileY = j;
				go.name = "tile_" + i + "_" + j;
				go.transform.SetParent (this.transform);
			}
		}
	}
}
