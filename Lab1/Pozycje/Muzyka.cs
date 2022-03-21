internal class Muzyka : Pozycja
{
    public Muzyka(string tytul, string opis, DateTime dataPublikacji) : base(tytul, opis, dataPublikacji)
    {
        Typ = "Muzyka";
    }
}