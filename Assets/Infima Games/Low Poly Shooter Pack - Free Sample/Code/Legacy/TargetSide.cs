using System;
using UnityEngine;

namespace Infima_Games.Low_Poly_Shooter_Pack___Free_Sample.Code.Legacy
{
    public class TargetSide : MonoBehaviour
    {
        [SerializeField] private bool isRightSide;

        private TargetScript targetScript;

        private void Awake()
        {
            targetScript = GetComponentInParent<TargetScript>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void OnTriggerEnter(Collider other)
        {
            print("targetSide OnTriggerEnter");
            targetScript.HitSide(isRightSide);
        }
    }
}