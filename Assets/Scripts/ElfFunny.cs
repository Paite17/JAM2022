using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfFunny : MonoBehaviour
{
    [SerializeField] private GameObject replacement;
    [SerializeField] private Transform target;
    [SerializeField] private int moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject gateDialogueTrigger;
    [SerializeField] private GameObject gateWall;

    private Vector2 movement;
    // awful hard-coded scripted sequence be like
    public void RunIntoWall()
    {
        StartCoroutine(ElfDie())
;    }

    IEnumerator ElfDie()
    {
        Debug.Log("Coroutine time!");
        gameObject.GetComponent<SpriteRenderer>().flipX = true;

        Vector3 direction = target.position - transform.position;
        direction.Normalize();
        movement = direction;
        MoveCharacter(direction);

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
        replacement.SetActive(true);

        GameObject playerObj = GameObject.Find("Player");
        PlayerMovement player = playerObj.GetComponent<PlayerMovement>();
        player.hasPasscode = true;
        gateDialogueTrigger.SetActive(false);
        gateWall.SetActive(false);
    }

    private void MoveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

}
