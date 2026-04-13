import java.rmi.Naming;
import java.rmi.registry.LocateRegistry;

public class Server {
    public static void main(String[] args) {
        try {
            LocateRegistry.createRegistry(1099);
        } catch (Exception e) {
            e.printStackTrace();
        }

        try {
            Kviz kviz = new KvizImpl();
            Naming.rebind("rmi://localhost/MatematickiKviz", kviz);
            System.out.println("Server je pokrenut...");
        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
