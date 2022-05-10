internal abstract class Odtwarzacz : IOdtwarzacz
{
    public virtual void Odtworz(IPozycja pozycja)
    {
        if (pozycja is Ksiazka)
        {
            throw new InvalidOperationException("Pozycja nie może być książką. Otrzymałem: " + pozycja.GetInformacje());
        }

        Console.WriteLine($"Odtwarzam pozycje: {pozycja.GetInformacje()}");
    }
}
