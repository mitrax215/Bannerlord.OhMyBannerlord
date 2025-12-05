using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.GameComponents;

namespace Bannerlord.OhMyBannerlord.Patches

{
    [HarmonyPatch]
    public static class RemoveFogOfWarPatch

    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Hero), "IsKnownToPlayer",MethodType.Getter)]
        public static bool IsKnownToPlayer(ref bool __result)
        {
            if (Configs.Instance == null || !Configs.Instance.EnableForHeroes)
            {
                return true;
            }

            __result = true;

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(DefaultInformationRestrictionModel), "DoesPlayerKnowDetailsOf", new System.Type[] { typeof(Settlement) })]
        public static bool DoesPlayerKnowDetailsOf(ref bool __result)
        {
            if (Configs.Instance == null || !Configs.Instance.EnableForFiefs)
            {
                return true;
            }

            __result = true;

            return false;
        }
    }
}