using Grpc.Core;
using Grpc.Net.Client;
using Zad1;

var channel = GrpcChannel.ForAddress("https://localhost:7075");
var client = new KorisnikService.KorisnikServiceClient(channel);
string? unos;

try
{
    do
    {
        Console.WriteLine("Dodaj korisnika - 1");
        Console.WriteLine("Dodaj vise korisnika - 2");
        Console.WriteLine("Obrisi korisnika - 3");
        Console.WriteLine("Obrisi vise korisnika - 4");
        Console.WriteLine("Vrati korisnike - 5");
        Console.WriteLine("Prekini sa izvrsenjem programa - 6");
        unos = Console.ReadLine();
        switch (unos)
        {
            case "1":
                await DodajKorisnika();
                break;
            case "2":
                await DodajViseKorisnika();
                break;
            case "3":
                await ObrisiKorisnika();
                break;
            case "4":
                await ObrisiViseKorisnika();
                break;
            case "5":
                await PrikaziListuKorisnika();
                break;
            case "6":
                Console.WriteLine("Prekid programa!");
                break;
            default:
                break;
        }
    } while (unos != "6");
} catch (HttpRequestException)
{
    Console.WriteLine("Server nije spreman!");
}

async Task DodajKorisnika()
{
    Console.WriteLine("Unesite id korisnika:");
    int id = int.Parse(Console.ReadLine());
    Console.WriteLine("Unesite ime korisnika:");
    string? ime = Console.ReadLine();
    Console.WriteLine("Unesite prezime korisnika:");
    string? prezime = Console.ReadLine();
    Console.WriteLine("Unesite adresu korisnika:");
    string? adresa = Console.ReadLine();
    List<string> brojeviTelefona = new List<string>();
    string? broj;
    Console.WriteLine("Unesite brojeve telefona korisnika:");
    do
    {
        broj = Console.ReadLine();
        if (!string.IsNullOrEmpty(broj))
            brojeviTelefona.Add(broj);
    } while (!string.IsNullOrEmpty(broj));

    try
    {
        var request = new Korisnik
        {
            Id = id,
            Ime = ime,
            Prezime = prezime,
            Adresa = adresa,
        };
        var response = await client.DodajKorisnikaAsync(request);
        Console.WriteLine(response.Tekst);
    }
    catch (RpcException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
async Task DodajViseKorisnika() 
{
    List<Korisnik> noviKorisnici = new List<Korisnik>();
    Console.WriteLine("Unesite broj korisnika koje zelite da dodate:");
    int brojKorisnika = int.Parse(Console.ReadLine());
    for (int i = 0; i < brojKorisnika; i++)
    {
        Console.WriteLine($"Unesite podatke za korisnika {i + 1}:");
        Console.WriteLine("Unesite id korisnika:");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("Unesite ime korisnika:");
        string? ime = Console.ReadLine();
        Console.WriteLine("Unesite prezime korisnika:");
        string? prezime = Console.ReadLine();
        Console.WriteLine("Unesite adresu korisnika:");
        string? adresa = Console.ReadLine();
        List<string> brojeviTelefona = new List<string>();
        string? broj;
        Console.WriteLine("Unesite brojeve telefona korisnika:");
        do
        {
            broj = Console.ReadLine();
            if (!string.IsNullOrEmpty(broj))
                brojeviTelefona.Add(broj);
        } while (!string.IsNullOrEmpty(broj));

        noviKorisnici.Add(new Korisnik
        {
            Id = id,
            Ime = ime,
            Prezime = prezime,
            Adresa = adresa,
            BrojeviTelefona = { brojeviTelefona }
        });
    }

    try
    {
        var call = client.DodajKorisnike();

        var readTask = Task.Run(async() =>
        {
            await foreach (var response in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(response.Tekst);
            }
        });

        foreach (var korisnik in noviKorisnici)
        {
            await call.RequestStream.WriteAsync(korisnik);
        }

        await call.RequestStream.CompleteAsync();
        await readTask;

        Console.WriteLine("Svi korisnici su poslati serveru.");
    }
    catch (RpcException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
async Task ObrisiKorisnika()
{
    Console.WriteLine("Unesite id korisnika kojeg zelite da obrisete:");
    int id = int.Parse(Console.ReadLine());

    try
    {
        var request = new KorisnikId { Id = id };
        var response = await client.ObrisiKorisnikaAsync(request);
        Console.WriteLine(response.Tekst);
    }
    catch (RpcException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
async Task ObrisiViseKorisnika()
{
    List<KorisnikId> korisniciZaBrisanje = new List<KorisnikId>();
    Console.WriteLine("Unesite broj korisnika koje zelite da obrisete:");
    int brojKorisnika = int.Parse(Console.ReadLine());

    for (int i = 0; i<brojKorisnika; i++)
    {
        Console.WriteLine("Unesite id korisnika kojeg zelite da obrisete:");
        int id = int.Parse(Console.ReadLine());
        KorisnikId korisnikId = new KorisnikId { Id = id };
        korisniciZaBrisanje.Add(korisnikId);
    }

    try
    {
        var call = client.ObrisiKorisnike();

        var readTask = Task.Run(async () =>
        {
            await foreach (var response in call.ResponseStream.ReadAllAsync())
            {
                Console.WriteLine(response.Tekst);
            }
        });

        foreach (var id in korisniciZaBrisanje)
        {
            await call.RequestStream.WriteAsync(id);
        }

        await call.RequestStream.CompleteAsync();
        await readTask;

        Console.WriteLine("Svi zahtevi za brisanje su poslati serveru.");
    }
    catch (RpcException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
async Task PrikaziListuKorisnika()
{
    Console.WriteLine("Unesite pocetni id korisnika:");
    int pocetak = int.Parse(Console.ReadLine());
    Console.WriteLine("Unesite krajnji id korisnika:");
    int kraj = int.Parse(Console.ReadLine());

    try
    {
        var request = new OpsegId { Pocetak = pocetak, Kraj = kraj };
        var call = client.VratiListuKorisnika(request);

        await foreach (var response in call.ResponseStream.ReadAllAsync())
        {
            Console.WriteLine(response.Id + " " + response.Ime + " " + response.Prezime + " " + response.Adresa + " " + string.Join(", ", response.BrojeviTelefona));
        }
    } catch (RpcException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
