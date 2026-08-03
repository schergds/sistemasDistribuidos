# Memória Computacional

## Relação com Seção Crítica

Memória computacional está diretamente relacionada ao conceito de **seção crítica**, pois envolve o acesso concorrente a dados compartilhados.

- **Seção crítica**
  - **Sincronismo**
    - Controle de tempo de acesso
    - Bloqueio (mutex, semáforos, etc.)

## Compartilhamento de Memória

**Compartilhamento de memória:** ocorre quando duas ou mais threads acessam as mesmas variáveis na memória.

### Com compartilhamento de memória (Threads)

- Compartilham o mesmo espaço de memória.
- Comunicação rápida entre as threads.
- Exige mecanismos de sincronização (mutex, semáforos, monitores, etc.) para evitar conflitos, como condições de corrida (*race conditions*).

### Sem compartilhamento de memória (Processos)

- Cada processo possui seu próprio espaço de memória.
- A comunicação ocorre por mecanismos de IPC (*Inter-Process Communication*), como:
  - Pipes
  - Filas de mensagens
  - Sockets
  - Memória compartilhada
- É mais seguro, pois os processos são isolados.
- A troca de dados tende a ser mais lenta do que entre threads.

### Em resumo

| Linguagem | Uso de Threads                                          | Particularidade/limitação                  |
| --------- | ------------------------------------------------------- | ------------------------------------------ |
| Java      | Suporte completo, fácil, robusto com Executor Framework | Muito usado em sistemas distribuídos       |
| Python    | Suporte para threads, mas GIL limita paralelismo real   | Ideal para I/O-bound, não CPU-bound        |
| C#        | Suporte completo, com async/await e Tasks modernos      | Amplamente usado em aplicações de servidor |