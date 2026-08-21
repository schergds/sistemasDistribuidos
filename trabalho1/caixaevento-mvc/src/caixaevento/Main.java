package caixaevento;

import caixaevento.controller.CaixaEventoController;
import caixaevento.model.CaixaCentral;
import caixaevento.view.CaixaEventoView;

public class Main {

    public static void main(String[] args) throws InterruptedException {

        CaixaCentral model = new CaixaCentral();
        CaixaEventoView view = new CaixaEventoView();
        CaixaEventoController controller = new CaixaEventoController(model, view);

        controller.executar();
    }
}
