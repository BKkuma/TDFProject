using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPEnemy : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    private bool isDestroy = false;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0 && !isDestroy)
        {
            EnamySpawner.onEnamyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroy = true;
            Destroy(gameObject);
        }
    }
 
}
