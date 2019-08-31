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
    public GameObject Sword;
    public GameObject Bow;
    public GameObject Legendary;
    Transform cameraT;
    Animator anim;
    public static bool UseLegendary = false;

    bool isGrounded = true;
    public static int comboCounter = 0;
    public static bool canGetInput = false;
    public static bool inCombat = false;

    public GameObject[] Slashes;

    private void Start()
    {
        cameraT = camera.transform;
        trail.emitting = false;
        anim = transform.GetChild(0).GetComponent<Animator>();
        Sword.SetActive(true);
        Bow.SetActive(false);
        Legendary.SetActive(false);
        //distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        if (anim.GetInteger("Die") < 1)
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
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
                {
                    inCombat = false;
                    comboCounter = 0;
                    anim.SetInteger("Combo", 0);
                }
                anim.SetBool("Run", false);
            }

            if (!inCombat)
            {
                Bow.SetActive(false);

                if (UseLegendary)
                {
                    Legendary.SetActive(true);
                    Sword.SetActive(false);
                    Legendary.GetComponent<Collider>().enabled = false;
                }
                else
                {
                    Sword.SetActive(true);
                    Legendary.SetActive(false);
                    Sword.GetComponent<Collider>().enabled = false;
                }

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
            if(this.transform.position.y<-100){
            this.transform.position = new Vector3(0f,100f,0f);
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
        float searchDistance = (UseLegendary) ? 10 : 3;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, searchDistance, layerMask);
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

            transform.LookAt(new Vector3(hitColliders[min_index].transform.position.x, transform.position.y, hitColliders[0].transform.position.z));
        }

        inCombat = true;
        anim.SetInteger("Combo", ++comboCounter);
        if (UseLegendary)
        {
            GameObject slash = Slashes[Random.Range(0, Slashes.Length)];
            GameObject instance = Instantiate(slash, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), slash.transform.rotation);
            instance.transform.up = transform.forward;
            instance.GetComponent<Rigidbody>().AddForce(instance.transform.up * 10, ForceMode.Impulse);
        }
    }


    void Shoot(Vector2 inputDir)
    {
        Bow.SetActive(true);
        if (UseLegendary) {
            Legendary.SetActive(false);
        }
        else
        {
            Sword.SetActive(false);
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10, layerMask);
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

            transform.LookAt(new Vector3(hitColliders[min_index].transform.position.x, hitColliders[min_index].transform.position.y + 0.5f, hitColliders[min_index].transform.position.z));
        }

        inCombat = true;
        anim.SetTrigger("Shoot");
        GameObject projectile = Instantiate(arrow,new Vector3(transform.position.x, transform.position.y + 0.5f ,transform.position.z),Quaternion.identity);
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
