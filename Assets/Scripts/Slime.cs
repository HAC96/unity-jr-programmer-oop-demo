using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Monster // INHERITANCE
{
    protected override int MaxHitPoints { get => hasSplit ? maxHitPoints / 2 : maxHitPoints; }
    protected override float MoveSpeed { get => moveSpeed; }
    protected override float AttackRange { get => attackRange; }
    protected override float AttackCooldown { get => attackCooldown; }
    protected override float MinDamage { get => (hasSplit ? minDamage / 2 : minDamage) * difficultyMult; }
    protected override float MaxDamage { get => (hasSplit ? maxDamage / 2 : maxDamage) * difficultyMult; }
    protected override float DetectionRange { get => detectionRange; }

    [SerializeField] int maxHitPoints = 30;
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float attackRange = 1;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float minDamage = 3;
    [SerializeField] float maxDamage = 12;
    [SerializeField] float detectionRange = 6;

    protected bool hasSplit = false;

    // POLYMORPHISM
    protected override void Die()
    {
        if (!hasSplit)
        {
            hasSplit = true;
            HitPoints = MaxHitPoints;
            // ABSTRACTION
            Split();
            Debug.Log($"{gameObject.name} split");
        }
        else
        {
            base.Die();
        }
    }

    protected void Split()
    {
        Vector2 translation = facing.y == 0 ? Vector2.up : Vector2.left;
        animator.SetBool("Dead_b", false);
        Instantiate(gameObject, transform.position + (Vector3)translation, Quaternion.identity);
        transform.Translate(-translation);
    }
}
