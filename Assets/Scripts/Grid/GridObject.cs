using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridPosition gridPosition; // Bu GridObject'in izgara pozisyonu.
    private GridSystem<GridObject> gridSystem; // Bu GridObject'in baglý oldugu izgara sistemi.
    
    //Bir gridin ustunden birden fazla unitler gectiginde hata olmasin diye List olarak tanimlamamizi yapiyoruz
    private List<Unit> unitList; // Bu GridObject üzerindeki birimi temsil eden deðiþken.

    private IInteractable interactable;

    // Constructor: GridObject nesnesi oluþturulurken çagrýlýr ve gerekli baslangýc degerlerini alir.
    public GridObject(GridSystem<GridObject> gridSystem, GridPosition gridPosition)
    {
        this.gridPosition = gridPosition; // GridObject'in izgara pozisyonunu ayarlar.
        this.gridSystem = gridSystem; // Baglý oldugu izgara sistemi ayarlanir.

        unitList = new List<Unit>();
    }

    // Bu GridObject'in dize temsilini oluþturur. Pozisyonu ve üzerindeki birimi içerir.
    public override string ToString()
    {
        string unitString = "";
        foreach (Unit unit in unitList)
        {
            unitString += unit + "\n";
        }

        return gridPosition.ToString() + "\n" + unitString;
    }

    // Bu GridObject üzerine bir birim yerleþtirir.
    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
        //this.unit = unit; // Bu GridObject üzerindeki birimi ayarlar.
    }

    public void RemoveUnit(Unit unit) 
    {
        unitList.Remove(unit);
    }

    // Bu GridObject üzerindeki birimi döndürür.
    public List<Unit> GetUnitList()
    {
        return unitList; // Bu GridObject üzerindeki birimi döndürür.
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