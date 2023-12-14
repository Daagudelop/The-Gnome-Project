using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Config", menuName = "Guns/Shoot Config", order = 2)]
public class ShootConfigScriptableObject : ScriptableObject
{
    public LayerMask HitMask;
    public float FireRate = 0.25f;
    public Vector3 Spread = new Vector3 (0.1f, 0.1f, 0);
}
