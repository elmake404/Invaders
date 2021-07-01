using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlay : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particle;
    private void OnEnable()
    {
        _particle.Play();
    }
}
