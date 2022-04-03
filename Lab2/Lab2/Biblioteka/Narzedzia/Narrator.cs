
internal class Narrator : OdtwarzaczMuzyki
{
    private Czytnik czytnik;

    public Narrator(Czytnik czytnik)
    {
        this.czytnik = czytnik;
    }

    public override void Odtworz(IPozycja pozycja)
    {
        if (pozycja is not Ksiazka && pozycja is not AudioKsiazka)
        {
            throw new InvalidOperationException($"Narrator nie może przeczytać tą pozycję: {pozycja.GetInformacje()}, ponieważ nie jest ona książką bądź audio książką.");
        }
        if (pozycja is Ksiazka)
        {
            czytnik.WyswietlTekst(pozycja);
        }
        string typPozycjiSlownie = pozycja is Ksiazka ? "książkę" : "audio książkę";
        Console.WriteLine($"Słuchasz {typPozycjiSlownie}: {pozycja.GetInformacje()}");
    }
}
