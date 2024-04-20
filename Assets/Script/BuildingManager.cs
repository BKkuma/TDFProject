using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager main;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPhefab;
    [SerializeField] private Tower[] towers;

    private int selectTower = 0;

    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[selectTower];
    }

    public void SetSelectedTower(int _selectedtower)
    {
        selectTower = _selectedtower;
    }
}
