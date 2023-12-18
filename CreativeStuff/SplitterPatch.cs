using HarmonyLib;
namespace CreativeStuff
{
    public static class SplitterPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CargoTraffic), "UpdateSplitter")]
        public static void CargoTraffic_UpdateSplitter_Prefix(CargoTraffic __instance, ref SplitterComponent sp, long time)
        {
            if (sp.outFilter != 0 && AllInputIsNone(ref sp))
            {
                int filter = sp.outFilter;
                DoOutputForOutput(filter, sp.output0);
                DoOutputForOutput(filter, sp.output1);
                DoOutputForOutput(filter, sp.output2);
                DoOutputForOutput(filter, sp.output3);
            }

            static bool AllInputIsNone(ref SplitterComponent sp)
            {
                return sp.input0 == 0 && sp.input1 == 0 && sp.input2 == 0 && sp.input3 == 0;
            }

            void DoOutputForOutput(int filter, int output)
            {
                if (output <= 0) return;

                var outputPath = __instance.GetCargoPath(__instance.beltPool[output].segPathId);
                if (outputPath != null && outputPath.TestBlankAtHead() == 0)
                {
                    outputPath.TryInsertItemAtHead(filter, CreativeStuff.SplitterConfig.OutputStackSize.Value, CreativeStuff.SplitterConfig.SprayLevel.Value);
                }
            }
        }
    }
}