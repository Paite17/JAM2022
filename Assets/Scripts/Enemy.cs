using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    ENEMY,
    BOSS
}

public class Enemy : MonoBehaviour
{
    // keeep this public or AI will die!!!
    public GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    private Vector2 movement;
    public int enemyHP;
    public EnemyType enemyType;

    
    // Start is called before the first frame update
    void Start()
    {
        // prevent collisions with the player object
        GameObject playerObj = GameObject.Find("Player");
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), playerObj.GetComponent<Collider2D>());
        player = playerObj;
    }

    // Update is called once per frame
    void Update()
    {   
        // flip sprite based on where the player is facing
        if (transform.position.x > player.transform.position.x)
        {
            
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if (transform.position.x < player.transform.position.x)
        {
            
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;

        // check death
        if (enemyHP < 1)
        {
            Dead();
        }
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void FixedUpdate()
    {
        MoveCharacter(movement);
    }

    public void TakeDamage(int baseDmg, int lowerDmg, int upperDmg)
    {
        Debug.Log("baseDmg = " + baseDmg + " lowerDmg = " + lowerDmg + " upperDmg = " + upperDmg);
        // add some variety to damage numbers
        int randomised = Random.Range(lowerDmg, upperDmg);

        // crit chance :3
        int critChance = Random.Range(1, 50);
        if (critChance == 1)
        {
            baseDmg *= 4;
        }

        int finalDmg = randomised + baseDmg;
        Debug.Log("finalDmg = " + finalDmg);

        // subtract from health
        enemyHP -= finalDmg;
    }

    // death
    private void Dead()
    {
        // TODO: funny death sequence with sfx
        Destroy(gameObject);

        if (enemyType == EnemyType.BOSS)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Ending");
        }
    }

    // take damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();
            TakeDamage(bullet.baseDmg, bullet.lowerDmg, bullet.upperDmg);
        }
    }
}
