using BepInEx;
using System;

namespace Creative_tools
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    [BepInProcess("DSPGAME.exe")]
    public class CreativeTools : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "nl.jessestam.creativeStuff";
        public const string PLUGIN_NAME = "creativeStuff";
        public const string PLUGIN_VERSION = "1.0.2.0";

        private void Awake()
        {
        }
    }
}