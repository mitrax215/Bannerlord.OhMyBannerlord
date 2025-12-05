using HarmonyLib;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.CampaignBehaviors;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace Bannerlord.OhMyBannerlord.Patches
{
    [HarmonyPatch(typeof(MarriageOfferCampaignBehavior))]
    class MarriageOfferCampaignBehaviorPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch("DailyTickClan")]
        public static bool DailyTickClan(Clan consideringClan, MarriageOfferCampaignBehavior __instance, Dictionary<Hero, Hero> ____acceptedMarriageOffersThatWaitingForAvailability)
        {
            var CanOfferMarriageForClanMethod = AccessTools.Method(typeof(MarriageOfferCampaignBehavior), "CanOfferMarriageForClan");
            var ConsiderMarriageForPlayerClanMemberMethod = AccessTools.Method(typeof(MarriageOfferCampaignBehavior), "ConsiderMarriageForPlayerClanMember");

            if ((bool)CanOfferMarriageForClanMethod.Invoke(__instance, new object[] { consideringClan }));
            {
                MobileParty.NavigationType navigationType = consideringClan.HasNavalNavigationCapability ? MobileParty.NavigationType.All : MobileParty.NavigationType.Default;

                float distance = Campaign.Current.Models.MapDistanceModel.GetDistance(Clan.PlayerClan.FactionMidSettlement, consideringClan.FactionMidSettlement, false, false, navigationType);

                if (MBRandom.RandomFloat >= distance / Campaign.Current.Models.MapDistanceModel.GetMaximumDistanceBetweenTwoConnectedSettlements(navigationType) - 0.5f)
                {
                    foreach (Hero hero in Clan.PlayerClan.Heroes)
                    {
                        if (Configs.Instance.PlayerReceivesMarriageOffers && hero.CanMarry() && !____acceptedMarriageOffersThatWaitingForAvailability.ContainsKey(hero) && !(bool)ConsiderMarriageForPlayerClanMemberMethod.Invoke(__instance,new object[] { hero, consideringClan }))
                        {
                            break;
                        }
                    }
                }
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("ConsiderMarriageForPlayerClanMember")]
        public static bool ConsiderMarriageForPlayerClanMember(Hero playerClanHero, Clan consideringClan, MarriageOfferCampaignBehavior __instance,ref bool __result)
        {
            MarriageModel marriageModel = Campaign.Current.Models.MarriageModel;

            foreach (Hero hero in consideringClan.Heroes)
            {
                float num = marriageModel.NpcCoupleMarriageChance(playerClanHero, hero);

                var randomMarriageOfferPossibility = MBRandom.RandomFloat - (Configs.Instance.MarriageOfferPossibility * 0.001f);

                if (num > 0f && randomMarriageOfferPossibility < num)
                {
                    foreach (Romance.RomanticState romanticState in Romance.RomanticStateList)
                    {
                        if (romanticState.Level >= Romance.RomanceLevelEnum.MatchMadeByFamily && (romanticState.Person1 == playerClanHero || romanticState.Person2 == playerClanHero || romanticState.Person1 == hero || romanticState.Person2 == hero))
                        {
                            __result = false;
                        }
                    }

                    __instance.CreateMarriageOffer(playerClanHero, hero);

                    __result = true;
                }
            }

            __result = false;

            return false;
        }
    }
}
