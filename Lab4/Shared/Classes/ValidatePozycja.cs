public static class PozycjaValidator
{
    public static bool IsValid(Pozycja pozycja)
    {
        return pozycja.DataPublikacji < DateTime.Now && !String.IsNullOrEmpty(pozycja.Tytul) && !String.IsNullOrEmpty(pozycja.Opis);
    }
}
