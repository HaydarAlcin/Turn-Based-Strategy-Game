using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridPosition gridPosition; // Bu GridObject'in izgara pozisyonu.
    private GridSystem<GridObject> gridSystem; // Bu GridObject'in bagl� oldugu izgara sistemi.
    
    //Bir gridin ustunden birden fazla unitler gectiginde hata olmasin diye List olarak tanimlamamizi yapiyoruz
    private List<Unit> unitList; // Bu GridObject �zerindeki birimi temsil eden de�i�ken.

    private IInteractable interactable;

    // Constructor: GridObject nesnesi olu�turulurken �agr�l�r ve gerekli baslang�c degerlerini alir.
    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridPosition = gridPosition; // GridObject'in izgara pozisyonunu ayarlar.
        this.gridSystem = gridSystem; // Bagl� oldugu izgara sistemi ayarlanir.

        unitList = new List<Unit>();
    }

    // Bu GridObject'in dize temsilini olu�turur. Pozisyonu ve �zerindeki birimi i�erir.
    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n";
        }

        return gridPosition.ToString() + "\n" + unitString;
    }

    // Bu GridObject �zerine bir birim yerle�tirir.
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
        //this.unit = unit; // Bu GridObject �zerindeki birimi ayarlar.
    }

    public void RemoveUnit(Unit unit) 
    {
        unitList.Remove(unit);
    }

    // Bu GridObject �zerindeki birimi d�nd�r�r.
    public List<Unit> GetUnitList()
    {
        return unitList; // Bu GridObject �zerindeki birimi d�nd�r�r.
    }

    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }

    public Unit GetUnit()
    {
        if (HasAnyUnit())
        {
            return unitList[0];
        }
        else
        {
            return null;
        }

    }


    public IInteractable GetInteractable()
    {
        return interactable;
    }

    public void SetInteractable(IInteractable interactable)
    {
        this.interactable = interactable;
    }
}