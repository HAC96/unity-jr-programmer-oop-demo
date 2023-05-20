using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] bool hasMcGuffin;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("Open_t");
            other.GetComponent<Animator>().SetTrigger("Grab_item_t");
            if (hasMcGuffin)
            {
                GameManager.Instance.WinGame();
            }
        }
    }
}
