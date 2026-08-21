package relatoriovendas.model;

import java.util.List;

/* representa os dados de vendas de uma filial */
public class Filial {

    private final String nome;
    private final List<Integer> vendas;
    private long somaLocal;

    public Filial(String nome, List<Integer> vendas) {
        this.nome = nome;
        this.vendas = vendas;
    }

    /* calcula a soma das vendas desta filial e guarda o resultado no matributo */
    public void calcularSoma() {
        long soma = 0;
        for (int venda : vendas) {
            soma += venda;
        }
        this.somaLocal = soma;
    }

    public String getNome() {
        return nome;
    }

    public long getSomaLocal() {
        return somaLocal;
    }
}
