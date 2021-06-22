using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : HeathMeter
{
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
}
