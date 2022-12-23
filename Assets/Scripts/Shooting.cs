using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenShot;
    private Scene currentScene;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = currentScene.name;
    }

    // might put these in functions later -lewis

    // Update is called once per frame
    void Update()
    {
        // set mouse position
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // rotation vector
        Vector3 rotation = mousePos - transform.position;

        // z axis rotation
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        // set the rotation of the object itself
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        // cooldown timer
        if (!canFire)
        {
            timer += Time.deltaTime;

            // when timer is finished
            if (timer > timeBetweenShot)
            {
                canFire = true;
                timer = 0;
            }
        }

        // shooting input
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            // so that you can't shoot everyone too early on
            if (sceneName == "SampleScene")
            {
                Instantiate(bullet, bulletTransform.position, Quaternion.identity);

                // prevent spam
                canFire = false;
            }
        }
    }
}
