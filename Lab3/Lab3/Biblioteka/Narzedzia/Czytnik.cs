internal class Czytnik : ICzytnik
{
    public void WyswietlTekst(IPozycja pozycja)
    {
        if (pozycja is not Ksiazka)
        {
            throw new InvalidOperationException("Pozycja nie jest książką. Otrzymałem: " + pozycja.GetInformacje());
        }

        Console.WriteLine($"Czytasz książkę: {pozycja.GetInformacje()}");
    }
}
