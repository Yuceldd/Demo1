using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBullet : MonoBehaviour
{
    public Rigidbody rb;
    public int clickForce = 500;
    private Plane plane = new Plane(Vector3.up, Vector3.zero);
  
    void FixedUpdate () {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("mouse");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
            float enter;
            if (plane.Raycast(ray, out enter))
            {
                var hitPoint = ray.GetPoint(enter);               
                var mouseDir = hitPoint - gameObject.transform.position;   
                mouseDir = mouseDir.normalized;    
                rb.AddForce(mouseDir * clickForce);
            }
        }
    }
}
