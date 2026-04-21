using System.Diagnostics;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    const float lifetime = 3.0f;
    const float colliderActivationTime = 0.75f;
    float currentLifetime = 0.0f;
    float creationTime;

    float speed = 10.0f;

    //bool started = false;

    private Collider coll;

    void Start()
    {
        creationTime = Time.time;
        coll = GetComponent<Collider>();
        coll.enabled = false;

        print("bullet at: " + this.transform.position);
    }

    void Update()
    {
        //if (!started) { this.Start(); started = true; }
        this.transform.position += this.transform.forward * speed * Time.deltaTime;

        currentLifetime += Time.deltaTime;

        if (currentLifetime >= creationTime + lifetime)
        {
            print("Killing instance");
            Destroy(this);
        }
        if (!coll.enabled)
        {
            if(currentLifetime > colliderActivationTime)
            {
                coll.enabled = true;
            }
        }
    }
}
