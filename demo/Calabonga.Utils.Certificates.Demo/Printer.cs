namespace Calabonga.Utils.Certificates.Demo;

public static class Printer
{
    private const int TotalWidth = 80;

    public static void PrintTitle(string title)
    {
        Console.WriteLine("");
        Console.WriteLine("".PadRight(TotalWidth, '-'));
        Console.WriteLine(title);
    }

    public static void PrintCollection(List<CertificateInfo> items)
    {
        if (items.Any())
        {
            foreach (var info in items)
            {
                Console.WriteLine("".PadRight(TotalWidth, '-'));
                Console.WriteLine(info.ToString());
            }
            Console.WriteLine("".PadRight(TotalWidth, '-'));
            Console.WriteLine($"Totally found items: {items.Count}");
        }
        else
        {
            Console.WriteLine("".PadRight(TotalWidth, '-'));
            Console.WriteLine("Not found.");
        }
    }
}