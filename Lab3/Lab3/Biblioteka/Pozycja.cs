internal abstract class Pozycja : FabrykaPozycji, IPozycja
{
    protected string Tytul { get; set; }
    protected string Opis { get; set; }
    protected Type Typ { get; set; }
    protected DateTime DataPublikacji { get; set; }

    protected Pozycja(Type typ, string tytul, string opis, DateTime dataPublikacji)
    {
        Typ = typ;
        Tytul = tytul;
        Opis = opis;
        DataPublikacji = dataPublikacji;
    }
    
    public String GetInformacje()
    {
        return Tytul + " " + Opis + " " + ToString() + " " + DataPublikacji.ToString();
    }

    public Type GetTyp()
    {
        return Typ;
    }

    public abstract void Otworz();

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            throw new Exception("wystapil blad");
        }

        if (obj is not Pozycja innaPozycja)
        {
            throw new Exception("wystapil blad");
        }

        return innaPozycja.Tytul.Equals(Tytul) && innaPozycja.Opis.Equals(Opis) && innaPozycja.Typ.Equals(Typ) && innaPozycja.DataPublikacji.Equals(DataPublikacji);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}