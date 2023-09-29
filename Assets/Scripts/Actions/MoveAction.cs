using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] private int maxMoveDistance = 4;

    private Vector3 targetPosition;
    private Unit unit;

    private void Awake()
    {
        unit = GetComponent<Unit>();  
        targetPosition = transform.position;
    }


    private void Update()
    {
        float stoppingDistance = 0.1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;

            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            //Uniti hedef noktaya dogru dondurme
            float rotationSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
            //transform.forward = moveDirection;

            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }


        
    }

    public void Move(GridPosition gridPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }


    //Gecerli olan tum gridleri tutuyoruz
    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList= new List<GridPosition>();
        GridPosition gridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = gridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    //bir sonraki for dongusune girer alt satirlari atlar
                    continue;
                }

                if (gridPosition==testGridPosition)
                {
                    //Eger testgrid ustunde durdugumuz grid ise onun zaten uzerinde oldugumuz icin hesaplamaya gerek yok
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitGridPosition(testGridPosition))
                {
                    //herhangi bir unit grid uzerindeyse yine onu almiyoruz
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
