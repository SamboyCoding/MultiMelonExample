namespace MultiMelonExample;

public class MultiMelonSubModAttribute : Attribute
{
    public string Name;
    public string Version;
    public string Author;
    
    public MultiMelonSubModAttribute(string name, string version, string author)
    {
        Name = name;
        Version = version;
        Author = author;
    }
}