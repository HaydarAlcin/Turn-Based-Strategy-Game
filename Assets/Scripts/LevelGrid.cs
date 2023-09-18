using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }


    [SerializeField] private Transform gridDebugObjectPrefab; // Hata ay�klama nesnelerini olu�turmak i�in kullan�lacak prefab.
    private GridSystem gridSystem; // Oyun �zgaras� ile etkile�im i�in kullan�lacak �zgara sistemi nesnesi.

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem(10, 10, 2f); // 10x10 boyutunda bir �zgara sistemi olu�turulur, h�cre boyutu 2 birim.
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab); // Hata ay�klama nesneleri �zgaraya yerle�tirilir.
    }

    // Belirli bir grid pozisyonuna bir birim (Unit) yerle�tirir.
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition); // Belirli bir h�crenin GridObject �rne�i al�n�r.
        gridObject.AddUnit(unit); // Bu h�creye bir birim yerle�tirilir.
    }

    // Belirli bir �zgara pozisyonundaki birimi al�r ve d�nd�r�r.
    public List<Unit> GetUnitListAtGridPostition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition); // Belirli bir h�crenin GridObject �rne�i al�n�r.
        return gridObject.GetUnitList(); // Bu h�credeki birimi d�nd�r�r.
    }

    // Belirli bir �zgara pozisyonundaki birimi kald�r�r.
    public void RemoveUnitAtGridposition(GridPosition gridPosition,Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition); // Belirli bir h�crenin GridObject �rne�i al�n�r.
        gridObject.RemoveUnit(unit); // Bu h�credeki birimi kald�r�r.
    }


    //
    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition,GridPosition toGridPosition)
    {
        RemoveUnitAtGridposition(fromGridPosition,unit);

        AddUnitAtGridPosition(toGridPosition, unit);
    }

    // Grid pozisyonunu donduruyoruz
    public GridPosition GetGridPosition(Vector3 worldpos)=> gridSystem.GetGridPosition(worldpos); // parantezler acipta return ile dondurme islemi yapabilirdik ayni sey ikisi de 
}