using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class GestureManager : MonoBehaviour, IInputClickHandler {

	public Transform cursor;
	public GameObject platform;
	public GameObject placeholder;
	public float cylinderWidth;

	private List<Vector3> points;
	private Vector3 start;
	private Vector3 end;
	private bool firstPointSet = false;
	
	private void Start()
	{
		InputManager.Instance.PushFallbackInputHandler(gameObject);
		points = new List<Vector3>(); 
	}

	public virtual void OnInputClicked(InputClickedEventData eventData) {
		points.Add(cursor.position);
		if (!firstPointSet) {
			start = cursor.position;
//			placeholder.transform.position = cursor.position;
//			Instantiate(placeholder);
			firstPointSet = true;
		}
		else {
//			DestroyImmediate(placeholder, true);
			end = cursor.position;
			Vector3 offset = end - start;
			Debug.Log(offset);
			Vector3 position = start + offset / 2.0f;
			var cylinder = Instantiate(platform, position, Quaternion.identity);
			cylinder.transform.up = offset;
			cylinder.transform.localScale = new Vector3(cylinderWidth, offset.magnitude, cylinderWidth);
			start = end;
		}
		//DoStuff
		Debug.Log("Tap");
	}
}
