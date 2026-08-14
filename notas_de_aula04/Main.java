import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        ArrayList<Integer> lista = new ArrayList<>();
        TarefaPopular.popular(lista, 10000);

        int supersoma = 0;
        for (Integer integer : lista) {
            supersoma += integer;
        }

        int[] resultados = new int[4];

        Thread t1 = new Thread(() -> {
            int soma = 0;
            for (int i = 0; i < 2500; i++) {
                soma += lista.get(i);
            }
            resultados[0] = soma;
            System.out.println("Thread 1: " + soma);
        });

        Thread t2 = new Thread(() -> {
            int soma = 0;
            for (int i = 2500; i < 5000; i++) {
                soma += lista.get(i);
            }
            resultados[1] = soma;
            System.out.println("Thread 2: " + soma);
        });

        Thread t3 = new Thread(() -> {
            int soma = 0;
            for (int i = 5000; i < 7500; i++) {
                soma += lista.get(i);
            }
            resultados[2] = soma;
            System.out.println("Thread 3: " + soma);
        });

        Thread t4 = new Thread(() -> {
            int soma = 0;
            for (int i = 7500; i < 10000; i++) {
                soma += lista.get(i);
            }
            resultados[3] = soma;
            System.out.println("Thread 4: " + soma);
        });

        t1.start();
        t2.start();
        t3.start();
        t4.start();

        try {
            t1.join();
            t2.join();
            t3.join();
            t4.join();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }

        int somaTotal = resultados[0] + resultados[1] + resultados[2] + resultados[3];

        System.out.println("Supersoma: " + supersoma);
        System.out.println("Soma das threads: " + somaTotal);
    }
}