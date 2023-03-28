using System.Collections.Generic;

public class ZvezdaA
{
    private List<Cvor> IzracunajPutanju(Cvor cvor)
    {
        List<Cvor> lista = new();
        while (cvor != null)
        {
            lista.Add(cvor);
            cvor = cvor.roditelj;
        }

        lista.Reverse();
        return lista;
    }

    private float CenaHeuristike(Cvor trenutniCvor, Cvor ciljniCvor)
    {
        return (trenutniCvor.pozicija - ciljniCvor.pozicija).magnitude;
    }
    public List<Cvor> NadjiPut(Cvor pocetniCvor, Cvor ciljniCvor)
    {
        RedPrioritetaCvorova otvorenaLista = new();
        otvorenaLista.DodajCvor(pocetniCvor);
        pocetniCvor.cenaDoSada = 0.0f;
        pocetniCvor.fVrednost = CenaHeuristike(pocetniCvor, ciljniCvor);

        Cvor cvor = null;
        HashSet<Cvor> listaPredjenihCvorova = new();
        while (otvorenaLista.DuzinaListeCvorova != 0)
        {
            cvor = otvorenaLista.IzbrisiPrviCvor();
            if (cvor.pozicija == ciljniCvor.pozicija)
                return IzracunajPutanju(cvor);

            foreach (Cvor komsijskiCvor in Mreza.Instanca.IzvuciKomsije(cvor))
            {
                if (listaPredjenihCvorova.Contains(komsijskiCvor))
                    continue;

                float ukupnaCena = cvor.cenaDoSada + Mreza.Instanca.VelicinaPolja;
                float heuristika = CenaHeuristike(komsijskiCvor, ciljniCvor);

                komsijskiCvor.cenaDoSada = ukupnaCena;
                komsijskiCvor.roditelj = cvor;
                komsijskiCvor.fVrednost = ukupnaCena + heuristika;

                if (!listaPredjenihCvorova.Contains(komsijskiCvor))
                    otvorenaLista.DodajCvor(komsijskiCvor);
            }

            listaPredjenihCvorova.Add(cvor);
        }

        //Ako je prosao sve cvorove i nije uspeo da nadje put
        if (cvor.pozicija != ciljniCvor.pozicija)
            return null;

        return IzracunajPutanju(cvor);
    }
}