using BepInEx.Configuration;
using UnityEngine;

namespace CreativeStuff.Data;

public  class SplitterConfig
{
    public  ConfigEntry<byte> OutputStackSize;
    public  ConfigEntry<byte> SprayLevel;

    public SplitterConfig(ConfigFile config)
    {
        OutputStackSize = config.Bind<byte>("General", "Stacksize", 4, "The output stack size (Range 1-8) above that has not been tested");
        SprayLevel = config.Bind<byte>("General", "SprayLevel", 4 * 4, "How much spray has been applied to the output? (Range 0-255) the values seems to scale in some odd way");
        
        OutputStackSize.Value = (byte)Mathf.Clamp(OutputStackSize.Value, 1, 8);
        SprayLevel.Value = (byte)Mathf.Clamp(SprayLevel.Value, 0, 255);
    }
  
}