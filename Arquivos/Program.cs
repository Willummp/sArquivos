class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.White;

        string diretorioAtual = @"C:\Users";
        string diretorioAlvo = string.Empty;
        bool pararNaPastaAtual = false;
        bool parado = false;

        while (true)
        {
            if (!parado)
            {
                Console.WriteLine($"Diretório Atual: {diretorioAtual}");

                Console.ForegroundColor = ConsoleColor.Blue;

                string pergunta = diretorioAtual == @"C:\Users" ? "Entre com seu nome de usuário (ou 'sair' para finalizar, 'parar' para parar na pasta atual):" : $"Qual pasta você deseja acessar em '{diretorioAtual}' ('sair' para finalizar ou 'parar' para parar na pasta selecionada):";
                Console.WriteLine(pergunta);
                Console.ForegroundColor = ConsoleColor.Blue;
                string nomePasta = Console.ReadLine();

                if (nomePasta == "sair")
                    break;

                if (nomePasta == "parar")
                {
                    if (pararNaPastaAtual)
                    {
                        Console.WriteLine($"Você já parou na pasta '{diretorioAtual}'");
                        continue;
                    }

                    Console.WriteLine($"Parou na pasta '{diretorioAtual}'");
                    pararNaPastaAtual = true;
                    parado = true;
                    continue;
                }

                diretorioAlvo = Path.Combine(diretorioAtual, nomePasta);

                if (!Directory.Exists(diretorioAlvo))
                {
                    Console.WriteLine("Nome de pasta inválido. Por favor, tente novamente.");
                    continue;
                }

                diretorioAtual = diretorioAlvo;
                ListarPastas(diretorioAlvo);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Digite a extensão de arquivo que você deseja organizar (por exemplo, .torrent):");
                Console.ForegroundColor = ConsoleColor.Blue;
                string extensaoArquivo = Console.ReadLine();

                if (!ExisteTipoArquivo(diretorioAtual, extensaoArquivo))
                {
                    Console.WriteLine($"Não existem arquivos com a extensão '{extensaoArquivo}' na pasta '{diretorioAtual}'.");
                    break;
                }

                OrganizarArquivos(diretorioAtual, extensaoArquivo);
                break;
            }
        }

        Console.WriteLine("Pressione qualquer tecla para sair.");
        Console.ReadKey();
    }

    static void ListarPastas(string diretorio)
    {
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("Pastas no diretório atual:");
        string[] pastas = Directory.GetDirectories(diretorio);
        foreach (string pasta in pastas)
        {
            Console.WriteLine(Path.GetFileName(pasta));
        }
    }

    static bool ExisteTipoArquivo(string diretorio, string extensaoArquivo)
    {
        string[] arquivos = Directory.GetFiles(diretorio, $"*{extensaoArquivo}");
        return arquivos.Length > 0;
    }

    static void OrganizarArquivos(string diretorio, string extensaoArquivo)
    {
        string pastaAlvo = Path.Combine(diretorio, extensaoArquivo.TrimStart('.') + "s");
        Directory.CreateDirectory(pastaAlvo);

        string[] arquivos = Directory.GetFiles(diretorio, $"*{extensaoArquivo}");
        foreach (string arquivo in arquivos)
        {
            string nomeArquivo = Path.GetFileName(arquivo);
            string caminhoDestino = Path.Combine(pastaAlvo, nomeArquivo);
            File.Move(arquivo, caminhoDestino);
        }

        Console.Write($"Arquivos com a extensão '{extensaoArquivo}' foram organizados na pasta '");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(pastaAlvo);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(".");
    }
}