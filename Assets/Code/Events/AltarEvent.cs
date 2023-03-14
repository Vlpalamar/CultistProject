
using System.Collections.Generic;
using UnityEngine;

public class AltarEvent : EventArea
{
    
    List<Enemy> enemies = new List<Enemy>();

    public List<Enemy> Enemies { get => enemies;  }

    protected virtual void Start()
    {
        Initialise();

        EventManager.Instance.events.Add(this);
      
        
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
