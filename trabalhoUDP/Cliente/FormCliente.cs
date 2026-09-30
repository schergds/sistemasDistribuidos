using System.Net;
using System.Net.Sockets;
using Comum;

namespace Cliente
{
    // Tela do cliente. Os controles são criados direto no código (sem o Designer)
    // para ficar fácil de ler e estudar.
    public class FormCliente : Form
    {
        const string IP_SERVIDOR = "127.0.0.1";
        const int PORTA_SERVIDOR = 5000;
        const int INTERVALO_TOKEN_SEGUNDOS = 10; // de quanto em quanto tempo pede o token

        Comunicador comunicador;
        IPEndPoint servidor;

        TextBox txtNome;
        TextBox txtEmail;
        Button btnCadastrar;
        Button btnToken;
        Label lblToken;
        Label lblValidade;
        ListBox lstLog;

        // Timer que pede o token periodicamente
        System.Windows.Forms.Timer timerToken;
        // Timer que atualiza a contagem regressiva na tela a cada 1 segundo
        System.Windows.Forms.Timer timerContagem;
        int segundosRestantes = 0;

        public FormCliente()
        {
            CriarTela();

            comunicador = new Comunicador(0); // porta 0 = o sistema escolhe uma livre
            comunicador.DefinirTimeout(2000); // espera no máximo 2s pela resposta
            servidor = new IPEndPoint(IPAddress.Parse(IP_SERVIDOR), PORTA_SERVIDOR);

            timerToken = new System.Windows.Forms.Timer();
            timerToken.Interval = INTERVALO_TOKEN_SEGUNDOS * 1000;
            timerToken.Tick += (s, e) => SolicitarToken();

            timerContagem = new System.Windows.Forms.Timer();
            timerContagem.Interval = 1000;
            timerContagem.Tick += (s, e) => AtualizarContagem();
            timerContagem.Start();
        }

        void CriarTela()
        {
            Text = "Cliente UDP";
            Size = new Size(460, 440);
            StartPosition = FormStartPosition.CenterScreen;

            Controls.Add(new Label { Text = "Nome completo:", Location = new Point(15, 18), AutoSize = true });
            txtNome = new TextBox { Location = new Point(120, 15), Width = 310 };
            Controls.Add(txtNome);

            Controls.Add(new Label { Text = "E-mail:", Location = new Point(15, 50), AutoSize = true });
            txtEmail = new TextBox { Location = new Point(120, 47), Width = 310 };
            Controls.Add(txtEmail);

            btnCadastrar = new Button { Text = "Cadastrar", Location = new Point(120, 80), Width = 150 };
            btnCadastrar.Click += (s, e) => Cadastrar();
            Controls.Add(btnCadastrar);

            btnToken = new Button { Text = "Solicitar token agora", Location = new Point(280, 80), Width = 150 };
            btnToken.Click += (s, e) => SolicitarToken();
            Controls.Add(btnToken);

            lblToken = new Label
            {
                Text = "Token: -",
                Location = new Point(15, 125),
                AutoSize = true,
                Font = new Font("Consolas", 16, FontStyle.Bold)
            };
            Controls.Add(lblToken);

            lblValidade = new Label { Text = "Validade: -", Location = new Point(15, 160), AutoSize = true };
            Controls.Add(lblValidade);

            Controls.Add(new Label { Text = "Mensagens:", Location = new Point(15, 190), AutoSize = true });
            lstLog = new ListBox { Location = new Point(15, 210), Size = new Size(415, 170) };
            Controls.Add(lstLog);
        }

        // Envia uma mensagem para o servidor e espera a resposta.
        // Devolve null se o servidor não responder a tempo.
        string EnviarEReceber(string mensagem)
        {
            Log("Enviado: " + mensagem);
            comunicador.Enviar(mensagem, servidor);
            try
            {
                IPEndPoint origem;
                string resposta = comunicador.Receber(out origem);
                Log("Recebido: " + resposta);
                return resposta;
            }
            catch (SocketException)
            {
                Log("Servidor não respondeu.");
                return null;
            }
        }

        void Cadastrar()
        {
            string nome = txtNome.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (nome == "" || email == "")
            {
                MessageBox.Show("Preencha nome e e-mail.");
                return;
            }
            if (nome.Contains(';') || email.Contains(';'))
            {
                MessageBox.Show("Não use ';' nos campos.");
                return;
            }

            string resposta = EnviarEReceber("CADASTRO;" + nome + ";" + email);
            if (resposta == null)
            {
                MessageBox.Show("Servidor não respondeu. Ele está ligado?");
                return;
            }

            string[] partes = resposta.Split(';');
            MessageBox.Show(partes[1]);

            if (partes[0] == "OK")
            {
                // Cadastro feito: trava os campos e começa a pedir o token periodicamente
                txtNome.Enabled = false;
                txtEmail.Enabled = false;
                btnCadastrar.Enabled = false;
                SolicitarToken();
                timerToken.Start();
            }
        }

        void SolicitarToken()
        {
            string email = txtEmail.Text.Trim();
            if (email == "")
            {
                MessageBox.Show("Informe o e-mail.");
                return;
            }

            string resposta = EnviarEReceber("TOKEN;" + email);
            if (resposta == null)
            {
                return;
            }

            string[] partes = resposta.Split(';');
            if (partes[0] == "TOKEN")
            {
                lblToken.Text = "Token: " + partes[1];
                segundosRestantes = int.Parse(partes[2]);
                AtualizarContagem();
            }
            else
            {
                Log("Erro: " + partes[1]);
            }
        }

        void AtualizarContagem()
        {
            if (segundosRestantes > 0)
            {
                lblValidade.Text = "Validade: " + segundosRestantes + " segundos";
                segundosRestantes--;
            }
            else if (lblToken.Text != "Token: -")
            {
                lblValidade.Text = "Validade: expirado (aguardando novo token)";
            }
        }

        void Log(string texto)
        {
            lstLog.Items.Add(DateTime.Now.ToString("HH:mm:ss") + "  " + texto);
            lstLog.TopIndex = lstLog.Items.Count - 1; // rola para a última linha
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            timerToken.Stop();
            timerContagem.Stop();
            comunicador.Fechar();
            base.OnFormClosed(e);
        }
    }
}
