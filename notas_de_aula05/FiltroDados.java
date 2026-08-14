import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class FiltroDados {

    static class FiltroThread extends Thread {

        private List<String> dados;
        private List<String> resultado;

        public FiltroThread(List<String> dados) {
            this.dados = dados;
            this.resultado = new ArrayList<>();
        }

        @Override
        public void run() {

            for (String nome : dados) {

                String nomeLimpo = nome.trim();
                nomeLimpo = nomeLimpo.toUpperCase();
                resultado.add(nomeLimpo);
            }
        }

        public List<String> getResultado() {
            return resultado;
        }
    }

    public static void main(String[] args) {

        List<String> usuarios = Arrays.asList(
                " joao ",
                " maria ",
                " pedro ",
                " ana ",
                " carlos ",
                " fernanda "
        );

        int meio = usuarios.size() / 2;

        List<String> primeiraMetade =
                new ArrayList<>(usuarios.subList(0, meio));

        List<String> segundaMetade =
                new ArrayList<>(usuarios.subList(meio, usuarios.size()));

        FiltroThread threadA = new FiltroThread(primeiraMetade);
        FiltroThread threadB = new FiltroThread(segundaMetade);

        threadA.start();
        threadB.start();

        try {
            threadA.join();
            threadB.join();

        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
            System.out.println("Thread principal interrompida.");
        }

        List<String> listaFinal = new ArrayList<>();

        listaFinal.addAll(threadA.getResultado());
        listaFinal.addAll(threadB.getResultado());

        System.out.println("Lista final:");

        for (String usuario : listaFinal) {
            System.out.println(usuario);
        }
    }
}