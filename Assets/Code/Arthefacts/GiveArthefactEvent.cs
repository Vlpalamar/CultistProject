using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveArthefactEvent : MonoBehaviour
{
    public event Action<GiveArthefactEvent, GiveArthefactEventArgs> OnGiveArthefact;

    public void CallGiveArthefactEvent(Arthefact arthefact)
    {
        OnGiveArthefact?.Invoke(this, new GiveArthefactEventArgs() { Arthefact = arthefact });
    }
}
public class GiveArthefactEventArgs : EventArgs
{
    private Arthefact arthefact;

    public Arthefact Arthefact { get => arthefact; set => arthefact = value; }
}
