using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObstacle : HeathMeter
{
    private void Awake()
    {
        Died += Death;
    }
    private void Start()
    {
        ExposeHealth();
    }
    private void Death()
    {
        Destroy(gameObject);
    }
}
