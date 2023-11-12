using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    public event EventHandler OnDoorOpened;

    [SerializeField] bool isOpen;

    private GridPosition gridPosition;

    private Animator animator;

    private Action onInteractionComplete;

    private bool isActive;
    private float timer;

    [SerializeField] private Transform hidleEffectCube;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.SetInteractableAtGridPosition(gridPosition, this);

        if (isOpen)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        timer-=Time.deltaTime;
        if (timer<=0f)
        {
            isActive = false;
            onInteractionComplete();
        }
    }
    public void Interact(Action onInteractionComplete)
    {
        
        this.onInteractionComplete = onInteractionComplete;
        isActive = true;
        timer = .6f;
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition,true);
        animator.SetBool("IsOpen", isOpen);
        OnDoorOpened?.Invoke(this, EventArgs.Empty);
        hidleEffectCube.gameObject.SetActive(false);
    }

    private void CloseDoor()
    {
        isOpen = false;
        Pathfinding.Instance.SetIsWalkableGridPosition(gridPosition, false);
        animator.SetBool("IsOpen", isOpen);
        hidleEffectCube.gameObject.SetActive(true);
    }
}
