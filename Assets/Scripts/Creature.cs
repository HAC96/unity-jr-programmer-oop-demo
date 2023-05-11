using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
    public virtual int HitPoints
    {
        // ENCAPSULATION
        get
        {
            return hitPoints;
        }
        set
        {
            if (value < hitPoints) { animator.SetTrigger("Hit_t"); }
            if (value < 0)
            {
                hitPoints = 0;
                animator.SetBool("Dead_b", true);
                Die();
            }
            else if (value > MaxHitPoints)
            {
                hitPoints = MaxHitPoints;
            }
            else
            {
                hitPoints = value;
            }
        }
    }
    [SerializeField] protected int hitPoints;
    protected abstract int MaxHitPoints { get; }

    protected abstract float MoveSpeed { get; }
    protected Rigidbody2D rb // using rb so it collides with obstacles
    {
        get => GetComponent<Rigidbody2D>();
    }

    protected abstract float AttackRange { get; }
    protected abstract float AttackCooldown { get; }
    protected abstract float MinDamage { get; }
    protected abstract float MaxDamage { get; }
    protected bool isAttackCooldown = false;
    protected Vector2 facing;

    protected Animator animator { get => GetComponent<Animator>(); }

    protected virtual void Start()
    {
        hitPoints = MaxHitPoints;
    }

    protected virtual void Update()
    {
        animator.SetFloat("Speed_f", rb.velocity.magnitude);
    }

    protected void Move(Vector2 direction)
    {
        if (GameManager.Instance.gameOver)
        {
            rb.velocity = Vector2.zero;
            rb.Sleep();
            return;
        }
        rb.velocity = direction * MoveSpeed;
        // ABSTRACTION
        FaceCardinalDirection(direction);
    }

    protected void FaceCardinalDirection(Vector2 direction)
    {
        facing = direction.x > 0 && direction.x > Mathf.Abs(direction.y) ? Vector2.right
            : direction.x < 0 && -direction.x > Mathf.Abs(direction.y) ? Vector2.left
                : direction.y > 0 ? Vector2.up
                    : direction.y < 0 ? Vector2.down
                        : facing;
        switch (facing.x, facing.y)
        {
            case (0, 1):
                animator.SetInteger("Facing_i", 0);
                break;
            case (1, 0):
                animator.SetInteger("Facing_i", 1);
                break;
            case (0, -1):
                animator.SetInteger("Facing_i", 2);
                break;
            case (-1, 0):
                animator.SetInteger("Facing_i", 3);
                break;
        }
//        Debug.Log($"{gameObject.name} is facing ({facing.x},{facing.y})");
        // TODO: add some code to make the sprite show the one facing that direction
    }

    protected virtual void Attack(Creature target)
    {
        if (isAttackCooldown) { return; } // empty return stops function here if executed
        // ABSTRACTION
        if (target != null && IsInRange(target)) { DamageTarget(target); }
        animator.SetTrigger("Attack_t");
        isAttackCooldown = true;
        StartCoroutine(AttackCooldownCoroutine());
    }

    protected bool IsInRange(Creature target)
    {
        float distanceToTarget = (target.transform.position - transform.position).magnitude;
        distanceToTarget -= BoxColliderRangeDiff(gameObject);
        distanceToTarget -= BoxColliderRangeDiff(target.gameObject);
        return distanceToTarget < AttackRange;
    }

    protected float BoxColliderRangeDiff(GameObject obj)
    {
        BoxCollider2D box = obj.GetComponent<BoxCollider2D>();
        return box == null ? 0 : facing.y == 0 ? box.size.x / 2 : box.size.y / 2;
    }

    protected virtual void DamageTarget(Creature target)
    {
        float damage = Random.Range(MinDamage, MaxDamage);
        target.HitPoints -= Mathf.RoundToInt(damage);
        Debug.Log($"{gameObject.name} hit {target.gameObject.name} for {damage}");
    }

    protected IEnumerator AttackCooldownCoroutine()
    {
        yield return new WaitForSeconds(AttackCooldown);
        isAttackCooldown = false;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        Debug.Log($"{gameObject.name} died");
    }

    // some code to fix an issue where without using physics movement creatures walked through walls but with them they push each other around
    // I tried googling the problem and found this: https://forum.unity.com/threads/prevent-rigidbodies-from-pushing-each-other.228870/#post-7883485
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.otherRigidbody != null)
        {
            Vector2 dirVec = collision.transform.position - transform.position;
            Vector2 velInDir = (Vector2)Vector3.Project(rb.velocity, dirVec);
            if (Vector3.Dot(velInDir.normalized, dirVec.normalized) > 0)
            {
                rb.velocity -= velInDir;
            }
        }
    }
}