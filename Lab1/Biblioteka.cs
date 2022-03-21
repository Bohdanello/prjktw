#nullable enable

using Lab1.Pozycje;

internal class Biblioteka : FabrykaPozycji
{
    private static Biblioteka _instance;

    private List<Pozycja> pozycje;
    private int id;

    private Biblioteka()
    {
        id = new Random().Next();
        pozycje = new List<Pozycja>();
    }

    public static Biblioteka Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Biblioteka();
            }

            return _instance;
        }
    }

    public override Pozycja StworzPozycje(string tytul, string opis, string typ, DateTime dataPublikacji)
    {
        switch (typ)
        {
            case "AudioKsiazka":
                return new AudioKsiazka(tytul, opis, dataPublikacji);
            case "Filmy":
                return new Filmy(tytul, opis, dataPublikacji);
            case "Ksiazka":
                return new Ksiazka(tytul, opis, dataPublikacji);
            case "Muzyka":
                return new Muzyka(tytul, opis, dataPublikacji);
            default:
                throw new ApplicationException("Nie znaleziono typu pozycji");
        }
    }

    public void DodajPozycje(Pozycja pozycja)
    {
        pozycje.Add(pozycja);
        Console.WriteLine("Dodano pozycje: " + pozycja.GetInformacje());
    }

    public void UsunPozycje(Pozycja pozycja)
    {
        /*int id = pozycje.FindIndex(0, 1, p => p.Equals(pozycja));
        pozycje.RemoveAt(id);*/
        pozycje.Remove(pozycja);
    }

    public Pozycja? WyszukajPozycje(string wartosc)
    {
        return pozycje.Find(p => p.GetInformacje().Contains(wartosc));
    }

    public String GetInformacje()
    {
        if (pozycje.Count > 0)
        {
            Console.WriteLine("Pozycje w bibliotece(" + pozycje.Count + "): ");
            return pozycje
                .Select((p, i) => (p, i))
                .Select(element => element.i.ToString() + ": " + element.p.GetInformacje())
                .Aggregate((acc, val) => acc + "\n" + val);
        }
        return "Biblioteka jest pusta";        
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            throw new Exception("wystapil blad");
        }

        Biblioteka innaBiblioteka = obj as Biblioteka;

        if (innaBiblioteka == null)
        {
            throw new Exception("wystapil blad");
        }

        return innaBiblioteka.id == this.id;
    }
}
