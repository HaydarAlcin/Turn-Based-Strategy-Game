using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootAction : BaseAction
{

    public event EventHandler<OnShootEventArgs> OnShoot;
    public static event EventHandler<OnShootEventArgs> OnAnyShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Unit targetUnit;
        public Unit shootingUnit;
    }

    //States
    private enum State
    {
        Aiming,
        Shooting,
        Cooloff
    }
    private State state;

    private int maxShootDistance = 4;
    private float stateTimer;

    //tek bir mermi atma
    private bool canShootBullet;

    private Unit targetUnit;

    [SerializeField] private LayerMask obstaclesLayerMask;

    private void Start()
    {
        if (unit.IsEnemy())
        {
            maxShootDistance = 7;
        }
    }

    private void Update()
    {
        if (!isActive) return;

        stateTimer-=Time.deltaTime;

        switch (state)
        {
            case State.Aiming:
                Vector3 aimDir = (targetUnit.GetWorldPosition() - unit.GetWorldPosition()).normalized;
                float rotationSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * rotationSpeed);
                break;
            case State.Shooting:
                if (canShootBullet)
                {
                    Shoot();
                    canShootBullet = false;
                }
                break;
            case State.Cooloff:
                break;
        }

        if (stateTimer <= 0)
        {
            NextState();
        }
    }

    public void NextState()
    {
        switch (state)
        {
            case State.Aiming:
                if (stateTimer <= 0)
                {
                    float shootingStateTime = 0.1f;
                    state = State.Shooting;
                    stateTimer = shootingStateTime;
                }
                break;
            case State.Shooting:
                if (stateTimer <= 0)
                {
                    float cooloffStateTime = 0.5f;
                    state = State.Cooloff;
                    stateTimer = cooloffStateTime;
                }
                break;
            case State.Cooloff:
                ActionComplete();
                break;
        }
    }



    private void Shoot()
    {
        OnAnyShoot?.Invoke(this, new OnShootEventArgs
        {
            targetUnit = targetUnit,
            shootingUnit = unit
        });

        OnShoot?.Invoke(this, new OnShootEventArgs
        {
            targetUnit = targetUnit,
            shootingUnit = unit
        });
        targetUnit.Damage(40);
        
    }

    public override string GetActionName()
    {
        return "Shoot";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition=unit.GetGridPosition();
        return GetValidActionGridPositionList(unitGridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList(GridPosition unitGridPosition)
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        GridPosition gridPosition = unit.GetGridPosition();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++)
        {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = gridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    //bir sonraki for dongusune girer alt satirlari atlar
                    continue;
                }


                int testDistance=Mathf.Abs(x)+Mathf.Abs(z);
                if (testDistance>maxShootDistance)
                {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitGridPosition(testGridPosition))
                {
                    //herhangi bir unit grid uzerinde degil
                    continue;
                }

                Unit targetUnit= LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (targetUnit.IsEnemy()== unit.IsEnemy())
                {
                    //aksiyona giren unit ile target unit ayn� takimda
                    continue;
                }

                Vector3 unitWorldPosition = LevelGrid.Instance.GetWorldPosition(unitGridPosition);
                Vector3 shootDirection= (targetUnit.GetWorldPosition()- unitWorldPosition).normalized;
                float unitShoulderHeight = 1.7f;
                if (Physics.Raycast(
                    unitWorldPosition + Vector3.up*unitShoulderHeight,shootDirection,
                    Vector3.Distance(unitWorldPosition,targetUnit.GetWorldPosition()),
                    obstaclesLayerMask))
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
    {
        
        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);

        state = State.Aiming;
        float aimingStateTime = 1f;
        stateTimer = aimingStateTime;
        canShootBullet = true;
        ActionStart(onActionComplete);

    }


    public Unit GetTargetUnit()
    {
        return targetUnit;
    }

    public int GetMaxShootDistance()
    {
        return maxShootDistance;
    }

    public override EnemyAIAction GetEnemyAIAction(GridPosition gridPosition)
    {

        Unit targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        return new EnemyAIAction
        {
            gridPosition = gridPosition,
            actionValue = 100+ Mathf.RoundToInt(1- targetUnit.GetHealthNormalized())*100,
        };
    }

    public int GetTargetCountAtPosition(GridPosition gridPosition)
    {
        return GetValidActionGridPositionList(gridPosition).Count;
    }
}
