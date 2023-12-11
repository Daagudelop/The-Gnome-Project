using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gameController : MonoBehaviour
{
    public static gameController instance;
    //static stats
    private static int maxHealth = 6;
    private static float moveSpeed = 9f;
    private static float fireRate = 0.6f;
    //variable stats
    private static int health = 6;
    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }

    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health + "/" + maxHealth;
        }
    }

    public static void DamagePlayer(int damage)
    {
        health -= damage;

        if (Health <= 0)
        {
            KillPlayer();
        }
    }

    public static void HealPlayer(int healAmount)
    {
        health = Mathf.Min(MaxHealth, Health + healAmount);
    }
    public static void maxHealthChange(int maxHealth)
    {
        MaxHealth += maxHealth;
    }
    public static void MoveSpeedChange(float speed)
    {
        MoveSpeed += speed;
    }
    public static void FireRateChange(float rate)
    {
        FireRate -= rate;
    }
    private static void KillPlayer()
    {

    }
}
