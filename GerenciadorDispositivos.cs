using System;


namespace SmartNetManager
{
    public class GerenciadorDispositivos
    {
       
        private List<Dispositivo> dispositivos; //Cria a lista para receber os dados que veio 
                                                

        public GerenciadorDispositivos()
        {
            dispositivos = new List<Dispositivo>(); //Passo 6, aqui é cria o construtor que armazena os dados
        }

        // bloco reponsavel pelo CRUD

        public bool Cadastrar(Dispositivo dispositivo)
        {
            //  Passo 7
            //  verifica se tem dados
            if (dispositivos.Any(d =>
                d.MacAddress == dispositivo.MacAddress))
            {
                return false;
            }

            // Passo Final
            // Responsavel por adicionar os registro na lista 
            dispositivos.Add(dispositivo);
            return true;
        }

              
        public List<Dispositivo> Listar()
        {
            return dispositivos;
        }



        public Dispositivo ConsultarPorMac(string mac)
        {
            return dispositivos.FirstOrDefault(d => d.MacAddress == mac);
        }

        public bool Remover(string mac)
        {
            Dispositivo dispositivo = ConsultarPorMac(mac);

            if (dispositivo == null)
            {
                return false;
            }

            dispositivos.Remove(dispositivo);

            return true;
        }
    }
}
