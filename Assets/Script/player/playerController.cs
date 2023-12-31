using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class playerController : MonoBehaviour
{
    private float speed;
    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private float horVel;
    private float vertVel;
    [SerializeField] Animator playerMovement;
    [SerializeField] GameObject Aim;
    [SerializeField] bool isPlayer;
    [SerializeField] Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //playerMovement = GetComponent<Animator>();
        //Aim = GameObject.FindGameObjectWithTag("aim");
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }
    void movement()
    {
        if (isPlayer)
        {
            speed = playerStats.MoveSpeed;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            horVel = horizontal * speed;
            vertVel = vertical * speed;
            rigidbody.velocity = new Vector3(horVel, vertVel, 0);

            float aimVar = GetComponent<Weapon>().rotZ;
            if (aimVar >= -45f && aimVar < 45f)
            {
                if (horVel != 0 || vertVel != 0)
                {
                    playerMovement.Play("player_walk_right");
                }
                else {
                    playerMovement.Play("player_right_idle");
                        }
            }
            else if (aimVar >= 45f && aimVar < 135f)
            {
                if (horVel != 0 || vertVel != 0)
                {
                    playerMovement.Play("player_walk_up");
                }
                else
                {
                    playerMovement.Play("player_up_idle");
                }
            }
            else if ((aimVar >= 135f && aimVar < 180f) || (aimVar >= -180f && aimVar < -135f))
            {
                if (horVel != 0 || vertVel != 0)
                {
                    playerMovement.Play("player_walk_left");
                }
                else
                {
                    playerMovement.Play("player_left_idle");
                }
            }
            else if (aimVar >= -135f && aimVar < -45f)
            {
                if (horVel != 0 || vertVel != 0)
                {
                    playerMovement.Play("player_walk_down");
                }
                else
                {
                    playerMovement.Play("player_down_idle");
                }
            }
        }
        else if(!isPlayer)
        {
            if (weapon.facingDirection.x < 0 && weapon.facingDirection.y < 0)
            {
                
            }
            if (weapon.facingDirection.x < 0 && weapon.facingDirection.y > 0)
            {

            }
            if (weapon.facingDirection.x > 0 && weapon.facingDirection.y < 0)
            {

            }
            if (weapon.facingDirection.x > 0 && weapon.facingDirection.y < 0)
            {

            }
        }
    }

    

}
