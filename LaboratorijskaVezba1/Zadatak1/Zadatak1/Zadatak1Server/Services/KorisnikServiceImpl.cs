using Grpc.Core;
using Zad1;

namespace Zadatak1Server.Services
{
    public class KorisnikServiceImpl : KorisnikService.KorisnikServiceBase
    {
        public override Task<Poruka> DodajKorisnika(Korisnik request, ServerCallContext context)
        {
            if (Korisnici.Instanca().Baza.ContainsKey(request.Id))
            {
                return Task.FromResult(new Poruka { Tekst = "Korisnik sa datim ID-jem već postoji." });
            }

            Korisnici.Instanca().Baza[request.Id] = request;

            return Task.FromResult(new Poruka { Tekst = "Korisnik uspešno dodat." });
        }

        public override async Task DodajKorisnike(IAsyncStreamReader<Korisnik> requestStream, IServerStreamWriter<Poruka> responseStream, ServerCallContext context)
        {
            await foreach (var korisnik in requestStream.ReadAllAsync())
            {
                if (Korisnici.Instanca().Baza.ContainsKey(korisnik.Id)) {
                    await responseStream.WriteAsync(new Poruka { Tekst = "Korisnik sa datim ID-jem već postoji." });
                } 
                else
                {
                    Korisnici.Instanca().Baza[korisnik.Id] = korisnik;
                    await responseStream.WriteAsync(new Poruka { Tekst = "Korisnik uspešno dodat." });
                }
            }
        }
        public override Task<Poruka> ObrisiKorisnika(KorisnikId request, ServerCallContext context)
        {
            if (!Korisnici.Instanca().Baza.ContainsKey(request.Id))
            {
                return Task.FromResult(new Poruka { Tekst = "Korisnik sa datim ID-jem ne postoji." });
            }

            Korisnici.Instanca().Baza.Remove(request.Id);
            return Task.FromResult(new Poruka { Tekst = "Korisnik uspešno obrisan." });
        }

        public override async Task ObrisiKorisnike(IAsyncStreamReader<KorisnikId> requestStream, IServerStreamWriter<Poruka> responseStream, ServerCallContext context)
        {
            await foreach (var kid in requestStream.ReadAllAsync())
            {
                if (!Korisnici.Instanca().Baza.ContainsKey(kid.Id))
                {
                    await responseStream.WriteAsync(new Poruka { Tekst = "Korisnik sa datim ID-jem ne postoji." });
                }
                else
                {
                    Korisnici.Instanca().Baza.Remove(kid.Id);
                    await responseStream.WriteAsync(new Poruka { Tekst = "Korisnik uspešno obrisan." });
                }
            }
        }

        public override async Task VratiListuKorisnika(OpsegId request, IServerStreamWriter<Korisnik> responseStream, ServerCallContext context)
        {
            for (int i = request.Pocetak; i <= request.Kraj; i++)
            {
                if (Korisnici.Instanca().Baza.ContainsKey(i))
                {
                    await responseStream.WriteAsync(Korisnici.Instanca().Baza[i]);
                }
            }
        }
    }
}
