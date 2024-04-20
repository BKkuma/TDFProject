
using System;
using UnityEngine;

[Serializable]
public class Tower
{
    public string name;
    public int coust;
    public GameObject phefab;

    public Tower (string _name, int _coust, GameObject _phefab)
    {
        name = _name;
        coust = _coust;
        phefab = _phefab;
    }
}
