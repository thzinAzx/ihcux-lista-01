class Program
{
    static Dictionary<int, (string nome, double preco)> cardapio = new()
    {
        { 1,  ("Coxinha",          5.00) },
        { 2,  ("Pão de Queijo",    3.50) },
        { 3,  ("Suco de Laranja",  6.00) },
        { 4,  ("Água Mineral",     2.50) },
        { 5,  ("Sanduíche Natural",9.00) },
        { 6,  ("Café",             3.00) },
        { 7,  ("Refrigerante",     4.50) },
        { 8,  ("Iogurte",          4.00) },
        { 9,  ("Barra de Cereal",  3.00) },
        { 10, ("Brownie",          6.50) },
    };

    static void MostrarProgresso(int passo, int total, string descricao)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║     CANTINA UNA — PEDIDO ONLINE      ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();

        string barra = "[";
        for (int i = 1; i <= total; i++)
            barra += i <= passo ? "█" : "░";
        barra += "]";

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($" {barra}  Passo {passo} de {total}: {descricao}");
        Console.ResetColor();
        Console.WriteLine(new string('─', 42));
    }

    static void MostrarCardapio()
    {
        Console.WriteLine("\n  Cardápio disponível:");
        foreach (var item in cardapio)
            Console.WriteLine($"  [{item.Key,2}] {item.Value.nome,-22} R$ {item.Value.preco:F2}");
        Console.WriteLine();
    }

    static void Cancelar()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n  Pedido cancelado. Até logo!");
        Console.ResetColor();
        Environment.Exit(0);
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string nomeAluno = "";
        int codigoProduto = 0;
        int quantidade = 0;
        int etapa = 1;


        while (etapa <= 3)
        {
            if (etapa == 1)
            {
                MostrarProgresso(1, 3, "Identificação do Aluno");
                Console.Write("  Seu nome (ou 'cancelar'): ");
                string entrada = Console.ReadLine()?.Trim() ?? "";

                if (entrada.Equals("cancelar", StringComparison.OrdinalIgnoreCase))
                    Cancelar();

                if (string.IsNullOrWhiteSpace(entrada))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ⚠  O nome não pode ficar em branco. Tente novamente.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                nomeAluno = entrada;
                etapa = 2;
            }

            else if (etapa == 2)
            {
                MostrarProgresso(2, 3, "Seleção do Item");
                MostrarCardapio();
                Console.Write("  Código do produto (ou 'voltar' / 'cancelar'): ");
                string entrada = Console.ReadLine()?.Trim() ?? "";

                if (entrada.Equals("cancelar", StringComparison.OrdinalIgnoreCase))
                    Cancelar();

                
                if (entrada.Equals("voltar", StringComparison.OrdinalIgnoreCase))
                {
                    etapa = 1;
                    continue;
                }

                if (!int.TryParse(entrada, out int codigo))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ⚠  Digite apenas números. Tente novamente.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                if (!cardapio.ContainsKey(codigo))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  ⚠  Código {codigo} não encontrado. Nossos códigos vão de 1 a 10. Tente novamente.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                codigoProduto = codigo;
                etapa = 3;
            }

            else if (etapa == 3)
            {
                MostrarProgresso(3, 3, "Quantidade");
                var (nome, preco) = cardapio[codigoProduto];
                Console.WriteLine($"  Item selecionado: {nome} — R$ {preco:F2}");
                Console.Write("\n  Quantidade desejada (ou 'voltar' / 'cancelar'): ");
                string entrada = Console.ReadLine()?.Trim() ?? "";

                if (entrada.Equals("cancelar", StringComparison.OrdinalIgnoreCase))
                    Cancelar();

                if (entrada.Equals("voltar", StringComparison.OrdinalIgnoreCase))
                {
                    etapa = 2;
                    continue;
                }

                if (!int.TryParse(entrada, out int qtd) || qtd <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  ⚠  Quantidade inválida. Digite um número inteiro maior que zero.");
                    Console.ResetColor();
                    Console.ReadKey();
                    continue;
                }

                quantidade = qtd;
                etapa = 4;
            }
        }

        Console.Clear();
        var produto = cardapio[codigoProduto];
        double total = produto.preco * quantidade;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║         PEDIDO CONFIRMADO! ✔         ║");
        Console.WriteLine("╚══════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine($"\n  Aluno   : {nomeAluno}");
        Console.WriteLine($"  Item    : {produto.nome}");
        Console.WriteLine($"  Qtd.    : {quantidade}x  R$ {produto.preco:F2}");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"  TOTAL   : R$ {total:F2}");
        Console.ResetColor();
        Console.WriteLine("\n  Retire seu pedido na cantina. Bom apetite!");
        Console.WriteLine(new string('─', 42));
    }
}