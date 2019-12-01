using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public GameObject playerRef;
    public bool PlayerPresent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        if (playerRef != null)
            PlayerPresent = true;
        else
            PlayerPresent = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            playerRef = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
            playerRef = null;
    }


}
