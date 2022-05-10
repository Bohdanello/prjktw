using System.Collections;

internal class BibliotekaEnumerator : IEnumerator<IPozycja>
{
    private readonly IEnumerator<IPozycja> enumerator;

    public IPozycja Current => enumerator.Current;

    object IEnumerator.Current => enumerator.Current;

    public BibliotekaEnumerator(IEnumerable<IPozycja> pozycje)
    {
        enumerator = pozycje.GetEnumerator();
    }

    public void Dispose()
    {
        enumerator.Dispose();
    }

    public virtual bool MoveNext()
    {
        return enumerator.MoveNext();
    }

    public void Reset()
    {
        enumerator.Reset();
    }
}
