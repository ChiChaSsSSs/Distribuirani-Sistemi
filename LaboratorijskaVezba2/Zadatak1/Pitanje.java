import java.rmi.Remote;
import java.rmi.RemoteException;

public interface Pitanje extends Remote {
    public String vratiTekst() throws RemoteException;
}