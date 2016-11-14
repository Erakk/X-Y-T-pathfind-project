using UnityEngine;
using System.Collections.Generic;

public class MoveAI : MonoBehaviour {

	public bool startMoving = false;
	public Vector2 start;
	public Vector2 target;
	public List<Vector2> sleeps;
	public List<int> sleepTurns;
	float timer = 0;
	public List<Vector2> currentPath;

	// Use this for initialization
	void Start () {
		currentPath = new List<Vector2> ();
		sleeps = new List<Vector2> ();
		sleepTurns = new List<int> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (startMoving) {
			MoveThis ();
		}
	}

	public void CalcPath()
	{
		PathfindingReverse.pathfinding.selectedUnit = this.gameObject;
		PathfindingReverse.pathfinding.AstarPathfinding (start, target);
	}
	void MoveThis()
	{
		Vector2 thisMove;
		thisMove.x = currentPath [0].x;
		thisMove.y = currentPath [0].y;

		if (sleeps.Count != 0) {
			if (sleeps[0].x == thisMove.x && sleeps[0].y == thisMove.y) {
				//wait second
				timer += Time.deltaTime;
				Debug.Log (timer);
				if (timer >= 1) {
					sleeps.RemoveAt(0);
					timer = 0;
				}

			} 
			else {
				this.transform.position = Vector2.MoveTowards (this.transform.position, thisMove, Time.deltaTime * 2);

				if (this.transform.position.x == thisMove.x && transform.position.y == thisMove.y) {
					currentPath.RemoveAt (0);
					if (currentPath.Count == 0) {
						startMoving = false;
						currentPath = new List<Vector2> ();
					}
				}
			}
		} 
		else {
			this.transform.position = Vector2.MoveTowards (this.transform.position, thisMove, Time.deltaTime * 2);

			if (this.transform.position.x == thisMove.x && transform.position.y == thisMove.y) {
				currentPath.RemoveAt (0);
				if (currentPath.Count == 0) {
					startMoving = false;
					currentPath = new List<Vector2> ();
				}
			}
		}




	}
}
