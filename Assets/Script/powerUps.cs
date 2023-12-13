using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string description;
    public Sprite itemImage;
}
public class powerUps : MonoBehaviour
{
    public Item item;
    //player powerup variables
    public int addToMaxHealth, addToMaxDashes, addToFireRate, addToMaxAmmo, addToWeaponDamage; // New
    public float addToMoveSpeed;  // New

    //PowerUp Boolean Identifier
    public bool maxHealthBool, moveSpeedChangeBool, maxDashesBool, fireRateChangeBool, maxAmmoBool, damageWeaponChangeBool; // New
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemImage;
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true; // Makes the trigger work as a trigger, XD.
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // New
            // check for maxHealthBool, moveSpeedChangeBool, maxDashesBool, fireRateChangeBool, maxAmmoBool, damageWeaponChangeBool
            if (maxHealthBool)
            {
                playerStats.MaxHealthChange(addToMaxHealth);
            }
            if (moveSpeedChangeBool)
            {
                playerStats.MoveSpeedChange(addToMoveSpeed);
            }
            if (maxDashesBool)
            {
                playerStats.MaxDashesChange(addToMaxDashes);
            }
            if (fireRateChangeBool)
            {
                playerStats.FireRateChange(addToFireRate);
            }
            if (maxAmmoBool)
            {
                playerStats.MaxAmmoChange(addToMaxAmmo);
            }
            if (damageWeaponChangeBool)
            {
                playerStats.DamageWeaponChange(addToWeaponDamage);
            }
            Destroy(gameObject);
        }
    }
}
