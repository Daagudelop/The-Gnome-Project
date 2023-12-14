using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public enum Weapons1
{
    Pistol,
    Shotgun,
    Uzi,
    Rifle,
    GranadeLauncher,
    Melee,
}
public class Weapon : MonoBehaviour
{
    [Header("Is the player´s weapon?")]
    [SerializeField]bool isPlayer = false;
    
    [Header("Current Weapon")]
    public Weapons1 currentWeapon = Weapons1.Melee;

    [Header("Current Weapon Stats")]
    [SerializeField] int fireRate;
    [SerializeField] float rpm;
    [SerializeField] int damage;
    [SerializeField] bool BouncingAmmo =false;

    bool gunLoaded = true;
    Vector2 facingDirection;
    //Vector3 mousePos;
    public float rotZ;

    [Header("Editable")]

    [SerializeField] float timeTillDespawn = 2;
    [SerializeField] Camera mainCamera;
    [SerializeField] Bullet BulletPrefab;
    //[SerializeField] Transform BulletPrefab;
    [SerializeField] Transform aim;
    [SerializeField] Transform player;

    public delegate void ShootDelegate();
    private ShootDelegate selectedWayToShoot;

    private ObjectPool<Bullet> bulletPool;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        Pistol();
        //fireRate = 5;
        //damage = 5;
        //selectedWayToShoot = ToShoot;
        //rpm = (1 / fireRate) / 0.10f;
    }
    // Update is called once per frame
    void Update()
    {
        teclas();
        Aim();
        selectedWayToShoot();
    }

    //metodo provisional:
    private void teclas()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Pistol();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Shotgun();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Uzi();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Rifle();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            GranadeLauncher();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Melee();
        }
    }

    void Aim()
    {
        if (isPlayer)
        {
            facingDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        else
        {
            facingDirection = player.transform.position - transform.position;
        }
        aim.position = transform.position + (Vector3)facingDirection.normalized;
        rotZ = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
    }
    void ToShoot()
    {
        //  Si click izq 
        if (Input.GetMouseButton(0) && gunLoaded)
        {
            gunLoaded = false;
            float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
            Quaternion BulletDirection = Quaternion.Euler(-angle, 90, 0);
            Instantiate(BulletPrefab, aim.position, BulletDirection);
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
            for (int i = 0; i < 5; i++)
            {
                angle += 15f;
                Quaternion BulletDirection = Quaternion.Euler(-angle, 90, 0);
                Instantiate(BulletPrefab, transform.position, BulletDirection);
            }
            StartCoroutine(ReloadGun());
        }
    }


    private void setCurrentWeapon(Weapons1 newWeapon)
    {
        if (newWeapon == Weapons1.Pistol)
        {
            fireRate = 22;
            damage = 5;
                selectedWayToShoot = ToShoot;
        }
        else if (newWeapon == Weapons1.Shotgun)
        {
            fireRate = 30;
            damage = 15;
            
            
                selectedWayToShoot = ShotgunShoot;  
            
        }
        else if (newWeapon == Weapons1.Uzi)
        {
            fireRate = 15;
            damage = 5;
                selectedWayToShoot = ToShoot;
        }

        else if (newWeapon == Weapons1.Rifle)
        {
            fireRate = 20;
            damage = 5;
                selectedWayToShoot = ToShoot;
        }

        else if (newWeapon == Weapons1.GranadeLauncher)
        {
            //TODO: colocar la lógica del GameOver
        }
        else if (newWeapon == Weapons1.Melee)
        {
            //TODO: colocar la lógica del GameOver
        }

        //Crea las instancias.
        this.currentWeapon = newWeapon;
    }
    public void Pistol()
    {
        setCurrentWeapon(Weapons1.Pistol);
    }
    public void Shotgun()
    {
        setCurrentWeapon(Weapons1.Shotgun);
    }
    public void Uzi()
    {
        setCurrentWeapon(Weapons1.Uzi);
    }
    public void Rifle()
    {
        setCurrentWeapon(Weapons1.Rifle);
    }
    public void GranadeLauncher()
    {
        setCurrentWeapon(Weapons1.GranadeLauncher);
    }
    public void Melee()
    {
        setCurrentWeapon(Weapons1.Melee);
    }

    
    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(fireRate/20);
        
        gunLoaded = true;
    }

}
