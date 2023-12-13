using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IEntity
{
    int MaxHealt { get; set; }
    int CurrentHealth { get; set; }
    int attackRange { get; set; }
    float MoveSpeed { get; set; }
    float FireRate { get; set; }
    bool GunLoaded { get; set; }
    Vector3 MoveDirection { get; set; }
    Vector2 FacingDirection { get; set; }

    Transform Aim { get; set; }
    Transform Target { get; set; }
    Transform BulletPrefab { get; set; }
    Rigidbody2D RigidBody { get; set; }
    Animator Animator { get; set; }
    SpriteRenderer SpriteRenderer { get; set; }
    NavMeshAgent Agent { get; set; }
    void ToAttack(Transform aim);
}