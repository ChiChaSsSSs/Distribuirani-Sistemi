import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class StanjeImpl extends UnicastRemoteObject implements Stanje {
    private String broj;
    private int minuti;
    private int poruke;
    private int internet;
    private float racun;

    public StanjeImpl(String broj, int minuti, int poruke, int internet, float racun) throws RemoteException {
        this.broj = broj;
        this.minuti = minuti;
        this.poruke = poruke;
        this.internet = internet;
        this.racun = racun;
    }

    public int vratiMinute() throws RemoteException {
        return minuti;
    }

    public int vratiPoruke() throws RemoteException {
        return poruke;
    }

        public int vratiInternet() throws RemoteException {
        return internet;
    }

        public float vratiRacun() throws RemoteException {
        return racun;
    }
}
