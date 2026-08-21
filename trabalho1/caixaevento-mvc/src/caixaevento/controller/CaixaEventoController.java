package caixaevento.controller;

import caixaevento.model.CaixaCentral;
import caixaevento.view.CaixaEventoView;

/* controller do sistema de caixa do evento */
public class CaixaEventoController {

    /* quantidade de caixas físicos do evento */
    private static final int quantidade = 5;

    /* quantidade de fichas vendidas por caixa */
    private static final int fichas = 1000;

    private final CaixaCentral model;
    private final CaixaEventoView view;

    public CaixaEventoController(CaixaCentral model, CaixaEventoView view) {
        this.model = model;
        this.view = view;
    }

    /* executa a simulação */
    public void executar() throws InterruptedException {

        Thread[] caixas = new Thread[quantidade];

        for (int i = 0; i < quantidade; i++) {
            caixas[i] = new Thread(() -> {
                for (int j = 0; j < fichas; j++) {
                    model.venderFicha();
                }
            });
        }

        for (Thread caixa : caixas) {
            caixa.start();
        }

        for (Thread caixa : caixas) {
            caixa.join();
        }

        view.exibirSaldoFinal(model.getSaldoCentral());
    }
}
