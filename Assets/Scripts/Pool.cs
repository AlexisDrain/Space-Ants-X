using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour {

	[HideInInspector]
	public List<GameObject> pooledObjects;
	
	public int objectsToCreate = 6;
	public bool setPoolTransformAsParent = false;

	public bool reuseActiveObjects = false;
	private int currentReuseIndex = 0;

	public GameObject prefeb;

	// Use this for initialization
	void Awake () {
		
		pooledObjects = new List<GameObject>();

		for (int i = 0; i < objectsToCreate; i += 1) {
			MakeNewSprite ();
		}
	}
	private GameObject MakeNewSprite () {

		GameObject instance;
		
		instance = (GameObject) Instantiate (prefeb, new Vector3 (0f, 0f, 0f), Quaternion.identity) as GameObject;
		instance.SetActive (false);

		if (setPoolTransformAsParent == true) {
			instance.transform.SetParent(transform);
		}

		pooledObjects.Add (instance);

		return instance;
	}

	public GameObject Spawn (Vector3 position) {

		// loops through all objects eventually, will NEVER make a new object.
		// downside: there's currently no way to skip already active object and get an inactive object.
		// @ToDo Search all objects before pulling an original object.
		if (reuseActiveObjects == true) {
			GameObject obj = pooledObjects[currentReuseIndex];

            //if (obj.GetComponent<Rigidbody>() != null) {
            //	obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            //}
            obj.transform.position = position;
            obj.SetActive(false);
			obj.SetActive(true);

			currentReuseIndex += 1;
			if (currentReuseIndex == pooledObjects.Count) {
				currentReuseIndex = 0;
			}
			return obj;
		}

		// make new objects (Default)
		for (int i = 0; i < pooledObjects.Count; i += 1) {
			if (pooledObjects [i].activeSelf == false) {
				GameObject obj = pooledObjects [i];

				if (obj.GetComponent<Rigidbody>() != null) {
					obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
				}
				obj.transform.position = position;
				obj.SetActive(true);

				return obj;
			}
		}

		MakeNewSprite ();
		return Spawn(position);
	}

	public void DeactivateAllSounds () {
		for (int i = 0; i < pooledObjects.Count; i++) {
			pooledObjects [i].transform.position = new Vector3 (100f, float.MaxValue, 0f);
			pooledObjects [i].SetActive (false);
		}
	}
}
