import java.rmi.server.UnicastRemoteObject;
import java.rmi.RemoteException;

public class PitanjeImpl extends UnicastRemoteObject implements Pitanje {
    private String tekst;
    private String a;
    private String b;
    private String c;

    public PitanjeImpl(String tekst, String a, String b, String c) throws RemoteException {
        this.tekst = tekst;
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public String vratiTekst() throws RemoteException {
        String pitanje = tekst + "\n" + "a: " + a + "\n" + "b: " + b + "\n" + "c: " + c + "\n";
        return pitanje;
    }
}