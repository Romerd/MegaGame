using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    //public Rigidbody theRB;
    public float jumpForce;
    public CharacterController controller;
    public float gravityScale;
    public float rotSpeed;
    //private float rot = 0f;

    private Animator anim;


    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //theRB = GetComponent<Rigidbody>();
        controller = GetComponent <CharacterController>() ;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GetInput();
    }

    void GetInput()
    {
        if (controller.isGrounded)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(anim.GetBool("running") == true)
                {
                    anim.SetBool("running", false);
                    anim.SetInteger("condition", 0);
                }

                if (anim.GetBool("running") == false)
                {
                    Attacking();
                }

            }
        }
    }
    void Movement()
    {
        /*theRB.velocity = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, theRB.velocity.y, Input.GetAxis("Vertical") * moveSpeed);

       if (Input.GetButtonDown("Jump")) //Проверяем на нажатие кнопки
       {
           theRB.velocity = new Vector3(theRB.velocity.x, jumpForce, theRB.velocity.z);
       }
       */

        // moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical"))
                            + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);

            if (Input.GetKey(KeyCode.W))
            {
                if (anim.GetBool("attacking") == true)
                {
                    return;
                }
                else if (anim.GetBool("attacking") == false)
                {
                    anim.SetBool("running", true);
                    anim.SetInteger("condition", 1);
                }


                //if (Input.GetKey(KeyCode.Mouse0))
                //{
                //    anim.SetInteger("condition", 2);
                //}

                //if (Input.GetMouseButton(0))
                //{
                //    anim.SetInteger("condition", 4);
                //}
                //moveDirection = new Vector3(0, 0, 1);
                //moveDirection *= moveSpeed;
                //moveDirection = transform.TransformDirection(moveDirection);
            }

            //if (Input.GetKey (KeyCode.Mouse0))
            //{
            //    anim.SetInteger("condition", 2);
            //}

            //if (Input.GetMouseButton(0))
            //{
            //    anim.SetInteger("condition", 4);
            //}

            if (Input.GetKey(KeyCode.F))
            {
                anim.SetInteger("condition", 3);
            }

            if (Input.GetKeyUp(KeyCode.F))
            {
                anim.SetInteger("condition", 0);
            }

            //if (Input.GetMouseButtonUp(0))
            //{
            //    anim.SetInteger("condition", 0);
            //}

            //if (Input.GetKeyUp (KeyCode.Mouse0))
            //{
            //    anim.SetInteger("condition", 0);
            //}

            if (Input.GetKeyUp(KeyCode.W))
            {
                anim.SetBool("running", false);
                anim.SetInteger("condition", 0);
                //moveDirection = new Vector3(0, 0, 0);
            }


            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump")) //Проверяем на нажатие кнопки
            {
                moveDirection.y = jumpForce;
            }


        }

        //rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        //transform.eulerAngles = new Vector3(0, rot, 0);

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);// * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }
    void Attacking()
    {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine()
    {
        anim.SetBool("attacking", true);
        anim.SetInteger("condition", 2);
        yield return new WaitForSeconds(1);
        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);
    }
}
