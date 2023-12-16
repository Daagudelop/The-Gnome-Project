using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum Enemies
{
    CuerpoACuerpo,
    Ranger,
    Explosive,
}
public class DrillerIA : MonoBehaviour
{
    [SerializeField] Weapon enemyWeapon;
    [SerializeField] int Health = 20;
    [SerializeField] int damage = 20;
    [SerializeField] int fireRate = 20;


    [SerializeField] Stats DrillerStats;

    [SerializeField] Transform target;

    [SerializeField] NavMeshAgent agent;

    private void Awake()
    {
        
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    private void OnEnable()
    {
        Health = 20;
        damage = 20;
    }

    private void Disable()
    {
        //ParticleSystem1.Stop();
        gameObject.SetActive(false);
    }
    private void Update()
    {
        agent.SetDestination(target.position);
    }

    private void OnParticleCollision(GameObject other)
    {
        Health--;
        Debug.Log(Health);
        if (Health<=0)
        {
            Debug.Log(Health);
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
