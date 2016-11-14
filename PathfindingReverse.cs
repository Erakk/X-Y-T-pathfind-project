using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PathfindingReverse : MonoBehaviour {

	public static PathfindingReverse pathfinding;
	Tilemap map;
	public GameObject selectedUnit;

	//	TileReservedTimer tileReserveTimer;

	//	public int unitID;

	public Node[,] graph;
	Vector2 startTile;
	Vector2 endTile;
	Vector2 currentTile;



	List<Vector2> closedList = new List<Vector2>();
	List<Vector2> openList = new List<Vector2>();

	void Awake()
	{
		pathfinding = this;
	}

	public void GeneratePathfindingGraph()
	{
		map = Tilemap.tilemap;
		closedList = new List<Vector2>();
		openList = new List<Vector2>();
		graph = new Node[map.mapSizeX, map.mapSizeY];

		for (int x = 0; x < map.mapSizeX; x++) {
			for (int y = 0; y < map.mapSizeY; y++) {
				graph [x, y] = new Node ();
				graph [x, y].x = x;
				graph [x, y].y = y;
			}
		}

	}



	public void AstarPathfinding(Vector2 endTile, Vector2 startTile)
	{
		closedList = new List<Vector2>();
		openList = new List<Vector2>();
		this.startTile = startTile;
		this.endTile = endTile;
		//		graph [(int)startTile.x, (int)startTile.y].totalCost = 0;
		currentTile = new Vector2 (-1, -1);
		//		bool canSearch = true;
		openList.Add (startTile);
		GameObject.Find ("tile_" + startTile.x + "_" + startTile.y).GetComponent<SpriteRenderer> ().color = new Color (1,0,0,1);  
		while (openList.Count != 0) {
			currentTile = GetTileWithLowestTotalCost (openList);

			if (currentTile.x == endTile.x && currentTile.y == endTile.y) 
			{
				break;
			} 
			else 
			{
				openList.Remove (currentTile);
				closedList.Add (currentTile);
				List<Vector2> neighbourTiles = GetNeighbours (currentTile);
				foreach ( Vector2 neighbour in neighbourTiles) 
				{

					if (!openList.Contains(neighbour) && !closedList.Contains(neighbour)) 
					{
						openList.Add (neighbour);

						Node node = graph[(int)neighbour.x, (int)neighbour.y];
						node.cost = graph[(int)currentTile.x, (int)currentTile.y].cost + 1;
						//						node.heurastic = ManhattanDistance(neighbour);
						node.heurastic = LondonDistance(neighbour);
						node.totalCost = node.cost + node.heurastic;
						//						Debug.Log (node.totalCost);
						GameObject.Find ("tile_" + neighbour.x + "_" + neighbour.y).GetComponent<SpriteRenderer> ().color = new Color (1,0,0,1);  
					}
				}

			}

		}
		selectedUnit.GetComponent<MoveAI> ().currentPath = FinalPath ();


	}


	List<Vector2> GetNeighbours(Vector2 currentTile)
	{
		List<Vector2> neighbourTiles = new List<Vector2> ();
		Vector2 neighbourTile;


		bool isWalkable;
		//		neighbourTiles.Add (currentTile);
		neighbourTile = new Vector2 (currentTile.x, currentTile.y + 1);
		if (neighbourTile.y < map.mapSizeY) {		
			isWalkable = GameObject.Find("tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isWalkable;
			//			isOccupied = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isOccupied;

			if (isWalkable) {
				neighbourTiles.Add (neighbourTile);


			}
		}
		neighbourTile = new Vector2 (currentTile.x, currentTile.y - 1);
		if (neighbourTile.y >= 0) {
			isWalkable = GameObject.Find("tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isWalkable;
			//			isOccupied = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isOccupied;

			if (isWalkable ) {
				neighbourTiles.Add (neighbourTile);


			}
		}
		neighbourTile = new Vector2 (currentTile.x + 1, currentTile.y);
		if (neighbourTile.x < map.mapSizeX) {
			isWalkable = GameObject.Find("tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isWalkable;
			//			isOccupied = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isOccupied;

			if (isWalkable ) {
				neighbourTiles.Add (neighbourTile);

			}
		}
		neighbourTile = new Vector2 (currentTile.x - 1, currentTile.y);
		if (neighbourTile.x >= 0) {	
			isWalkable = GameObject.Find("tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isWalkable;
			//			isOccupied = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isOccupied;

			if (isWalkable) {
				neighbourTiles.Add (neighbourTile);

			}
		}
		/*
		// next 4
		neighbourTile = new Vector2 (currentTile.x + 1, currentTile.y + 1);
		if (neighbourTile.y < map.mapSizeY && neighbourTile.x < map.mapSizeX) {		
			isWalkable = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isWalkable;
			//			isOccupied = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isOccupied;

			if (isWalkable) {
				neighbourTiles.Add (neighbourTile);


			}
		}

		neighbourTile = new Vector2 (currentTile.x - 1, currentTile.y - 1);
		if (neighbourTile.y >= 0 && neighbourTile.x >= 0) {
			isWalkable = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isWalkable;
			//			isOccupied = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isOccupied;

			if (isWalkable ) {
				neighbourTiles.Add (neighbourTile);


			}
		}
		neighbourTile = new Vector2 (currentTile.x + 1, currentTile.y - 1);
		if (neighbourTile.x < map.mapSizeX && neighbourTile.y >= 0) {
			isWalkable = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isWalkable;
			//			isOccupied = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isOccupied;

			if (isWalkable ) {
				neighbourTiles.Add (neighbourTile);

			}
		}
		neighbourTile = new Vector2 (currentTile.x - 1, currentTile.y + 1);
		if (neighbourTile.x > 0 && neighbourTile.y < map.mapSizeY) {	
			isWalkable = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isWalkable;
			//			isOccupied = GameObject.Find("Tile_" + neighbourTile.x + "_" + neighbourTile.y).GetComponent<ClickableTile>().isOccupied;

			//			if (isWalkable && (!isOccupied || final)) {
			if (isWalkable) {

				neighbourTiles.Add (neighbourTile);

			}
		}
		*/
		return neighbourTiles;

	}

	Vector2 GetTileWithLowestTotalCost(List<Vector2> openlist)
	{
		Vector2 tileWithLowestTotalCost = new Vector2(-1, -1);
		int lowestTotal = int.MaxValue;

		foreach (Vector2 opentile in openlist)
		{
			if (graph[(int)opentile.x, (int)opentile.y].totalCost < lowestTotal) 
			{
				lowestTotal = graph [(int)opentile.x, (int)opentile.y].totalCost;
				tileWithLowestTotalCost = new Vector2 ((int)opentile.x, (int)opentile.y);


			}

		}
		return tileWithLowestTotalCost;
	}
	int LondonDistance(Vector2 neighbour)
	{

		int london = Mathf.Abs((int)(endTile.x - neighbour.x)) + Mathf.Abs((int)(endTile.y - neighbour.y));
		return london;
	}
	int ManhattanDistance(Vector2 neighbour)
	{

		int manhattan = Mathf.Abs((int)(endTile.x - neighbour.x)) + Mathf.Abs((int)(endTile.y - neighbour.y));
		return manhattan;
	}



	List <Vector2> FinalPath()
	{
		bool startFound = false;
		Vector2 waited = new Vector2 (-1,-1);
		int waitedCount = 0;
		Vector2 currentTile = endTile;
		List<Vector2> pathTiles = new List<Vector2> ();
		int currentTileCost = 0;
//		pathTiles.Add (currentTile);
		//		int tileNumber = 0;
		GameObject.Find ("tile_" + currentTile.x + "_" + currentTile.y).GetComponent<SpriteRenderer> ().color = new Color (0,1,0,1);  
		int counter = 0;
		while (startFound == false) 
		{
			A:
			counter++;
			List<Vector2> neighbours = GetNeighbours (currentTile);
//			Debug.Log ("current counter: " + counter +  "    tileX: " + currentTile.x + "     tileY: " + currentTile.y);
			foreach (Vector2 neighbour in neighbours) 
			{
//				Debug.Log ( "counter: " + counter +  "    tileX: " + neighbour.x + "     tileY: " + neighbour.y);

				if (neighbour.x == startTile.x && neighbour.y == startTile.y) 
				{
					startFound = true;
					pathTiles.Add (neighbour);
					GameObject.Find ("tile_" + neighbour.x + "_" + neighbour.y).GetComponent<SpriteRenderer> ().color = new Color (0,1,0,1);  

				}
				if (closedList.Contains(neighbour)  || openList.Contains(neighbour)) 
				{
					if (waitedCount > 0) {
						if (graph [(int)neighbour.x, (int)neighbour.y].cost < currentTileCost && graph [(int)neighbour.x, (int)neighbour.y].totalCost > 0) 
						{
							// testaan onko samaan aikaan tässä tilessa mahdollisesti muita
							if (GetNeighboursTime(neighbour, pathTiles.Count, waitedCount)) {

								currentTile.x = neighbour.x;
								currentTile.y = neighbour.y;

								pathTiles.Add (neighbour);
								GameObject.Find ("tile_" + neighbour.x + "_" + neighbour.y).GetComponent<SpriteRenderer> ().color = new Color (0,1,0,1);  

								if (waited.x == neighbour.x && waited.y == neighbour.y) {
									waited = new Vector2 (-1,-1);
									selectedUnit.GetComponent<MoveAI> ().sleeps.Add (neighbour);
									selectedUnit.GetComponent<MoveAI> ().sleepTurns.Add (pathTiles.Count);
									GameObject.Find ("tile_" + neighbour.x + "_" + neighbour.y).GetComponent<SpriteRenderer> ().color = new Color (0,0,1,1); 

									// lisää MoveAI scriptiin lista jossa kertoo kuinka kauan odottaa nimenomaa tässä tilessä
								}
								waitedCount = 0;
								break;
							} 
							// lisätään hintaan odotus lisä ja merkataan odotus variableen


							else {

								graph [(int)neighbour.x, (int)neighbour.y].cost++;
								if (waited.x == neighbour.x && waited.y == neighbour.y) {
									waitedCount++;
								} else {
									waitedCount = 1;
								}
								waited.x = neighbour.x;
								waited.y = neighbour.y;
								goto A;

							}

						}
					}
					else if (graph [(int)neighbour.x, (int)neighbour.y].cost < graph [(int)currentTile.x, (int)currentTile.y].cost && graph [(int)neighbour.x, (int)neighbour.y].totalCost > 0) 
					{
						// testaan onko samaan aikaan tässä tilessa mahdollisesti muita
						if (GetNeighboursTime(neighbour, pathTiles.Count, waitedCount)) {
							
							currentTile.x = neighbour.x;
							currentTile.y = neighbour.y;

							pathTiles.Add (neighbour);
							GameObject.Find ("tile_" + neighbour.x + "_" + neighbour.y).GetComponent<SpriteRenderer> ().color = new Color (0,1,0,1);  

							if (waited.x == neighbour.x && waited.y == neighbour.y) {
								waited = new Vector2 (-1,-1);
								selectedUnit.GetComponent<MoveAI> ().sleeps.Add (neighbour);
								GameObject.Find ("tile_" + neighbour.x + "_" + neighbour.y).GetComponent<SpriteRenderer> ().color = new Color (0,0,1,1); 

								// lisää MoveAI scriptiin lista jossa kertoo kuinka kauan odottaa nimenomaa tässä tilessä
							}
							waitedCount = 0;
							break;
						} 
						// lisätään hintaan odotus lisä ja merkataan odotus variableen

					
						else {
							currentTileCost = graph [(int)currentTile.x, (int)currentTile.y].cost + 1;
							graph [(int)neighbour.x, (int)neighbour.y].cost++;
							Debug.Log ("graph" + graph [(int)neighbour.x, (int)neighbour.y].cost);
							Debug.Log ("currenttile" + currentTileCost);
							if (waited.x == neighbour.x && waited.y == neighbour.y) {
								waitedCount++;
							} else {
								waitedCount = 1;
							}
							waited.x = neighbour.x;
							waited.y = neighbour.y;
							goto A;

						}

					}
				}
			}
			if (counter > 50) {
				Debug.LogError ("counter bug");
				return null;

			}
		}

		//		ReserveTime (pathTiles);
		return pathTiles;
	}
	bool GetNeighboursTime(Vector2 v2, int time, int waits)
	{
		GameObject[] AI = GameObject.FindGameObjectsWithTag ("AI");
		foreach (GameObject go in AI) {
			int counter = 0;
			MoveAI mai = go.GetComponent<MoveAI> ();
			List<Vector2> path =  mai.currentPath;
			foreach (Vector2 tile in path) {
				int thisSleeps = selectedUnit.GetComponent<MoveAI> ().sleeps.Count;
				Debug.Log (thisSleeps);
				int maiSleeps = mai.sleeps.Count;
				Debug.Log (maiSleeps);
				counter++;
				if (tile.x == v2.x && tile.y == v2.y && counter + maiSleeps == time + 1 + thisSleeps + waits) 
				{
					return false;
				}
				/*
				// if  nobody waited 
				if (thisSleeps == 0 && maiSleeps == 0) {
					
				}

				// else if prev have waited
				else if (thisSleeps == 0 && maiSleeps > 0) {
					
				}
				// else if this have waited
				else if (thisSleeps > 0 && maiSleeps == 0) {
					if (tile.x == v2.x && tile.y == v2.y && counter == time + 1 + thisSleeps) 
					{
						return false;
					}
				}
				// else  both have waited
				else {
					
				}
				*/
				//	Debug.Log ( "counter: " + counter +  "    time: " + (time + 1));
				//	Debug.Log ( "tile.x: " + tile.x +  "    v2.x: " + v2.x);
				//	Debug.Log ( "tile.y: " + tile.y +  "    v2.y: " + v2.y);

			}


		}
		return true;
	}


}

