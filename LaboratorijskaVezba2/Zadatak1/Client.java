import java.rmi.Naming;
import java.util.Scanner;

public class Client {
    public static void main(String[] args) {
        try
        {
            Scanner myObj = new Scanner(System.in);
            Kviz kviz = (Kviz) Naming.lookup("rmi://localhost/MatematickiKviz");

            kviz.pocetak();

            for (int i = 0; i < 4; i++) {
                Pitanje pitanje = kviz.vratiPitanje();
                String pitanjeZaPrikaz = pitanje.vratiTekst();
                System.out.println(pitanjeZaPrikaz);

                System.out.println("Unesite odgovor na ovo pitanje:");
                String odgovor = myObj.nextLine();
                kviz.odgovori(odgovor);
            }

            int ukupnoPoena = kviz.vratiBrojPoena();
            System.out.println("Ukupan broj poena: " + ukupnoPoena);

            myObj.close();
        } catch (Exception e) {
            e.printStackTrace();
        }
    }   
}
