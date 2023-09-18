using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }


    [SerializeField] private Transform gridDebugObjectPrefab; // Hata ayýklama nesnelerini oluþturmak için kullanýlacak prefab.
    private GridSystem gridSystem; // Oyun ýzgarasý ile etkileþim için kullanýlacak ýzgara sistemi nesnesi.

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        gridSystem = new GridSystem(10, 10, 2f); // 10x10 boyutunda bir ýzgara sistemi oluþturulur, hücre boyutu 2 birim.
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


    //
    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition,GridPosition toGridPosition)
    {
        RemoveUnitAtGridposition(fromGridPosition,unit);

        AddUnitAtGridPosition(toGridPosition, unit);
    }

    // Grid pozisyonunu donduruyoruz
    public GridPosition GetGridPosition(Vector3 worldpos)=> gridSystem.GetGridPosition(worldpos); // parantezler acipta return ile dondurme islemi yapabilirdik ayni sey ikisi de 
}