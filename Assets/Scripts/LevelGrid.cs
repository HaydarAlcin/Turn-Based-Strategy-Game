using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }

    public event EventHandler OnAnyUnitMoveGridPosition;


    [SerializeField] private Transform gridDebugObjectPrefab; // Hata ayýklama nesnelerini oluþturmak için kullanýlacak prefab.
    private GridSystem<GridObject> gridSystem; // Oyun ýzgarasý ile etkileþim için kullanýlacak ýzgara sistemi nesnesi.

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem<GridObject>(10, 10, 2f,
            (GridSystem<GridObject> g,GridPosition gridPosition)=>new GridObject(g,gridPosition)); // 10x10 boyutunda bir ýzgara sistemi oluþturulur, hücre boyutu 2 birim.
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab); // Hata ayýklama nesneleri ýzgaraya yerleþtirilir.
    }

    // Belirli bir grid pozisyonuna bir birim (Unit) yerleþtirir.
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition); // Belirli bir hücrenin GridObject örneði alýnýr.
        gridObject.AddUnit(unit); // Bu hücreye bir birim yerleþtirilir.
    }

    // Belirli bir ýzgara pozisyonundaki birimi alýr ve döndürür.
    public List<Unit> GetUnitListAtGridPostition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition); // Belirli bir hücrenin GridObject örneði alýnýr.
        return gridObject.GetUnitList(); // Bu hücredeki birimi döndürür.
    }

    // Belirli bir ýzgara pozisyonundaki birimi kaldýrýr.
    public void RemoveUnitAtGridposition(GridPosition gridPosition,Unit unit)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition); // Belirli bir hücrenin GridObject örneði alýnýr.
        gridObject.RemoveUnit(unit); // Bu hücredeki birimi kaldýrýr.
    }


    //bir gridden diger gride giderken ki guncelleme islemi fonksiyonu
    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition,GridPosition toGridPosition)
    {
        RemoveUnitAtGridposition(fromGridPosition,unit);

        AddUnitAtGridPosition(toGridPosition, unit);

        OnAnyUnitMoveGridPosition?.Invoke(this, EventArgs.Empty);
    }

    // Grid pozisyonunu donduruyoruz
    public GridPosition GetGridPosition(Vector3 worldpos)=> gridSystem.GetGridPosition(worldpos); // parantezler acipta return ile dondurme islemi yapabilirdik ayni sey ikisi de 

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

    public int GetWidth() => gridSystem.GetWidth();
    public int GetHeight() => gridSystem.GetHeight();

    public bool HasAnyUnitGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);

        return gridObject.HasAnyUnit();
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);

        return gridObject.GetUnit();
    }

}