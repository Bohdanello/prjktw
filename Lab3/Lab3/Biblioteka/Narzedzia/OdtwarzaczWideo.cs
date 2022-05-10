internal class OdtwarzaczWideo : Odtwarzacz
{
    public override void Odtworz(IPozycja pozycja)
    {
        if (pozycja is Ksiazka)
        {
            throw new InvalidOperationException("Pozycja nie jest filmem. Otrzymałem: " + pozycja.GetInformacje());
        }

        Console.WriteLine($"Odtwarzam film: {pozycja.GetInformacje()}");
    }
}
