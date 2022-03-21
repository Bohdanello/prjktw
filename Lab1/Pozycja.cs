internal abstract class Pozycja : IPozycja
{
    protected string Tytul { get; set; }
    protected string Opis { get; set; }
    protected string Typ { get; set; }
    protected DateTime DataPublikacji { get; set; }

    protected Pozycja(string tytul, string opis, DateTime dataPublikacji)
    {
        Tytul = tytul;
        Opis = opis;
        DataPublikacji = dataPublikacji;
    }

    public String GetInformacje()
    {
        return Tytul + " " + Opis + " " + Typ + " " + DataPublikacji.ToString();
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            throw new Exception("wystapil blad");
        }

        Pozycja innaPozycja = obj as Pozycja;

        if (innaPozycja == null)
        {
            throw new Exception("wystapil blad");
        }

        return innaPozycja.Tytul.Equals(Tytul) && innaPozycja.Opis.Equals(Opis) && innaPozycja.Typ.Equals(Typ) && innaPozycja.DataPublikacji.Equals(DataPublikacji);
    }
}