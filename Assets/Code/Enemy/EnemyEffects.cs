using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyEffects : MonoBehaviour
{
    #region
    [Space(5)]
    [Header("Effects")]
    #endregion
    [SerializeField] private GameObject bloodEffect;

    Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }
    public void PlayOnGetDamage()
    {
        Instantiate(bloodEffect, _enemy.transform.position, bloodEffect.transform.rotation);

        //ParticleSystem gameObject =  (ParticleSystem)PoolManager.Instance.ReuseComponent(bloodEffect, Vector3.zero, Quaternion.identity);
        //gameObject.Play();
    }


}

