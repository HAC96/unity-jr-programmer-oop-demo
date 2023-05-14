using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : Creature // INHERITANCE
{
    protected abstract float DetectionRange { get; }
    protected Vector2 toPlayer;
    protected Player player;
    protected virtual LayerMask mask
    {
        get => LayerMask.GetMask("Player", "Obstacle");
    }

    // POLYMORPHISM
    protected override void Start()
    {
        player = FindObjectOfType<Player>();
        base.Start();
    }

    protected virtual void FixedUpdate()
    {
        if (player == null) {
            player = FindObjectOfType<Player>();
            Debug.Log("Couldn't find Player reference");
            return;
        }
        toPlayer = player.transform.position - transform.position;
        if (CanSeePlayer() && IsInRange(player)) // ABSTRACTION
        {
 //           Debug.Log($"{gameObject.name} is attacking the player");
            FaceCardinalDirection(toPlayer);
            Attack(player);
            if (player.HitPoints <= 0) { GameManager.Instance.killedBy = gameObject.name; }
        }
        else if (CanSeePlayer())
        {
//            Debug.Log($"{gameObject.name} is chasing the player");
            Move(toPlayer);
        }
    }

    protected bool CanSeePlayer()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, toPlayer, DetectionRange, mask);
//        Debug.DrawRay(transform.position, toPlayer, Color.red);
//        Debug.Log($"{gameObject.name} can see {hits[0].transform.gameObject.name}");
        if (hits.Length == 0) { return false; }
        return hits[0].transform.gameObject.CompareTag("Player"); // true if first object hit by ray is player
    }
}
