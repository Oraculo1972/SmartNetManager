using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartNetManager
{
    public class Firewall
    {
        public List<Dispositivo> AplicarFirewall(
            List<Dispositivo> dispositivosAtivos,
            Func<Dispositivo, bool> regraBloqueio)
        {
            var bloqueados =
                dispositivosAtivos
                .Where(regraBloqueio)
                .ToList();

            foreach (var dispositivo in bloqueados)
            {
                dispositivosAtivos.Remove(dispositivo);
            }

            return bloqueados;
        }
    }
}
