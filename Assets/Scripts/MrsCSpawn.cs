using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MrsCSpawn : MonoBehaviour
{
    [SerializeField] private GameObject mrsC;
    

    public void Spawn()
    {
        GameObject temp = Instantiate(mrsC, gameObject.transform);
        temp.transform.position = transform.position;
    }
}
