using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;

namespace TestMod
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInProcess("DSPGAME.exe")]
    public class CreativeStuff : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "nl.jessestam.creativeStuff";
        public const string PLUGIN_NAME = "creativeStuff";
        public const string PLUGIN_VERSION = "1.0.4.0";

        public static ConfigEntry<byte> outputStacksize;
        public static byte sprayCount;
        public static ConfigEntry<byte> sprayLevel;

        private Harmony harmony;

        private void Awake()
        {
            outputStacksize = Config.Bind<byte>("General", "Stacksize", 4, "The output stack size (Range 1-8) above that has not been tested");
            sprayLevel = Config.Bind<byte>("General", "SprayLevel", 3, "How much spray has been applied to the output? (Range 0-3) above has not been tested");

            sprayLevel.Value = (byte)Mathf.Clamp(sprayLevel.Value, 0, 3);
            outputStacksize.Value = (byte)Mathf.Clamp(outputStacksize.Value, 0, 8);
            Config.Save();

            sprayCount = (byte)(sprayLevel.Value * outputStacksize.Value);

            harmony = new Harmony(PLUGIN_GUID);
            harmony.PatchAll(typeof(Patches));
        }

        private void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }
}