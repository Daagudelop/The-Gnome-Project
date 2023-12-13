using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class collectables : MonoBehaviour
{
    public Item item;
    //player collectable variables
    public int addToCurrentHealth;

    //PowerUp Boolean Identifier
    public bool healBool; // New
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
            // check for HealBool
            if (healBool)
            {
                playerStats.HealPlayer(addToCurrentHealth);
            }
            Destroy(gameObject);
        }
    }
}
