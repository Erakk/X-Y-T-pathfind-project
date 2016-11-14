using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

	public string ChangeSelection = "floor";


	public void AddWall()
	{
		ChangeSelection = "wall";
	}
	public void AddFloor()
	{
		ChangeSelection = "floor";
	}
	public void AddAI()
	{
		ChangeSelection = "AI";
	}
	public void StartSim()
	{
		GameObject[] AIs = GameObject.FindGameObjectsWithTag ("AI");
		foreach (GameObject go in AIs) {
			go.GetComponent<MoveAI> ().startMoving = true;
		}
	}
	public void StopSim()
	{
		GameObject[] AIs = GameObject.FindGameObjectsWithTag ("AI");

		foreach (GameObject go in AIs) {
			go.GetComponent<MoveAI> ().startMoving = false;
		}
	}
	public void CalcPaths()
	{
		GameObject[] AIs = GameObject.FindGameObjectsWithTag ("AI");
		foreach (GameObject go in AIs) {
			go.GetComponent<MoveAI> ().CalcPath ();
		}
	}

}
