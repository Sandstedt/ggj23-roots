using Assets._Game.Scripts.Gameplay;
using Assets._Game.Scripts.Gameplay.Characters;
using Assets._Game.Scripts.Gameplay.Missiles;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private CharacterTeam team;
    
    [SerializeField] int healthCurrent, healthMax;
    [SerializeField] GameObject spawnOnDamage;
    [SerializeField] GameObject spawnOnDeath;

    public CharacterRespawner characterRespawner;

    [SerializeField] private PlayerHealthBarModel playerHealthBarModel;


    private void Start()
    {
        healthCurrent = healthMax;
        if (playerHealthBarModel == null)
        {
            if (team == CharacterTeam.team1)
            {
                playerHealthBarModel = StaticReferences.Instance.playerHealthBarModelTeam1;
            }
            else
            {
                playerHealthBarModel = StaticReferences.Instance.playerHealthBarModelTeam2;
            }
            playerHealthBarModel.team = team;
            // playerHealthBarModel =  characterRespawner.GetComponentInChildren<PlayerHealthBarModel>();
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
        else
        {
            //Debug.Log( "playerpos: " + transform.position );
            //Debug.Log( "collision.transform.position: " + collision.transform.position );

            Vector3 v = collision.transform.position - transform.position;

            if(Vector3.Dot( v.normalized, Vector3.up ) > 0.7)
                Damage( 5, collision.transform.position );

            //Debug.Log( "fromabove: " +  ); 
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
            playerHealthBarModel.Die();
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
