public class Pozycja
{
    public string? Tytul { get; set; }
    public string? Opis { get; set; }
    public string? Rodzaj { get; set; }
    public string? Author { get; set; }
    public DateTime DataPublikacji { get; set; }

    public bool Contains(string wartosc) => (Tytul + Opis + Rodzaj + Author + DataPublikacji.ToString()).ToLower().Replace(" ", "").Contains(wartosc.ToLower().Replace(" ", ""));
}
