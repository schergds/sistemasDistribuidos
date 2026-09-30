using System.Net;
using Comum;

namespace Servidor
{
    // Protocolo (mensagens de texto separadas por ';'):
    //   Cliente -> Servidor: CADASTRO;nome;email
    //   Servidor -> Cliente: OK;mensagem   ou   ERRO;mensagem
    //
    //   Cliente -> Servidor: TOKEN;email
    //   Servidor -> Cliente: TOKEN;chave;segundosRestantes   ou   ERRO;mensagem
    class Program
    {
        const int PORTA = 5000;
        const int VALIDADE_TOKEN_SEGUNDOS = 60;

        static List<Pessoa> pessoas = new List<Pessoa>();
        static Random random = new Random();

        static void Main(string[] args)
        {
            Comunicador comunicador = new Comunicador(PORTA);
            Console.WriteLine("Servidor UDP escutando na porta " + PORTA);

            while (true)
            {
                IPEndPoint cliente;
                string mensagem = comunicador.Receber(out cliente);
                Console.WriteLine("Recebido de " + cliente + ": " + mensagem);

                string resposta = ProcessarMensagem(mensagem);

                comunicador.Enviar(resposta, cliente);
                Console.WriteLine("Enviado para " + cliente + ": " + resposta);
            }
        }

        static string ProcessarMensagem(string mensagem)
        {
            string[] partes = mensagem.Split(';');
            string comando = partes[0];

            if (comando == "CADASTRO" && partes.Length == 3)
            {
                return Cadastrar(partes[1], partes[2]);
            }
            else if (comando == "TOKEN" && partes.Length == 2)
            {
                return GerarToken(partes[1]);
            }
            else
            {
                return "ERRO;Comando inválido";
            }
        }

        static string Cadastrar(string nome, string email)
        {
            if (nome.Trim() == "" || email.Trim() == "")
            {
                return "ERRO;Nome e e-mail são obrigatórios";
            }

            // Impede cadastro duplicado (mesmo e-mail)
            if (BuscarPorEmail(email) != null)
            {
                return "ERRO;E-mail já cadastrado";
            }

            Pessoa nova = new Pessoa(nome.Trim(), email.Trim());
            pessoas.Add(nova);
            Console.WriteLine("Novo cadastro: " + nova + " (total: " + pessoas.Count + ")");
            return "OK;Cadastro realizado com sucesso";
        }

        static string GerarToken(string email)
        {
            Pessoa pessoa = BuscarPorEmail(email);
            if (pessoa == null)
            {
                return "ERRO;Usuário não cadastrado";
            }

            double segundosPassados = (DateTime.Now - pessoa.TokenGeradoEm).TotalSeconds;

            // Se ainda não tem token, ou se o token já passou de 60 segundos, gera um novo.
            // Caso contrário, mantém o mesmo token.
            if (pessoa.Token == null || segundosPassados >= VALIDADE_TOKEN_SEGUNDOS)
            {
                pessoa.Token = NovoTokenAleatorio();
                pessoa.TokenGeradoEm = DateTime.Now;
                segundosPassados = 0;
                Console.WriteLine("Novo token para " + pessoa.Email + ": " + pessoa.Token);
            }

            int restantes = (int)(VALIDADE_TOKEN_SEGUNDOS - segundosPassados);
            return "TOKEN;" + pessoa.Token + ";" + restantes;
        }

        static Pessoa BuscarPorEmail(string email)
        {
            foreach (Pessoa p in pessoas)
            {
                if (p.Email.ToLower() == email.Trim().ToLower())
                {
                    return p;
                }
            }
            return null;
        }

        static string NovoTokenAleatorio()
        {
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string token = "";
            for (int i = 0; i < 8; i++)
            {
                token += caracteres[random.Next(caracteres.Length)];
            }
            return token;
        }
    }
}
