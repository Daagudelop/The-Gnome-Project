using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletObjectPooling : MonoBehaviour
{
    public static BulletObjectPooling sharedInstanceOP;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 20;
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
            GameObject bullet = Instantiate(bulletPrefab);
            
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
