using MonoMod.RuntimeDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace JammedLotJ
{
    public class JammedLotJModule : ETGModule
    {
        public override void Init()
        {
        }

        public override void Start()
        {
            new Hook(typeof(PlayerStats).GetMethod("RecalculateStatsInternal", BindingFlags.Public | BindingFlags.Instance), typeof(JammedLotJModule).GetMethod("AddJamToLotJ", BindingFlags.Public | BindingFlags.Static));
        }

        public static void AddJamToLotJ(Action<PlayerStats, PlayerController> orig, PlayerStats self, PlayerController player)
        {
            orig(self, player);
            if(PlayerStats.GetTotalCurse() >= 20 && GameManager.Instance != null && GameManager.Instance.Dungeon != null && GameManager.Instance.Dungeon.CurseReaperActive && SuperReaperController.Instance != null)
            {
                BecomeJammedLord(SuperReaperController.Instance);
            }
        }

        public static void BecomeJammedLord(SuperReaperController controller)
        {
            if (controller.GetComponent<JammedLordController>() != null)
            {
                return;
            }
            controller.gameObject.AddComponent<JammedLordController>().Initialize(controller);
        }

        public static void UnbecomeJammedLord(SuperReaperController controller)
        {
            if (controller.GetComponent<JammedLordController>() != null)
            {
                controller.GetComponent<JammedLordController>().Uninitialize();
            }
        }

        public override void Exit()
        {
        }
    }
}
