using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyObjectPoolingEnemy1 : MonoBehaviour
{
    public static EnemyObjectPoolingEnemy1 sharedInstanceOP;

    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private int poolSize = 50;
    [SerializeField] private List<GameObject> bulletList;
    private void Awake()
    {
        if (sharedInstanceOP == null)
        {
            sharedInstanceOP = this;
        }
    }

    private void Start()
    {
        AddBulletToPool(poolSize);
    }

    private void AddBulletToPool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(EnemyPrefab);
            
            bullet.SetActive(false);
            bulletList.Add(bullet);
        }
    }

    public GameObject RequestBullet()
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)
            {
                bulletList[i].SetActive(true);
                return bulletList[i];
            }
        }
        AddBulletToPool(1);
        bulletList[bulletList.Count - 1].SetActive(true);
        return bulletList[bulletList.Count - 1];
    }
}
