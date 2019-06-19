using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour {

    public GameObject audioObject;
    public int alcohol;
    GameObject alcoholManager;

    private void Start()
    {
        alcoholManager = GameObject.Find("AlcoholManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Character")
        {
            alcoholManager.GetComponent<AlcoholController>().AddAlcohol(alcohol);
            alcoholManager.GetComponent<AlcoholController>().numberOfActiveBottles -= 1;
            Instantiate(audioObject,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
