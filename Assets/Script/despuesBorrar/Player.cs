using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private Vector3 playerStartPosition;
    public Vector3 LobbyPlayerStartPosition;

    float ejeHorizontal;
    float ejeVertical;

    const string STATE_IS_ALIVE = "isAlive";
    const string STATE_ON_FLOOR = "onFloor";
    const string STATE_IS_FALLING = "isFalling";
    const string STATE_IS_MOVING = "isMoving";

    public float jumpForce = 6f;
    public LayerMask groundMask;

    bool actionRun = false;
    public bool actionGoMap = false;
    public bool actionGoMapNeg = false;
    bool gunLoaded = true;
    bool actionJump = true;

    Vector3 moveDirection;
    Vector2 facingDirection;

    [SerializeField] float fireRate = 7;
    [SerializeField] float moveSpeed;
    //[SerializeField] int health = 3;
    [SerializeField] Transform aim;
    [SerializeField] Camera mainCamera;
    [SerializeField] CinemachineVirtualCamera virtualMainCamera;
    [SerializeField] Transform BulletPrefab;

    private Rigidbody2D playeRigidBody;
    private Animator playeAnimator;
    private SpriteRenderer playeSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake()
    {
        //playeRigidBody = GetComponent<Rigidbody2D>();
        //playeAnimator = GetComponent<Animator>();
        //playeSpriteRenderer = GetComponent<SpriteRenderer>();
        playerStartPosition = this.transform.position;
        //LobbyPlayerStartPosition = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        ActionRecolector();
    }

    private void FixedUpdate()
    {        
            //ToMove(moveSpeed);
            Aim();
            //ToShoot();
            ShotgunShoot();
            //GoToMap();
            //Jump();
    }

    public void StartGame()
    {
    }


    void ActionRecolector()
    {
        //--------------------------------
        //walking.
        ejeHorizontal = Input.GetAxis("Horizontal");
        //ejeVertical = Input.GetAxis("Vertical");
        actionJump = (Input.GetButtonDown("Jump"));
        moveDirection.x = ejeHorizontal;
        moveDirection.y = 0;
        //--------------------------------
        //Running
        actionRun = Input.GetButton("Fire3");
        //--------------------------------
        //Go to Map
        actionGoMap = Input.GetButton("Fire1");
        actionGoMapNeg = Input.GetButton("Fire2");
    }

    void ToShoot()
    {
        //  Si click izq 
        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion BulletDirection = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(BulletPrefab, transform.position, BulletDirection);
            StartCoroutine(ReloadGun());
        }
    }

    void ShotgunShoot()
    {
        //  Si click izq 
        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg - 45;
            //Quaternion BulletDirection = Quaternion.AngleAxis(angle, Vector3.forward);
            for (int i = 0; i < 5; i++) 
            {
                angle += 15f;
                Quaternion BulletDirection = Quaternion.AngleAxis(angle, Vector3.forward);
                Instantiate(BulletPrefab, transform.position, BulletDirection);
            }
            StartCoroutine(ReloadGun());
        }
    }

    void Aim()
    {
        
        facingDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //En este algoritmo se coje el vector2 y se le dice a unity que lo interprete como un vector3 y se normaliza (magnitud = 1)
        aim.position = transform.position + (Vector3)facingDirection.normalized;
    }

    //********************************
    //Corutinas
    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }
}
