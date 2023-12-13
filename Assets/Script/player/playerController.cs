using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed;
    private new Rigidbody2D rigidbody;
    private float horVel;
    private float vertVel;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }
    void movement()
    {
        speed = gameController.MoveSpeed;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        horVel = horizontal * speed;
        vertVel = vertical * speed;
        rigidbody.velocity = new Vector3(horVel, vertVel, 0);
    }

    

}
