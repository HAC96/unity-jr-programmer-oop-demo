using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : Monster // INHERITANCE
{
    protected override int MaxHitPoints { get => maxHitPoints; }
    protected override float MoveSpeed { get => moveSpeed; }
    protected override float AttackRange { get => attackRange; }
    protected override float AttackCooldown { get => attackCooldown; }
    protected override float MinDamage { get => isRaging ? minDamage * 2 : minDamage; }
    protected override float MaxDamage { get => isRaging ? maxDamage * 2 : maxDamage; }
    protected override float DetectionRange { get => detectionRange; }

    [SerializeField] int maxHitPoints = 50;
    [SerializeField] float moveSpeed = 2;
    [SerializeField] float attackRange = 1;
    [SerializeField] float attackCooldown = 0.75f;
    [SerializeField] float minDamage = 4;
    [SerializeField] float maxDamage = 12;
    [SerializeField] float detectionRange = 6;

    protected bool isRaging = false;

    protected void Update()
    {
        if (HitPoints < MaxHitPoints / 2)
        {
            isRaging = true;
            Debug.Log($"{gameObject.name} became enraged");
        }
    }
}
