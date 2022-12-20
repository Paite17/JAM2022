using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        ProcessInputs();

        // animation stuff
        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

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
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        
        if (moveX < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // move
    // my comments are so insightful
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
