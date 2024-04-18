using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEnemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;


    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0)
        {
            EnamySpawner.onEnamyDestroy.Invoke();
            Destroy(gameObject);
        }
    }
 
}
