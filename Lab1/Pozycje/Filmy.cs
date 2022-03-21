namespace Lab1.Pozycje
{
    internal class Filmy : Pozycja
    {
        public Filmy(string tytul, string opis, DateTime dataPublikacji) : base(tytul, opis, dataPublikacji)
        {
            Typ = "Filmy";
        }
    }
}