using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : HeathMeter
{
    public delegate float PassTheNumber();
    public event PassTheNumber GetHealth;

    private void Awake()
    {
        Died += EndOfTheGame;
    }
    private void Start()
    {
        ExposeHealth();
    }
    private void EndOfTheGame()
    {
        GameStage.Instance.ChangeStage(Stage.LostGame);
    }
    private void OnTriggerEnter(Collider other)
    {
        BonusHealth health = other.GetComponent<BonusHealth>();
        if (health != null) Damage(health.GetHelath());
    }
}
