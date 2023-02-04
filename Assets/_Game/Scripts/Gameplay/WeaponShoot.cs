using Assets._Game.Scripts.Gameplay;
using Assets._Game.Scripts.Gameplay.Missiles;
using System.Collections;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [SerializeField] float firingTime = 1f;

    [SerializeField] float firingForceRight = -1000f;
    [SerializeField] float firingForceUp = 2;

    [SerializeField] WeaponMissile arrowToSpawn;

    [SerializeField] Transform missileSpawnPos;

    [SerializeField] GameObject modelShowWhenReady;

    [SerializeField] RandomSoundPlay soundShoot;


    private bool canFire = true;


    private void Update()
    {
        if (canFire && Input.GetButton("Fire1"))
        {
            FireWeapon();
        }
    }

    public void FireWeapon()
    {
        Debug.Log("FireWeapon");
        if (canFire)
        {
            soundShoot.PlayRandomSound();
            StartCoroutine(SpawnMissile());
        }
    }

    IEnumerator SpawnMissile()
    {
        modelShowWhenReady.SetActive(false);

        var arrow = Instantiate(arrowToSpawn, missileSpawnPos.position, transform.rotation);
        Rigidbody arrowBody = arrow.GetComponent<Rigidbody>();
        arrowBody.AddForce(arrowBody.transform.right * firingForceRight);
        arrowBody.AddForce(arrowBody.transform.up * firingForceUp);
        canFire = false;

        yield return new WaitForSeconds(firingTime);
        modelShowWhenReady.SetActive(true);
        canFire = true;
    }
}
