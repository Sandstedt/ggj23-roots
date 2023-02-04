using Assets._Game.Scripts.Gameplay;
using Assets._Game.Scripts.Gameplay.Missiles;
using System.Collections;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    private int healthCurrent;
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

            Damage(missile.damage);
            missile.MissileHitObject();
        }
    }

    public void Damage(int damage)
    {
        healthCurrent -= damage;
        if (healthCurrent <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (spawnOnDeath != null)
        {
            Instantiate(spawnOnDeath, transform.position, transform.rotation);
        }

        if (characterRespawner != null)
        {
            characterRespawner.RespawnCharacter();
        }

        Destroy(gameObject);
    }
}
