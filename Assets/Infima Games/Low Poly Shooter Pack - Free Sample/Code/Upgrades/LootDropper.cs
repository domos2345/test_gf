using System.Collections;
using System.Collections.Generic;
using Infima_Games.Low_Poly_Shooter_Pack___Free_Sample.Code.Upgrades;
using UnityEngine;
using UnityEngine.InputSystem;

public class LootDropper : MonoBehaviour
{
    private List<UpgradeData> allUpgradeBonusOrbs;

    // Start is called before the first frame update
    void Awake()
    {
        allUpgradeBonusOrbs = new List<UpgradeData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LogAllLootDrops();
        }
    }

    private void LogAllLootDrops()
    {
        string log = "All Loot drops :\n";
        foreach (UpgradeData upgradeData in allUpgradeBonusOrbs)
        {
            log += upgradeData + "\n==========================================\n";
        }

        Debug.Log(log);
    }

    public void AddUpgradeData(UpgradeData upgradeData)
    {
        allUpgradeBonusOrbs.Add(upgradeData);
    }

    public void RemoveUpgrade(UpgradeData upgradeData)
    {
        allUpgradeBonusOrbs.Remove(upgradeData);
    }
}