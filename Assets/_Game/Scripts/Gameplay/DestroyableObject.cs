using Assets._Game.Scripts.Gameplay;
using Assets._Game.Scripts.Gameplay.Missiles;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class DestroyableObject : MonoBehaviour
{
    private int healthCurrent;
    [SerializeField] GameObject spawnOnDamage;
    [SerializeField] GameObject spawnOnDeath;

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
            Damage(missile.damage, collision.transform.position);
            missile.MissileHitObject();
        }
    }

    public void Damage(int damage, Vector3 impactPos)
    {
        if (healthCurrent > 0)
        {
            healthCurrent -= damage;
            Debug.Log("Tree damaged: " + damage + " / " + healthCurrent);
        }

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
            Instantiate(spawnOnDeath, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
