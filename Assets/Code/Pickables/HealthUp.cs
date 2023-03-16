using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : Pickable
{
    [SerializeField]float health;
    public override void PickUp() 
    {
        if (_player.Health.CurrentHealth == _player.Health.HealthDetails.MaximumHealth) return;
        
        _player.Health.AddHealth(health);
        Destroy(this.gameObject);
    }
}
