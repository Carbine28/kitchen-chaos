using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerAnimator : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObjected += ContainerCounter_OnPlayerGrabbedObjected;
    }

    private void ContainerCounter_OnPlayerGrabbedObjected(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
