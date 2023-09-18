using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();        
    }

    private void Start()
    {
        //Evente Subscribe oluyoruz
        UnitActionSystem.Instance.OnSelectedUnitChange += UnityActionSystem_OnSelectedUnitChanged;
        UpddateVisual();
    }

    private void UnityActionSystem_OnSelectedUnitChanged(object sender,EventArgs empty)
    {
        UpddateVisual();
    }

    private void UpddateVisual()
    {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
