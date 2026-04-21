using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class LookCamera : MonoBehaviour 
{
    public float speedNormal = 30.0f;
    public float speedFast   = 80.0f;

    public float mouseSensitivityX = 5.0f;
	public float mouseSensitivityY = 5.0f;
    
	float rotY = 0.0f;

    PlayerInput control;
    Vector2 movementH;
    float movementV;
    Vector2 look;


	void Start()
	{
        if (GetComponent<PlayerInput>())
        {
            control = GetComponent<PlayerInput>();
        }

    }



    void Update()
	{

        transform.Translate(new Vector3(movementH.x, 0, movementH.y) * speedNormal * Time.deltaTime,Space.World);

        if (Input.GetMouseButton(1))
        {
            Vector3 rot = transform.localEulerAngles;
            rot.y += look.x * mouseSensitivityX * Time.deltaTime;
            rot.x -= look.y * mouseSensitivityY * Time.deltaTime;
            Mathf.Clamp(rot.x, -89.5f, 89.5f);
            rot.z = 0;
            transform.localEulerAngles = rot;
            //print("moved: " + look.ToString() + "!");
        }
    }

    public void OnMove(InputValue value)
    {
        movementH = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        look = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        movementV = value.Get<float>();
        print("Jumped: " + movementV + "!");
    }
    public void OnCrouch(InputValue value)
    {
        movementV = -value.Get<float>();
        print("Crouched: " + movementV + "!");
    }
}
