using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    protected override int MaxHitPoints { get => maxHitPoints; }
    protected override float MoveSpeed { get => moveSpeed; }
    protected override float AttackRange { get => attackRange; }
    protected override float AttackCooldown { get => attackCooldown; }
    protected override float MinDamage { get => minDamage * difficultyMult; }
    protected override float MaxDamage { get => maxDamage * difficultyMult; }
    protected override float DetectionRange { get => detectionRange; }

    [SerializeField] int maxHitPoints = 20;
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float attackRange = 1;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float minDamage = 3;
    [SerializeField] float maxDamage = 10;
    [SerializeField] float detectionRange = 6;

    [SerializeField] float resurrectChance = 0.5f;

    protected override void Die()
    {
        if (Random.Range(0f, 1f) < resurrectChance)
        {
            HitPoints = MaxHitPoints / 2;
            resurrectChance /= 2;
            Debug.Log("Skeleton resurrected");
            animator.SetBool("Dead_b", false);
        }
        else
        {
            base.Die();
        }
    }
}
