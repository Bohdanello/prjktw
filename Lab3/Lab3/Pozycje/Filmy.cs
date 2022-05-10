[Etykieta("filmy")]
internal class Filmy : Pozycja
{
    public Filmy(
        string tytul,
        string opis,
        DateTime dataPublikacji) : base(typeof(Filmy), tytul, opis, dataPublikacji) { }

    public override string ToString()
    {
        return "Filmy";
    }

    public override void Otworz()
    {
        new OdtwarzaczWideo().Odtworz(this);
    }
}