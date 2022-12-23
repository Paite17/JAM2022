using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject bullet;
    [SerializeField] private EntitySpawn[] spawners;

    public int playerHP;
    public bool hasPasscode;
    public bool canMoveOn = true;
    private Vector2 moveDirection;
    public float moveSpeed;

    private float moving;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }


    

    private void Start()
    {
        //Physics2D.IgnoreCollision(GetComponent<Collider2D>(), enemy.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());
    }

    // Update is called once per frame
    private void Update()
    {
        if (dialogueUI.isOpen)
        {
            return;
        }

        ProcessInputs();

        // animation stuff
        animator.SetFloat("Speed", moving);
        



        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            if (Interactable != null)
            {
                Debug.Log("Interactable != null!");
                Interactable.Interact(this);
            }
        }


        // death
        if (playerHP < 1)
        {
            // gameover
            SceneManager.LoadScene("GameOver");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GuardedEntrance")
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (collision.gameObject.tag == "Hat")
        {
            /*GameObject spawnerObj = GameObject.Find("EntitySpawner");
            EntitySpawn spawn = spawnerObj.GetComponent<EntitySpawn>();
            spawn.startTrigger = true;
            collision.gameObject.SetActive(false); */

            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].startTrigger = true;
            }
        }

        if (collision.gameObject.tag == "Level1Door")
        {
            if (canMoveOn == true)
            {
                SceneManager.LoadScene("BossRoom");
            }
        }

        if (collision.gameObject.tag == "Enemy")
        {
            // take damage
            int random = Random.Range(3, 10);

            playerHP -= random;
        }

        if (collision.gameObject.tag == "MrsCDialougeTrigger")
        {
            Interactable.Interact(this);
            collision.gameObject.SetActive(false);
        }
    }

    // for physics calculations 
    private void FixedUpdate()
    {
        Move();
    }

    // takes the inputs of the player and identifies what sort of direction to take
    private void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        // flipping sprite cus the sheet only had one side
        if (moveX > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        
        if (moveX < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        // dumb ish solution to fix the animation problem
        // it does work tho
        if (moveX != 0)
        {
            moving = 1;
        }
        else if (moveX == 0 && moveY == 0)
        {
            moving = 0;
        }

        if (moveY != 0)
        {
            moving = 1;
        }
    }

    // move
    // my comments are so insightful
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

}
