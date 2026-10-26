using System;


namespace SmartNetManager
{
    public class ServidorDHCP
    {
        private Dictionary<string, string> _reservasIP;
        public ServidorDHCP()
        {
            _reservasIP = new Dictionary<string, string>();
        }

        public bool CadastrarReserva(string macAddress, string enderecoIP)
        {
            if (_reservasIP.ContainsKey(macAddress))
            {
                return false;
            }

            _reservasIP.Add(macAddress, enderecoIP);
            return true;
        }
        public void ListarReservas()
        {
            Console.WriteLine("\n===== RESERVAS DHCP =====");

            if (_reservasIP.Count == 0)
            {
                Console.WriteLine("Nenhuma reserva cadastrada.");
                return;
            }

            foreach (var reserva in _reservasIP)
            {
                Console.WriteLine($"MAC: {reserva.Key}");
                Console.WriteLine($"IP : {reserva.Value}");
                Console.WriteLine("--------------------------");
            }
        }

    }

}
