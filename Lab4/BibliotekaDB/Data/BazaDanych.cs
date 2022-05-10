public class BazaDanych
{
    private readonly List<Pozycja> pozycje;
    private bool seeded;

    public IEnumerable<Pozycja> Pozycje => pozycje; 

    public BazaDanych()
    {
        pozycje = new List<Pozycja>();
        seeded = false;
    }

    public void Seed()
    {
        if (!seeded)
        {
            for (int i = 0; i < 1000; i++)
            {
                pozycje.Add(LosowaPozycja.Pozycja);
            }

            seeded = true;
        }
    }

    public IEnumerable<Pozycja> Szukaj(string wyszukiwanie)
    {
        return pozycje.FindAll(p => p.Contains(wyszukiwanie));
    }
}
