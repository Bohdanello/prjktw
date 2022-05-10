[AttributeUsage(AttributeTargets.Class)]
internal class Etykieta : Attribute
{
    private readonly string wartosc;

    public string Wartosc { get { return wartosc; } }

    public Etykieta(string wartosc)
    {
        this.wartosc = wartosc;
    }
}
