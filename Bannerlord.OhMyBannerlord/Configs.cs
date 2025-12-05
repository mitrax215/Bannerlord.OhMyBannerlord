using MCM.Abstractions.Attributes;
using MCM.Abstractions.Base.Global;
using MCM.Abstractions.Attributes.v2;


namespace Bannerlord.OhMyBannerlord
{
    public class Configs : AttributeGlobalSettings<Configs>
    {
        public override string Id => "Bannerlord.OhMyBannerlord";

        public override string DisplayName => "Oh My Bannerlord";

        public override string FolderName => "OhMyBannerlord";

        public override string FormatType => "json2";

        private const string HeadingRemoveFogOfWar = "{=OhMyBannerlord.b5527329}Fog of war";
        private const string HeadingMarriage = "Marriage";

        // Fog of war configs
        [SettingPropertyBool(displayName: "{=OhMyBannerlord.4f8e20f7}Heroes", Order = 0, RequireRestart = false, HintText = "{=OhMyBannerlord.6ffb53b3}It allows the player to get to know all the heroes.")]
        [SettingPropertyGroup(HeadingRemoveFogOfWar)]
        public bool EnableForHeroes { get; set; } = true;

        [SettingPropertyBool(displayName: "{=OhMyBannerlord.6d91efcd}Settlements", Order = 1, RequireRestart = false, HintText = "{=OhMyBannerlord.42c2da62}It allows the player to explore all the settlements.")]
        [SettingPropertyGroup(HeadingRemoveFogOfWar)]
        public bool EnableForFiefs { get; set; } = true;

        // Marriage configs
        [SettingPropertyBool(displayName: "Player receives marriage offers", Order = 1, RequireRestart = false, HintText = "Allows the player to receive marriage offer.")]
        [SettingPropertyGroup(HeadingMarriage)]
        public bool PlayerReceivesMarriageOffers { get; set; } = true;

        [SettingPropertyFloatingInteger("Marriage offer possibility", 1f, 9f,RequireRestart = false,HintText = "Increases marriage offer possibility.")]
        [SettingPropertyGroup(HeadingMarriage)]
        public float MarriageOfferPossibility { get; set; } = 1f;
    }
}