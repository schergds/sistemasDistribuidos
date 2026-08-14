import java.util.ArrayList;
import java.util.Random;

public class TarefaPopular {

    private static final Random gerador = new Random();

    public static void popular(ArrayList<Integer> lista, int quantidade) {
        for (int i = 0; i < quantidade; i++) {
            lista.add(gerador.nextInt(200));
        }
    }
}