using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]

[DisallowMultipleComponent]
public class CurrentArthefact : MonoBehaviour
{
    Arthefact arthefact;

    public void ChangeArthefact(Arthefact arthefact)
    {
        if (arthefact!=null)
            arthefact.DropTheArthefact();

         this.arthefact = arthefact;
        arthefact.TakeTheArthefact();

    }

    private void Update()
    {
        if (arthefact == null) return;

        if (!arthefact.IsAlways) return;

        arthefact.UseAlways();
        
    }

}
