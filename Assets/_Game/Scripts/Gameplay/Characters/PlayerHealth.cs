using Assets._Game.Scripts.Gameplay;
using Assets._Game.Scripts.Gameplay.Characters;
using Assets._Game.Scripts.Gameplay.Missiles;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int healthCurrent, healthMax;
    [SerializeField] GameObject spawnOnDamage;
    [SerializeField] GameObject spawnOnDeath;

    public CharacterRespawner characterRespawner;

    [SerializeField] private PlayerHealthBarModel playerHealthBarModel;


    private void Start()
    {
        healthCurrent = healthMax;
        if (characterRespawner != null && playerHealthBarModel == null)
        {
            playerHealthBarModel = characterRespawner.GetComponentInChildren<PlayerHealthBarModel>();
        }
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
            if (playerHealthBarModel != null)
            {
                playerHealthBarModel.PlayerDamaged(healthCurrent);
            }
            if (spawnOnDamage != null)
            {
                Instantiate(spawnOnDamage, impactPos, transform.rotation);
            }
        }
    }

    private void Die(Vector3 impactPos)
    {
        if (playerHealthBarModel != null)
        {
            playerHealthBarModel.PlayerDamaged(healthCurrent);
        }

        if (spawnOnDeath != null)
        {
            Debug.Log("Player died: " + name);
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
        playerHealthBarModel.Restore();
    }
}
