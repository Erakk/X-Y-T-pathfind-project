using UnityEngine;
using System.Collections.Generic;

public class Node  {

	public List<Node> neighbours;
	public int x;
	public int y;
	public int cost = 0;
	public int totalCost = 0;
	public int heurastic = 0;

	public bool isWalkable;
	public bool blockVision;

	public Node(){
		neighbours = new List<Node> ();
	}


}
