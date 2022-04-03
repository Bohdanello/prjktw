using System.Text;
using static Pozycja;

internal class Biblioteka
{
    private static Biblioteka? _instance;

    private List<Pozycja> pozycje;
    private int id;

    public int LiczbaPozycji { get { return pozycje.Count; } }

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
    
    public void DodajPozycje(Pozycja pozycja)
    {
        pozycje.Add(pozycja);
    }

    public void UsunPozycje(Pozycja pozycja)
    {
        if (!pozycje.Remove(pozycja))
        {
            throw new ApplicationException("Nie uda³o siê usun¹æ pozycji");
        }
    }

    public List<Pozycja> WyszukajPozycje(string wartosc)
    {
        return pozycje.Where(p => p.GetInformacje().Contains(wartosc)).ToList();
    }

    public String GetInformacje()
    {
        if (pozycje.Any())
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Pozycje w bibliotece({pozycje.Count}):");
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
}
