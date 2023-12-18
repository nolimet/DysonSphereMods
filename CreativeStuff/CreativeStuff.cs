using BepInEx;
using BepInEx.Configuration;
using CreativeStuff.Data;
using HarmonyLib;
using UnityEngine;

namespace CreativeStuff
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInProcess("DSPGAME.exe")]
    public class CreativeStuff : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "nl.jessestam.creativeStuff";
        public const string PLUGIN_NAME = "creativeStuff";
        public const string PLUGIN_VERSION = "1.0.6.0";

        public static SplitterConfig SplitterConfig;

        private Harmony harmony;

        private void Awake()
        {
            SplitterConfig = new(Config);

          
            Config.Save();

            harmony = new Harmony(PLUGIN_GUID);
            harmony.PatchAll(typeof(SplitterPatch));
        }

        private void OnSprayLevelChanged(string arg0)
        {
            if (byte.TryParse(arg0, out var sprayLevel))
            {
                SplitterConfig.SprayLevel.Value = sprayLevel;
            }
        }

        private void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }
}