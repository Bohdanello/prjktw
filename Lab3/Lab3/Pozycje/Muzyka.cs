[Etykieta("muzyka")]
internal class Muzyka : Pozycja
{
    public Muzyka(
        string tytul,
        string opis,
        DateTime dataPublikacji) : base(typeof(Muzyka), tytul, opis, dataPublikacji) { }

    public override string ToString()
    {
        return "Muzyka";
    }

    public override void Otworz()
    {
        new OdtwarzaczMuzyki().Odtworz(this);
    }
}