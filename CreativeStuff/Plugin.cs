using BepInEx;
using HarmonyLib;
using System.Threading.Tasks;

namespace TestMod
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInProcess("DSPGAME.exe")]
    public class TestMod : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "nl.jessestam.creativeStuff";
        public const string PLUGIN_NAME = "creativeStuff";
        public const string PLUGIN_VERSION = "1.0.0.0";

        private void Awake()
        {
            Harmony harmony = new Harmony(PLUGIN_GUID);
            harmony.PatchAll(typeof(Patches));
        }
    }
}