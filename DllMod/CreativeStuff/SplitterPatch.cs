using HarmonyLib;

namespace CreativeStuff;

public static class SplitterPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(CargoTraffic), "UpdateSplitter")]
    [HarmonyPatch(typeof(CargoTraffic), "UpdateSplitterAsync")]
    public static void CargoTraffic_UpdateSplitter_Prefix(CargoTraffic __instance, ref SplitterComponent sp)
    {
        if (sp.outFilter != 0 && AllInputIsNone(ref sp))
        {
            var filter = sp.outFilter;
            TryDoOutputForOutput(filter, sp.output0);
            TryDoOutputForOutput(filter, sp.output1);
            TryDoOutputForOutput(filter, sp.output2);
            TryDoOutputForOutput(filter, sp.output3);
        }

        static bool AllInputIsNone(ref SplitterComponent sp)
        {
            return sp is { input0: 0, input1: 0, input2: 0, input3: 0 };
        }

        void TryDoOutputForOutput(int filter, int output)
        {
            if (output <= 0) return;

            var outputPath = __instance.GetCargoPath(__instance.beltPool[output].segPathId);
            if (outputPath != null && outputPath.TestBlankAtHead() == 0)
                outputPath.TryInsertItemAtHeadAndFillBlank(filter, CreativeStuff.SplitterConfig.OutputStackSize.Value, CreativeStuff.SplitterConfig.SprayLevel.Value);
        }
    }
}
