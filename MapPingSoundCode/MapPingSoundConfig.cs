using BaseLib.Config;

namespace MapPingSound.MapPingSoundCode;

public class MapPingSoundConfig: SimpleModConfig
{
    public static Sound Sound { get; set; } = Sound.Quiet;
}