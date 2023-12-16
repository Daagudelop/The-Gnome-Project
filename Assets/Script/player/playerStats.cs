using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerStats : MonoBehaviour
{
    public static playerStats instance;
    //static stats
    private static int maxDashes = 2; // New
    private static int maxHealth = 6;
    private static float moveSpeed = 10f;
    private static int fireRateAdd = 0; // New
    private static int damageWeaponAdd = 0; // New
    //variable stats
    private static int maxAmmoAdd = 0;
    private static int currDashes = maxDashes; // New
    [SerializeField]private static int health = 6;

    // Getters / setters
    public static int CurrDashes { get => currDashes; set => currDashes = value; } // New
    public static int MaxDashes { get => maxDashes; set => maxDashes = value; } // New
    public static int DamageWeaponAdd { get => damageWeaponAdd; set => damageWeaponAdd = value; } // New
    public static int MaxAmmoAdd { get => maxAmmoAdd; set => maxAmmoAdd = value; } // New
    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static int FireRateAdd { get => fireRateAdd; set => fireRateAdd = value; } // New
    
    public TextMeshProUGUI healthText;
    private static GameObject _gameOverPanel;

    // Start is called before the first frame update
    private void Awake()
    {
        var _dontDestroyBetweenScenesPause = FindObjectsOfType<PauseMenu>();

        if (_dontDestroyBetweenScenesPause.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        if (instance == null)
        {
            instance = this;
        }
        _gameOverPanel = GameObject.FindGameObjectWithTag("GameOver").GetComponent<GameOver>()._gameOverScreen;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + health + "/" + maxHealth;
        }
    }

    // Health related Methods
    public static void DamagePlayer(int damage)
    {
        health -= damage;

        if (Health <= 0)
        {
            Time.timeScale = 0;
            _gameOverPanel.SetActive(true);
            KillPlayer();
        }
    }
    private static void KillPlayer()
    {

    }
    public static void HealPlayer(int healAmount)    // Collectable
    {
        Debug.Log("Health: "+health);
        health = Mathf.Min(MaxHealth, Health + healAmount);
        Debug.Log("Health: " +health);
    }
    public static void MaxHealthChange(int maxHealthAdd)     // PowerUp
    {
        Debug.Log("Max Health: " + maxHealth);
        maxHealth += maxHealthAdd;
        Debug.Log("Max Health: " + maxHealth);
    }

    // Movement related Methods
    public static void MoveSpeedChange(float speedAdd)      // PowerUp
    {
        Debug.Log("Move speed: " + moveSpeed);
        moveSpeed += speedAdd;
        Debug.Log("Move speed: " + moveSpeed);
    }
    public static void MaxDashesChange(int dashesAdd)       // PowerUp
    {
        Debug.Log("Max Dashes: " + maxDashes);
        maxDashes += dashesAdd;
        Debug.Log("Max Dashes: " + maxDashes);
    }

    // Shooting related Methods
    public static void FireRateChange(int rateAdd)          // PowerUp
    {
        Debug.Log("FireRateAdd: " + fireRateAdd);
        fireRateAdd += rateAdd;
        Debug.Log("FireRateAdd: " + fireRateAdd);
    }
    public static void MaxAmmoChange(int ammoToAdd)         // PowerUp
    {
        Debug.Log("Max Ammo Add: " + maxAmmoAdd);
        maxAmmoAdd += ammoToAdd;
        Debug.Log("Max Ammo Add: " + maxAmmoAdd);
    }
    public static void DamageWeaponChange(int ammoToAdd)       // PowerUp
    {
        Debug.Log("Damage Weapon Add: " + damageWeaponAdd);
        damageWeaponAdd += ammoToAdd;
        Debug.Log("Damage Weapon Add: " + damageWeaponAdd);
    }
}
