using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Monster // INHERITANCE
{
    protected override int MaxHitPoints { get => maxHitPoints; }
    protected override float MoveSpeed { get => moveSpeed; }
    protected override float AttackRange { get => attackRange; }
    protected override float AttackCooldown { get => attackCooldown; }
    protected override float MinDamage { get => minDamage * difficultyMult; }
    protected override float MaxDamage { get => maxDamage * difficultyMult; }
    protected override float DetectionRange { get => detectionRange; }

    [SerializeField] int maxHitPoints = 15;
    [SerializeField] float moveSpeed = 4;
    [SerializeField] float attackRange = 1;
    [SerializeField] float attackCooldown = 0.25f;
    [SerializeField] float minDamage = 1;
    [SerializeField] float maxDamage = 5;
    [SerializeField] float detectionRange = 6;

    // POLYMORPHISM
    protected override LayerMask mask { get => LayerMask.GetMask("Player"); }
}
