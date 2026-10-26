using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNetManager
{
    public class Dashboard
    {
        public void Exibir(List<Dispositivo> dispositivosAtivos)
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("          DASHBOARD");
            Console.WriteLine("=================================");

            if (dispositivosAtivos == null ||
                dispositivosAtivos.Count == 0)
            {
                Console.WriteLine("\nNenhum dispositivo ativo.");
                return;
            }

            Console.WriteLine();

            ExibirConsumoTotal(dispositivosAtivos);

            Console.WriteLine();

            ExibirTop3Consumidores(dispositivosAtivos);

            Console.WriteLine();

            ExibirPorFabricante(dispositivosAtivos);

            Console.WriteLine();

            ExibirAnomalias(dispositivosAtivos);
        }

        private void ExibirConsumoTotal(
            List<Dispositivo> dispositivos)
        {
            double total =
                dispositivos.Sum(d => d.ConsumoBanda);

            Console.WriteLine("CONSUMO TOTAL DE BANDA");
            Console.WriteLine($"Total: {total:F2} Mbps");
        }

        private void ExibirTop3Consumidores(
            List<Dispositivo> dispositivos)
        {
            Console.WriteLine("TOP 3 DISPOSITIVOS");

            var top3 =
                dispositivos
                .OrderByDescending(d => d.ConsumoBanda)
                .Take(3);

            int posicao = 1;

            foreach (var dispositivo in top3)
            {
                Console.WriteLine(
                    $"{posicao}º {dispositivo.Nome} - " +
                    $"{dispositivo.ConsumoBanda} Mbps");

                posicao++;
            }
        }

        private void ExibirPorFabricante(
            List<Dispositivo> dispositivos)
        {
            Console.WriteLine("DISPOSITIVOS POR FABRICANTE");

            var fabricantes =
                dispositivos
                .GroupBy(d => d.Fabricante)
                .OrderBy(g => g.Key);

            foreach (var grupo in fabricantes)
            {
                Console.WriteLine(
                    $"{grupo.Key}: {grupo.Count()}");
            }
        }

        private void ExibirAnomalias(
            List<Dispositivo> dispositivos)
        {
            Console.WriteLine("ALERTA DE ANOMALIA");

            var anomalias =
                dispositivos
                .Where(d => d.ConsumoBanda > 500)
                .ToList();

            if (!anomalias.Any())
            {
                Console.WriteLine(
                    "Nenhuma anomalia detectada.");
                return;
            }

            foreach (var dispositivo in anomalias)
            {
                Console.WriteLine(
                    $"{dispositivo.Nome} está consumindo " +
                    $"{dispositivo.ConsumoBanda} Mbps");
            }
        }
    }
}
