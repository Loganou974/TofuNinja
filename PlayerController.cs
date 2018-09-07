using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isAiming;
    //test
    public Material trailMat;
    int snapFactor = 16;
    public bool moveSnap;

    public GameObject mouseIco;
    Transform mouseDownToken, mouseUpToken;
    public Vector3 direction; // xz
    private float xDir, zDir;
    public float xMin, xMax, zMin, zMax;
    public float moveForce = 0.5f;
    Rigidbody rb;
    LineRenderer lineRend;
    // Use this for initialization
    GameObject newGo1, newGo2, newGo3, dirGo;
   Vector3 desiredVelocity ;
   public float lastSqrMag;
   public float sqrMag;
    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        CreateMouseTokens();
        lastSqrMag = Mathf.Infinity;  
    }

    void CreateMouseTokens()
    {
        newGo1 = Instantiate(mouseIco, transform.position, Quaternion.identity) as GameObject;
        newGo1.name = "point1";
        newGo2 = Instantiate(mouseIco, transform.position, Quaternion.identity) as GameObject;
        newGo2.name = "point2";

        dirGo = new GameObject();
       // dirGo.transform.parent = transform;

        lineRend = dirGo.AddComponent<LineRenderer>();
        lineRend.material = trailMat;
        lineRend.startWidth = 0.2f;
        lineRend.endWidth = 0.2f;
        lineRend.numCapVertices = 10;
        lineRend.numCornerVertices = 10;

        //newGo3 = Instantiate(mouseIco,transform.position,Quaternion.identity) as GameObject;
        //newGo3.name = "souris";
        mouseDownToken = newGo1.transform;

        mouseUpToken = newGo2.transform;
        mouseDownToken.GetComponent<SphereCollider>().enabled = false;
        mouseUpToken.GetComponent<SphereCollider>().enabled = false;
        // newGo3.GetComponent<SphereCollider>().enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lineRend.enabled = true;
            isAiming = true;

            mouseDownToken.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseUpToken.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseDownToken.transform.position = new Vector3(mouseDownToken.transform.position.x, 0, mouseDownToken.transform.position.z);
            mouseUpToken.transform.position = new Vector3(mouseUpToken.transform.position.x, 0, mouseUpToken.transform.position.z);
        }

        if (Input.GetMouseButton(0) && isAiming)
        {
            mouseUpToken.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseUpToken.transform.position = new Vector3(mouseUpToken.transform.position.x, 0, mouseUpToken.transform.position.z);
            direction = mouseUpToken.position - mouseDownToken.position;
            dirGo.transform.position = transform.position + direction;

            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, dirGo.transform.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if( isAiming)
            {
                desiredVelocity = direction;
                //Move(direction, moveForce);
                MoveTo(dirGo.transform.position);
                isAiming = false;
                lineRend.enabled = false;
            }
        }
        rb.velocity = desiredVelocity * 2;

    }

    void Move(Vector3 _dir, float _force)
    {
        if (moveSnap)
        {

            //arrondir la direction (divise par 16 directions)
            _dir = new Vector3(Mathf.Round((_dir.x / snapFactor) * snapFactor), 0, Mathf.Round((_dir.z / snapFactor) * snapFactor));

        }

        //clamp la valeur
        _dir = new Vector3(Mathf.Clamp(_dir.x, xMin, xMax), 0, Mathf.Clamp(_dir.z, zMin, zMax));

        rb.AddForce(_dir * _force, ForceMode.VelocityChange);
    }
    
    void MoveTo(Vector3 target)
    {
       
        if ( sqrMag > lastSqrMag ) desiredVelocity = Vector3.zero;

        lastSqrMag = sqrMag;
    }


    void FixedUpdate()
    {
        sqrMag = (dirGo.transform.position - transform.position).sqrMagnitude;

         if(dirGo.transform.position != transform.position) MoveTo(dirGo.transform.position);
    }
}
