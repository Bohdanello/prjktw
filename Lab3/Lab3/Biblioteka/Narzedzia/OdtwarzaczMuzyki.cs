internal class OdtwarzaczMuzyki : Odtwarzacz
{
    public override void Odtworz(IPozycja pozycja)
    {
        if (pozycja is not Muzyka && pozycja is not AudioKsiazka)
        {
            throw new InvalidOperationException("Pozycja nie jest muzyką lub audioksiążką. Otrzymałem: " + pozycja.GetInformacje());
        }

        if (pozycja is AudioKsiazka)
        {
            Console.WriteLine($"Słuchasz audio książkę: {pozycja.GetInformacje()}");
        }
        else
        {
            Console.WriteLine($"Odtwarzam utwór (kompozycję): ${pozycja.ToString()}");
        }
    }
}