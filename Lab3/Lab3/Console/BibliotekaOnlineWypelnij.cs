internal class BibliotekaOnlineWypelnij : BibliotekaOnline
{
    public override int Wypelnij()
    {
        for (int i = 0; i < 3; i++)
        {
            DodajPozycje();
        }

        return biblioteka.Count();
    }

    public override string ToString()
    {
        return "BibliotekaOnlineWypelnij";
    }
}
