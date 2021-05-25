using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int hit=0;
   
    private Vector3 template;
    private ShaderVariantCollection temp;
    private Vector3 pointA=new Vector3 (-6,2,42);
    private Vector3 pointB= new Vector3 (9,2,42);
    private bool isMove=true;
    [SerializeField] private float speed = 1;
    private float t;

    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ThrowedBullet"))
        {
            Debug.Log(hit);
            hit++;
            if (hit == 2)
            {
               gameObject.GetComponent<Rigidbody>().mass = 1; 
            }
            
            collision.gameObject.tag = "TouchedBullet";
        }
    }
    void FixedUpdate()
    {
        t += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(pointA, pointB, t);
        if (isMove)
        {
            if (t >= 1)
                    {
                        var b = pointB;
                        var a = pointA;
                        pointA = b;
                        pointB = a;
                        t = 0;
                    }
        }
        else
        {
            
        }
        
        if (hit == 3)
        {
             hit = 0;
                StartCoroutine(Wait(2f));
        }
        
    }
    IEnumerator Wait(float duration)
    {   isMove=false;
        yield return new WaitForSeconds(duration);   
        Application.LoadLevel(Application.loadedLevel);
        
    }
    

}
