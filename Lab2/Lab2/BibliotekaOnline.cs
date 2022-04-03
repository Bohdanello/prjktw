internal class BibliotekaOnline
{
    private Biblioteka biblioteka;

    public BibliotekaOnline()
    {
        biblioteka = Biblioteka.Instance;
    }

    public void Uruchom()
    {
        int liczba = Wypelnij();
        WyswietlPozycje();
        WyszukajPozycje();

        // Klasa Narrator może przeczytać Książkę jako AudioKsiążkę (jest adapterem)
        OtworzPozycje();

        bool petla = true;
        int licznikUsunietych = 0;

        while (petla)
        {
            WyswietlPozycje();
            if (licznikUsunietych == liczba)
            {
                petla = false;
            }
            else
            {
                UsunPozycje();
                licznikUsunietych++;
            }
        }
    }

    public int Wypelnij()
    {
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("aaabbbb", "123456789", "Ksiazka", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("abc123", "Hello, World!", "Muzyka", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("aaabbbb", "123456789", "AudioKsiazka", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("Przykładowy tytuł", "123456789", "Filmy", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("aduawidhadw", "123456789", "Filmy", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("hello", "123456789", "Ksiazka", new DateTime(2000, 1, 1)));
        biblioteka.DodajPozycje(Pozycja.StworzPozycje("aaabbbb", "123456789", "Ksiazka", new DateTime(2000, 1, 1)));

        return biblioteka.LiczbaPozycji;
    }

    public void WyswietlPozycje()
    {
        Console.WriteLine(biblioteka.GetInformacje());
    }

    public void WyszukajPozycje()
    {
        Console.WriteLine("Wyszukaj pozycje: ");
        Pozycja wyszukana = Wyszukaj();

        Console.WriteLine("Znaleziono taka pozycje: ");
        Console.WriteLine(wyszukana.GetInformacje());
    }

    public void OtworzPozycje()
    {
        Console.WriteLine("Otworz pozycje: ");
        Pozycja wyszukana = Wyszukaj();

        wyszukana.Otworz();
    }

    private Pozycja Wyszukaj()
    {
        string wyszukiwanie = Console.ReadLine()!;

        Pozycja? wyszukanaPoz = biblioteka.WyszukajPozycje(wyszukiwanie).FirstOrDefault();

        while (wyszukanaPoz is null)
        {
            Console.WriteLine("Nie znaleziono pozycji.");
            wyszukiwanie = Console.ReadLine()!;
            wyszukanaPoz = biblioteka.WyszukajPozycje(wyszukiwanie).FirstOrDefault();
        }

        return wyszukanaPoz;
    }

    private string Wczytaj(Predicate<string> pred, string walidacjaMsg)
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

    public bool WczytajPotwierdzenie(string potwierdzenieMsg)
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
            return opcje.Keys.Contains(odpowiedz);
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

    public void DodajPozycje()
    {
        Console.WriteLine("Dodaj pozycje: {");
        Console.WriteLine("\tTytul: ");
        string tytul = Wczytaj(t => t.Length > 4, "Tytuł musi zawierać co najmniej 4 znaki.");
        Console.WriteLine("\tOpis: ");
        string opis = Wczytaj(o => o.Length > 8, "Opis musi zaierać co najmniej 8 znaków.");
        Console.WriteLine("\tTyp: ");
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
        Console.WriteLine($"Pozycja {wynik} została dodana.");
    }

    public void UsunPozycje()
    {
        Console.Write("Usuń pozycję: ");
        Pozycja wyszukana = Wyszukaj();

        if (WczytajPotwierdzenie("Naprawdę chcesz usunąć tą pozycję?"))
        {
            biblioteka.UsunPozycje(wyszukana);
        }
    }
}
