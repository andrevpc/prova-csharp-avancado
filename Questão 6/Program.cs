using System;
using System.Reflection;

Clear clear = new Clear();

string input = Console.ReadLine();

var assembly = Assembly.GetExecutingAssembly();

foreach (var type in assembly.GetTypes())
{
    var att = type.GetCustomAttribute<CommandAttribute>();
    
    if(att is not null && input == type.Name)
    {
        var met = type.GetMethod("Run");
        met.Invoke(clear, new object[] { });
    }
}

[CommandAttribute]
public class Clear
{
    public void Run()
    {
        System.Console.Clear();
        
    }
}


public class CommandAttribute : Attribute
{ }