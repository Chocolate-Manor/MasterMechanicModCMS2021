using MelonLoader;
using HarmonyLib;
using UnityEngine;
using Il2Cpp;


namespace MasterMechanicCMS2021Mod
{
    public class MyMod : MelonMod
    {

        [HarmonyPatch(typeof(GameMode), nameof(GameMode.SetCurrentMode))]
        class ShowHideItemsToMountPatch
        {
            static void Prefix(gameMode newGameMode)
            {
                MelonLogger.Msg("GameMode.SetCurrentMode!");
                GlobalData.XrayBodyNormal = 0f;
            }
        }

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Master Mechanic Mod Initialized");

            HarmonyInstance.PatchAll();

            MelonLogger.Msg("Harmony PatchAll Called");
        }
    }
}
