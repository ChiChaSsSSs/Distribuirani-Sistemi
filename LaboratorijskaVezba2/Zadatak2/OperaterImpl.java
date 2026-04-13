import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.HashMap;
import java.util.Map;

public class OperaterImpl extends UnicastRemoteObject implements Operater {
    public Map<String, Korisnik> korisnici;

    public OperaterImpl() throws RemoteException {
        korisnici = new HashMap<String, Korisnik>();
        Korisnik k1 = new KorisnikImpl("555333", 0, 0, 0, 3, 1, 5);
        Korisnik k2 = new KorisnikImpl("111222", 0, 0, 0, 2, 1, 3);
        Korisnik k3 = new KorisnikImpl("222333", 0, 0, 0, 4, 2, 2);
        Korisnik k4 = new KorisnikImpl("999999", 0, 0, 0, 2, 1, 8);
        Korisnik k5 = new KorisnikImpl("676767", 0, 0, 0, 6, 4, 8);

        korisnici.put("555333", k1);
        korisnici.put("111222", k2);
        korisnici.put("222333", k3);
        korisnici.put("999999", k4);
        korisnici.put("676767", k5);
    }

    public Korisnik vratiKorisnika(String broj) throws RemoteException {
        return (KorisnikImpl) korisnici.get(broj);
    }
}
