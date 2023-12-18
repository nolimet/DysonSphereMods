using UnityEngine;

namespace NoUtil.Async.Internal
{
    public class WaitForUpdate : CustomYieldInstruction
    {
        public override bool keepWaiting
        {
            get { return false; }
        }
    }
}