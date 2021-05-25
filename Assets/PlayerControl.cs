using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
 
class PlayerControl : MonoBehaviour {
    private float moveSpeed = 9f;
    private float gridSize = 1f;
    private float levelHardness = 5f;
    private float numOfBullets = 1;
    public GameObject Bullet;
    public GameObject ThrowableBullet;
    private enum Orientation {
        Horizontal,
        Vertical
    };
    
    public Rigidbody rb;
    public int clickForce = 500;
    private Plane plane = new Plane(Vector3.up, Vector3.zero);

    private int numOfb;
    private Orientation gridOrientation = Orientation.Horizontal;
    private bool final=true;
    private bool throw_;
    private bool allowDiagonals = true;
    private bool correctDiagonalSpeed = true;
    private Vector2 input;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float t;
    private float factor;
    private Vector3 v;
    private float nb;

    public void Update()
    {
        if (final)
        {
            v = new Vector3(0, 0, levelHardness);

            transform.position += v * Time.deltaTime;
            transform.rotation = Quaternion.identity;
            if (!isMoving)
            {
                input = new Vector2(Input.GetAxis("Horizontal"), 0);
                if (!allowDiagonals)
                {
                    transform.position += transform.forward * Time.deltaTime * levelHardness;
                    if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                    {
                        input.y = 0;
                    }
                    else
                    {
                        input.x = 0;
                    }
                }

                if (input != Vector2.zero)
                {
                    StartCoroutine(move(transform));
                }
            }
        }
        if (throw_)
        {
       
            while (nb < numOfb)
            {
                Vector3 v3;
                v3 = transform.position;
                v3.y += 1;
                v3.y += nb/4;
                ThrowableBullet = Instantiate(ThrowableBullet, v3, Quaternion.Euler(90, 0, 0));
                

                nb++;
            } 
           
            var to = GameObject.FindGameObjectsWithTag("ThrowableBullet");
            if (Input.GetMouseButtonDown(0)) 
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
                float enter;
                if (plane.Raycast(ray, out enter))
                {
                    var hitPoint = ray.GetPoint(enter);               
                    var mouseDir = hitPoint - gameObject.transform.position;   
                    mouseDir = mouseDir.normalized;    
                    to[0].GetComponent<Rigidbody>().AddForce(mouseDir * clickForce);
                }
                 to[0].GetComponent<Collider>().isTrigger = false;
                   to[0].tag = "ThrowedBullet";

            }
        }
    }

    public IEnumerator move(Transform transform) {
        isMoving = true;
        startPosition = transform.position;
        t = 0;
 
        if(gridOrientation == Orientation.Horizontal) {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y, startPosition.z+0.5f + System.Math.Sign(input.y) * gridSize);
        } else {
            endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gridSize,
                startPosition.y + System.Math.Sign(input.y) * gridSize, startPosition.z);
        }
 
        if(allowDiagonals && correctDiagonalSpeed && input.x != 0 && input.y != 0) {
            factor = 0.7071f;
        } else {
            factor = 1f;
        }
 
        while (t < 1f) {
            t += Time.deltaTime * (moveSpeed/gridSize) * factor;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }
 
        isMoving = false;
        yield return 0;
    }

    void OnCollisionEnter(Collision collider)
    {
        
        if (collider.gameObject.CompareTag("Bullet"))
        {
            
            numOfBullets++;
   
            var clone = Instantiate(Bullet, transform);
            
            var localPos = clone.transform.localPosition;
            localPos.x = 0;
            localPos.y = numOfBullets/3;
            clone.transform.localPosition = localPos;
            Destroy(collider.gameObject);
        }

        if (collider.gameObject.CompareTag("FinalWall"))
        {
            var objects = GameObject.FindGameObjectsWithTag("CollectedBullet"); 
            numOfb = objects.Length;
            foreach (var var in objects)
            {
                
                Destroy(var); 
            }

            final = false;
            throw_=true;
            nb = 0;
        }
        
    }
}