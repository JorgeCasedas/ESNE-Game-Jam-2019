using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLevel : MonoBehaviour {

    public GameObject toiletScene;
    public ParticleSystem pee;
    bool moduleEnabled;

    public GameObject CharacterCamera;
    public CharacterMovment charScript;
    public AlcoholController alcoholScript;
    public AudioSource peeSound;
    public AudioSource cumbioteMusic;

    // Use this for initialization
    void Start () {
        moduleEnabled = false;
        var emission = pee.emission;
        emission.enabled = moduleEnabled;
        alcoholScript.isPeing = false;
        Exit();
    }
	
	// Update is called once per frame
	void Update () {
        if (toiletScene.activeSelf)
        {
            if (alcoholScript.drunkLevel <= 0)
            {
                moduleEnabled = false;
                var emission = pee.emission;
                emission.enabled = moduleEnabled;
                alcoholScript.isPeing = false;
                Exit();
            }
            if (Input.GetMouseButtonDown(0))
            {
                peeSound.volume = 1;
                moduleEnabled = true;
                var emission = pee.emission;
                emission.enabled = moduleEnabled;
                alcoholScript.isPeing = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                peeSound.volume = 0;
                moduleEnabled = false;
                var emission = pee.emission;
                emission.enabled = moduleEnabled;
                alcoholScript.isPeing = false;
            }
            if (Input.GetMouseButtonDown(1))
            {
                moduleEnabled = false;
                var emission = pee.emission;
                emission.enabled = moduleEnabled;
                alcoholScript.isPeing = false;
                Exit();
            }
        }
	}
    public void Exit()
    {
        cumbioteMusic.spatialBlend = 0;
        peeSound.volume = 0;
        toiletScene.SetActive(false);
        CharacterCamera.SetActive(true);
        charScript.lockMov=false;
    }
    public void Enter()
    {
        cumbioteMusic.spatialBlend = 0.5f;
        toiletScene.SetActive(true);
        CharacterCamera.SetActive(false);
        charScript.lockMov = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( alcoholScript.drunkLevel>0 && other.gameObject.tag == "Character")
        {
            Enter();
        }
    }
}
