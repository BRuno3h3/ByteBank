using System.Security.Cryptography;
using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Exceptions;


namespace bytebank_ATENDIMENTO.bytebank.Atendimento
{
    public class ByteBankAtendimento
    {
        private List<ContaCorrente> _listaDeContas = new List<ContaCorrente>(){
        new ContaCorrente(95, "123456-X"){Saldo=100, Titular = new Cliente{Cpf="12345678910",Nome = "Bruno Tomasi"}},
        new ContaCorrente(95, "951258-X"){Saldo=200, Titular = new Cliente{Cpf="09876543210",Nome = "Jaimir Tomasi"}},
        new ContaCorrente(94, "987321-W"){Saldo=60,  Titular = new Cliente{Cpf="12378945601",Nome = "Daiana Moreira"}}
};

        public void AtendimentoCliente()
        {
            try
            {
                char opcao = '0';
                while (opcao != '6')
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("===       Atendimento       ===");
                    Console.WriteLine("===1 - Cadastrar Conta      ===");
                    Console.WriteLine("===2 - Listar Contas        ===");
                    Console.WriteLine("===3 - Remover Conta        ===");
                    Console.WriteLine("===4 - Ordenar Contas       ===");
                    Console.WriteLine("===5 - Pesquisar Conta      ===");
                    Console.WriteLine("===6 - Sair do Sistema      ===");
                    Console.WriteLine("===============================");
                    Console.WriteLine("\n\n");
                    Console.Write("Digite a opção desejada: ");
                    try
                    {
                        opcao = Console.ReadLine()[0];
                    }
                    catch (Exception excecao)
                    {
                        throw new ByteBankException(excecao.Message);
                    }

                    switch (opcao)
                    {
                        case '1':
                            CadastrarConta();
                            break;
                        case '2':
                            ListarContas();
                            break;
                        case '3':
                            RemoverContas();
                            break;
                        case '4':
                            OrdenarContas();
                            break;
                        case '5':
                            PesquisarContas();
                            break;
                        case '6':
                            EncerrarAtendimento();
                            break;
                        default:
                            Console.WriteLine("Opcao não implementada.");
                            break;
                    }
                }
            }
            catch (ByteBankException excecao)
            {
                Console.WriteLine($"{excecao.Message}");
            }

        }

        private void EncerrarAtendimento()
        {
            System.Console.WriteLine("... Encerrando a aplicação ...");
            Console.ReadKey();
        }

