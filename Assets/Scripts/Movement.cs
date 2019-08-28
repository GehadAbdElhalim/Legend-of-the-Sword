using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float movementSpeed;
    public float dashAmount;
    public float cooldown;
    public float currentCooldown = 0;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    //private float distToGround;

    public GameObject camera;
    public LayerMask layerMask;
    public TrailRenderer trail;
    public GameObject arrow;
    Transform cameraT;
    Animator anim;

    bool isGrounded = true;
    public static int comboCounter = 0;
    public static bool canGetInput = false;
    public static bool inCombat = false;

    private void Start()
    {
        cameraT = camera.transform;
        trail.emitting = false;
        anim = transform.GetChild(0).GetComponent<Animator>();
        //distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
        else
        {
            currentCooldown = 0;
        }

        if (inCombat)
        {
            anim.SetBool("Run", false);
        }

        if (!inCombat)
        {
            comboCounter = 0;
            //Movement
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            anim.SetBool("Run", input != Vector2.zero);
            Vector2 inputDir = input.normalized;

            if (inputDir != Vector2.zero)
            {
                Run(inputDir);
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            //Dash
            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && currentCooldown == 0)
            {
                Dash(inputDir);
            }

            //Attack
            if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded)
            {
                Attack(inputDir);
            }

            //Ranged
            if (Input.GetKeyDown(KeyCode.Mouse1) && isGrounded)
            {
                Shoot(inputDir);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void Run(Vector2 inputDir)
    {
        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        transform.Translate(transform.forward * movementSpeed * Time.deltaTime, Space.World);
    }

    void Jump()
    {
        anim.SetTrigger("Jump");
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 4f, 0), ForceMode.Impulse);
    }

    void Dash(Vector2 inputDir)
    {
        anim.SetTrigger("Dash");
        trail.emitting = true;
        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
        transform.eulerAngles = Vector3.up * targetRotation;
        transform.position += transform.forward * dashAmount;
        currentCooldown = cooldown;
        Invoke("EndTrail", 0.1f);
    }

    void EndTrail()
    {
        trail.emitting = false;
    }

    void Attack(Vector2 inputDir)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2, layerMask);
        if (hitColliders.Length != 0)
        {
            int min = 2;
            int min_index = 0;
                
            for(int i = 0; i < hitColliders.Length; i++)
            {
                float distance = (transform.position - hitColliders[i].transform.position).magnitude;
                if (distance < min)
                {
                    min_index = i;
                }
            }

            transform.LookAt(new Vector3(hitColliders[min_index].transform.position.x, 0, hitColliders[0].transform.position.z));
        }

        inCombat = true;
        anim.SetInteger("Combo", ++comboCounter);
    }

    void Shoot(Vector2 inputDir)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5, layerMask);
        if (hitColliders.Length != 0)
        {
            int min = 2;
            int min_index = 0;

            for (int i = 0; i < hitColliders.Length; i++)
            {
                float distance = (transform.position - hitColliders[i].transform.position).magnitude;
                if (distance < min)
                {
                    min_index = i;
                }
            }

            transform.LookAt(new Vector3(hitColliders[min_index].transform.position.x, 0, hitColliders[0].transform.position.z));
        }

        inCombat = true;
        anim.SetTrigger("Shoot");
        GameObject projectile = Instantiate(arrow,transform.position,Quaternion.identity);
        projectile.transform.forward = transform.forward;
        projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 10, ForceMode.Impulse);
    }

    public void OutOfCombat()
    {
        inCombat = false;
    }

    public void Cancel()
    {
        canGetInput = false;
        anim.SetInteger("Combo", comboCounter);
    }
}
