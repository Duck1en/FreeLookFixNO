using BepInEx;
using HarmonyLib;

namespace FreeLookFixNO
{
    [BepInPlugin("com.ducken.nuclearoption.freelookfix", "NO Free Look Fix", "1.0.0")]
    public sealed class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("NO Free Look Fix loaded (Awake)");
            var h = new Harmony("com.ducken.nuclearoption.freelookfix");
            h.PatchAll();
        }

        internal static bool IsFreeLookHeld()
        {
            // Uses the game's action map (Rewired wrapper used by the game)
            return GameManager.playerInput != null && GameManager.playerInput.GetButton("Free Look");
        }
    }
}
