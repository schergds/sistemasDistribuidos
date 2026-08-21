package relatoriovendas.view;

import relatoriovendas.model.Filial;

import java.util.List;

/* view do sistema de relatório de vendas */
public class RelatorioVendasView {

    /* exibe o faturamento de cada filial e o faturamento total anual */
    public void exibirRelatorio(List<Filial> filiais, long faturamentoTotal) {
        for (Filial filial : filiais) {
            System.out.println("Faturamento " + filial.getNome() + ": " + filial.getSomaLocal());
        }
        System.out.println("Faturamento total anual: " + faturamentoTotal);
    }
}
