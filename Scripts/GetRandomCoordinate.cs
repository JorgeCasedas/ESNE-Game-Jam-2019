using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRandomCoordinate : MonoBehaviour {

    Vector3 origin;
    Vector3 range;
    Vector3 randomRange;
    Vector3 randomCoordinate;
    public Color GizmosColor = new Color(0.5f, 0.5f, 0.5f, 0.2f);
    // Use this for initialization
    void Start () {
        origin = transform.position;
        range = transform.localScale / 2.0f;
    }
	
    public Vector3 GetCoordinate()
    {
        randomRange = new Vector3(Random.Range(-range.x, range.x),
                                          Random.Range(-range.y, range.y),
                                          Random.Range(-range.z, range.z));
        return randomCoordinate = origin + randomRange;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
