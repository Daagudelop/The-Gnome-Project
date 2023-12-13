using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public enum Weapons
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
    [Header("Current Weapon")]
    public Weapons currentWeapon = Weapons.Melee;

    int fireRate;
    public float rotZ;

    bool BouncingAmmo =false;
    bool isPlayer = false;
    bool gunLoaded = true;
    Vector2 facingDirection;
    Vector3 mousePos;

    [SerializeField] float timeTillDespawn = 2;
    [SerializeField]Camera mainCamera;
    [SerializeField] ParticleSystem shot;
    [SerializeField] Transform BulletPrefab;
    [SerializeField] Transform aim;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Aim();
        
    }

    void Aim()
    {
        facingDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
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

    IEnumerator ReloadGun()
    {
        yield return new WaitForSeconds(1 / fireRate);
        gunLoaded = true;
    }

    private void setCurrentWeapon(Weapons newWeapon)
    {
        if (newWeapon == Weapons.Pistol)
        {
            //TODO: colocar la lógica del menu
        }
        else if (newWeapon == Weapons.Shotgun)
        {
            //TODO: colocar la lógica del ingame
        }
        else if (newWeapon == Weapons.Uzi)
        {
            //TODO: colocar la lógica del GameOver
        }

        else if (newWeapon == Weapons.Rifle)
        {
            //TODO: colocar la lógica del GameOver
        }

        else if (newWeapon == Weapons.GranadeLauncher)
        {
            //TODO: colocar la lógica del GameOver
        }
        else if (newWeapon == Weapons.Melee)
        {
            //TODO: colocar la lógica del GameOver
        }

        //Crea las instancias.
        this.currentWeapon = newWeapon;
    }

    public void Pistol()
    {
        setCurrentWeapon(Weapons.Pistol);
    }
    public void Shotgun()
    {
        setCurrentWeapon(Weapons.Shotgun);
    }
    public void Uzi()
    {
        setCurrentWeapon(Weapons.Uzi);
    }
    public void Rifle()
    {
        setCurrentWeapon(Weapons.Rifle);
    }
    public void GranadeLauncher()
    {
        setCurrentWeapon(Weapons.GranadeLauncher);
    }
    public void Melee()
    {
        setCurrentWeapon(Weapons.Melee);
    }
}
