package relatoriovendas.controller;

import relatoriovendas.model.Filial;
import relatoriovendas.view.RelatorioVendasView;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;

/* controller do sistema de relatório de vendas.*/
public class RelatorioVendasController {

    private static final int qtdfiliais = 4;
    private static final int vendasfilial = 10000;

    private final RelatorioVendasView view;

    public RelatorioVendasController(RelatorioVendasView view) {
        this.view = view;
    }

    /* executa a simulação completa: gera os dados, processa as 4 filiais */
    public void executar() throws InterruptedException {

        List<Filial> filiais = new ArrayList<>();
        for (int i = 1; i <= qtdfiliais; i++) {
            filiais.add(new Filial("Filial " + i, gerarVendas(vendasfilial)));
        }

        List<Thread> threads = new ArrayList<>();
        for (Filial filial : filiais) {
            Thread t = new Thread(filial::calcularSoma);
            threads.add(t);
            t.start();
        }

        for (Thread t : threads) {
            t.join();
        }

        long faturamentoTotal = 0;
        for (Filial filial : filiais) {
            faturamentoTotal += filial.getSomaLocal();
        }

        view.exibirRelatorio(filiais, faturamentoTotal);
    }

    /* gera uma lista de vendas aleatórias simulando uma filial. */
    private List<Integer> gerarVendas(int quantidade) {
        Random gerador = new Random();
        List<Integer> vendas = new ArrayList<>();
        for (int i = 0; i < quantidade; i++) {
            vendas.add(gerador.nextInt(200));
        }
        return vendas;
    }
}
