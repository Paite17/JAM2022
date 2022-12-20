using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private DialogueUI dialogueUI;
    private Vector2 moveDirection;
    public float moveSpeed;

    // these are for shooting
    [SerializeField] private Transform aimGunEndPointTransform;
    [SerializeField]private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    

    public DialogueUI DialogueUI => dialogueUI;

    public IInteractable Interactable { get; set; }

    

    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        ProcessInputs();

        // input for shooting
        if (Input.GetMouseButtonDown(0))
        {
            ShootBullet();
        }

        // set the rotation based on mosuepos
        firePoint.transform.rotation = Quaternion.Euler(0, 0, Input.mousePosition.z);
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
    }

    // move
    // my comments are so insightful
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    // shooty shooty pow pow bang
    private void ShootBullet()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
