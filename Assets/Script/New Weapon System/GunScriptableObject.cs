using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "Gun", menuName = "Guns/Gun", order = 0)]
public class GunScriptableObject : ScriptableObject
{
    public Weapon WeaponSelected;
    public string Name;
    public GameObject ModelPrefab;//Aqui pondremos el prefab manitos
    public Vector3 SpawnPoint; //en nuestro caso sera la mira
    public Vector3 SpawnRotation; //en nuestro caso sera basandonos las funciones shooting de el script deprecado. 

    private float LastShootTime;

    public ShootConfigScriptableObject ShootConfig;
    public TrailConfigScriptableObject TrailConfig;

    private MonoBehaviour ActiveMonoBehaviour;
    private GameObject Model;
    private ParticleSystem ShootSystem; //las particulas de disparos
    private ObjectPool<TrailRenderer> TrailPool; //que curioso!!, unity si tiene un object pool predeterminado y optimizado.

    public void Shoot()
    {
        if (Time.time > ShootConfig.FireRate + LastShootTime)
        {
            LastShootTime = Time.time;
            ShootSystem.Play();
            //Tengo que adecuar esto a nuestro entorno porque es la dirección en que dispara, sin contar con que abran diferentes formas de disparar*******
            Vector3 shootDirection = ShootSystem.transform.forward + new Vector3(
                Random.Range(
                    -ShootConfig.Spread.x,
                    ShootConfig.Spread.x
                    ),
                Random.Range(
                    -ShootConfig.Spread.y,
                    ShootConfig.Spread.y
                    ),
                Random.Range(
                    -ShootConfig.Spread.z,
                    ShootConfig.Spread.z
                    )
                );
            shootDirection.Normalize();
            //************************************************
            if (Physics.Raycast(
                    ShootSystem.transform.position,
                    shootDirection,
                    out RaycastHit hit,
                    float.MaxValue,
                    ShootConfig.HitMask
                ))
            {
                ActiveMonoBehaviour.StartCoroutine(
                    PlayTrail(
                        ShootSystem.transform.position,
                        hit.point,
                        hit
                    )
                );
            }
            else
            {
                ActiveMonoBehaviour.StartCoroutine(
                    PlayTrail(
                        ShootSystem.transform.position,
                        ShootSystem.transform.position + (
                            shootDirection *
                            TrailConfig.MissDistance),
                        new RaycastHit()
                    )
                );
            }
        }
    }

    public void Spawn(Transform Parent, MonoBehaviour ActiveMonoBehaviour)
    {
        this.ActiveMonoBehaviour = ActiveMonoBehaviour;

        TrailPool = new ObjectPool<TrailRenderer>(CreateTrail);

        Model = Instantiate(ModelPrefab);
        Model.transform.SetParent(Parent, false);
        Model.transform.localPosition = SpawnPoint;
        Model.transform.localRotation = Quaternion.Euler(SpawnRotation);


        ShootSystem = Model.GetComponentInChildren<ParticleSystem>();
    }

    private TrailRenderer CreateTrail()
    {
        GameObject instance = new GameObject("Bullet Trail");
        TrailRenderer trail = instance.AddComponent<TrailRenderer>();
        trail.colorGradient = TrailConfig.Color;
        trail.material = TrailConfig.Material;
        trail.widthCurve = TrailConfig.WidthCurve;
        trail.time = TrailConfig.Duration;
        trail.minVertexDistance = TrailConfig.MinVertexDistance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        return trail;
    }

    private IEnumerator PlayTrail(Vector3 StartPoint, Vector3 EndPoint, RaycastHit Hit, int Iteration = 0)
    {
        TrailRenderer instance = TrailPool.Get();
        instance.gameObject.SetActive(true);
        instance.transform.position = StartPoint;
        yield return null;

        instance.emitting = true;

        float distance = Vector3.Distance(StartPoint, EndPoint);
        float remainingDistance = distance;
        while (remainingDistance > 0)
        {
            instance.transform.position = Vector3.Lerp(
                StartPoint,
                EndPoint,
                Mathf.Clamp01(1 - (remainingDistance / distance))
            );
            remainingDistance -= TrailConfig.SimulationSpeed * Time.deltaTime;

            yield return null;
        }
        instance.transform.position = EndPoint;



        yield return new WaitForSeconds(TrailConfig.Duration);
        yield return null;
        instance.emitting = false;
        instance.gameObject.SetActive(false);
        TrailPool.Release(instance);

    } 
}
