using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private GridPosition gridPosition; // Bu GridObject'in ýzgara pozisyonu.
    private GridSystem gridSystem; // Bu GridObject'in baðlý olduðu ýzgara sistemi.
    
    //Bir gridin ustunden birden fazla unitler gectiginde hata olmasin diye List olarak tanimlamamizi yapiyoruz
    private List<Unit> unitList; // Bu GridObject üzerindeki birimi temsil eden deðiþken.

    // Constructor: GridObject nesnesi oluþturulurken çaðrýlýr ve gerekli baþlangýç deðerlerini alýr.
    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridPosition = gridPosition; // GridObject'in ýzgara pozisyonunu ayarlar.
        this.gridSystem = gridSystem; // Baðlý olduðu ýzgara sistemi ayarlanýr.

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
}