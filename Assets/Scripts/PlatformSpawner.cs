using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit;
using UnityEngine.EventSystems;


public class PlatformSpawner : MonoBehaviour {

	private bool display = true;

	public GameObject go;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void onSelect(BaseEventData eventData) {
		Debug.Log(eventData);
		display = !display;
		go.SetActive(display);
		
	}
}
