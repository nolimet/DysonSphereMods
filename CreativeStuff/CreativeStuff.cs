using BepInEx;
using BepInEx.Configuration;
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

        public static ConfigEntry<byte> outputStacksize;
        public static ConfigEntry<byte> sprayLevel;

        private Harmony harmony;

        private void Awake()
        {
            outputStacksize = Config.Bind<byte>("General", "Stacksize", 4, "The output stack size (Range 1-8) above that has not been tested");
            sprayLevel = Config.Bind<byte>("General", "SprayLevel", 4 * 4, "How much spray has been applied to the output? (Range 0-255) the values seems to scale in some odd way");

            sprayLevel.Value = (byte)Mathf.Clamp(sprayLevel.Value, 0, 255);
            outputStacksize.Value = (byte)Mathf.Clamp(outputStacksize.Value, 1, 8);
            Config.Save();

            harmony = new Harmony(PLUGIN_GUID);
            harmony.PatchAll(typeof(Patches));

            //value debug code
            //CanvasBehaviour canvas = CanvasBehaviour.Create();
            //canvas.transform.SetParent(transform);
            //canvas.AddContent(InputBehaviour.Create("Spray Level", OnSprayLevelChanged).transform);
        }

        private void OnSprayLevelChanged(string arg0)
        {
            if (byte.TryParse(arg0, out var sprayLevel))
            {
                CreativeStuff.sprayLevel.Value = sprayLevel;
            }
        }

        private void OnDestroy()
        {
            harmony.UnpatchSelf();
        }
    }
}