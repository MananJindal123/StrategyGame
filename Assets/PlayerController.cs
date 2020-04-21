using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float EndturnTime = 10f;

    float endTurnCount = 0;
    public bool endingTurn;
    int turn = 1;
    public Factions faction;
    IRaycastable selectedRaycastable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        if (endingTurn)
        {
            if (endTurnCount > 0)
            {
                endTurnCount = Mathf.Max(0, endTurnCount - Time.deltaTime);
            }
            else
            {
                BeginNewTurn();
            }
        }
    }  

    private void ProcessInput()
    {   
        if (Input.GetMouseButtonDown(0) && !InteractWithUI())
        {
            RaycastIntoWorld();
        }       
    }

    private void RaycastIntoWorld()
    {
        RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
        //Vector3 hitPoint = hits[0].point;
        //Camera.main.transform.position = new Vector3(hitPoint.x, Camera.main.transform.position.y, hitPoint.z);
        foreach (RaycastHit hit in hits)
        {
            IRaycastable iRaycastable = hit.transform.GetComponent<IRaycastable>();
            if (iRaycastable == null)
            {
                continue;
            }
            transform.position = new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z); //consider transmiting
            DeselectRaycastable();
            selectedRaycastable = iRaycastable.Select();
            return;
        }
        DeselectRaycastable();
    }
    public void EndTurn()
    {
        print("ending turn...");
        if (endTurnCount == 0)
        {
            endTurnCount = EndturnTime;
            endingTurn = true;
            ToggleArmies(true);
        }        
    }
    private void BeginNewTurn()
    {
        print("new turn has started");
        endingTurn = false;
        turn++;
        ToggleArmies(false);
    }
    public void InterauptTurnEnding()
    {
        endingTurn = false;
        ToggleArmies(false);
    }
    private void ToggleArmies(bool x)
    {
        Army[] armies = FindObjectsOfType<Army>();
        if (x)
        {
            foreach (Army army in armies)
            {
                army.EnableMovement();
            }
        }
        else
        {
            foreach (Army army in armies)
            {
                army.DisableMovement();
            }
        }
    }
    private void DeselectRaycastable()
    {
        if (selectedRaycastable != null)
        {
            selectedRaycastable.DeSelect();
            selectedRaycastable = null;
        }
    }

    private static Ray GetMouseRay()
    {
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
    private bool InteractWithUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
