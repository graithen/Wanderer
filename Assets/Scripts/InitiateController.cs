using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateController : MonoBehaviour
{
    void Start()
    {
        this.gameObject.transform.DetachChildren();
        Destroy(this.gameObject);
    }
}
