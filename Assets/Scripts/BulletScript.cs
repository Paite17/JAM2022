using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public int baseDmg;
    public int lowerDmg;
    public int upperDmg;
    private float timer;

   

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

        // prevent collisions with the player object
        GameObject playerObj = GameObject.Find("Player");
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerObj.GetComponent<Collider2D>());
    }

    // collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // fix something quirky
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }

        // dry fix
        if (collision.gameObject.tag == "Tilemap")
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // simulate some sort of knockback
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        // the fact i had to do this is stupid but tilemap collisions don't like the projectile
        timer += Time.deltaTime;

        if (timer > 5)
        {
            Destroy(gameObject);
        }
    }
}
