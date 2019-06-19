using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortByDepthZ : MonoBehaviour {

    SpriteRenderer sp;

	// Use this for initialization
	void Start () {
        sp = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        sp.sortingOrder =(int)(transform.position.z * 100);
	}
}
