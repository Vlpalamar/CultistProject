
using System.Collections.Generic;
using UnityEngine;

public class AltarEvent : EventArea
{
    
    List<Enemy> enemies = new List<Enemy>();

    [SerializeField] Transform rewardTransform;
    [SerializeField] GameObject rewardGameObj;
    [SerializeField] Arthefact arthefact;


    public List<Enemy> Enemies { get => enemies;  }

  

    protected virtual void Start()
    {
        Initialise();

        EventManager.Instance.events.Add(this);
      
        
    }

    public override void CompleteEvent()
    {

       
        WeaponLoot newWeaponLoot = rewardGameObj.GetComponent<WeaponLoot>();
      
        newWeaponLoot.ArthefactOnPedestal = arthefact;
        Instantiate(newWeaponLoot.gameObject, rewardTransform);
        newWeaponLoot.Init(null, arthefact);
       
    }

    private void Initialise()
    {
        this.EventName= "AltarEvent";


        foreach (Transform point in EnemySpawnPoints)
        {
            int enemyIndex = Random.Range(0, enemyPool.Count-1);
          
            
            Enemy enemy = (Enemy)PoolManager.Instance.ReuseComponent(enemyPool[enemyIndex].EnemyPrefab, point.position, Quaternion.identity);
            enemy.gameObject.SetActive(true);
            Enemies.Add(enemy);
            Debug.LogWarning("ENemies: " + enemies.Count);
        }
        
    }

    
}
