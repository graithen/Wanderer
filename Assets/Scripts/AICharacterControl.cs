using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class AICharacterControl : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public ThirdPersonCharacter character { get; private set; } // the character we are controlling
    public Transform target;                                    // target to aim for

    public float MovementSpeed = 1.5f;
    public bool Running;

    private Camera mainCamera;
    private bool camControlActive;

    private void Start()
    {
        mainCamera = Camera.main;

        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }


    private void Update()
    {
        HandleCameraActive();

        if (!IsPointerOverUIObject() && !camControlActive)
        {
            GetInputVector();
        }

        if (target != null)
            agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance)
            character.Move(agent.desiredVelocity, false, false);
        else
            character.Move(Vector3.zero, false, false);
    }


    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void GetInputVector()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Running = true;
            StartCoroutine(InitiateRunning());

            RaycastHit hit;

            if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
            Running = false;

        if (!Running)
            agent.speed = MovementSpeed / 3;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void HandleCameraActive()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (IsPointerOverUIObject())
                camControlActive = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
            camControlActive = false;
    }

    IEnumerator InitiateRunning()
    {
        yield return new WaitForSeconds(0.2f);
        if (Running)
            agent.speed = MovementSpeed;
    }
}