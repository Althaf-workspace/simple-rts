using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; set; }
    public List<GameObject> allUnitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();
    Camera cam;

    public LayerMask clickable;
    public LayerMask ground;
    public LayerMask attackable;
    public bool attackCursorVisible;
    public GameObject groudMaker;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            // if hitting clickable
            if (Physics.Raycast(ray,out hit, Mathf.Infinity,clickable))
            {
                if (Keyboard.current.leftShiftKey.isPressed)
                {
                    MultiSelectUnit(hit.collider.gameObject);
                }
                else
                {
                    SelectByClicking(hit.collider.gameObject);
                }
            }
            else
            {
                DeselectAll();
            }
        }
        if (Mouse.current.rightButton.wasPressedThisFrame && unitSelected.Count > 0)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            // if hitting clickable
            if (Physics.Raycast(ray,out hit, Mathf.Infinity,ground))
            {
                groudMaker.transform.position = hit.point;
                groudMaker.SetActive(false);
                groudMaker.SetActive(true);
            }
        }
        if (unitSelected.Count > 0 && AtLeastOneOffensiveUnit(unitSelected))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());

            // if hitting clickable
            if (Physics.Raycast(ray,out hit, Mathf.Infinity,attackable))
            {
                attackCursorVisible = true;

                if (Mouse.current.rightButton.wasPressedThisFrame)
                {
                    Transform target = hit.transform;
                    foreach (GameObject unit in unitSelected)
                    {
                        if (unit.GetComponent<AttackController>())
                        {
                         unit.GetComponent<AttackController>().targetToAttack = target;   
                        }
                    }
                }
                
            }
            else
            {
                attackCursorVisible = false;
            }
            
        }
        
    }

    private bool AtLeastOneOffensiveUnit(List<GameObject> unitSelected)
    {
        foreach (GameObject unit in unitSelected)
        {
            if (unit.GetComponent<AttackController>())
            {
                return true;
            }
        }
        return false;
    }

    private void MultiSelectUnit(GameObject unit)
    {
        if (unitSelected.Contains(unit) == false)
        {
            SelectUnit(unit,true);
            unitSelected.Add(unit);
        }
        else
        {
            SelectUnit(unit,false);
            unitSelected.Remove(unit);
        }
    }

    private void DeselectAll()
    {
        foreach (var unit in unitSelected)
        {
            SelectUnit(unit, false);
        }
        unitSelected.Clear();
        groudMaker.SetActive(false);
    }

    private void SelectByClicking(GameObject unit)
    {
        DeselectAll();

        unitSelected.Add(unit);

        SelectUnit(unit, true);
    }

    private void SelectUnit(GameObject unit, bool shouldMove)
    {
        unit.GetComponent<UnitMovement>().enabled = shouldMove;
        EnableUnitIndicator(unit,shouldMove);
    }

    private void EnableUnitIndicator(GameObject unit, bool visiblity)
    {
        unit.transform.GetChild(0).gameObject.SetActive(visiblity);
    }

    internal void DragSelect(GameObject unit)
    {
        if (unitSelected.Contains(unit) == false)
        {
            unitSelected.Add(unit);
            SelectUnit(unit,true);
        }
    }
}
