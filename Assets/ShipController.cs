using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.UI;

public class ShipController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject guns;

    Vector3 tip;

    Rigidbody rb;
    PlayerInput control;

    //Vector2 movementH;
    float movementV;

    Vector2 look;

    const float speedNormal = 3.0f;
    const float speedFast = 8.0f;

    public float speed = speedNormal;

    bool isJumping = false;
    bool isCrouched = false;
    bool isBoosting = false;

    bool shotQueued = false;

    public float mouseSensitivityX = 5.0f;
    public float mouseSensitivityY = 5.0f;

    float rotY = 0.0f;

    static float shootInterval = 0.5f;
    float lastShotTime = shootInterval;

    float bulletForce = 10.0f;

    bool isKinematic;

    void KinematicMove()
    {
        Vector3 dir;
        dir = this.transform.forward;

        speed = (isBoosting ? speedFast : speedNormal);

        if (isCrouched) dir += -transform.up;
        if (isJumping) dir += transform.up;

        this.transform.position += dir * speed * Time.deltaTime;
        MouseLook();
    }

    void DynamicMove()
    {
        //if (movementH != Vector2.zero)
        //{
        //    if (movementH.x == 0 || movementH.y == 0)
        //    {
        //        Vector3 torque = transform.up.normalized * (movementH.x - movementH.y);
        //        rb.AddTorque(torque);
        //        print(torque + " , " + (movementH.x - movementH.y));
        //    }
        //    else
        //    {
        //        rb.AddRelativeForce(transform.forward * speedNormal);
        //    }
        //}
    }

    void MouseLook()
    {
        Vector3 rot = this.transform.localEulerAngles;
        rot.y += look.x * mouseSensitivityX * Time.deltaTime;
        rot.x -= look.y * mouseSensitivityY * Time.deltaTime;
        if (rot.x > 89.5f && rot.x < 180.0f)
        {
            rot.x = 89.5f;
        }
        else if (rot.x < 270.5f && rot.x > 180.0f)
        {
            rot.x = 270.5f;
        }
        rot.z = 0;
        this.transform.localEulerAngles = rot;
    }

    void Start()
    {
        if (GetComponent<PlayerInput>())
        {
            control = GetComponent<PlayerInput>();
        }
        if (rb = GetComponent<Rigidbody>())
        {
            rb = GetComponent<Rigidbody>();
            isKinematic = rb.isKinematic;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        guns = this.transform.GetChild(2).gameObject;
        tip = guns.transform.position;
        lastShotTime += Time.deltaTime;

        if (shotQueued && shootInterval <= lastShotTime)
        {
            GameObject currentBullet = Instantiate(bullet, tip, guns.transform.rotation);
            //currentBullet.GetComponent<Rigidbody>().AddForce(currentBullet.transform.forward.normalized * bulletForce);

            //currentBullet.transform.SetParent(guns.transform,true);
            //currentBullet.transform.position = tip;
            //currentBullet.transform.rotation = this.transform.rotation;


            lastShotTime = 0.0f;
            //print("Shot!");
            print("shot from: " + tip);
        }

        //Quaternion 
        //this.transform.eulerAngles;s
    }
    private void FixedUpdate()
    {
        if (isKinematic)
        {
            KinematicMove();
        }
        else
        {
            DynamicMove();
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //movementH = value.Get<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
        //print("looking: " + look);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.performed;
        print("Jumped: " + isJumping + "!");
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        isCrouched = context.performed;
        print("Crouched: " + isCrouched + "!");
    }

    public void OnSwitchMode(InputAction.CallbackContext context)
    {
        isKinematic = context.performed;
        rb.isKinematic = isKinematic;
        print("Kinematic: " + isKinematic);
    }

    public void OnBoosters(InputAction.CallbackContext context)
    {
        isBoosting = context.performed;
        print("Boosting: " + isBoosting);
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        shotQueued = context.performed;
        print("Shotqueued: " + shotQueued);
    }
}
