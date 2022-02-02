using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace TestMod
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInProcess("DSPGAME.exe")]
    public class TestMod : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "nl.jessestam.creativeStuff";
        public const string PLUGIN_NAME = "creativeStuff";
        public const string PLUGIN_VERSION = "1.0.2.0";

        public static ConfigEntry<byte> ouputStacksize;
        public static ConfigEntry<byte> sprayCount;

        private Harmony harmony;

        private void Awake()
        {
            ouputStacksize = Config.Bind<byte>("General", "Stacksize", 4, "The output stack size");
            sprayCount = Config.Bind<byte>("General", "SprayCount", 4, "How much spray has been applied to the output?");

            Config.Save();

            harmony = new Harmony(PLUGIN_GUID);
            harmony.PatchAll(typeof(Patches));
        }

        private void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }
}