import java.rmi.Remote;
import java.rmi.RemoteException;

public interface Operater extends Remote {
    public Korisnik vratiKorisnika(String broj) throws RemoteException;
}