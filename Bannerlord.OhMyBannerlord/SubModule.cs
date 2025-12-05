using HarmonyLib;
using TaleWorlds.MountAndBlade;

namespace Bannerlord.OhMyBannerlord
{
    public class SubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            _ = Configs.Instance;

            var harmony = new Harmony("com.bannerlord.ohmybannerlord");

            harmony.PatchAll();
        }
    }
}