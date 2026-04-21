using Unity.VisualScripting;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    TrailRenderer tr;
    Renderer parentRend;
    Texture2D parentTexture;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tr = this.GetComponent<TrailRenderer>();

        parentRend = transform.parent.GetComponentInParent<Renderer>();
        parentTexture = parentRend.material.mainTexture as Texture2D;

        Color startColor = parentTexture.GetPixelBilinear(0.5f, 0.5f);
        startColor *= 0.9f;

        Color endColor = startColor;
        endColor *= 0.3f;
        

        tr.startColor = startColor;
        tr.endColor = endColor;


        tr.startWidth = this.transform.parent.localScale.magnitude/2.0f;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
