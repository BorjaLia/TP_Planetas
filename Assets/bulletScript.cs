using System.Diagnostics;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    const float lifetime = 3.0f;
    const float colliderActivationTime = 0.05f;
    float currentLifetime = 0.0f;
    float creationTime;

    float speed = 30.0f;

    //bool started = false;

    private Collider coll;
    private Rigidbody rb;

    void Start()
    {
        creationTime = Time.time;
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        coll.enabled = false;

        if (rb)
        {
            rb.AddForce(this.transform.forward * speed, ForceMode.Impulse);
        }

        print("bullet at: " + this.transform.position);
    }

    void Update()
    {
        //if (!started) { this.Start(); started = true; }
        //this.transform.position += this.transform.forward * speed * Time.deltaTime;

        currentLifetime += Time.deltaTime;

        if (currentLifetime >= creationTime + lifetime)
        {
            print("Killing instance");
            Destroy(gameObject);
        }
        if (!coll.enabled)
        {
            if (currentLifetime > colliderActivationTime)
            {
                coll.enabled = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (rb)
        {
            //rb.linearVelocity = this.transform.forward * speed;
        }
    }

}
