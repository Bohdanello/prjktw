using System.Collections;
using System.Text;

internal class Biblioteka : IEnumerable<IPozycja>
{
    private static Biblioteka? _instance;

    private readonly IList<IPozycja> pozycje;
    private readonly int id;

    private Biblioteka()
    {
        id = new Random().Next();
        pozycje = new List<IPozycja>();
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
    
    public void DodajPozycje(IPozycja pozycja)
    {
        pozycje.Add(pozycja);
    }

    public void UsunPozycje(IPozycja pozycja)
    {
        if (!pozycje.Remove(pozycja))
        {
            throw new ApplicationException("Nie uda³o siê usun¹æ pozycji");
        }
    }

    public List<IPozycja> WyszukajPozycje(string wartosc)
    {
        return this.Where(p => p.GetInformacje().Contains(wartosc)).ToList();
    }

    public string GetInformacje()
    {
        return GetInformacje(this);
    }

    public static string GetInformacje(IEnumerable<IPozycja> pozycje)
    {
        if (pozycje.Any())
        {
            StringBuilder sb = new();

            sb.Append($"Pozycje w bibliotece({pozycje.Count()}):");
            sb.Append("\n\n");
            sb.Append(pozycje
                        .Select((p, i) => (p, i))
                        .Select(element => (element.i + 1).ToString() + ": " + element.p.GetInformacje())
                        .Aggregate("", (acc, val) => acc + val + "\n"));

            return sb.ToString();
        }
        return "Biblioteka jest pusta";
    }

    public override bool Equals(object? obj)
    {
        Biblioteka? innaBiblioteka = (obj as Biblioteka);

        if (obj is null || innaBiblioteka is null)
        {
            return false;
        }

        return innaBiblioteka.id == this.id;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public IEnumerable<IPozycja> Filtruj(Type typ)
    {
        return this.Where(p => p.GetTyp().Equals(typ));
    }

    public IEnumerator<IPozycja> GetEnumerator()
    {
        return new BibliotekaEnumerator(pozycje);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new BibliotekaEnumerator(pozycje);
    }
}
