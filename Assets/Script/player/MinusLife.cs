using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusLife : MonoBehaviour
{
    [SerializeField] Weapon Player;
    private void OnParticleCollision(GameObject other)
    {
        playerStats.Health -= 1;
    }
}
