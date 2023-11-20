using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infima_Games.Low_Poly_Shooter_Pack___Free_Sample.Code.Upgrades
{
    public class UpgradeData : MonoBehaviour
    {
        // [SerializeField, Tooltip("sprinting buff in %.")]
        // private float sprintingSpeedBuff;
        //
        // [SerializeField, Tooltip("reload time buff in %.")]
        // private float reloadSpeedBuff;
        //
        // [SerializeField, Tooltip("ammo capacity for all weapons buff in %.")]
        // private float ammoCapacityBuff;

        [SerializeField, Tooltip("All buffs in % value.")]
        private List<UpgradeBonus> upgrades = new List<UpgradeBonus>();

        private LootDropper lootDropper;


        // for spawning upgrade orbs
        private bool isAboveTheGround;
        private float floatDuration = 1f;
        private float desiredHeight = 0.5f;


        private void Start()
        {
            lootDropper = FindObjectOfType<LootDropper>();
            lootDropper.AddUpgradeData(this);
        }

        private void Update()
        {
            if (isAboveTheGround) return;

            StartCoroutine(FloatToObjectHeight());
            isAboveTheGround = true;
        }

        private IEnumerator FloatToObjectHeight()
        {
            Vector3 initialPosition = transform.position;

            Vector3 targetPosition = new Vector3(initialPosition.x, 0, initialPosition.z) + Vector3.up * desiredHeight;

            float elapsedTime = 0f;

            while (elapsedTime < floatDuration)
            {
                // Interpolate between initial and target positions over time
                transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / floatDuration);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            // Ensure the final position is exactly the target position
            transform.position = targetPosition;
            isAboveTheGround = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            PlayerUpgrades playerUpgrades = other.gameObject.GetComponent<PlayerUpgrades>();
            if (playerUpgrades == null)
            {
                return;
            }

            playerUpgrades.OnUpgradePickUp(this);
            lootDropper.RemoveUpgrade(this);
            Destroy(gameObject);
        }

        public List<UpgradeBonus> GetUpgrades()
        {
            return upgrades;
        }

        public override string ToString()
        {
            string str = "Name: " + name + "\n";
            str += "Position: " + transform.position + "\n";

            foreach (var upgradeBonus in upgrades)
            {
                str += upgradeBonus.type + ": " + upgradeBonus.bonusValue + "%\n";
            }

            return str;
        }
    }

    [Serializable]
    public class UpgradeBonus
    {
        public UpgradeType type;

        public float bonusValue;
    }
}