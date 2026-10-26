using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace SmartNetManager
{
    public class Dispositivo
    {
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public string MacAddress { get; set; }
        public string EnderecoIP { get; set; }
        public double ConsumoBanda { get; set; }
        public DateTime DataConexao {  get; set; }

        public static bool ValidarMac(string mac)
        {
            string padrao =
                @"^([0-9A-Fa-f]{2}:){5}[0-9A-Fa-f]{2}$";

            return Regex.IsMatch(mac, padrao);
        }

        public static bool ValidarIP(string ip)
        {
            string padrao =
                @"^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

            return Regex.IsMatch(ip, padrao);
        }

        public override string ToString()
        {
            return  $"{Nome} | {Fabricante} | " +
                    $"{MacAddress} | {EnderecoIP} | " +
                    $"{ConsumoBanda:F2} Mbps | " +
                    $"{DataConexao:dd/MM/yyyy HH:mm}";
        }

    }
}
