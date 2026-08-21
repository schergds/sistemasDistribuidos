package relatoriovendas;

import relatoriovendas.controller.RelatorioVendasController;
import relatoriovendas.view.RelatorioVendasView;

public class Main {

    public static void main(String[] args) throws InterruptedException {

        RelatorioVendasView view = new RelatorioVendasView();
        RelatorioVendasController controller = new RelatorioVendasController(view);

        controller.executar();
    }
}
