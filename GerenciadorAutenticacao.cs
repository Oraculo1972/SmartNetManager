using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNetManager
{
    public class GerenciadorAutenticacao
    {
        private Queue<Dispositivo> filaAutenticacao;
        private List<Dispositivo> dispositivosAtivos;

        public GerenciadorAutenticacao()
        {
            filaAutenticacao = new Queue<Dispositivo>();
            dispositivosAtivos = new List<Dispositivo>();
        }

       
        public bool AdicionarNaFila(Dispositivo dispositivo)
        {
            bool jaNaFila =
                filaAutenticacao.Any(
                    d => d.MacAddress == dispositivo.MacAddress);

            bool jaAtivo =
                dispositivosAtivos.Any(
                    d => d.MacAddress == dispositivo.MacAddress);

            if (jaNaFila || jaAtivo)
            {
                return false;
            }

            filaAutenticacao.Enqueue(dispositivo);

            return true;
        }



        public Queue<Dispositivo> ObterFila()
        {
            return filaAutenticacao;
        }
        

        public List<Dispositivo> ObterAtivos()
        {
            return dispositivosAtivos;
        }

       

        public Dispositivo ProcessarFila()
        {
            if (filaAutenticacao.Count == 0)
            {
                Console.WriteLine("Fila vazia.");
                return null;
            }

            Dispositivo dispositivo = filaAutenticacao.Dequeue();

            dispositivo.DataConexao = DateTime.Now;

            dispositivosAtivos.Add(dispositivo);

            Console.WriteLine(
                $"Dispositivo {dispositivo.Nome} autenticado com sucesso.");

            return dispositivo;
        }

        public void ExibirFila()
        {
            Console.WriteLine("\n===== FILA =====");

            if (filaAutenticacao.Count == 0)
            {
                Console.WriteLine("Nenhum dispositivo na fila.");
                return;
            }

            foreach (var dispositivo in filaAutenticacao)
            {
                Console.WriteLine(
                    $"{dispositivo.Nome} - {dispositivo.Fabricante}");
            }
        }

        public void ExibirAtivos()
        {
            Console.WriteLine("\n===== ATIVOS =====");

            if (dispositivosAtivos.Count == 0)
            {
                Console.WriteLine("Nenhum dispositivo ativo.");
                return;
            }

            foreach (var dispositivo in dispositivosAtivos)
            {
                Console.WriteLine(
                    $"{dispositivo.Nome} - {dispositivo.EnderecoIP}");
            }
        }
    }
}
