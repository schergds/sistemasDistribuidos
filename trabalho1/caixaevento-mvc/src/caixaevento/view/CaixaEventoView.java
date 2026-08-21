package caixaevento.view;

/* view do sistema de caixa do evento
   responsável apenas por exibir informações no console */

public class CaixaEventoView {

    /* valor final acumulado no saldo central */
    public void exibirSaldoFinal(double saldoFinal) {
        System.out.println("Saldo final do evento: R$ " + saldoFinal);
    }
}
