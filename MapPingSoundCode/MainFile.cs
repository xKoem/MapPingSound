using BaseLib.Config;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace MapPingSound.MapPingSoundCode;

//You're recommended but not required to keep all your code in this package and all your assets in the MapPingSound folder.
[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "MapPingSound"; //At the moment, this is used only for the Logger and harmony names.

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } =
        new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        Harmony harmony = new(ModId);
        ModConfigRegistry.Register(ModId, new MapPingSoundConfig());


        harmony.PatchAll();
    }
    
    [HarmonyPatch(typeof(AudioStreamPlayer), "Play")]
    public class ReplaceMapOpenSound
    {
        static void Prefix(AudioStreamPlayer __instance)
        {
            if (__instance.Stream == null)
                return;

            var path = __instance.Stream.ResourcePath;

            if (path != null && path.Contains("map_ping.mp3"))
            {
                if (MapPingSoundConfig.Sound == Sound.Default)
                {
                    return;
                }
                
                __instance.Stream = ResourceLoader.Load<AudioStream>(
                    SoundPathMapper.Path(MapPingSoundConfig.Sound)
                );
            }
        }
    }
}