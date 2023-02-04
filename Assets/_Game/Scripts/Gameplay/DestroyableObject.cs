using System.Collections;
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

        StartCoroutine(DieAfterTime());
    }

    IEnumerator DieAfterTime()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        Die();
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
            Instantiate(spawnOnDeath, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}
