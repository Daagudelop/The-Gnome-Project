using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //[SerializeField] float moveSpeed = 5;
    int Damage;
    ParticleSystem ParticleSystem1;

    [SerializeField] float timeTillDespawn = 2;

    private void Awake()
    {
        ParticleSystem1 = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        ParticleSystem1.Play();
        StartCoroutine(DisableBullet());
    }

    private void Disable()
    {
        //ParticleSystem1.Stop();
        gameObject.SetActive(false);
    }

    IEnumerator DisableBullet()
    {
        yield return new WaitForSeconds(timeTillDespawn);

        Disable();
    }

}
