using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    //Event
    public event EventHandler OnSelectedUnitChange;

    //Singleton
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField]private Unit selectedUnit;

    [SerializeField] private LayerMask unitsLayerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (HandleUnitSelection()) return;
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }


    private bool HandleUnitSelection()
    {
        // Kameranin ortasindan manuel olarak farenin konumuna bir isin gonderiyoruz
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitsLayerMask))
        {
            // GetComponent yerine TryGetComponent kullandik burada true ya da false degeri donuyor ve eger true ise uniti selected unite atýyoruz
            // TryGetComponentte bir out degeri verebiliyoruz bu onemli
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }


    public void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        
        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);

        /* IKISI AYNI SEYI IFADE EDIYOR
         
            if (OnSelectedUnitChange!=null)
            {
            OnSelectedUnitChange(this, EventArgs.Empty);
            }
        */
    }


    //Private degiskenimizi disariya public yapmadan iletiyoruz
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

}
