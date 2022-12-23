using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quirky : MonoBehaviour
{
    [SerializeField] private GameObject wall;

    public void RemoveWall()
    {
        Debug.Log("The FUNNY");
        wall.SetActive(false);
    }
}
