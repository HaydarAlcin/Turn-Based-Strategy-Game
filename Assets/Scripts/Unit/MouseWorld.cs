using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;

    // Layer ekleyerek plane nesnelerimizde bu hareketi sglamis oluyoruz
    [SerializeField] private LayerMask mouseLayerMask;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Konumunu fareye esitliyoruz
        transform.position = MouseWorld.GetPosition();
    }

    public static Vector3 GetPosition()
    {
        // Kameranin ortasindan manuel olarak farenin konumuna bir isin gonderiyoruz
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition());
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mouseLayerMask);
        
        return raycastHit.point;
    }
}
