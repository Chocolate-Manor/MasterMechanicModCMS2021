using MelonLoader;
using HarmonyLib;
using UnityEngine;
using Il2Cpp;


namespace MasterMechanicCMS2021Mod
{
    public class MyMod : MelonMod
    {

        [HarmonyPatch(typeof(CarLoader), nameof(CarLoader.ShowHideItemsToMount))]
        class ShowHideItemsToMountPatch
        {
            static void Postfix(CarLoader __instance, bool b)
            {
                MelonLogger.Msg("ShowHideItemsToMount!");

                if (__instance.carParts == null)
                {
                    return;
                }
                for (int i = 0; i < __instance.carParts.Count; i++)
                {
                    CarPart carPart = __instance.carParts[i];
                    if (!carPart.Unmounted)
                    {
                        for (int j = 0; j < carPart.ConnectedParts.Count; j++)
                        {
                            CarPart carPart2 = __instance.GetCarPart(carPart.ConnectedParts[j]);
                            if (carPart2.Unmounted && !carPart.TakeOnOffInProgress && carPart.handle.GetComponent<InteractiveObject>().CanMount())
                            {
                                InteractiveObject component = __instance.GetCarPart(carPart2.name).handle.GetComponent<InteractiveObject>();
                                if (component && carPart2.handle.layer != 13 && carPart2.handle.layer != 14)
                                {
                                    if (b)
                                    {
                                        component.SetLayerRecursively(10); // tried setting layers, although it would make parts uninteractable
                                    }
                                    else
                                    {
                                        component.SetLayerRecursively(24);
                                    }
                                    CarHelper.SetXrayAlpha(carPart2.handle, 0); // try setting alpha as 0 instead.
                                }
                            }
                        }
                    }
                    else if (!__instance.IsMountUnmoutWith(carPart.name))
                    {
                        InteractiveObject component2 = carPart.handle.GetComponent<InteractiveObject>();
                        if (component2 && carPart.handle.layer != 13 && carPart.handle.layer != 14 && !carPart.InProgress && !carPart.TakeOnOffInProgress && carPart.handle.GetComponent<InteractiveObject>().CanMount())
                        {
                            if (b)
                            {
                                component2.SetLayerRecursively(10); 
                            }
                            else
                            {
                                component2.SetLayerRecursively(24);
                            }
                            CarHelper.SetXrayAlpha(carPart.handle, 0);
                        }
                    }
                }
            }
        }

        public override void OnInitializeMelon()
        {
            MelonLogger.Msg("Master Mechanic Mod Initialized");

            HarmonyInstance.PatchAll();

            MelonLogger.Msg("Harmony PatchAll Called");

            //harmony patch all is called automatically
            //harmony patches are also disabled automatically on deInitialize. So nothing needs to be done.
        }
    }
}
