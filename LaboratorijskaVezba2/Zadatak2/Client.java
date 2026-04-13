import java.rmi.Naming;
import java.util.Scanner;

public class Client {
    public static void main(String[] args) {
        try {
            Scanner myObj = new Scanner(System.in);
            Operater operater = (Operater) Naming.lookup("rmi://localhost/MobilniOperater");
            String unos;
            String brojUnos;
            String dodatakUnos;

            do {
                System.out.println("Dobrodosli u korisnicki servis mobilnog operatera. Za nastavak izaberite opciju:");
                System.out.println("a) Uplata Minuta");
                System.out.println("b) Uplata Poruka");
                System.out.println("c) Uplata Interneta");
                System.out.println("d) Provera Stanja");
                System.out.println("e) Kraj");

                unos = myObj.nextLine();

                switch (unos) {
                    case "a": {
                        System.out.println("Izbrali ste opciju za uplatu dodatnih minuta:");
                        System.out.println("Unesite broj telefona korisnika:");
                        brojUnos = myObj.nextLine();
                        System.out.println("Unesite broj minuta:");
                        dodatakUnos = myObj.nextLine();

                        Korisnik korisnik = operater.vratiKorisnika(brojUnos);
                        korisnik.uplatiMinute(Integer.parseInt(dodatakUnos));

                        break;
                    }
                    case "b": {
                        System.out.println("Izbrali ste opciju za uplatu dodatnih poruka:");
                        System.out.println("Unesite broj telefona korisnika:");
                        brojUnos = myObj.nextLine();
                        System.out.println("Unesite broj poruka:");
                        dodatakUnos = myObj.nextLine();

                        Korisnik korisnik = operater.vratiKorisnika(brojUnos);
                        korisnik.uplatiPoruke(Integer.parseInt(dodatakUnos));

                        break;
                    }
                    case "c": {
                        System.out.println("Izbrali ste opciju za uplatu dodatnog interneta:");
                        System.out.println("Unesite broj telefona korisnika:");
                        brojUnos = myObj.nextLine();
                        System.out.println("Unesite broj megabajta:");
                        dodatakUnos = myObj.nextLine();

                        Korisnik korisnik = operater.vratiKorisnika(brojUnos);
                        korisnik.uplatiInternet(Integer.parseInt(dodatakUnos));

                        break;
                    }
                    case "d": {
                        System.out.println("Izbrali ste opciju za proveru stanja:");
                        System.out.println("Unesite broj telefona korisnika:");
                        brojUnos = myObj.nextLine();

                        Korisnik korisnik = operater.vratiKorisnika(brojUnos);
                        Stanje stanje = korisnik.vratiStanje();
                        float racun = stanje.vratiRacun();
                        System.out.println("Vas racun iznosi: " + racun + " dinara.");

                        break;
                    }
                    case "e": {
                        System.out.println("Vidimo se kasnije!");

                        break;
                    }
                    default: {
                        System.out.println("Pogresan unos! Probajte opet.");
                    }
                }

            } while (!unos.equals("e"));

            myObj.close();

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