        private void PesquisarContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===     PESQUISAR CONTAS    ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            System.Console.WriteLine("Deseja pesquisar por 1-NÚ MERO DA CONTA ou 2-CPF TITULAR ou 3-AGÊNCIA");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    {
                        System.Console.WriteLine("Informe o número da Conta: ");
                        string _numeroConta = Console.ReadLine();
                        ContaCorrente consultaConta = ConsultarPorNumeroConta(_numeroConta);
                        System.Console.WriteLine(consultaConta.ToString());
                        Console.ReadKey();
                        break;
                    }
                case 2:
                    {
                        System.Console.WriteLine("Informe o número do CPF do titular: ");
                        string _cpf = Console.ReadLine();
                        ContaCorrente consultaCpf = ConsultarPorCpfTitular(_cpf);
                        System.Console.WriteLine(consultaCpf.ToString());
                        Console.ReadKey();
                        break;
                    }
                case 3:
                    {
                        System.Console.WriteLine("Informe o número da Agência do titular: ");
                        int _numeroAgencia = int.Parse(Console.ReadLine());
                        var consultaAgencia = ConsultarPorAgencia(_numeroAgencia);
                        ExibirListaDeContas(consultaAgencia);
                        Console.ReadKey();
                        break;
                    }
                default:
                    {
                        System.Console.WriteLine("Opção não valida");
                        break;
                    }

            }
        }

        private void ExibirListaDeContas(List<ContaCorrente> consultarPorAgencia)
        {
            if (ConsultarPorAgencia == null)
            {
                System.Console.WriteLine("... A Consulta não retornou dados ...");
            }
            else
            {
                foreach (var item in consultarPorAgencia)
                {
                    System.Console.WriteLine(item.ToString());
                }
            }
        }

        private List<ContaCorrente> ConsultarPorAgencia(int numeroAgencia)
        {
            var consulta = (

                from conta in _listaDeContas
                where conta.Numero_agencia == numeroAgencia
                select conta).ToList();

            return consulta;
        }

        private ContaCorrente ConsultarPorCpfTitular(string? cpf)
        {
            // // ContaCorrente conta = null;
            // // for (int i = 0; i < _listaDeContas.Count; i++)
            // // {
            // //     if (_listaDeContas[i].Titular.Cpf.Equals(cpf))
            // //     {
            // //         conta = _listaDeContas[i];
            // //     }
            // // }
            // return conta;
            return _listaDeContas.Where(conta => conta.Titular.Cpf == cpf).FirstOrDefault();
        }

        private ContaCorrente ConsultarPorNumeroConta(string? numeroConta)
        {
            ContaCorrente conta = null;
            //     for (int i = 0; i < _listaDeContas.Count; i++)
            //     {
            //         if (_listaDeContas[i].Conta.Equals(numeroConta))
            //         {
            //             conta = _listaDeContas[i];
            //         }
            //     }
            //     return conta;
            return _listaDeContas.Where(conta => conta.Conta == numeroConta).FirstOrDefault();
        }

        private void OrdenarContas()
        {
            _listaDeContas.Sort();
            Console.WriteLine("... Lista de Contas ordenadas ...");
            Console.ReadKey();
        }

        private void RemoverContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===      REMOVER CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            Console.Write("Informe o número da Conta: ");
            string numeroConta = Console.ReadLine();
            ContaCorrente conta = null;
            foreach (var item in _listaDeContas)
            {
                if (item.Conta.Equals(numeroConta))
                {
                    conta = item;
                }
            }
            if (conta != null)
            {
                _listaDeContas.Remove(conta);
                Console.WriteLine("... Conta removida da lista! ...");
            }
            else
            {
                Console.WriteLine(" ... Conta para remoção não encontrada ...");
            }
            Console.ReadKey();
        }

        private void ListarContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===     LISTA DE CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            if (_listaDeContas.Count <= 0)
            {
                Console.WriteLine("... Não há contas cadastradas! ...");
                Console.ReadKey();
                return;
            }
            foreach (ContaCorrente item in _listaDeContas)
            {
                // Console.WriteLine("===  Dados da Conta  ===");
                // Console.WriteLine("Número da Conta : " + item.Conta);
                // Console.WriteLine("Saldo da Conta : " + item.Saldo);
                // Console.WriteLine("Titular da Conta: " + item.Titular.Nome);
                // Console.WriteLine("CPF do Titular  : " + item.Titular.Cpf);
                // Console.WriteLine("Profissão do Titular: " + item.Titular.Profissao);
                System.Console.WriteLine(item.ToString);
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                Console.ReadKey();
            }

        }

        private void CadastrarConta()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===   CADASTRO DE CONTAS    ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            Console.WriteLine("=== Informe dados da conta ===");

            Console.Write("Número da Agência: ");
            int numeroAgencia = int.Parse(Console.ReadLine());
            ContaCorrente conta = new ContaCorrente(numeroAgencia);
            
            System.Console.WriteLine($"Numero da conta [NOVA] : {conta.Conta}");

            Console.Write("Informe o saldo inicial: ");
            conta.Saldo = double.Parse(Console.ReadLine());

            Console.Write("Infome nome do Titular: ");
            conta.Titular.Nome = Console.ReadLine();

            Console.Write("Infome CPF do Titular: ");
            conta.Titular.Cpf = Console.ReadLine();

            Console.Write("Infome Profissão do Titular: ");
            conta.Titular.Profissao = Console.ReadLine();

            _listaDeContas.Add(conta);

            Console.WriteLine("... Conta cadastrada com sucesso! ...");
            Console.ReadKey();
        }
    }
}