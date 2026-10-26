using System;
using System.Collections.Generic;

namespace SmartNetManager
{
    public class Relatorio
    {
        public void ImprimirRelatorio(IEnumerable<Dispositivo> colecao)
        {
            Console.WriteLine();
            Console.WriteLine("===== RELATÓRIO =====");

            bool possuiItens = false;

            foreach (var dispositivo in colecao)
            {
                possuiItens = true;

                Console.WriteLine("---------------------");
                Console.WriteLine(dispositivo);
            }

            if (!possuiItens)
            {
                Console.WriteLine(
                    "Nenhum dispositivo encontrado.");
            }
        }
    }
}
