using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private Animator animator;

    private Vector2 moveDirection;
    public float moveSpeed;

    private float horizontalMove;

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }



    private void Start()
    {

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
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            if (Interactable != null)
            {
                Debug.Log("Interactable != null!");
                Interactable.Interact(this);
            }
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
    }
    // move
    // my comments are so insightful
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
