using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : HeathMeter
{
    void Start()
    {
        Died += Сollapse;
        ExposeHealth();
    }
    private void Сollapse() => Destroy(gameObject);
}
