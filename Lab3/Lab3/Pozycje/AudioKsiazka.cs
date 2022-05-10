[Etykieta("audioksiazka")]
internal class AudioKsiazka : Pozycja
{
    public AudioKsiazka(
        string tytul,
        string opis,
        DateTime dataPublikacji) : base(typeof(AudioKsiazka), tytul, opis, dataPublikacji) { }

    public override string ToString()
    {
        return "Audio Ksi��ka";
    }

    public override void Otworz()
    {
        new Narrator(new Czytnik()).Odtworz(this);
    }
}