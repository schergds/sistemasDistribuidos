package caixaevento.model;

/* representa o saldo centralizado do evento */
public class CaixaCentral {

    private static final double valorficha = 10.0;

    /* saldo total acumulado do evento */
    private double saldoCentral = 0.0;

    /* registra a venda de uma ficha, somando seu valor ao saldo central */
    public synchronized void venderFicha() {
        saldoCentral += valorficha;
    }

    /* retorna o saldo total acumulado até o momento.*/
    public double getSaldoCentral() {
        return saldoCentral;
    }
}
