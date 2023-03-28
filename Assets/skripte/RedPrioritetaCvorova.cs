using System.Collections.Generic;
using System.Linq;

public class RedPrioritetaCvorova
{
    private readonly List<Cvor> cvorovi = new();

    public int DuzinaListeCvorova { get => cvorovi.Count; }

    public bool SadrziCvor(Cvor cvor) => cvorovi.Contains(cvor);

    public Cvor IzbrisiPrviCvor()
    {
        if (cvorovi.Count == 0)
            return null;

        var cvor = cvorovi[0];
        cvorovi.RemoveAt(0);
        return cvor;
    }

    public void DodajCvor(Cvor cvor)
    {
        if (cvorovi.Contains(cvor))
        {
            var node = cvorovi.First(n => n.Equals(cvor));
            if (node.fVrednost <= cvor.fVrednost)
                return;
            else
                cvorovi.Remove(node);
        }
        cvorovi.Add(cvor);
        cvorovi.Sort((n1, n2) => n1.fVrednost < n2.fVrednost ? -1 : 1);
    }
}