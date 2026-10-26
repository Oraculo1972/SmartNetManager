using System;


namespace SmartNetManager
{
    public class Auditoria
    {
        private Stack<string> historico;

        public Auditoria()
        {
            historico = new Stack<string>();
        }

        public void RegistrarAcao(string mensagem)
        {
            historico.Push(
                $"{DateTime.Now:dd/MM/yyyy HH:mm:ss} - {mensagem}");
        }

        public void DesfazerUltimaAcao()
        {
            if (historico.Count == 0)
            {
                Console.WriteLine(
                    "Nenhuma ação para desfazer.");

                return;
            }

            string acao = historico.Pop();

            Console.WriteLine();
            Console.WriteLine("AÇÃO DESFEITA:");
            Console.WriteLine(acao);
        }

        public void ExibirHistorico()
        {
            Console.WriteLine();
            Console.WriteLine(
                "===== HISTÓRICO DE AUDITORIA =====");

            if (historico.Count == 0)
            {
                Console.WriteLine(
                    "Nenhuma ação registrada.");

                return;
            }

            foreach (string item in historico)
            {
                Console.WriteLine(item);
            }
        }
    }
}
