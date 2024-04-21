using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnamyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;

    private Transform tartget;
    private int pathIndex = 0;

    private float baseSpeed;
    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = moveSpeed;
        tartget = LevelManager.main.path[pathIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(tartget.position,transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex >= LevelManager.main.path.Length)
            {
                EnamySpawner.onEnamyDestroy.Invoke();
                Destroy(gameObject);
                return;
            }
            else
            {
                tartget = LevelManager.main.path[pathIndex];
            }
        }
        
    }
    private void FixedUpdate()
    {
        Vector2 direction = (tartget.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}
