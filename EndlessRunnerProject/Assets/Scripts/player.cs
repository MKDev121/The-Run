using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed = 5f;
    public float swapSpeed = 10f;
    public float jumpVelo = 60f;
    float velo;
    public GameObject[] characters;

    // Rigidbody rb;
    CharacterController controller;
    public Transform camTransform;
    Vector3 offset;
    public float horizontalIndex=0f;
    public float horizontalMovementMultiplier = 2f;
    public float jumpforce=500f;
    stats plStats;
    AudioSource audioSource;
    bool gameOver=false;
    GameManager gameManager;
    bool onGround;
    //Animations
    Animator animator;

    //touch contols
    Vector2 startPos;
    Vector2 touchDir;
    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characters[gameManager.charIndex].SetActive(true);
        controller = GetComponent<CharacterController>();
        offset = camTransform.position-transform.position;
        plStats = GetComponent<stats>();
        audioSource = GetComponent<AudioSource>();
        animator = gameObject.GetComponentInChildren<Animator>();
        animator.SetBool("run", true);
       
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Gravity();
        Debug.Log(onGround);
        touchControls();
      
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "coin")
        {
            Destroy(other.gameObject);
            audioSource.Play();
            plStats.coin += 1;
           
        }
        if(other.tag == "Obstacle")
        {
           
        }
    }
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.tag=="Obstacle")
        {
            Debug.Log("gameover");
            // rb.velocity = Vector3.zero;
            speed = 0f;
            gameManager.difficulty = 0f;
            
            animator.SetTrigger("Hit");
            animator.SetBool("run", false);
            gameOver = true;
            gameManager.Restart();

        }
        if(hit.collider.tag=="ground")
        {
            onGround = true;
            animator.SetBool("onGround", true);
        }

    }
   
   
    void Movement( )
    {
        if(gameOver==false)
        {

            // rb.velocity = new Vector3(0f, rb.velocity.y, speed*Time.deltaTime);
            
            controller.Move(new Vector3(0f,0f, speed * Time.deltaTime));
            camTransform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + offset;
            if (Input.GetKeyDown("a")||touchDir.x<-0f && Mathf.Abs(touchDir.x) > Mathf.Abs(touchDir.y))
            {
                horizontalIndex = Mathf.Clamp(horizontalIndex - 1f, -1f, 1f);
                touchDir = Vector2.zero;

            }
            else if (Input.GetKeyDown("d")||touchDir.x>0f && Mathf.Abs(touchDir.x)> Mathf.Abs(touchDir.y))
            {
                horizontalIndex = Mathf.Clamp(horizontalIndex + 1f, -1f, 1f);
                touchDir = Vector2.zero;
            }
            Vector3 target = new Vector3(horizontalIndex * horizontalMovementMultiplier, transform.position.y, transform.position.z);
           transform.position = Vector3.MoveTowards(transform.position, target, swapSpeed * Time.deltaTime);
            
            if (Input.GetKeyDown("w")||touchDir.y>0f&&onGround== true && Mathf.Abs(touchDir.x) <touchDir.y)
            {
                animator.SetTrigger("jump");
               // rb.AddForce(transform.up * jumpforce);
                onGround = false;
                touchDir = Vector2.zero;
                animator.SetBool("onGround", false);

            }
        }
        
    }
    void touchControls()
    {
       
       // Input.multiTouchEnabled = false;
            
            Touch touch = Input.GetTouch(0);

         switch (touch.phase)
         {
             case TouchPhase.Began:
                 startPos = touch.position;
                 break;
             case TouchPhase.Ended:
                 touchDir = touch.position - startPos;
                 break;

         }
        touchDir.Normalize();
        //Debug.Log(touchDir);
       
           // touchDir = touch.deltaPosition / touch.deltaTime;
        
        
    }
    void Gravity()
    {
      
        if (onGround == false)
        {
            transform.position +=  new Vector3(0f, velo*Time.deltaTime, 0f);
            velo -= 13f*Time.deltaTime;
        }
        else
        {
            velo = jumpVelo;
        }
    }
}
