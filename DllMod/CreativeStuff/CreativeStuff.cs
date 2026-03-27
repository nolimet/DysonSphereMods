using BepInEx;
using CreativeStuff.Data;
using HarmonyLib;

namespace CreativeStuff;

[BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
[BepInProcess("DSPGAME.exe")]
public class CreativeStuff : BaseUnityPlugin
{
    public const string PLUGIN_GUID = "nl.jessestam.creativeStuff";
    public const string PLUGIN_NAME = "creativeStuff";
    public const string PLUGIN_VERSION = "1.0.7.0";

    public static SplitterConfig SplitterConfig;

    private Harmony harmony;

    private void Awake()
    {
        SplitterConfig = new SplitterConfig(Config);

        Config.Save();

        harmony = new Harmony(PLUGIN_GUID);
        harmony.PatchAll(typeof(SplitterPatch));
    }

    private void OnDestroy()
    {
        harmony.UnpatchSelf();
    }
}
