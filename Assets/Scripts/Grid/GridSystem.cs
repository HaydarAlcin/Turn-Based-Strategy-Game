using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    //Hangi grid olduklarýný tuttugumuz degiskenler Ornek (0,0),(0,1) ...
    private int width;
    private int height;

    //Gridlerin boyutu yani genisligi
    private float cellSize;

    //Gridleri saklamak icin dizimiz
    private GridObject[,] gridObjectsArray;

    public GridSystem(int width, int height, float cellSize)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridObjectsArray=new GridObject[width,height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x,z);

                gridObjectsArray[x, z] = new GridObject(this, gridPosition);
            }
        }

        
    }

    
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x,0,gridPosition.z) * cellSize;
    }


    //Struct ile dunya degerlerine bakip grid pozisyonumuza ulasiyoruz
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
            );
    }


    //Gridleri olusturup adýna olusturdugumuz fonksiyon
    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition=new GridPosition(x,z);

                //Monobehavior dan miras alamadigimizdan Instantiate methodu bu sekil kullanilir
                Transform debugTransform= GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);

                GridDebugObject gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition)
    {
        return gridObjectsArray[gridPosition.x,gridPosition.z];
    }


    //Bu Grid gecerli bir grid mi alanin disinda mi
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return gridPosition.x>=0 && 
                gridPosition.z>=0 && 
                gridPosition.x<width && 
                gridPosition.z<height;
    }


    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }
}
