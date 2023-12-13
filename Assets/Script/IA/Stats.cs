using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stats : MonoBehaviour
{
    public int MaxHealt;
    public int CurrentHealth;
    public int attackRange;
    public float MoveSpeed;
    public float FireRate;
    public bool GunLoaded;
    public Vector3 MoveDirection;
    public Vector2 FacingDirection;
     
    public Transform Aim;
    public Transform Target;
    //public Transform BulletPrefab;
    //public Rigidbody2D RigidBody;
    //public Animator Animator;
    //public SpriteRenderer SpriteRenderer;
    //public NavMeshAgent Agent;
}
