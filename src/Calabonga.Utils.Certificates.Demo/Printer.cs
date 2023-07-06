namespace Calabonga.Utils.Certificates.Demo;

public static class Printer
{
    private const int _totalWidth = 80;

    public static void PrintTitle(string title)
    {
        Console.WriteLine("");
        Console.WriteLine("".PadRight(_totalWidth, '-'));
        Console.WriteLine(title);
    }

    public static void PrintCollection(List<CertificateInfo> items)
    {
        if (items.Any())
        {
            foreach (var info in items)
            {
                Console.WriteLine("".PadRight(_totalWidth, '-'));
                Console.WriteLine(info.ToString());
            }
            Console.WriteLine("".PadRight(_totalWidth, '-'));
            Console.WriteLine($"Totally found items: {items.Count}");
        }
        else
        {
            Console.WriteLine("".PadRight(_totalWidth, '-'));
            Console.WriteLine("Not found.");
        }
    }
}