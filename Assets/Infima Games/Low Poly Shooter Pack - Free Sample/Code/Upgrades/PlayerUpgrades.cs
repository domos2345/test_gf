using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Infima_Games.Low_Poly_Shooter_Pack___Free_Sample.Code.Upgrades
{
    public class PlayerUpgrades : MonoBehaviour
    {
        [SerializeField] private List<Upgrade> upgrades = new List<Upgrade>();

        [SerializeField] private UpgradesUI upgradesUI;

        [SerializeField] private GameObject upgradeTextUI;

        [SerializeField] private Transform rowUI;

        void Awake()
        {
            foreach (Upgrade upgrade in upgrades)
            {
                GameObject textUIGo = Instantiate(upgradeTextUI, rowUI);
                upgrade.InitUI(textUIGo);
                upgrade.UpdateUI();

                //upgradesUI.DisplayUpgrade(textUIGo);
            }
        }

        public Upgrade GetUpgradeOfType(UpgradeType type)
        {
            foreach (var upgrade in upgrades)
            {
                if (upgrade.type == type)
                {
                    return upgrade;
                }
            }

            Debug.Log(
                "Type not found in list, returning null as default. Check if you added new bonus type for player upgrades");
            return null;
        }


        public void OnUpgradePickUp(UpgradeData upgradeData)
        {
            // update upgrades of player by adding bonus from upgrade orb (UpgradeData)
            foreach (var upgradeBonus in upgradeData.GetUpgrades())
            {
                Upgrade myUpgrade = GetUpgradeOfType(upgradeBonus.type);
                myUpgrade.currentValue = Clamp(myUpgrade.currentValue, upgradeBonus.bonusValue, myUpgrade.maxValue);
                myUpgrade.UpdateUI();
            }
        }

        private float Clamp(float actValue, float addValue, float maxValue)
        {
            float newValue = actValue + addValue;
            if (newValue < maxValue)
            {
                return newValue;
            }

            return maxValue;
        }

        public float GetCurrentBonusValueOfUpgrade(UpgradeType type)
        {
            return GetUpgradeOfType(type).currentValue / 100;
        }
    }

    [Serializable]
    public class Upgrade
    {
        public UpgradeType type;
        public float maxValue;
        public float currentValue;
        public string textValueUI;
        public TMP_Text displayValueTextField;

        public void UpdateUI()
        {
            displayValueTextField.text = textValueUI + " : " + currentValue + "%";
        }

        public void InitUI(GameObject textUIGo)
        {
            TMP_Text textUI = textUIGo.GetComponent<TMP_Text>();
            textUI.fontSize = 14;
            displayValueTextField = textUI;
        }
    }

    public enum UpgradeType
    {
        SPRINT_SPEED,
        RELOAD_SPEED,
        AMMO_CAPACITY_BONUS
    }
}