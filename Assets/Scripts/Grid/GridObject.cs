using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    private GridPosition gridPosition; // Bu GridObject'in �zgara pozisyonu.
    private GridSystem gridSystem; // Bu GridObject'in ba�l� oldu�u �zgara sistemi.
    
    //Bir gridin ustunden birden fazla unitler gectiginde hata olmasin diye List olarak tanimlamamizi yapiyoruz
    private List<Unit> unitList; // Bu GridObject �zerindeki birimi temsil eden de�i�ken.

    // Constructor: GridObject nesnesi olu�turulurken �a�r�l�r ve gerekli ba�lang�� de�erlerini al�r.
    public GridObject(GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridPosition = gridPosition; // GridObject'in �zgara pozisyonunu ayarlar.
        this.gridSystem = gridSystem; // Ba�l� oldu�u �zgara sistemi ayarlan�r.

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
}