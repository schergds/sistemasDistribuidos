# Resumo de Sistemas Distribuídos

## 1. Comunicação e Autenticação (Cliente-Servidor)

A troca de informações entre cliente e servidor exige módulos de comunicação e estruturação de dados em rede.

*   **TCP:** Confiável, orientado à conexão, garante entrega, porém mais lento.
*   **UDP:** Rápido, sem conexão, não garante entrega (pacotes podem ser perdidos).
*   **DTO (Data Transfer Object):** Objeto encapsulado contendo múltiplos campos (ex: nome, email, token, timestamp) para trafegar entre cliente e servidor. 
*   **Autenticadores e Tokens:** O servidor utiliza um Timer e Threads dedicadas por usuário para gerar periodicamente (ex: a cada 30s) um Token (código temporário e único) para validar a identidade do cliente.
*   **Segurança (hashCode):** O método `hashCode()` do Java gera um inteiro baseado no objeto, mas **não é seguro para autenticação real** (não é criptográfico). No mercado utilizam-se UUID, SHA-256, HMAC ou TOTP.

**Diretrizes de Implementação (Exercício UDP):**
*   **Comunicação 100% UDP:** O fluxo de cadastro e solicitação de token não utiliza TCP.
*   **Servidor:** Execução em console (sem GUI), gerencia usuários cadastrados e delega a geração de tokens para Threads independentes e simultâneas.
*   **Cliente:** Interface gráfica com áreas de Cadastro (Nome, Email) e Solicitação (Email -> Receber Token).

---

## 2. Manipulação de Texto em Java

A escolha do tipo de dado impacta diretamente no desempenho e na segurança (Thread-safe) do sistema distribuído.

| Característica | `String` | `StringBuffer` | `StringBuilder` |
| :--- | :--- | :--- | :--- |
| **Mutabilidade** | Imutável (cria novos objetos) | Mutável | Mutável |
| **Thread-safe** | Sim | Sim (métodos `synchronized`) | Não |
| **Desempenho** | Lento para alterações | Médio | Rápido (sem lock) |
| **Cenário Ideal** | Textos fixos/constantes | Alterações por **múltiplas threads** | Alterações por **uma única thread** (loops) |

---

## 3. RPC e RMI (Remote Procedure Call)

Uma abstração que permite a um programa invocar métodos ou funções em outra máquina da rede como se fossem locais, ocultando a complexidade de Sockets, TCP/UDP, IP, threads e serialização.

*   **Funcionamento (Stubs/Skeletons):** O cliente chama um *stub* local, que empacota a chamada, envia via rede ao *skeleton* no servidor, que executa e devolve o resultado.
*   **Serialização:** Necessária para transformar objetos em bytes antes de trafegá-los.
*   **Trade-off:** Simplifica drasticamente o desenvolvimento, mas possui desempenho inferior aos Sockets puros.

**Principais Tecnologias:**
*   **RPC Tradicional:** Modelo base de execução de procedimentos.
*   **RMI (Remote Method Invocation):** Específico do **Java**, orientado a objetos. Exige uma Interface Remota, uma Classe Implementadora, um Servidor (Registry) e o Cliente (Lookup).
*   **XML-RPC:** Trafega dados em XML via protocolo HTTP (comum em Python via `xmlrpc`).
*   **REST + JSON:** Padrão arquitetural que dominou o mercado moderno, substituindo abordagens antigas como SOAP/XML.

---

## 4. Comunicação Ponto-a-Ponto (Multicast)

Modelo de comunicação focado na transmissão de mensagens para um **grupo específico** de estações na rede.

*   **Tipos de Envio:**
    *   **Unicast:** 1 para 1.
    *   **Multicast:** 1 para N (apenas para membros de um grupo fechado).
    *   **Broadcast:** 1 para Todos (inunda toda a rede).
*   **Arquitetura e Protocolo:** Estrutura Ponto-a-Ponto (P2P) onde cada nó atua simultaneamente como cliente e servidor, operando essencialmente sobre **UDP** (`MulticastSocket`).
*   **Endereçamento:** Utiliza uma faixa de IP virtual restrita (Classe D), convencionada entre `224.0.0.0` e `239.255.255.255` (ex: `239.X.Y.W`), vinculada a uma porta padrão.
*   **Necessidade de Threads:** Operações UDP de `send()` (Emissor) e `receive()` (Receptor) são **bloqueantes**. É obrigatório instanciar no mínimo duas Threads (uma emissora, uma ouvidora) em cada máquina para que o processo de enviar dados não trave o recebimento simultâneo (e vice-versa).
