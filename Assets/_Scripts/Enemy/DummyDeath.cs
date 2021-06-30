using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyDeath : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private AnimationClip animation;

    private void Awake()
    {
    }
    private void OnEnable()
    {

        StartCoroutine(Die());
    }
    //public void Death()
    //{
    //    StartCoroutine(Die());
    //}
    private IEnumerator Die()
    {
        _animator.SetBool("Reboot", false);

        yield return new WaitForSeconds(animation.length);
        _animator.SetBool("Reboot",true);
        gameObject.SetActive(false);
    }

}
