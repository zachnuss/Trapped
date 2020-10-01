using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manage store UI Buttons
/// </summary>

public class StoreUI : MonoBehaviour
{
    public PlayerData playerData;

    //when player is ready to go to next level
    public void OnNextLevelButton()
    {
        playerData.LoadNextLevel();
    }

    //when player buys an upgrade
    public void OnDamageUpgradeButton()
    {
        playerData.UpgradeDamage();
    }

    public void OnHealthUpgradeButton()
    {
        playerData.UpgradeHealth();
    }

    public void OnSpeedUpgradeButton()
    {
        playerData.UpgradeSpeed();
    }
}
