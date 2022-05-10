public static class LosowaPozycja
{
    private static string[] a = new [] { "Jan Kowalski", "fesfafdadw", "Andrzej Duda", "jogfreojo" };
    private static string[] t = new [] { "abc1232", "aaaaabbbbbbccccc", "Przykładowy tytuł", "Hello, World!", "Tytuł" };
    private static string[] o = new [] { "Przykładowy opis", "Hello, World!", "To jest opis", "uhduiehfriueahfsihsaihsis" };
    private static string[] r = new [] { "Książka", "Audio Książka", "Muzyka", "Filmy" };
    private static string RndItem(string[] values) => values[new Random().Next(0, values.Length)];
    public static Pozycja Pozycja
    {
        get
        {
            return new Pozycja() { Author = RndItem(a), Tytul = RndItem(t), Opis = RndItem(o), Rodzaj = RndItem(r), DataPublikacji = new DateTime(new Random().Next(1800, DateTime.Now.Year + 1), new Random().Next(1, 13), new Random().Next(1, 28)) };
        }
    }
}
