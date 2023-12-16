using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using UnityEngine.UIElements;

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
    [SerializeField] bool isPlayer = false;

    [Header("Current Weapon")]
    public Weapons1 currentWeapon = Weapons1.Melee;

    [Header("Current Weapon Stats")]
    [SerializeField] float fireRate;
    [SerializeField] float rpm;
    public int damage;
    [SerializeField] bool BouncingAmmo = false;

    bool gunLoaded = true;
    public Vector2 facingDirection;
    //Vector3 mousePos;
    public float rotZ;

    [Header("Editable")]

    [SerializeField] float timeTillDespawn = 2;
    [SerializeField] Camera mainCamera;
    [SerializeField] ParticleSystem MuzzlePrefab;
    //[SerializeField] Transform BulletPrefab;
    [SerializeField] Transform aim;
    [SerializeField] Transform muzzleTransform;
    [SerializeField] Transform muzzleTransformBackUp;
    [SerializeField] Transform weaponTransform;
    [SerializeField] Transform player;
    [SerializeField] SpriteRenderer weapon;
    [SerializeField] Transform AKPrefab;
    [SerializeField] Transform MeleePrefab;
    [SerializeField] Transform enemyMeleePrefab;
    [SerializeField] Transform MeleePrefabVisual;
    [SerializeField] Transform UziPrefab;
    [SerializeField] Transform ShotgunPrefab;
    [SerializeField] Transform PistolPrefab;
    [SerializeField] Transform GLPrefab;
    [SerializeField] Transform WeaponToThrow;

    [SerializeField] Animator enemyAnimator;

    [SerializeField] Animator weaponAnimator;
    [SerializeField] bool aviableToTakeWeapon;
    [SerializeField] NavMeshAgent agent;
    //[SerializeField] Transform HandManager;

    public delegate void ShootDelegate();
    private ShootDelegate selectedWayToShoot;

    float ShootingTime;

    const string STATE_IS_UZI = "isUzi";
    const string STATE_IS_SHOOTING = "isShooting";
    const string STATE_IS_SHOTGUN = "isShotgun";
    const string STATE_IS_PISTOL = "isPistol";

    const string STATE_IS_RIFLE = "isRifle";
    const string STATE_IS_GRENADELAUNCHER = "isGrenadeLauncher";
    const string STATE_IS_MELEE = "isMelee";

    float alturaDeArma;
    float distanciaDelArma;

    Vector3 DistanciaMuzzle;
    Vector3 acercarMuzzle = Vector3.zero;
    // Start is called before the first frame update
    private void Start()
    {
        Pistol();
        Melee();
        //fireRate = 5;
        //damage = 5;
        //selectedWayToShoot = ToShoot;
        //rpm = (1 / fireRate) / 0.10f;
    }
    private void Awake()
    {
        muzzleTransformBackUp = muzzleTransform;
        //weapon = GetComponentInChildren<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Aim();
        if (isPlayer)
        {
            
            teclas();
            ThrowWeapon(WeaponToThrow);
            selectedWayToShoot();
        }
        else if (!isPlayer)
        {
            /*if (agent.remainingDistance > 13)
            {*/
                if (agent.remainingDistance <= 4)
                {
                    selectedWayToShoot();
                }
            //}
        }
    }

    private void FixedUpdate()
    {
        //MuzzlePrefab.Play();
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

        //Vector3 acercarMuzzle = Vector3.zero;
        Vector3 acercarArma = Vector3.zero;
        Vector3 anguloDeArma = Vector3.zero;
        //alturaDeArma = -0.25f;

        float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;

        if (facingDirection.x > 0)
        {
            weapon.flipY = false;
            //acercarMuzzle = DistanciaMuzzle;
            anguloDeArma = new Vector3(0, 0, angle);
            acercarArma = new Vector3(distanciaDelArma, alturaDeArma, 0.3f);
        }
        else if (facingDirection.x <= 0)
        {
            //DistanciaMuzzle.x = -DistanciaMuzzle.x;
            //acercarMuzzle = DistanciaMuzzle;
            weapon.flipY = true;
            anguloDeArma = new Vector3(0, 0, angle);
            acercarArma = new Vector3(-distanciaDelArma, alturaDeArma, 0.3f);
        }
        //muzzleTransform.position += acercarMuzzle; 
        aim.position = transform.position + ((Vector3)facingDirection.normalized - acercarArma);
        aim.eulerAngles = anguloDeArma;
        rotZ = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
        //Quaternion BulletDirection = Quaternion.Euler(-rotZ, 90, 0);
        //HandManager.rotation = BulletDirection;
    }
    void ToShoot()
    {
        if (isPlayer)
        {
            
            //  Si click izq 
            if (Input.GetMouseButton(0))
            {
                if (gunLoaded)
                {
                    gunLoaded = false;
                    weaponAnimator.SetBool(STATE_IS_SHOOTING, true);
                    float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
                    Quaternion BulletDirection = Quaternion.Euler(-angle, 90, 0);
                    GameObject bullet = BulletObjectPooling.sharedInstanceOP.RequestBullet();
                    bullet.transform.position = muzzleTransform.position;
                    bullet.transform.rotation = BulletDirection;
                    CameraShake.sharedInstanceCS.ShakeCamera();
                    MuzzlePrefab.Play();
                    //Instantiate(BulletPrefab, aim.position, BulletDirection);
                    StartCoroutine(ReloadGun());
                    //weaponAnimator.SetBool(STATE_IS_SHOOTING, false);
                    //Invoke("ShootingStopped", ShootingTime);
                }
                else
                {
                    ShootingStopped();
                }
            }
            else
            {
                //weaponAnimator.SetBool(STATE_IS_SHOOTING, false);
            }
        }
    }

    void ShotgunShoot()
    {
        if (isPlayer)
        {
            
            if (Input.GetMouseButton(0))
            {
                if (gunLoaded)
                {
                    gunLoaded = false;
                    weaponAnimator.SetBool(STATE_IS_SHOOTING, true);

                    float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg - 45;
                    for (int i = 0; i < 5; i++)
                    {
                        angle += 15f;
                        Quaternion BulletDirection = Quaternion.Euler(-angle, 90, 0);
                        GameObject bullet = BulletObjectPooling.sharedInstanceOP.RequestBullet();
                        bullet.transform.position = muzzleTransform.transform.position;
                        bullet.transform.rotation = BulletDirection;

                        //Instantiate(BulletPrefab, transform.position, BulletDirection);
                    }
                    CameraShake.sharedInstanceCS.ShakeCamera();
                    //Invoke("ShootingStopped", ShootingTime);
                    MuzzlePrefab.Play();
                    //weaponAnimator.SetBool(STATE_IS_SHOOTING, false);
                    StartCoroutine(ReloadGun());
                    Invoke("ShootingStopped", ShootingTime);
                }
                else
                {
                    ShootingStopped();
                }
            }
            else
            {
                //weaponAnimator.SetBool(STATE_IS_SHOOTING, false);
            }
        }
        //  Si click izq 
    }

    public void ThrowWeapon(Transform Throwable)
    {
        if (isPlayer)
        {
            if (Input.GetKeyDown(KeyCode.E) && !aviableToTakeWeapon)
            {

                float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
                Quaternion BulletDirection = Quaternion.Euler(-angle, 90, 0);

                Instantiate(Throwable, aim.position, BulletDirection);

                Melee();
            }
        }
    }

    public void Punch()
    {
        //  Si click izq 
        if (!isPlayer)
        {
            if (gunLoaded)
            {
                Debug.Log("nani");
                gunLoaded = false;
                weaponAnimator.SetBool(STATE_IS_SHOOTING, true);
                float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
                Quaternion BulletDirection = Quaternion.Euler(-angle, 90, 0);
                Instantiate(enemyMeleePrefab, aim.position, BulletDirection);
                Instantiate(MeleePrefabVisual, aim.position, BulletDirection);
                CameraShake.sharedInstanceCS.ShakeCamera();
                //MuzzlePrefab.Play();
                //Instantiate(BulletPrefab, aim.position, BulletDirection);
                StartCoroutine(ReloadGun());
                //weaponAnimator.SetBool(STATE_IS_SHOOTING, false);
                //Invoke("ShootingStopped", ShootingTime);
            }
            else
            {
                ShootingStopped();
            }
        }
        else if (Input.GetMouseButton(0) && isPlayer)
        {
            if (gunLoaded)
            {
                gunLoaded = false;
                weaponAnimator.SetBool(STATE_IS_SHOOTING, true);
                float angle = Mathf.Atan2(facingDirection.y, facingDirection.x) * Mathf.Rad2Deg;
                Quaternion BulletDirection = Quaternion.Euler(-angle, 90, 0);
                Instantiate(MeleePrefab, aim.position, BulletDirection);
                Instantiate(MeleePrefabVisual, aim.position, BulletDirection);
                CameraShake.sharedInstanceCS.ShakeCamera();
                //MuzzlePrefab.Play();
                //Instantiate(BulletPrefab, aim.position, BulletDirection);
                StartCoroutine(ReloadGun());
                //weaponAnimator.SetBool(STATE_IS_SHOOTING, false);
                //Invoke("ShootingStopped", ShootingTime);
            }
            else
            {
                ShootingStopped();
            }
        }
        else
        {
            //weaponAnimator.SetBool(STATE_IS_SHOOTING, false);
        }
    }

    public void allFalse()
    {
        weaponAnimator.SetBool(STATE_IS_SHOOTING, false);
        weaponAnimator.SetBool(STATE_IS_UZI, false);
        weaponAnimator.SetBool(STATE_IS_GRENADELAUNCHER, false);
        weaponAnimator.SetBool(STATE_IS_MELEE, false);
        weaponAnimator.SetBool(STATE_IS_PISTOL, false);
        weaponAnimator.SetBool(STATE_IS_SHOTGUN, false);
        weaponAnimator.SetBool(STATE_IS_RIFLE, false);

    }

    private void setCurrentWeapon(Weapons1 newWeapon)
    {
        if (isPlayer)
        {
            
            muzzleTransform.position = muzzleTransform.position - DistanciaMuzzle;
        
            acercarMuzzle = Vector3.zero;
            if (newWeapon == Weapons1.Pistol)
            {
                //muzzleTransform.position = acercarmuzzle
                distanciaDelArma =0.35f;
                alturaDeArma = -0.25f;
                /*DistanciaMuzzle = new Vector3(-0.40f,alturaDeArma / 2,0);
                acercarMuzzle = DistanciaMuzzle;
                muzzleTransform.position = muzzleTransformBackUp.position + acercarMuzzle;*/
                ShootingTime = 0.023f;
                aviableToTakeWeapon = false;
                WeaponToThrow = PistolPrefab;
                CameraShake.sharedInstanceCS.shakeIntensity = 1;
                allFalse();
                weaponAnimator.SetBool(STATE_IS_PISTOL, true);

                fireRate = 22;
                damage = 5;
                    selectedWayToShoot = ToShoot;
            }
            else if (newWeapon == Weapons1.Shotgun)
            {
                distanciaDelArma = 0.25f;
                alturaDeArma = -0.15f;
                /*DistanciaMuzzle = new Vector3(0.87f,alturaDeArma / 2,0);

                acercarMuzzle = DistanciaMuzzle;
                muzzleTransform.position = muzzleTransformBackUp.position + acercarMuzzle;*/
                ShootingTime = 0.12f;
                aviableToTakeWeapon = false;
                WeaponToThrow = ShotgunPrefab;
                allFalse();
                weaponAnimator.SetBool(STATE_IS_SHOTGUN, true);
                fireRate = 30;
                damage = 20;
                CameraShake.sharedInstanceCS.shakeIntensity = 1.6f;
                CameraShake.sharedInstanceCS.shakeTime = 0.4f;
                selectedWayToShoot = ShotgunShoot;  
            
            }
            else if (newWeapon == Weapons1.Uzi)
            {
                distanciaDelArma = 0.35f;
                alturaDeArma = -0.25f;
                /*DistanciaMuzzle = new Vector3(-0.40f, alturaDeArma / 2, 0);
                acercarMuzzle = DistanciaMuzzle;
                muzzleTransform.position = muzzleTransformBackUp.position + acercarMuzzle;*/
                ShootingTime = 0.02f;
                aviableToTakeWeapon = false;
                WeaponToThrow= UziPrefab;
                allFalse();
                weaponAnimator.SetBool(STATE_IS_UZI, true);
                CameraShake.sharedInstanceCS.shakeIntensity = 1;
                fireRate = 5;
                damage = 5;
                selectedWayToShoot = ToShoot;
            }

            else if (newWeapon == Weapons1.Rifle)
            {
                distanciaDelArma = -0.25f;
                alturaDeArma = -0.15f;
                /*DistanciaMuzzle = new Vector3(0.87f, alturaDeArma / 2, 0);

                acercarMuzzle = DistanciaMuzzle;
                muzzleTransform.position = muzzleTransformBackUp.position + acercarMuzzle;*/
                ShootingTime = 0.045f;
                aviableToTakeWeapon = false;
                WeaponToThrow = AKPrefab;
                allFalse();
            
                weaponAnimator.SetBool(STATE_IS_RIFLE, true);
                CameraShake.sharedInstanceCS.shakeIntensity = 1.2f;
                fireRate = 10;
                damage = 10;
                    selectedWayToShoot = ToShoot;
            }

            else if (newWeapon == Weapons1.GranadeLauncher)
            {
                distanciaDelArma = 0.35f;
                alturaDeArma = -0.25f;
                /*DistanciaMuzzle = new Vector3(0.87f, alturaDeArma/2, 0);

                acercarMuzzle = DistanciaMuzzle;
                muzzleTransform.position = muzzleTransformBackUp.position + acercarMuzzle;*/
                ShootingTime = 0.02f;
                aviableToTakeWeapon = false;
                WeaponToThrow = GLPrefab;
                allFalse();
                weaponAnimator.SetBool(STATE_IS_GRENADELAUNCHER, true);
                //TODO: colocar la lógica del GameOver
            }
            else if (newWeapon == Weapons1.Melee)
            {
                distanciaDelArma = 0.35f;
                alturaDeArma = -0.25f;
                /*DistanciaMuzzle = new Vector3(-0.40f, alturaDeArma/2, 0);
                acercarMuzzle = DistanciaMuzzle;
                muzzleTransform.position = muzzleTransformBackUp.position + acercarMuzzle;*/
                ShootingTime = 0.08f;
                aviableToTakeWeapon = true;
                allFalse();
                weaponAnimator.SetBool(STATE_IS_MELEE, true);
                CameraShake.sharedInstanceCS.shakeIntensity = 1.2f;
                fireRate = 20;
                damage = 3;
                selectedWayToShoot = Punch;
                //TODO: colocar la lógica del GameOver
            }

            //Crea las instancias.
            this.currentWeapon = newWeapon;
        }
        else
        {
            if (newWeapon == Weapons1.Melee)
            {
                distanciaDelArma = 0.35f;
                alturaDeArma = -0.25f;
                /*DistanciaMuzzle = new Vector3(-0.40f, alturaDeArma/2, 0);
                acercarMuzzle = DistanciaMuzzle;
                muzzleTransform.position = muzzleTransformBackUp.position + acercarMuzzle;*/
                ShootingTime = 0.08f;
                aviableToTakeWeapon = true;
                allFalse();
                weaponAnimator.SetBool(STATE_IS_MELEE, true);
                CameraShake.sharedInstanceCS.shakeIntensity = 1.2f;
                fireRate = 20;
                damage = 3;
                selectedWayToShoot = Punch;
                //TODO: colocar la lógica del GameOver
            }
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlayer)
        {
            PickUpWeapon(collision);
        }
    }


    void PickUpWeapon(Collider2D collision)
    {
        if (collision.CompareTag("AK"))
        {
            if (aviableToTakeWeapon)
            {
                Debug.Log("AK");
                Rifle();
                Destroy(collision.gameObject, 0.001f);
            }
        }
        else if (collision.CompareTag("Pistol"))
        {
            if (/*Input.GetKeyDown(KeyCode.E) &&*/ aviableToTakeWeapon)
            {
                Pistol();

                Destroy(collision.gameObject, 0.001f);
            }
        }

        else if (collision.CompareTag("GL"))
        {
            if (/*Input.GetKeyDown(KeyCode.E)*/ aviableToTakeWeapon)
            {
                GranadeLauncher();

                Destroy(collision.gameObject, 0.001f);
            }
        }
        else if (collision.CompareTag("Shotgun"))
        {
            if (/*Input.GetKeyDown(KeyCode.E) &&*/ aviableToTakeWeapon)
            {
                Shotgun();

                Destroy(collision.gameObject, 0.001f);
            }
        }
        else if (collision.CompareTag("Uzi"))
        {
            if (/*Input.GetKeyDown(KeyCode.E) && */aviableToTakeWeapon)
            {
                Uzi();
                
                Destroy(collision.gameObject,0.001f);
                
            }
        }
    }
    IEnumerator ReloadGun()
    {
        rpm = fireRate / 20;
        Debug.Log(rpm);

        yield return new WaitForSeconds(rpm);
        gunLoaded = true;
    }
    public void ShootingStopped()
    {
        weaponAnimator.SetBool(STATE_IS_SHOOTING, false);
    }

    
}
