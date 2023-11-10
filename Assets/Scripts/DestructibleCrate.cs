using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCrate : MonoBehaviour
{
    public static EventHandler OnAnyDestroy;

    [SerializeField] private Transform crateDestroyPrefab;

    private GridPosition gridPosition;

    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    }

    public void Damage()
    {
        Transform crateDestroyPrefabTransform = Instantiate(crateDestroyPrefab,transform.position,transform.rotation);
        ApplyExplosionToChildren(crateDestroyPrefabTransform, 150f, transform.position, 10f);
        OnAnyDestroy?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    private void ApplyExplosionToChildren(Transform root, float explosionForce, Vector3 explosionPos, float explosionRange)
    {
        foreach (Transform child in root)
        {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(explosionForce, explosionPos, explosionRange);
            }
            ApplyExplosionToChildren(child, explosionForce, explosionPos, explosionRange);
        }
    }


    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
}
