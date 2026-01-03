using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace FreeLookFixNO.Patches
{
    [HarmonyPatch(typeof(PilotPlayerState), "PlayerAxisControls")]
    internal static class PilotPlayerState_FreeLookFix
    {
        // Replace: Input.GetMouseButton(1)
        // With:    Plugin.IsFreeLookHeld()
        // Only inside this method, only at this call site.
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var getMouseButton = AccessTools.Method(
                typeof(UnityEngine.Input),
                nameof(UnityEngine.Input.GetMouseButton),
                new[] { typeof(int) }
            );

            var isFreeLookHeld = AccessTools.Method(
                typeof(Plugin),
                nameof(Plugin.IsFreeLookHeld)
            );

            var matcher = new CodeMatcher(instructions);

            // Pattern: ldc.i4.1 ; call bool UnityEngine.Input::GetMouseButton(int)
            matcher.MatchForward(
                useEnd: false,
                new CodeMatch(OpCodes.Ldc_I4_1),
                new CodeMatch(OpCodes.Call, getMouseButton)
            );

            if (!matcher.IsValid)
            {
                UnityEngine.Debug.LogWarning("[FreeLookFixNO] Pattern not found in PlayerAxisControls (game update?)");
                return matcher.InstructionEnumeration();
            }

            // Remove the pushed int argument (1), and replace the call
            matcher.SetAndAdvance(OpCodes.Nop, null);                  // nop instead of ldc.i4.1
            matcher.Set(OpCodes.Call, isFreeLookHeld);                 // call Plugin.IsFreeLookHeld()

            return matcher.InstructionEnumeration();
        }
    }
}
