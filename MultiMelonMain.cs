using System.Reflection;
using MelonLoader;

[assembly:MelonInfo(typeof(MultiMelonExample.MultiMelonMain), "MultiMelonExample.Core", "1.0.0", "Samboy063")]
[assembly:MelonGame] // Universal

namespace MultiMelonExample;

public class MultiMelonMain : MelonMod
{
    public override void OnPreSupportModule()
    {
        OnRegister.Subscribe(OnRegistered);
        LoggerInstance.Msg("MultiMelonMain - OnPreSupportModule. Subscribed to OnRegister.");
    }

    private void OnRegistered()
    {
        LoggerInstance.Msg("MultiMelonMain - OnRegistered");
        var subMods = GetType().Assembly.GetTypes().Where(t => t.GetCustomAttribute<MultiMelonSubModAttribute>() != null).ToList();
        
        var gamesProp = typeof(MelonBase).GetProperty(nameof(Games), BindingFlags.Public | BindingFlags.Instance)!;
        var infoProp = typeof(MelonBase).GetProperty(nameof(Info), BindingFlags.Public | BindingFlags.Instance)!;
        var melonAssemblyProp = typeof(MelonBase).GetProperty(nameof(MelonAssembly), BindingFlags.Public | BindingFlags.Instance)!;
        var consoleColorProp = typeof(MelonBase).GetProperty(nameof(ConsoleColor), BindingFlags.Public | BindingFlags.Instance)!;
        var authorConsoleColorProp = typeof(MelonBase).GetProperty(nameof(AuthorConsoleColor), BindingFlags.Public | BindingFlags.Instance)!;
        
        var subModInstances = subMods.Select(t =>
        {
            var mod = (MelonBase)(Activator.CreateInstance(t) ?? throw new($"Failed to construct a sub mod of type {t}"));
            
            var subModAtt = t.GetCustomAttribute<MultiMelonSubModAttribute>()!;

            //Basic impl, feel free to support more attributes here
            gamesProp.SetValue(mod, Games); //Needed to actually load
            infoProp.SetValue(mod, new MelonInfoAttribute(t, subModAtt.Name, subModAtt.Version, subModAtt.Author)); //Needed for names etc
            melonAssemblyProp.SetValue(mod, MelonAssembly); //Needed to not throw in RegisterSorted
            consoleColorProp.SetValue(mod, ConsoleColor); //Needed for logging to not have the name be black text on black bg
            authorConsoleColorProp.SetValue(mod, AuthorConsoleColor); //As above
            

            return mod;
        }).ToList();
        
        MelonBase.RegisterSorted(subModInstances);
    }
}