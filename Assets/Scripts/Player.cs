using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : Creature // INHERITANCE
{
    protected override int MaxHitPoints { get => maxHitPoints; }
    protected override float MoveSpeed { get => moveSpeed; }
    protected override float AttackRange { get => attackRange; }
    protected override float AttackCooldown { get => attackCooldown; }
    protected override float MinDamage { get => minDamage / difficultyMult; }
    protected override float MaxDamage { get => maxDamage / difficultyMult; }

    [SerializeField] int maxHitPoints = 30;
    [SerializeField] float moveSpeed = 6;
    [SerializeField] float attackRange = 1;
    [SerializeField] float attackCooldown = 0.5f;
    [SerializeField] float minDamage = 3;
    [SerializeField] float maxDamage = 12;

    [SerializeField] Slider hpBar;
    [SerializeField] TextMeshProUGUI hpText;

    public override int HitPoints
    {
        get => base.HitPoints;
        set
        {
            HpBarUpdate(value);
            base.HitPoints = value;
        }
    }

    protected LayerMask mask
    {
        get => LayerMask.GetMask("Monster", "Obstacle", "Flying"); 
    }

    protected override void Start()
    {
        hpBar.maxValue = maxHitPoints;
        base.Start();
        HpBarUpdate(hitPoints);
    }

    protected void HpBarUpdate(int hp)
    {
        hpBar.value = hp;
        hpText.text = $"({hp}/{maxHitPoints})";
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