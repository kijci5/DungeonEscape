using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private int damageAmount=1;
    private bool canTakeDamage=true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null && canTakeDamage)
        {
            hit.Damage(damageAmount);
            canTakeDamage = false;
            StartCoroutine(ResetRoutine());
        }
    }

    private IEnumerator ResetRoutine()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
    }
}
