using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarPerson : MonoBehaviour {

    DialoguePopUp dialogueScript;
    public float pushForce = 1;
    Rigidbody rb;
    SpriteRenderer sp;
    float countDown;
    public float countDownSecs;
	// Use this for initialization
	void Start () {
        dialogueScript = GameObject.Find("DialogueManager").GetComponent<DialoguePopUp>();
        rb = GetComponent<Rigidbody>();
        sp = GetComponent<SpriteRenderer>();
        countDown = -1;
	}
	
	// Update is called once per frame
	void Update () {
        countDown -= Time.deltaTime;
	}

    private void OnTriggerEnter(Collider other)
    {

        if (countDown<0 && other.gameObject.tag == "Character")
        {
            countDown = countDownSecs;
            VisualFeedback();
        }
    }

    void VisualFeedback()
    {
        Debug.Log("Empujon");
        dialogueScript.OpenBarmanDialogue();
        rb.AddForce(Vector3.up* pushForce, ForceMode.Impulse);
        StartCoroutine(ChangeColor());
    }
    IEnumerator ChangeColor()
    {
        sp.color = new Color32(203, 87, 87, 255);
        yield return new WaitForSeconds(0.5f);
        sp.color = new Color32(255, 255, 255, 255);
    }
}
