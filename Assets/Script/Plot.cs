using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;


    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (tower != null) return;

        Tower towerToBuild = BuildingManager.main.GetSelectedTower();

        if (towerToBuild.coust > LevelManager.main.currency)
        {
            Debug.Log("You not have Coin");
            return;
        }
        
        LevelManager.main.SpendCurrency(towerToBuild.coust);
        tower = Instantiate(towerToBuild.phefab, transform.position, Quaternion.identity);
    }
}
