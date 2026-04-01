using Zad1;

namespace Zadatak1Server
{
    public class Korisnici
    { 
        public Dictionary<int, Korisnik> Baza { get; set; }
        private static Korisnici? instanca;
        private static object lockObj = new object();

        private Korisnici()
        {
            Baza = new Dictionary<int, Korisnik>() 
            {
                { 1, new Korisnik
                {
                    Id = 1,
                    Ime = "Marko",
                    Prezime = "Markovic",
                    Adresa = "Neka adresa 1",
                    BrojeviTelefona = { "123456789" },
                } },
                { 2, new Korisnik
                {
                    Id = 2,
                    Ime = "Jelena",
                    Prezime = "Jovanovic",
                    BrojeviTelefona = { "987654321" },
                } },
                { 3, new Korisnik
                {
                    Id = 3,
                    Ime = "Petar",
                    Prezime = "Petrovic",
                    Adresa = "Neka adresa 3",
                    BrojeviTelefona = { "555555555", "12312431" },
                } },
                { 4, new Korisnik
                {
                    Id = 4,
                    Ime = "Ana",
                    Prezime = "Anic",
                    BrojeviTelefona = { "444444444", "421412412", "41241241242" },
                } },
                { 5, new Korisnik
                {
                    Id = 5,
                    Ime = "Milos",
                    Prezime = "Milosevic",
                    Adresa = "Neka adresa 5",
                    BrojeviTelefona = { "333333333" },
                } },
            };
        }

        public static Korisnici Instanca()
        {
            if (instanca == null)
            {
                lock (lockObj)
                {
                    if (instanca == null)
                    {
                        instanca = new Korisnici();
                    }
                }
            }

            return instanca;
        }
    }
}
