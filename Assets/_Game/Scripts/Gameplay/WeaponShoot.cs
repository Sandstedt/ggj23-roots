using Assets._Game.Scripts.Gameplay;
using Assets._Game.Scripts.Gameplay.Missiles;
using System.Collections;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] float firingTime = 1f;

    [SerializeField] float firingForceRight = -1000f;
    [SerializeField] float firingForceUp = 2;

    [SerializeField] float throwForceRight = -300f;
    [SerializeField] float throwForceUp = 20;

    [SerializeField] float rotationForce = 20f;
    [SerializeField] float rotationForceThrow = 20f;

    [SerializeField] WeaponMissile arrowToSpawn;

    [SerializeField] WeaponMissile weaponThrowAway;

    [SerializeField] Transform missileSpawnPos;
    [SerializeField] Transform weaponThrowAwaySpawnPos;

    [SerializeField] GameObject weaponModel;

    [SerializeField] GameObject modelShowWhenReady;
    [SerializeField] GameObject modelLaserShowWhenReady;

    [SerializeField] RandomSoundPlay soundShoot;
    [SerializeField] RandomSoundPlay soundThrow;

    private Collider plrCollider;

    [SerializeField] int ammoMax;

    public int ammoCurrent { get; private set; }

    private bool canFire = false;

    private void Start()
    {
        plrCollider = GetComponentInParent<Collider>();
        WeaponEnable();
    }

    public void WeaponEnable()
    {
        ammoCurrent = ammoMax;
        canFire = true;
        weaponModel.SetActive(true);
        modelShowWhenReady.SetActive(true);
        modelLaserShowWhenReady.SetActive(true);
    }


    // private void Update()
    // {
    //     // if (canFire && Input.GetButton("Fire1"))
    //     // {
    //     //     FireWeapon();
    //     // }
    // }

    public void FireWeapon()
    {
        // Debug.Log("FireWeapon");
        if (canFire)
        {
            StartCoroutine(SpawnMissile());
        }
    }
    private void ShootMissile()
    {
        soundShoot.PlayRandomSound();

        WeaponMissile arrow = Instantiate(arrowToSpawn, missileSpawnPos.position, transform.rotation);
        Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();

        arrow.SetIgnoreObject(plrCollider);
        arrowBody.AddForce(arrowBody.transform.right * firingForceRight);
        arrowBody.AddForce(arrowBody.transform.up * firingForceUp);

        if (rotationForce > 0)
        {
            arrowBody.AddTorque(arrowBody.transform.right * rotationForce * 1);
        }
    }

    public void ShootThrowWeapon()
    {
        soundThrow.PlayRandomSound();

        WeaponMissile weaponThrow = Instantiate(weaponThrowAway, weaponThrowAwaySpawnPos.position, transform.rotation);
        Rigidbody weaponThowBody = weaponThrow.GetComponent<Rigidbody>();

        weaponThrow.SetIgnoreObject(plrCollider);
        weaponThowBody.AddForce(weaponThowBody.transform.right * throwForceRight);
        weaponThowBody.AddForce(weaponThowBody.transform.up * throwForceUp);

        if (rotationForceThrow > 0)
        {
            weaponThowBody.AddTorque(weaponThowBody.transform.right * rotationForceThrow * 1);
        }

        canFire = false;
        ammoCurrent = 0;

        weaponModel.SetActive(false);
        modelShowWhenReady.SetActive(false);
        modelLaserShowWhenReady.SetActive(false);
    }

    IEnumerator SpawnMissile()
    {
        modelShowWhenReady.SetActive(false);
        modelLaserShowWhenReady.SetActive(false);

        ammoCurrent--;
        canFire = false;


        if (ammoCurrent > 0)
        {
            ShootMissile();
        }
        else
        {
            ShootThrowWeapon();
        }

        yield return new WaitForSeconds(firingTime);

        if (ammoCurrent > 0)
        {
            canFire = true;
            modelLaserShowWhenReady.SetActive(true);

            if (ammoCurrent > 1)
            {
                modelShowWhenReady.SetActive(true);
            }
        }
        else
        {
            canFire = false;
        }
    }
}
