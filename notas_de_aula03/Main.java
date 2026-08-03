class PrimeiraThread extends Thread {
    public void run() {
        for (int i = 0; i <= 50; i++) {
            System.out.println("Thread um executando!" + i);
        }
    }
}

class SegundaThread extends Thread {
    public void run() {
        for (int i = 0; i <= 50; i++) {
            System.out.println("Thread dois executando!" + i);
        }
    }
}

public class Main {
    public static void main(String[] args) {
        PrimeiraThread primeira = new PrimeiraThread();
        SegundaThread segunda = new SegundaThread();
        primeira.start();
        segunda.start();
    }
}