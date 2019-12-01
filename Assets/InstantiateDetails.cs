using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractiveObject))]

public class InstantiateDetails : MonoBehaviour
{
    private InteractiveObject interactiveScript;

    public GameObject ItemUI;
    public TextAsset detailsText;
    public bool InstanceInScene;

    public Camera ObservationCam;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        interactiveScript = GetComponent<InteractiveObject>();

        if (ObservationCam != null)
        {
            GetComponentInChildren<Camera>().enabled = false;
            mainCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(interactiveScript != null && !InstanceInScene)
        {
            Interaction();
        }
    }

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && interactiveScript.PlayerPresent)
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100);

            if (hit.transform.gameObject == this.gameObject)
            {
                Debug.Log("You are clicking on: " + this.gameObject.name);
                Debug.Log("Activating object!");

                AlternateCamerasCheck(); //check to see if there is an optional observational camera for this object

                if (ItemUI != null)
                    InstantiateUIComponent();

                Time.timeScale = 0;
            }
        }
    }

    public void InstantiateUIComponent()
    {
        GameObject instantiatedUI = Instantiate(ItemUI, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        instantiatedUI.transform.SetParent(transform);
        InstanceInScene = true;
    }

    public void AlternateCamerasCheck()
    {
        if (ObservationCam != null && !InstanceInScene)
        {
            mainCamera.enabled = false;
            ObservationCam.enabled = true;
        }
        if (ObservationCam != null && InstanceInScene)
        {
            Debug.Log("Trying to switch cams");
            ObservationCam.enabled = false;
            mainCamera.enabled = true;
        }
    }
}
