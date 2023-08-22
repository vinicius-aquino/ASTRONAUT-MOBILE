using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvencible : PowerUpBase
{

    protected override void StartPowerUp()
    {
        base.StartPowerUp();
        PlayerController.Instance.SetPowerUpText("Invencible");
        PlayerController.Instance.SetInvencible();
    }

    protected override void EndPowerUp()
    {
        base.EndPowerUp();
        PlayerController.Instance.SetPowerUpText("");
        PlayerController.Instance.SetInvencible(false);
    }
}
