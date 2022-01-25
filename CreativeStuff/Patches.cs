using HarmonyLib;

namespace TestMod
{
    public static class Patches
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(CargoTraffic), "UpdateSplitter")]
        public static bool CargoTraffic_UpdateSplitter_Prefix(CargoTraffic __instance, int id, int input0, int input1, int input2, int output0, int output1, int output2, int filter)
        {
            if (filter != 0 && input0 == 0 && input1 == 0 && input2 == 0)
            {
                OutputForOutput(output0);
                OutputForOutput(output1);
                OutputForOutput(output2);
            }

            void OutputForOutput(int output)
            {
                if (output != 0)
                {
                    var outputPath = __instance.GetCargoPath(__instance.beltPool[output].segPathId);
                    if (outputPath != null && outputPath.TestBlankAtHead() == 0)
                    {
                        outputPath.TryInsertItemAtHead(filter, 1, 1);
                    }
                }
            }

            return true;
        }
    }
}