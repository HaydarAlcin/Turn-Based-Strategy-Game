using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrenadeProjectile : MonoBehaviour
{
    public static EventHandler OnAnyGrenadeExplosed;

    private Action onGrenadeBehaviourComplete;

    [SerializeField] private Transform grenadeExplodeVFXPrefab;
    [SerializeField] private AnimationCurve arcYAnimationCurve;

    private Vector3 targetPosition;
    private Vector3 positionXZ;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float reachedTargetDistance;
    [SerializeField] private float totalDistance;


    

    private void Update()
    {
        Vector3 moveDir=(targetPosition - positionXZ).normalized;
        positionXZ += moveDir * moveSpeed * Time.deltaTime;

        float distance=Vector3.Distance(positionXZ, targetPosition);
        float distanceNormalized= 1-distance/totalDistance;

        float maxHeight = totalDistance / 3f;
        float positionY = arcYAnimationCurve.Evaluate(distanceNormalized) * maxHeight;
        transform.position = new Vector3(positionXZ.x, positionY, positionXZ.z);
        if (Vector3.Distance(positionXZ,targetPosition)<reachedTargetDistance)
        {
            float damageRadius = 4f;
            Collider[] colliderArray = Physics.OverlapSphere(targetPosition, damageRadius);

            foreach (Collider collider in colliderArray)
            {
                if(collider.TryGetComponent<Unit>(out Unit targetUnit))
                {
                    targetUnit.Damage(30);
                }
            }
            OnAnyGrenadeExplosed?.Invoke(this, EventArgs.Empty);
            Instantiate(grenadeExplodeVFXPrefab, targetPosition+Vector3.up*1f, Quaternion.identity);
            Destroy(gameObject);
            onGrenadeBehaviourComplete();
        }
    }

    public void Setup(GridPosition targetGridPosition, Action onGrenadeBehaviourComplete)
    {
        this.onGrenadeBehaviourComplete = onGrenadeBehaviourComplete;
        targetPosition=LevelGrid.Instance.GetWorldPosition(targetGridPosition);

        positionXZ = transform.position;
        positionXZ.y=0;
        totalDistance =Vector3.Distance(positionXZ, targetPosition);
    }
}
