import java.rmi.server.UnicastRemoteObject;
import java.rmi.RemoteException;

public class KorisnikImpl extends UnicastRemoteObject implements Korisnik {
    private String broj;
    private int minuti;
    private int poruke;
    private int internet;
    private int minutiTarifa;
    private int porukeTarifa;
    private int internetTarifa;

    public KorisnikImpl(String broj, int minuti, int poruke, int internet, int minutiTarifa, int porukeTarifa, int internetTarifa) throws RemoteException {
        this.broj = broj;
        this.minuti = minuti;
        this.poruke = poruke;
        this.internet = internet;
        this.minutiTarifa = minutiTarifa;
        this.porukeTarifa = porukeTarifa;
        this.internetTarifa = internetTarifa;
    }

    public void uplatiMinute(int minuti) throws RemoteException {
        this.minuti += minuti;
    }

    public void uplatiPoruke(int poruke) throws RemoteException {
        this.poruke += poruke;
    }

    public void uplatiInternet(int internet) throws RemoteException {
        this.internet += internet;
    }

    public Stanje vratiStanje() throws RemoteException {
        float racun = minuti * minutiTarifa + poruke * porukeTarifa + internet * internetTarifa;
        return new StanjeImpl(broj, minuti, poruke, internet, racun);
    }
}
