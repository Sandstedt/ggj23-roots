using Assets._Game.Scripts.Gameplay;
using Assets._Game.Scripts.Gameplay.Missiles;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class DestroyableObject : MonoBehaviour
{
    private int healthCurrent;
    [SerializeField] GameObject spawnOnDamage;
    [SerializeField] GameObject spawnOnDeath;

    [SerializeField] CharacterRespawner characterRespawner;

    [SerializeField]
    private int healthMax;

    private void Start()
    {
        healthCurrent = healthMax;
    }

    void OnCollisionEnter(Collision collision)
    {
        var missile = collision.gameObject.GetComponent<WeaponMissile>();
        if (missile != null)
        {
            Debug.Log("Tree damaged: " + missile.damage + " / " + healthCurrent);

            Damage(missile.damage, missile.transform.position);
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

        Destroy(gameObject);
    }
}
