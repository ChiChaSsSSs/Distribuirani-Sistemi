import java.rmi.Remote;
import java.rmi.RemoteException;

public interface Korisnik extends Remote {
    public void uplatiMinute(int minuti) throws RemoteException;
    public void uplatiPoruke(int poruke) throws RemoteException;
    public void uplatiInternet(int internet) throws RemoteException;
    public Stanje vratiStanje() throws RemoteException;
}