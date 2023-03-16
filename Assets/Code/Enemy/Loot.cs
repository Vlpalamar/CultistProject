using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]

[DisallowMultipleComponent]
public class Loot : MonoBehaviour
{
    [SerializeField] private List<Pickable> commonLootitems;
    [SerializeField] private int persetnsToGetCommonLoot=25;
    public void DropLoot()
    {
        if (Random.Range(0,100)< persetnsToGetCommonLoot)
        {
            print("Loot");
            int i = Random.Range(0, commonLootitems.Count);
            print(i);
           
            Pickable newPicable=  (Pickable)PoolManager.Instance.ReuseComponent(commonLootitems[i].Prefab, Vector3.zero, Quaternion.identity);
            newPicable.gameObject.SetActive(true);
            newPicable.transform.position = transform.position;
        }
    }
}
