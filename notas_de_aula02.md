# Semana 1 - Aula 2

## Sistemas Paralelos
- Computadores iguais (hardware, sistema operacional e linguagem)
- Fortemente acoplados (ligados na mesma rede)
- Usam **Cluster Computacional**
- Arquitetura:
  - Ponto a Ponto (P2P)

### Objetivo
- Compartilhar recursos:
  - Processador
  - Memória

### Vantagens
- Tolerância a falhas
- Escalabilidade
- Segurança
- Facilidade de manutenção e atualização

## Sistemas Distribuídos
- Computadores diferentes (hardware, sistema operacional e linguagem)
- Fracamente acoplados (podem estar em locais diferentes)
- Usam **Grid Computacional**
- Arquiteturas:
  - Cliente-Servidor
  - Ponto a Ponto (P2P)

### Objetivo
- Compartilhar recursos:
  - Processador
  - Memória

### Controle necessário
- Sincronismo:
  - Relógio físico
  - Relógio lógico
- Controle de acesso:
  - Exclusão mútua (evita que dois processos usem o mesmo recurso ao mesmo tempo)

## Comunicação em Sistemas Distribuídos
- Depende do Sistema Operacional:
  - Gerenciamento de processos
  - Gerenciamento da comunicação
- Comunicação feita por **Sockets**:
  - Endereço IP
  - Porta
  - Máscara de rede
  - Objetos de leitura e escrita
- A comunicação é **bloqueante**:
  - Escrever
  - Ler
- Solução:
  - Uso de **Threads** para executar tarefas ao mesmo tempo

## Protocolos de Rede (TCP/IP)
- Endereço IP
- Porta lógica
- Máscara de rede
- Protocolos de transporte

## Características dos Sistemas Distribuídos

### Arquiteturas
- Cliente-Servidor
- Ponto a Ponto (P2P)
- Híbrida

### Comunicação
- Bloqueante
  - Escrita
  - Leitura

---
