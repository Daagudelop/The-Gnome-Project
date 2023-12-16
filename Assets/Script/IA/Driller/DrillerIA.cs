//using System.Collections;
//using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

public class DrillerIA : MonoBehaviour
{
    [SerializeField] Weapon PlayerWeapon;
    [SerializeField] Weapon itsWeapon;
    [SerializeField] int Health = 20;
    [SerializeField] int damage = 2;
    [SerializeField] int fireRate = 30;
    [SerializeField] int DamageReceive;

    [SerializeField] bool isRanger;
    [SerializeField] bool isCC;

    [SerializeField]GameObject thisEnemy;

    [SerializeField] Stats DrillerStats;

    [SerializeField] Transform target;

    [SerializeField] NavMeshAgent agent;

    private void Awake()
    {
        thisEnemy = GetComponent<GameObject>();

        /*if(isRanger)
        {
            agent.stoppingDistance = 6;
        }
        else if(isCC) 
        {
            agent.stoppingDistance = 2;
            itsWeapon.Melee();
        }*/
    }

    
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        itsWeapon.Melee();
    }
    private void OnEnable()
    {
        //Health = 20;
        //damage = 20;
    }



    private void Disable()
    {
        //ParticleSystem1.Stop();
        gameObject.SetActive(false);
    }
    private void Update()
    {

        agent.SetDestination(target.position);
        //Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance > 13)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }
        if (isRanger)
            {
            
            }
            else if (isCC)
            {
                if (agent.remainingDistance <= 4)
                {
                    //UnityEngine.Debug.Log("punch");
                    //itsWeapon.Melee();

                }
            }
    }

    private void OnParticleCollision(GameObject other)
    {
        Health -= PlayerWeapon.damage;
        //Debug.Log(Health);
        if (Health<=0)
        {
            //Debug.Log(Health);
            Disable();
        }
    }

    /*private void setCurrentWeapon(Enemies newWeapon)
    {
        if (newWeapon == Enemies.CuerpoACuerpo)
        {

        }
        else if (newWeapon == Enemies.Ranger)
        {

        }
        else if (newWeapon == Enemies.Explosive)
        {

        }


        //Crea las instancias.
        this.currentWeapon = newWeapon;
    }*/

}
