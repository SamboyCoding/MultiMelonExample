using MelonLoader;

namespace MultiMelonExample;

[MultiMelonSubMod("SubMod1", "1.0.0", "Samboy063")]
public class SubMod1 : MelonMod
{
    public override void OnEarlyInitializeMelon()
    {
        LoggerInstance.Msg("Submod1 OnEarlyInitializeMelon");
    }
}