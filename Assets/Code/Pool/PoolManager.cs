using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : SingletonMonoBehaviour<PoolManager>
{
    #region Tooltip
    [Tooltip("Populate this array with  Pool  variables")]
    #endregion
    [SerializeField] private Pool[] _poolArray = null;

    private Transform _poolManagerTransform;
    private Dictionary<int, Queue<Component>> _poolDictionary = new Dictionary<int, Queue<Component>>();



    [System.Serializable]
    public struct Pool
    {
        public int poolSize;
        public GameObject prefab;
        public string componentType;
    }
    protected override  void Awake()
    {
        base.Awake();
        _poolManagerTransform = this.gameObject.transform;
    }

    private void Start()
    {
        if (_poolArray.Length>0)
        for (int i = 0; i < _poolArray.Length; i++)
        {
            CreatePool(_poolArray[i].prefab, _poolArray[i].poolSize, _poolArray[i].componentType);
        }
    }

    private void CreatePool(GameObject prefab, int poolSize, string componentType)
    {
        int poolKey = prefab.GetInstanceID();
        print(poolKey);
        string prefabName = prefab.name;
        GameObject parentGameObject = new GameObject(prefabName + "Anchor");

        parentGameObject.transform.SetParent(_poolManagerTransform);

        if (!_poolDictionary.ContainsKey(poolKey))
        {
            _poolDictionary.Add(poolKey, new Queue<Component>());

            for (int i = 0; i < poolSize; i++)
            {
                GameObject newObject = Instantiate(prefab, parentGameObject.transform) ;

                newObject.SetActive(false);

                _poolDictionary[poolKey].Enqueue(newObject.GetComponent(Type.GetType(componentType)));
            }
        }
    }

    public Component ReuseComponent(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        int poolKey = prefab.GetInstanceID();

        if (_poolDictionary.ContainsKey(poolKey))
        {
            Component componentToReuse = GetComponentFromPool(poolKey);

            ResetObject(position, rotation, componentToReuse, prefab);

            return componentToReuse;
        }
        else
        {
            Debug.Log("No objects pool for" + prefab);
            return null;
        }
    }

   
    private Component GetComponentFromPool(int poolKey)
    {
        Component componentToReuse = _poolDictionary[poolKey].Dequeue();
        _poolDictionary[poolKey].Enqueue(componentToReuse);

        if (componentToReuse.gameObject.activeSelf==true)
        {
            componentToReuse.gameObject.SetActive(false);
        }

        return componentToReuse;
    }

    private void ResetObject(Vector3 position, Quaternion rotation, Component componentToReuse, GameObject prefab)
    {
        componentToReuse.transform.position = position;
        componentToReuse.transform.rotation = rotation;
      //  componentToReuse.gameObject.transform.localScale = prefab.transform.localScale;
    }
 

}
