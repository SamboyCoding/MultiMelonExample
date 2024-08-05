using MelonLoader;

namespace MultiMelonExample;

[MultiMelonSubMod("SubMod2", "1.0.0", "Samboy063")]
public class SubMod2 : MelonMod
{
    public override void OnEarlyInitializeMelon()
    {
        LoggerInstance.Msg("Submod2 OnEarlyInitializeMelon");
    }
}