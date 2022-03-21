namespace Lab1.Pozycje
{
    internal class Ksiazka : Pozycja
    {
        public Ksiazka(string tytul, string opis, DateTime dataPublikacji) : base(tytul, opis, dataPublikacji)
        {
            Typ = "Ksiazka";
        }
    }
}