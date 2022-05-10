using System.Text.RegularExpressions;

internal abstract class FabrykaPozycji
{
    private static readonly List<Type> obslugiwanePozycje = new() {
            typeof(Muzyka),
            typeof(Filmy),
            typeof(Ksiazka),
            typeof(AudioKsiazka)
        };

    public static IEnumerable<string> ObslugiwanieTypy
    {
        get
        {
            return obslugiwanePozycje.Select(t => GetEtykieta(t).Wartosc);
        }
    }

    public static Etykieta GetEtykieta(Type typ)
    {
        Attribute? attr = Attribute.GetCustomAttributes(typ).ToList().Find(a => a is Etykieta);
        ArgumentNullException.ThrowIfNull(attr);
        return (Etykieta)attr;
    }

    public static Type? WyszukajTyp(string wyszukwanie)
    {
        return obslugiwanePozycje.Select(typ =>
        {
            var etykieta = GetEtykieta(typ);
            string wartosc = etykieta.Wartosc;

            return (typ, wartosc);
        })
        .Where(t => t.wartosc.Contains(wyszukwanie))
        .Select(t => t.typ)
        .FirstOrDefault();
    }

    public static Type WyszukajTypThrow(string wyszukiwanie)
    {
        Type? t = WyszukajTyp(new Regex("\\s").Replace(wyszukiwanie.Trim().ToLower(), String.Empty));

        if (t is null)
        {
            throw new EntryPointNotFoundException("Nie znaleziono typu pozycji");
        }

        return t;
    }

    public static Pozycja StworzPozycje(string tytul, string opis, string typ, DateTime dataPublikacji)
    {
        Type? t = WyszukajTyp(new Regex("\\s").Replace(typ.Trim().ToLower(), String.Empty));

        if (t is null)
        {
            throw new EntryPointNotFoundException("Nie znaleziono typu pozycji");
        }

        object? o = Activator.CreateInstance(t, new object[] { tytul, opis, dataPublikacji });

        ArgumentNullException.ThrowIfNull(o);

        Pozycja p = (Pozycja)o;

        return p;
    }
}