using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    private int healthCurrent;
    [SerializeField] GameObject spawnOnDeath;

    [SerializeField]
    private int healthMax;

    private void Start()
    {
        healthCurrent = healthMax;
    }

    public void Damage(int damage)
    {
        healthCurrent -= damage;
        Die();
    }

    private void Die()
    {
        if (spawnOnDeath != null)
        {
            Instantiate(spawnOnDeath);

        }
    }
}
