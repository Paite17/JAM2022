using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    // TODO: Collisions

    // Start is called before the first frame update
    void Start()
    {
        // initiallising components
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        
        // get direction
        Vector3 direction = mousePos - transform.position;

        // get rotation
        Vector3 rotation = transform.position - mousePos;

        // fire bullet
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // set rotation of object based on  mouse dir
        float rotate = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotate + 90);
    }
}
