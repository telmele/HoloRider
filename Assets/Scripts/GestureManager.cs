using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class GestureManager : MonoBehaviour, IInputClickHandler {

	public Transform cursor;
	public Transform parent;
	public GameObject[] platforms;
	public GameObject placeholder;
	public GameObject player;
	public float cylinderWidth;

	private List<GameObject> pipes;
	private List<Vector3> points;
	private Vector3 start;
	private Vector3 end;
	private bool firstPointSet;
	
	private void Start()
	{
		InputManager.Instance.PushFallbackInputHandler(gameObject);
		points = new List<Vector3>();
		pipes = new List<GameObject>();
	}

	public virtual void OnInputClicked(InputClickedEventData eventData) {
		points.Add(cursor.position);
		if (!firstPointSet) {
			start = cursor.position;
			placeholder.transform.position = cursor.position;
			Instantiate(placeholder, parent);
			firstPointSet = true;
		}
		else {
			placeholder.transform.position = cursor.position;
			end = cursor.position;
			Vector3 offset = end - start;
			Vector3 position = start + offset / 2.0f;
			GameObject cylinder = Instantiate(platforms[1], position, Quaternion.identity, parent);
			cylinder.SendMessage("setStart", start);
			cylinder.SendMessage("setEnd", end);
			pipes.Add(cylinder);
			cylinder.transform.up = offset;
			cylinder.transform.localScale = new Vector3(cylinderWidth, offset.magnitude*3, cylinderWidth);
			start = end;
		}
	}

	public void reset() {
		foreach (Transform child in parent.transform) {
			Destroy(child.gameObject);
		}
		firstPointSet = false;
		points.Clear();
		start = Vector3.zero;
		end = Vector3.zero;
	}

	public void play() {
		if (points.Count > 0) {
			Instantiate(player, LerpByDistance(points[0], points[1], 0.1f), Quaternion.identity);		
		}
	}

	public void undo() {
		if (pipes.Count > 0) {
			Destroy(pipes[pipes.Count - 1]);
			pipes.RemoveAt(pipes.Count - 1);
			points.RemoveAt(points.Count - 1);
			start = points[points.Count - 1];
		}
	}

	private Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
	{
		Vector3 P = x * Vector3.Normalize(B - A) + A;
		return P;
	}

	public Vector3 Direction() {
		Vector3 heading = points[points.Count - 1] - points[points.Count - 2];
		float distance = heading.magnitude;
		Vector3 direction = heading / distance;
		return direction;
	}
}
