using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class GestureManager : MonoBehaviour, IInputClickHandler {

	public Transform cursor;
	public Transform parent;
	public GameObject[] platforms;
	public GameObject placeholder;
	public float cylinderWidth;

	private List<Vector3> points;
	private Vector3 start;
	private Vector3 end;
	private bool firstPointSet;
	
	private void Start()
	{
		InputManager.Instance.PushFallbackInputHandler(gameObject);
		points = new List<Vector3>(); 
	}

	public virtual void OnInputClicked(InputClickedEventData eventData) {
		points.Add(cursor.position);
		if (!firstPointSet) {
			start = cursor.position;
			placeholder.transform.position = cursor.position;
			Instantiate(placeholder);
			firstPointSet = true;
		}
		else {
			placeholder.transform.position = cursor.position;
			end = cursor.position;
			Vector3 offset = end - start;
			Vector3 position = start + offset / 2.0f;
			var cylinder = Instantiate(platforms[0], position, Quaternion.identity, parent);
			cylinder.transform.up = offset;
			cylinder.transform.localScale = new Vector3(cylinderWidth, offset.magnitude*3, cylinderWidth);
			start = end;
		}
	}

	public void reset() {
		firstPointSet = false;
		points.Clear();
		start = Vector3.zero;
		end = Vector3.zero;
		Destroy(placeholder);
	}
}
