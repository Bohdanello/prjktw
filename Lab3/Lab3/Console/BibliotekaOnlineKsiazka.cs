internal class BibliotekaOnlineKsiazka : BibliotekaOnline
{
    public override void WyswietlPozycje()
    {
        FiltrujPozycje(typeof(Ksiazka));
    }

    public override void FiltrujPozycje()
    {
        // Nic nie rób
        // Wszystko jest książką
    }

    protected override IPozycja Wyszukaj()
    {
        string wyszukiwanie = Console.ReadLine()!;

        var wyszukanaPoz = biblioteka.Filtruj(typeof(Ksiazka)).Where(p => p.GetInformacje().ToLower().Contains(wyszukiwanie)).FirstOrDefault();

        while (wyszukanaPoz is null)
        {
            Console.WriteLine("Nie znaleziono pozycji.");
            wyszukiwanie = Console.ReadLine()!;
            wyszukanaPoz = biblioteka.WyszukajPozycje(wyszukiwanie).FirstOrDefault();
        }

        return wyszukanaPoz;
    }

    public override void DodajPozycje()
    {
        Console.WriteLine("Dodaj książkę: {");
        Console.WriteLine("\tTytul: ");
        string tytul = Wczytaj(t => t.Length > 4, "Tytuł musi zawierać co najmniej 4 znaki.");
        Console.WriteLine("\tOpis: ");
        string opis = Wczytaj(o => o.Length > 8, "Opis musi zaierać co najmniej 8 znaków.");

        Console.WriteLine("\tData publikacji: ");
        DateTime dataPublikacji = DateTime.Now;
        string dtString = Wczytaj(dp => {
            bool parsed = DateTime.TryParse(dp, out dataPublikacji);
            bool valid = dataPublikacji <= DateTime.Now;
            return parsed && valid;
        }, "Wprowadzono niepoprwaną datę publikacji.");

        Console.WriteLine("}");

        var typ = FabrykaPozycji.GetEtykieta(typeof(Ksiazka)).Wartosc;
        var wynik = Pozycja.StworzPozycje(tytul, opis, typ, dataPublikacji);

        biblioteka.DodajPozycje(wynik);

        Console.WriteLine($"Pozycja {wynik} została dodana.");
    }

    public override string ToString()
    {
        return "BibliotekaOnlineKsiazka";
    }
}
