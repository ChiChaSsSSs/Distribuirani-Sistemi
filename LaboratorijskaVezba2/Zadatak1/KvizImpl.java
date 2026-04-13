import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

public class KvizImpl extends UnicastRemoteObject implements Kviz {
    private int brojPoena = 0;
    private int trenutnoPitanje = 0;
    private Pitanje[] pitanja = {
        new PitanjeImpl("2 + 2 = ?", "4", "5", "6"),
        new PitanjeImpl("2 * 2 = ?", "1", "2", "4"),
        new PitanjeImpl("10 - 7 = ?", "1", "3", "4"),
        new PitanjeImpl("5 - 4 = ?", "0", "3", "1")
    };
    private String[] tacniOdgovori = {
        "a", "b", "b", "c"
    };

    public KvizImpl() throws RemoteException {
        super();
    }

    public void pocetak() throws RemoteException {
        brojPoena = 0;
    }

    public Pitanje vratiPitanje() throws RemoteException {
        return pitanja[trenutnoPitanje];
    }

    public void odgovori(String odg) throws RemoteException {
        if (tacniOdgovori[trenutnoPitanje].equalsIgnoreCase(odg)) {
            System.out.println("Tacan odgovor!");
            brojPoena++;
            trenutnoPitanje = (trenutnoPitanje + 1) % pitanja.length;
        } else {
            System.out.println("Netacan odgovor!");
            trenutnoPitanje = (trenutnoPitanje + 1) % pitanja.length;
        }
    }

    public int vratiBrojPoena() throws RemoteException {
        return brojPoena;
    }
}
