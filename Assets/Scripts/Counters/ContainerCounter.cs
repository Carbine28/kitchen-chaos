using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObjected; // Used for ContainerAnimator, to trigger open animation
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   
    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnPlayerGrabbedObjected?.Invoke(this, EventArgs.Empty);
        }
        

        // Custom logic for spawning onto container for player to pick up instead.
        //if (HasKitchenObject())
        //{
        //    Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        //    kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player); // Immediately gives to player
        //}
        //else
        //{
        //    Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        //    kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this); 
        //}
    }
}
