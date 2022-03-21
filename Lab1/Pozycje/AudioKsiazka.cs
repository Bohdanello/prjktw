namespace Lab1.Pozycje
{
    internal class AudioKsiazka : Pozycja
    {
        public AudioKsiazka(string tytul, string opis, DateTime dataPublikacji) : base(tytul, opis, dataPublikacji)
        {
            Typ = "Audio Ksiazka";
        }
    }
}