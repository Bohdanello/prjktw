internal class BibliotekaOnline
{
    protected readonly Biblioteka biblioteka;

    public BibliotekaOnline()
    {
        biblioteka = Biblioteka.Instance;
    }

    public void Uruchom()
    {
        Console.WriteLine($"Uruchomiono {this}\n");

        Wypelnij();
        WyswietlPozycje();
        WyszukajPozycje();
        FiltrujPozycje();

        // Klasa Narrator może przeczytać Książkę jako AudioKsiążkę (jest adapterem)
        OtworzPozycje();
    }

    public virtual int Wypelnij()
    {
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("aaabbbb", "123456789", "Ksiazka", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("abc123", "Hello, World!", "Muzyka", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("aaabbbb", "123456789", "AudioKsiazka", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("Przykładowy tytuł", "123456789", "Filmy", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("aduawidhadw", "123456789", "Filmy", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("hello", "123456789", "Ksiazka", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("aaabbbb", "123456789", "Ksiazka", new DateTime(2000, 1, 1)));

        return biblioteka.Count();
    }

    public virtual void WyswietlPozycje()
    {
        Console.WriteLine(biblioteka.GetInformacje());
    }

    private static void WyswietlPozycje(IEnumerable<IPozycja> pozycje)
    {
        Console.WriteLine(Biblioteka.GetInformacje(pozycje));
    }

    public void WyszukajPozycje()
    {
        Console.WriteLine("Wyszukaj pozycje: ");
        IPozycja wyszukana = Wyszukaj();

        Console.WriteLine("Znaleziono taka pozycje: ");
        Console.WriteLine(wyszukana.GetInformacje());
    }

    private static string ZamienZnaki(string text)
    {
        var specialChars = new Dictionary<char, char>()
        {
            { 'ą', 'a' },
            { 'ę', 'e' },
            { 'ł', 'l' },
            { 'ó', 'o' },
            { 'ń', 'n' },
            { 'ż', 'z' },
            { 'ź', 'z' }
        };

        var wynik = text;

        foreach (var kv in specialChars)
        {
            wynik = wynik.Replace(kv.Key, kv.Value);
        }

        return wynik;
    }

    public virtual void FiltrujPozycje()
    {
        var opcje = "[ " + String.Join(", ", FabrykaPozycji.ObslugiwanieTypy.ToArray()) + " ]";

        Console.WriteLine($"Wprowadź typ pozycji {opcje}: ");
        
        var wczytane = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(wczytane);

        var typ = FabrykaPozycji.WyszukajTyp(ZamienZnaki(wczytane));

        while (typ is null)
        {
            Console.WriteLine($"Nie znaleziono typu pozycji, spróbuj ponownie");
            Console.WriteLine($"Wprowadź typ pozycji {opcje}: ");

            wczytane = Console.ReadLine();
            ArgumentNullException.ThrowIfNull(wczytane);
            typ = FabrykaPozycji.WyszukajTyp(ZamienZnaki(wczytane));
        }

        var przefiltrowane = biblioteka.Filtruj(typ);

        WyswietlPozycje(przefiltrowane);
    }

    protected void FiltrujPozycje(Type typ)
    {
        WyswietlPozycje(biblioteka.Filtruj(typ));
    }

    public void OtworzPozycje()
    {
        Console.WriteLine("Otworz pozycje: ");
        IPozycja wyszukana = Wyszukaj();

        wyszukana.Otworz();
    }

    protected virtual IPozycja Wyszukaj()
    {
        string wyszukiwanie = Console.ReadLine()!;

        IPozycja? wyszukanaPoz = biblioteka.WyszukajPozycje(wyszukiwanie).FirstOrDefault();

        while (wyszukanaPoz is null)
        {
            Console.WriteLine("Nie znaleziono pozycji.");
            wyszukiwanie = Console.ReadLine()!;
            wyszukanaPoz = biblioteka.WyszukajPozycje(wyszukiwanie).FirstOrDefault();
        }

        return wyszukanaPoz;
    }

    protected static string Wczytaj(Predicate<string> pred, string walidacjaMsg)
    {
        if (String.IsNullOrEmpty(walidacjaMsg))
        {
            throw new ArgumentException("Parametr walidacjaMsg nie może być pusty");
        }

        string? wczytane = Console.ReadLine();
        ArgumentNullException.ThrowIfNull(wczytane);

        while (!pred(wczytane))
        {
            Console.WriteLine(walidacjaMsg);
            wczytane = Console.ReadLine();
            ArgumentNullException.ThrowIfNull(wczytane);
        }
        return wczytane;
    }

    protected static bool WczytajPotwierdzenie(string potwierdzenieMsg)
    {
        var opcje = new Dictionary<char, bool>()
        {
            { 't', true },
            { 'n', false }
        };

        char takNie()
        {
            return (Console.ReadKey().KeyChar).ToString().ToLower()[0];
        }

        char wczytaj()
        {
            Console.Write($"{potwierdzenieMsg} [t/N]: ");
            char odpowiedz = takNie();
            Console.WriteLine("\n");

            return odpowiedz;
        }

        bool jestPoprawnie(char odpowiedz)
        {
            return opcje.ContainsKey(odpowiedz);
        }

        bool potwierdzono(char odpowiedz)
        {
            return opcje![odpowiedz];
        }

        char odpowiedz = wczytaj();

        while (!jestPoprawnie(odpowiedz))
        {
            odpowiedz = wczytaj();
        }

        return potwierdzono(odpowiedz);
    }

    public virtual void DodajPozycje()
    {
        var opcje = "[ " + String.Join(", ", FabrykaPozycji.ObslugiwanieTypy.ToArray()) + " ]";

        Console.WriteLine("Dodaj pozycje: {");
        Console.WriteLine("\tTytul: ");
        string tytul = Wczytaj(t => t.Length > 4, "Tytuł musi zawierać co najmniej 4 znaki.");
        Console.WriteLine("\tOpis: ");
        string opis = Wczytaj(o => o.Length > 8, "Opis musi zaierać co najmniej 8 znaków.");
        Console.WriteLine($"\tTyp {opcje}: ");
        string typ = Wczytaj(t => t.Length > 0, "Nazwa typu nie może być pusta");
        Pozycja.WyszukajTypThrow(typ);

        Console.WriteLine("\tData publikacji: ");
        DateTime dataPublikacji = DateTime.Now;
        string dtString = Wczytaj(dp => {
            bool parsed = DateTime.TryParse(dp, out dataPublikacji);
            bool valid = dataPublikacji <= DateTime.Now;
            return parsed && valid;
        }, "Wprowadzono niepoprwaną datę publikacji.");

        var wynik = Pozycja.StworzPozycje(tytul, opis, typ, dataPublikacji);

        biblioteka.DodajPozycje(wynik);

        Console.WriteLine($"Pozycja {wynik} została dodana.");
    }

    public void UsunPozycje()
    {
        Console.Write("Usuń pozycję: ");
        IPozycja wyszukana = Wyszukaj();

        if (WczytajPotwierdzenie("Naprawdę chcesz usunąć tą pozycję?"))
        {
            biblioteka.UsunPozycje(wyszukana);
        }
    }

    public override string ToString()
    {
        return "BibliotekaOnline";
    }
}
