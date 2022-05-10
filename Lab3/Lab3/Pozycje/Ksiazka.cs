[Etykieta("ksiazka")]
internal class Ksiazka : Pozycja
{
    public Ksiazka(
        string tytul,
        string opis,
        DateTime dataPublikacji) : base(typeof(Ksiazka), tytul, opis, dataPublikacji) { }

    public override string ToString()
    {
        return "Ksi¹¿ka";
    }

    public override void Otworz()
    {
        new Narrator(new Czytnik()).Odtworz(this);
    }
}