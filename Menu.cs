using System;
using System.Data;
using System.Collections.Generic;


namespace SmartNetManager
{
    public class Menu
    {
        private ServidorDHCP _dhcp;
        private GerenciadorDispositivos _gerenciadorDispositivos;
        private GerenciadorAutenticacao _autenticacao;
        private Dashboard _dashboard;
        private Relatorio _relatorio;
        private Auditoria _auditoria;
        private Firewall _firewall;

        //construtores
        public Menu()
        {
            _dhcp = new ServidorDHCP();
            _gerenciadorDispositivos = new GerenciadorDispositivos();
            _autenticacao = new GerenciadorAutenticacao();
            _dashboard = new Dashboard();
            _relatorio = new Relatorio();
            _auditoria = new Auditoria();
            _firewall = new Firewall();
        }


        internal void Exibir()
        {
            int opcao;
            do
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine(" SMARTNET MANAGER");
                Console.WriteLine(" Painel Administrativo");
                Console.WriteLine("======================================");
                Console.WriteLine();

                Console.WriteLine("1 - Gerenciar DHCP");
                Console.WriteLine("2 - Cadastro Mestre de Dispositivos");
                Console.WriteLine("3 - Fila de Autenticação");
                Console.WriteLine("4 - Exibir Dashboard");
                Console.WriteLine("5 - Firewall");
                Console.WriteLine("6 - Auditoria");
                Console.WriteLine("7 - Relatórios");
                Console.WriteLine("0 - Sair");

                Console.Write("\nEscolha uma opção: ");
                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    // acessa o gerenciador de DHCP
                    case 1:
                        MenuDhcp();
                        break;

                    // acessa o cadastro mestre de Dispositivo
                    case 2:
                        MenuDispositivos();
                        break;

                    // acessa a autenticaçao de Dispositivos
                    case 3:
                        MenuAutenticacao();
                        break;
                    // acessa o Dasshboard
                    case 4:

                        _dashboard.Exibir(_autenticacao.ObterAtivos());
                        break;

                    // acessa o Confiração de Firewall
                    case 5:
                        MenuFirewall();
                        break;

                    // acessa Auditoria
                    case 6:
                        MenuAuditoria();
                        break;

                    // acess Releatorios
                    case 7:
                        MenuRelatorios();
                        break;

                    //Sai do Sistema
                    case 0:
                        Console.WriteLine("\nEncerrando...");
                        break;

                    default:
                        Console.WriteLine("\nOpção inválida.");
                        break;
                }
                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione qualquer tecla...");
                    Console.ReadKey();
                }
            } while (opcao != 0);
        }

        #region DHCP   //Este trecho #region DHCP cadastra o Reseve de IP

        private void MenuDhcp()
        {
            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("===== DHCP =====");
                Console.WriteLine("1 - Cadastrar Reserva");
                Console.WriteLine("2 - Listar Reservas");
                Console.WriteLine("0 - Voltar");

                Console.Write("\nOpção: ");

                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:

                        Console.Write("MAC Address: ");
                        string mac = Console.ReadLine();

                        Console.Write("Endereço IP: ");
                        string ip = Console.ReadLine();

                        //Bonus   valida formato de MAC
                        if (!Dispositivo.ValidarMac(mac))
                        {
                            Console.WriteLine("\nMAC inválido.");
                            break;
                        }

                        //Bonus valida formato IP
                        if (!Dispositivo.ValidarIP(ip))
                        {
                            Console.WriteLine("\nIP inválido.");
                            break;
                        }

                        bool sucesso = _dhcp.CadastrarReserva(mac, ip);

                        // Condiçao verifica se já tem um MAC cadastrado
                        Console.WriteLine(
                                        sucesso
                                        ? "\nReserva cadastrada com sucesso."
                                        : "\nMAC já cadastrado.");

                        break;

                    case 2:

                        _dhcp.ListarReservas();

                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione uma tecla...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        #endregion  // Final do bloco Reserva de IP

        #region
        private void MenuDispositivos()
        {
            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("===== CADASTRO DE DISPOSITIVOS =====");
                Console.WriteLine("1 - Cadastrar");  //caminho da entrada dos dados Passo 1
                Console.WriteLine("2 - Consultar por MAC");
                Console.WriteLine("3 - Listar");
                Console.WriteLine("4 - Remover");
                Console.WriteLine("0 - Voltar");

                Console.Write("\nOpção: ");

                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:
                        CadastrarDispositivo();  // Passo 2, ao ativar o menu 1 Cadastrar o metodo é chamado
                        break;

                    case 2:
                        ConsultarDispositivo();
                        break;

                    case 3:
                        ListarDispositivos();
                        break;

                    case 4:
                        RemoverDispositivo();
                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione uma tecla...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        private void CadastrarDispositivo()
        {
            Dispositivo dispositivo =
                new Dispositivo();      // Passo 3, o objeto é criado para receber os dados
                                        // no escopo da classe Dispositivo

            //Passo 4 Recebe os dados e valida
            Console.Write("Nome: ");
            dispositivo.Nome = Console.ReadLine();

            Console.Write("Fabricante: ");
            dispositivo.Fabricante = Console.ReadLine();

            Console.Write("MAC Address: ");
            dispositivo.MacAddress = Console.ReadLine();

            //Valida o Mec
            if (!Dispositivo.ValidarMac(dispositivo.MacAddress))
            {
                Console.WriteLine("\nMAC inválido.");
                return;
            }

            Console.Write("Endereço IP: ");
            dispositivo.EnderecoIP = Console.ReadLine();

            //Valida IP
            if (!Dispositivo.ValidarIP(dispositivo.EnderecoIP))
            {
                Console.WriteLine("\nIP inválido.");
                return;
            }

            Console.Write("Consumo Banda (Mbps): ");

            //testa a entrada de banda para evitar queo o codigo quebre se usuario digitatr "teste", "abc"
            string banda = Console.ReadLine();
            banda = banda.Replace('.', ',');

            if (!double.TryParse(
                    banda, out double consumo))
            {
                Console.WriteLine(
                    "\nValor de consumo inválido.");

                return;
            }

            dispositivo.ConsumoBanda = consumo;

            // Passo 5, se tudo der certo (True) chama
            // o metodo Cadastra(dispositivo) na classe GerenciarDispositivo (Passo 6)
            bool sucesso =
                _gerenciadorDispositivos
                .Cadastrar(dispositivo);

            Console.WriteLine(
                sucesso
                ? "\nDispositivo cadastrado."
                : "\nMAC já existente.");
        }

        private void ConsultarDispositivo()
        {
            Console.Write("MAC Address: ");

            string mac = Console.ReadLine();

            var dispositivo =
                _gerenciadorDispositivos
                .ConsultarPorMac(mac);

            if (dispositivo == null)
            {
                Console.WriteLine("\nDispositivo não encontrado.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine(dispositivo);
        }

        private void ListarDispositivos()
        {
            var dispositivos =
                _gerenciadorDispositivos.Listar();

            if (dispositivos.Count == 0)
            {
                Console.WriteLine("\nNenhum dispositivo cadastrado.");
                return;
            }

            foreach (var dispositivo in dispositivos)
            {
                Console.WriteLine("--------------------------------");
                Console.WriteLine(dispositivo);
            }
        }

        private void RemoverDispositivo()
        {
            Console.Write("MAC Address: ");

            string mac = Console.ReadLine();

            bool removido =
                _gerenciadorDispositivos
                .Remover(mac);

            Console.WriteLine(
                removido
                ? "\nDispositivo removido."
                : "\nDispositivo não encontrado.");
        }

        #endregion  //Final do bloco cadastra dispositivo
        
        #region Autenticacao  // Inicio do bloco Autenticar o dispositivo

        private void MenuAutenticacao()
        {
            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine("===== FILA DE AUTENTICAÇÃO =====");
                Console.WriteLine("1 - Enviar Dispositivo para Fila");
                Console.WriteLine("2 - Exibir Fila");
                Console.WriteLine("3 - Autenticar Próximo");
                Console.WriteLine("4 - Exibir Ativos");
                Console.WriteLine("0 - Voltar");

                Console.Write("\nOpção: ");

                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:

                        AdicionarDispositivoFila();

                        break;

                    case 2:

                        _autenticacao.ExibirFila();

                        break;

                    case 3:

                        Dispositivo autenticado =
        			        _autenticacao.ProcessarFila();

    				    if (autenticado != null)
    				    {
        			    _auditoria.RegistrarAcao(
            			    $"Dispositivo autenticado: {autenticado.Nome} - {autenticado.MacAddress}");
    				    }

    			        break;
                        
                    case 4:

                        _autenticacao.ExibirAtivos();

                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine("\nPressione uma tecla...");
                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        private void AdicionarDispositivoFila()
        {
            Console.Write("MAC do dispositivo: ");

            string mac = Console.ReadLine();

            var dispositivo =
                _gerenciadorDispositivos
                .ConsultarPorMac(mac);

            if (dispositivo == null)
            {
                Console.WriteLine("\nDispositivo não cadastrado.");
                return;
            }

            bool sucesso =
    		_autenticacao
    		.AdicionarNaFila(dispositivo);

	    Console.WriteLine(
    			sucesso
    				? "\nDispositivo enviado para fila."
    				: "\nDispositivo já está na fila ou já está ativo.");
        }

        #endregion //Final da bloco Valida dispositivo
 
	#region // Inicio da Região imprime relatorio
        private void MenuRelatorios()
        {
            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine(
                    "===== RELATÓRIOS =====");

                Console.WriteLine(
                    "1 - Relatório da Fila");

                Console.WriteLine(
                    "2 - Relatório dos Ativos");

                Console.WriteLine(
                    "0 - Voltar");

                Console.Write("\nOpção: ");

                int.TryParse(
                    Console.ReadLine(),
                    out opcao);

                switch (opcao)
                {
                    case 1:

                        _relatorio.ImprimirRelatorio(
                            _autenticacao.ObterFila());

                     break;

                    case 2:

                        _relatorio.ImprimirRelatorio(
                            _autenticacao.ObterAtivos());

                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine(
                        "\nPressione uma tecla...");

                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

	#endregion  // Fim do bloco imprimir relatorio

	
	#region // Inicio do Bloco Auditoria

        private void MenuAuditoria()
        {
            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine(
                    "===== AUDITORIA =====");

                Console.WriteLine(
                    "1 - Exibir Histórico");

                Console.WriteLine(
                    "2 - Desfazer Última Ação");

                Console.WriteLine(
                    "0 - Voltar");

                Console.Write("\nOpção: ");

                int.TryParse(
                    Console.ReadLine(),
                    out opcao);

                switch (opcao)
                {
                    case 1:

                        _auditoria.ExibirHistorico();

                        break;

                    case 2:

                        _auditoria.DesfazerUltimaAcao();

                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine(
                        "\nPressione uma tecla...");

                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

	#endregion
	

	#region  // inicio do bloco Firewall
	
        private void MenuFirewall()
        {
            int opcao;

            do
            {
                Console.Clear();

                Console.WriteLine(
                    "===== FIREWALL =====");

                Console.WriteLine(
                    "1 - Bloquear Fabricante Apple");

                Console.WriteLine(
                    "2 - Bloquear Fabricante Samsung");

                Console.WriteLine(
                    "3 - Bloquear Consumo > 100 Mbps");

                Console.WriteLine(
                    "4 - Bloquear Consumo > 500 Mbps");

                Console.WriteLine(
                    "0 - Voltar");

                Console.Write("\nOpção: ");

                int.TryParse(
                    Console.ReadLine(),
                    out opcao);

                switch (opcao)
                {
                    case 1:

                        AplicarRegraFirewall(
                            d => d.Fabricante == "Apple",
                            "Bloqueio de Fabricante Apple");

                        break;

                    case 2:

                        AplicarRegraFirewall(
                            d => d.Fabricante == "Samsung",
                            "Bloqueio de Fabricante Samsung");

                        break;

                    case 3:

                        AplicarRegraFirewall(
                            d => d.ConsumoBanda > 100,
                            "Bloqueio por Consumo > 100 Mbps");

                        break;

                    case 4:

                        AplicarRegraFirewall(
                            d => d.ConsumoBanda > 500,
                            "Bloqueio por Consumo > 500 Mbps");

                        break;
                }

                if (opcao != 0)
                {
                    Console.WriteLine(
                        "\nPressione uma tecla...");

                    Console.ReadKey();
                }

            } while (opcao != 0);
        }

        private void AplicarRegraFirewall(
                    Func<Dispositivo, bool> regra,string descricao)
        {
            var bloqueados =
                _firewall.AplicarFirewall(
                    _autenticacao.ObterAtivos(),
                    regra);

            Console.WriteLine();
            Console.WriteLine($"Regra aplicada: {descricao}");

            if (!bloqueados.Any())
            {
                Console.WriteLine(
                    "Nenhum dispositivo bloqueado.");
            }
            else
            {
                Console.WriteLine(
                    "Dispositivos bloqueados:");

                foreach (var dispositivo in bloqueados)
                {
                    Console.WriteLine(
                        $"{dispositivo.Nome}");
                }
            }

            _auditoria.RegistrarAcao(
                $"Firewall aplicado: {descricao}");
        }
	#endregion
    }

}
