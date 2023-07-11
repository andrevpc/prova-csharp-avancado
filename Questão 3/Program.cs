using System;
using System.Collections.Generic;

Chefe chefePrincipal = new();
Chefe chefeSub1 = new();
Chefe chefeSub2 = new();

chefePrincipal.Add(chefeSub1);
chefePrincipal.Add(chefeSub2);

Secretario secretario = new();

chefeSub1.Add(secretario);
secretario.Add(new Programador());
secretario.Add(new Programador());
secretario.Add(new Estagiario());

chefeSub2.Add(new Programador());
chefeSub2.Add(new Programador());
chefeSub2.Add(new Estagiario());

string msg = "Importante";

chefeSub1.Receber(msg);
chefeSub2.Receber(msg);

public abstract class Empregado
{
    public abstract void Receber(string msg);
}

public class Chefe : Empregado //Composite
{
    private List<Empregado> list = new List<Empregado>();
    public IEnumerable<Empregado> Classes => list;
    public void Add(Empregado obj)
        => list.Add(obj);

    public override void Receber(string msg)
    {
        if (!msg.Contains("Confidencial"))
        {
            foreach (var obj in list)
                obj.Receber(msg);
        }
    }
}
public class Secretario : Empregado //Composite
{
    private List<Empregado> list = new List<Empregado>();
    DecoratorConfidencial dc = new DecoratorConfidencial();
    DecoratorImportante di = new DecoratorImportante();
    DecoratorMemorando dm = new DecoratorMemorando();
    public IEnumerable<Empregado> Classes => list;
    public void Add(Empregado obj)
        => list.Add(obj);

    public override void Receber(string msg)
    {
        foreach (var obj in list)
        {
            if (msg.Contains("Confidencial"))
            {
                dc.Wrapped = obj;
                
                dc.Receber(msg);
            }
            else if (msg.Contains("Importante"))
            {
                di.Wrapped = obj;
                di.Receber(msg);
            }
            
            else if (msg.Contains("Memorando"))
            {
                dm.Wrapped = obj;
                dm.Receber(msg);
            }
            else
                obj.Receber(msg);
        }
    }
}

public class DecoratorConfidencial : Empregado
{
    public Empregado Wrapped { get; set; }
    public override void Receber(string msg)
    {
        Wrapped.Receber("Confidencial: " + msg);
    }
}

public class DecoratorImportante : Empregado
{
    public Empregado Wrapped { get; set; }
    public override void Receber(string msg)
    {
        Wrapped.Receber("Importante: " + msg);
    }
}

public class DecoratorMemorando : Empregado
{
    public Empregado Wrapped { get; set; }
    public override void Receber(string msg)
    {
        Wrapped.Receber("Memorando: " + msg);
    }
}

public class Estagiario : Empregado
{
    public override void Receber(string msg)
    {
        if(!msg.Contains("Confidencial"))
            System.Console.WriteLine(msg);
    }
}
public class Programador : Empregado
{
    public override void Receber(string msg)
        => System.Console.WriteLine("Mensagem Recebida");
}