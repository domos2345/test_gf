using System.Collections.Generic;
using UnityEngine;

namespace Infima_Games.Low_Poly_Shooter_Pack___Free_Sample.Code.Upgrades
{
    public class UpgradeOrbHolder : MonoBehaviour
    {
        [SerializeField] private List<GameObject> allPossibleOrbDrops;


        public void DropRandomOrb(Vector3 pos)
        {
            Instantiate(allPossibleOrbDrops[Random.Range(0, allPossibleOrbDrops.Count)], pos,
                Quaternion.identity);
        }
    }
}