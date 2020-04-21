using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Army : MonoBehaviour, IRaycastable //use RPG's movement mechanique for setting the navmesh's destination
{
    [SerializeField] Canvas ArmyMenu;
    [SerializeField] float navMehSpeed = 3.5f;

    PlayerController player;
    bool Selected = false;
    public IRaycastable Select()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        if (player.faction == this.faction)
        {
            AcceptInteraction();            
            return this;
        }
        else
        {
            DenyInteraction();
            return null;
        }
    }

    private void AcceptInteraction()
    {
        ArmyMenu.GetComponent<Canvas>().enabled = true;
        Selected = true;
    }

    private void DenyInteraction()
    {
        
    }

    void IRaycastable.DeSelect()
    {
        ArmyMenu.GetComponent<Canvas>().enabled = false;
        Selected = false;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Selected)
        {
            if (Input.GetMouseButtonDown(1) && !player.endingTurn)
            {
                print("Recieved right mouth input");
                RaycastToWorld();
            }            
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
            }
        }
    }
    public void EnableMovement()
    {
        GetComponent<NavMeshAgent>().speed = navMehSpeed;
    }
    public void DisableMovement()
    {
        GetComponent<NavMeshAgent>().speed = 0;
    }
    private void RaycastToWorld()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        GetComponent<NavMeshAgent>().SetDestination(hits[0].point);
        print("Destination set");
    }
    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
