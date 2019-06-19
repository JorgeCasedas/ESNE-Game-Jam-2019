using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeControler : MonoBehaviour {

    public Transform aimObject;
    RaycastHit hit;
    public float speed;


    void Update()
    {

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
        {
            aimObject.position = hit.point;
        }

        Vector3 targetDir = aimObject.position - transform.position;

        // The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        // Move our position a step closer to the target.
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}
