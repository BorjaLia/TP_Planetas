using System;
using Unity.VisualScripting;
using UnityEngine;


public class Orbit : MonoBehaviour
{
    static float timescale = 1;

    [SerializeField] float startImpulse = 1;
    [SerializeField] float rotation = 10;

    Transform parentPos;
    Rigidbody parentRigidBody;

    Vector3 localPos;
    Rigidbody rb;

    Vector3 atractionForce;

    float startDist;

    private void Awake()
    {
        parentPos = transform.parent;
        parentRigidBody = GetComponentInParent<Rigidbody>();

        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        localPos = (transform.position - parentPos.position);
        Vector3 perpendicular;

        perpendicular.x = 0;
        perpendicular.y = 0;
        perpendicular.z = 0;

        perpendicular.Set(-localPos.z,0, localPos.x);

        perpendicular.Normalize();
        perpendicular = perpendicular * rb.mass * localPos.magnitude * localPos.magnitude * startImpulse;

        rb.AddForce(perpendicular);

        startDist = localPos.magnitude;

        rb.AddTorque(Vector3.up * rotation);

    }

    // Update is called once per frame
    void Update()
    {
        localPos = (transform.position - parentPos.position);
        atractionForce = -localPos.normalized;
        atractionForce = atractionForce * (rb.mass * parentRigidBody.mass / localPos.sqrMagnitude);
        //atractionForce = atractionForce * 100;

        atractionForce *= Time.deltaTime * 200;

        //print(1.0f/Time.deltaTime);

        rb.AddForce(atractionForce);
        if(localPos.magnitude * 0.7f > startDist || localPos.magnitude * 1.3f < startDist)
        {
            //print(this.name + "is out of range!");
            //print(startDist + " vs " + this.localPos.magnitude);
        }
    }
}
