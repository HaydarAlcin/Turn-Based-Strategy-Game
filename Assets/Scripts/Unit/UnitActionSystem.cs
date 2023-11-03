using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    //Event
    public event EventHandler OnSelectedUnitChange;
    public event EventHandler OnSelectedActionChange;

    //Busy UI Eventi
    public event EventHandler<bool> OnBusyChanged;

    //ActionPoint
    public event EventHandler OnActionStarted;

    //Singleton
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField]private Unit selectedUnit;
    

    [SerializeField] private LayerMask unitsLayerMask;

    private bool isBusy;
    private BaseAction selectedAction;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    private void Update()
    {
        if (isBusy)
        {
            return;
        }

        if (!TurnSystem.Instance.IsPlayerTurn())
        {
            return;
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (HandleUnitSelection()) 
        {
            return;
        }

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            //if (selectedAction.IsValidActionGridPosition(mouseGridPosition)) 
            //{
            //    if (selectedUnit.TrySpendActionPointsToTakeAction(selectedAction))
            //    {
            //        SetBusy();
            //        selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            //    }
            //}
            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition)) 
            {
                return;
            }
            
            if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction))
            {
                return;
            }
            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);
            OnActionStarted?.Invoke(this, EventArgs.Empty);

        }
    }

    private void SetBusy()
    {
        isBusy = true;

        //Busy UI icin olusturdugumuz eventi tetikliyoruz
        OnBusyChanged?.Invoke(this, isBusy);
    }

    private void ClearBusy()
    {
        isBusy = false;

        //
        OnBusyChanged?.Invoke(this, isBusy);

    }

    private bool HandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0)) 
        { 
            // Kameranin ortasindan manuel olarak farenin konumuna bir isin gonderiyoruz
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitsLayerMask))
            {
            // GetComponent yerine TryGetComponent kullandik burada true ya da false degeri donuyor ve eger true ise uniti selected unite atýyoruz
            // TryGetComponentte bir out degeri verebiliyoruz bu onemli
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {

                    if (unit==selectedUnit)
                    {
                        //bu unit zaten secilmis
                        return false;
                    }

                    if (unit.IsEnemy())
                    {
                        //Dusmani secemeyiz
                        return false;
                    }

                    SetSelectedUnit(unit);
                    return true;
                }
            }
        
        }
        return false;
    }


    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetAction<MoveAction>());

        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);

        /* IKISI AYNI SEYI IFADE EDIYOR
         
            if (OnSelectedUnitChange!=null)
            {
            OnSelectedUnitChange(this, EventArgs.Empty);
            }
        */
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;
        OnSelectedActionChange?.Invoke(this, EventArgs.Empty);
    }


    //Private degiskenimizi disariya public yapmadan iletiyoruz
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }


    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }

}
