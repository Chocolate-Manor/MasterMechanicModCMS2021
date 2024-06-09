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

                // this do nothing, but keeping it here in case future changes
                GlobalData.XrayPartNormal = 0f;
            }
        }


        [HarmonyPatch(typeof(PartScript), nameof(PartScript.Show))]
        class PartScriptShowPatch
        {
            static void Postfix(PartScript __instance)
            {
                MelonLogger.Msg("PartScript.Show!");
                __instance.Alpha0();

                foreach (PartScript partScript in __instance.unmountWith)
                {
                    partScript.Alpha0();
                }
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
