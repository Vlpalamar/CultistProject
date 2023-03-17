using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : Arthefact
{

    [SerializeField] private float _regularHealthRestor=10f;
    [SerializeField] private float _healthTickRate = 5f;
    private float _timer;
    public override void DropTheArthefact()
    {
        print("Drop");
    }

    public override void TakeTheArthefact()
    {
        print("Take");
        _player = GameManager.Instance.GetPlayer();
        _timer = Time.time;
    }

    public override void UseAlways()
    {
        if (_timer+_healthTickRate<Time.time)
        {
            print("heal");
            _timer = Time.time;
            _player.Health.AddHealth(_regularHealthRestor);
        }
    }
}
