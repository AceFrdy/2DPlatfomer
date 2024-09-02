using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeAttack : MonoBehaviour
{
    public int attackDamage = 20;
    public Vector2 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, attackRange, attackMask);
        foreach (var colInfo in hitColliders)
        {
            Damageable damageable = colInfo.GetComponent<Damageable>();
            if (damageable != null)
            {
                Vector2 deliveredKnockback = transform.localScale.x > 0 ? new Vector2(attackOffset.x, attackOffset.y) : new Vector2(-attackOffset.x, attackOffset.y);
                bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
                if (gotHit)
                    Debug.Log(colInfo.name + " hit for " + attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
