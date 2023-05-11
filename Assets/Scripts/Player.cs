using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Creature // INHERITANCE
{
    protected override int MaxHitPoints { get => maxHitPoints; }
    protected override float MoveSpeed { get => moveSpeed; }
    protected override float AttackRange { get => attackRange; }
    protected override float AttackCooldown { get => attackCooldown; }
    protected override float MinDamage { get => minDamage; }
    protected override float MaxDamage { get => maxDamage; }

    [SerializeField] int maxHitPoints = 30;
    [SerializeField] float moveSpeed = 6;
    [SerializeField] float attackRange = 1;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float minDamage = 3;
    [SerializeField] float maxDamage = 12;

    protected LayerMask mask
    {
        get => LayerMask.GetMask("Monster", "Obstacle", "Flying"); 
    }

    protected void OnMove(InputValue input)
    {
        Vector2 moveDir = input.Get<Vector2>();
        Move(moveDir);
    }

    protected void OnFire()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, facing, AttackRange + BoxColliderRangeDiff(gameObject), mask);
//        Debug.DrawRay(transform.position, facing);
        if (hits.Length == 0) {
            animator.SetTrigger("Attack_t");
            return;
        }
//        Debug.Log($"Player struck {hits[0].transform.gameObject.name}");
        if (hits[0].transform.gameObject.GetComponent<Monster>() != null)
        {
            Attack(hits[0].transform.gameObject.GetComponent<Monster>());
        }
    }

    protected override void Die()
    {
        GameManager.Instance.GameOver();
    }
}