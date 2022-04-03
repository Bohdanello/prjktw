#nullable enable

Biblioteka biblioteka = Biblioteka.Instance;
Console.Write("Następująca instancja biblioteki jest tą samą: ");
Console.WriteLine(biblioteka.Equals(Biblioteka.Instance) ? "Tak" : "Nie");

biblioteka.DodajPozycje(biblioteka.StworzPozycje("Jeff Bezos", "Amazon", "Ksiazka", new DateTime(2000, 1, 1)));
biblioteka.DodajPozycje(biblioteka.StworzPozycje("Elon Mask", "Tesla", "AudioKsiazka", new DateTime(2000, 1, 1)));
biblioteka.DodajPozycje(biblioteka.StworzPozycje("Feliks Kurp", "Programowanie", "Ksiazka", new DateTime(2000, 1, 1)));
biblioteka.DodajPozycje(biblioteka.StworzPozycje("Agnieszka Peszek", "I co dalej?", "AudioKsiazka", new DateTime(2000, 1, 1)));
biblioteka.DodajPozycje(biblioteka.StworzPozycje("Karin Slaughter", "Blondynka, niebieskie oczy", "Ksiazka", new DateTime(2000, 1, 1)));
biblioteka.DodajPozycje(biblioteka.StworzPozycje("Edgar Allan Poe", "Czarny kot", "Ksiazka", new DateTime(2000, 1, 1)));
biblioteka.DodajPozycje(biblioteka.StworzPozycje("Aleksander Fredro", "Zemsta", "Ksiazka", new DateTime(2000, 1, 1)));

bool petla = true;
int liczba = 7;
int licznikUsunietych = 0;

while (petla)
{
    Console.WriteLine(biblioteka.GetInformacje());

    if (licznikUsunietych == liczba)
    {
        return;
    }

    Console.WriteLine("Wyszukaj pozycje: ");
    string wyszukiwanie = Console.ReadLine();

    Pozycja? wyszukanaPoz = biblioteka.WyszukajPozycje(wyszukiwanie);

    while (wyszukanaPoz == null)
    {
        Console.WriteLine("Nie znaleziono pozycji.");
        wyszukiwanie = Console.ReadLine();
        wyszukanaPoz = biblioteka.WyszukajPozycje(wyszukiwanie);
    }

    Console.WriteLine("Znaleziono taka pozycje: ");
    Console.WriteLine(wyszukanaPoz.GetInformacje());

    Console.WriteLine("Chcesz usunac ta pozycje? [t/n]");

    char odpowiedz = Console.ReadKey().KeyChar;

    while (!(odpowiedz.Equals('t') || odpowiedz.Equals('T') || odpowiedz.Equals('N') || odpowiedz.Equals('n'))) {
        Console.WriteLine("Chcesz usunac ta pozycje? [t/n]");
        odpowiedz = Console.ReadKey().KeyChar;
    }

    switch (odpowiedz.ToString().ToLower())
    {
        case "t":
            biblioteka.UsunPozycje(wyszukanaPoz);
            licznikUsunietych++;
            break;
        case "n":
            petla = false;
            break;
        default:
            throw new ApplicationException("wystapil blad");
    }
}