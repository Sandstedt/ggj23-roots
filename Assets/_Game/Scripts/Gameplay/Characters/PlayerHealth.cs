using Assets._Game.Scripts.Gameplay;
using Assets._Game.Scripts.Gameplay.Missiles;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int healthCurrent, healthMax;
    [SerializeField] GameObject spawnOnDamage;
    [SerializeField] GameObject spawnOnDeath;

    [SerializeField] CharacterRespawner characterRespawner;


    private void Start()
    {
        healthCurrent = healthMax;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Damage(5, transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        var missile = collision.gameObject.GetComponent<WeaponMissile>();
        if (missile != null)
        {
            Debug.Log("Player damaged: " + missile.damage + " / " + healthCurrent);

            Damage(missile.damage, collision.transform.position);
            missile.MissileHitObject();
        }
    }

    public void Damage(int damage, Vector3 impactPos)
    {
        healthCurrent -= damage;
        if (healthCurrent <= 0)
        {
            Die(impactPos);
        }
        else
        {
            if (spawnOnDamage != null)
            {
                Instantiate(spawnOnDamage, impactPos, transform.rotation);
            }
        }
    }

    private void Die(Vector3 impactPos)
    {
        if (spawnOnDeath != null)
        {
            Instantiate(spawnOnDeath, impactPos, transform.rotation);
        }

        if (characterRespawner != null)
        {
            characterRespawner.RespawnCharacter();
        }
    }

    public void PlayerRespawned()
    {
        healthCurrent = healthMax;
    }
}
