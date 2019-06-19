using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovment : MonoBehaviour {

    [Header("CharacterInputs: ")]
    public float minDrunkSpeed;
    public float maxDrunkSpeed;

    [Space]
    [Header("References: ")]
    public GameObject gfx;

    [Space]
    [Header("DEBUGGING: ")]
    [SerializeField]Vector3 movementDirection;
    Rigidbody rb;
    [HideInInspector] public Animator anim;
    [SerializeField] float speed;
    public bool lockMov;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        anim = gfx.GetComponent<Animator>();
        changeSpeed(0);
        lockMov = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!lockMov)
        {
            movementDirection = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
            movementDirection.Normalize();

            anim.SetFloat("Horizontal", -Input.GetAxis("Horizontal"));

            if (movementDirection.magnitude != 0)
                anim.SetBool("walk", true);
            else
                anim.SetBool("walk", false);
        }    
    }
    private void FixedUpdate()
    {
        if(!lockMov)
            rb.velocity = movementDirection * speed * Time.deltaTime;
    }
    public void changeSpeed(float drunkLevel)
    {
        speed = Mathf.Lerp(minDrunkSpeed, maxDrunkSpeed,(float)drunkLevel/100);
    }

}
