namespace Servidor
{
    // Representa um usuário cadastrado no servidor
    public class Pessoa
    {
        public string Nome;
        public string Email;

        // Token atual da pessoa e o momento em que ele foi gerado
        public string Token;
        public DateTime TokenGeradoEm;

        public Pessoa(string nome, string email)
        {
            Nome = nome;
            Email = email;
            Token = null; // ainda não pediu token
        }

        public override string ToString()
        {
            return Nome + " <" + Email + ">";
        }
    }
}
