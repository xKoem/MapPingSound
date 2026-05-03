namespace MapPingSound.MapPingSoundCode;

public static class SoundPathMapper
{
    public static String Path(Sound sound)
    {
        return $"res://MapPingSound/debug_audio/map_ping_{sound.ToString().ToLowerInvariant()}.mp3";
    }
}