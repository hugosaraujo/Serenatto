using SerenattoEnsaio.Dados;
using SerenattoEnsaio.Modelos;
using SerenattoPreGravacao.Dados;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

IEnumerable<Cliente> clientes = DadosClientes.GetClientes().ToList();
IEnumerable<string> formasPagamento = DadosFormaDePagamento.FormasDePagamento;
IEnumerable<Produto> cardapioLoja = DadosCardapio.GetProdutos().ToList();


Console.WriteLine("RELATÓRIO DE DADOS CLIENTES");
foreach(var cliente in clientes)
{
    Console.WriteLine($"{cliente.Id} | {cliente.Nome} | {cliente.Endereco} | {cliente.Telefone}");
}


Console.WriteLine("-------------------------------------------------------------------------------");
var pesquisa = from p in formasPagamento
               where p.Contains('i')
               select p;

Console.WriteLine(string.Join(" ", pesquisa));

var pesquisaLinq = formasPagamento.Where(p => p.StartsWith('p'));
Console.WriteLine(string.Join(" ", pesquisaLinq));

//Console.WriteLine("-------------------------------------------------------------------------------");
//Console.WriteLine("RELATÓRIOS CLIENTES POR LETRA");

//Console.Write("Pesquisa uma letra para pesquisar o nome do cliente: ");
//char letraPesquisa = char.Parse(Console.ReadLine()!);
//var pesquisaPorNome = clientes.Where(c => c.Nome.StartsWith(letraPesquisa.ToString().ToUpper()));
//foreach(var cliente in pesquisaPorNome)
//{
//    Console.WriteLine(cliente.Nome);
//}

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIOS DADOS CARDÁPIOS");

foreach (var item in cardapioLoja)
{
    Console.WriteLine($"{item.Id} | {item.Nome} | {item.Descricao} | {item.Preco}");
}


Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIOS INFORMAÇÕES CARDÁPIOS POR NOME");

var itensUnicosCardapioPorNome = cardapioLoja.Select(p => p.Nome).Distinct();

foreach (var item in itensUnicosCardapioPorNome)
{
    Console.WriteLine(item);
}

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIOS INFORMAÇÕES CARDÁPIOS POR NOME E PRECO");

var itensPrecoENome = cardapioLoja.Select(p => new
{
    produtoNome = p.Nome,
    produtoPreco = p.Preco
});

foreach (var item in itensPrecoENome)
{
    Console.WriteLine($"{item.produtoNome} | R$ {item.produtoPreco},00");
}

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIOS INFORMAÇÕES CARDÁPIOS POR NOME E PRECO NO COMBO PAGUE 3 E LEVE 4");

var itensPrecoCombo = cardapioLoja.Select(p => new
{
    produtoNome = p.Nome,
    produtoPrecoCombo = p.Preco * 3
});

foreach (var item in itensPrecoCombo)
{
    Console.WriteLine($"{item.produtoNome} | R$ {item.produtoPrecoCombo},00");
}

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIO QUANTIDADE DE PRODUTOS PEDIDOS NO MÊS");

IEnumerable<int> totalPedidosMes = DadosPedidos.QuantidadeItensPedidosPorDia.SelectMany(lista => lista);

foreach (var pedido in totalPedidosMes)
{
    Console.Write($"{pedido} ");
}

Console.WriteLine();

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIO QUANTIDADE DE PEDIDOS INDIVIDUAIS MÊS");

var pedidosIndividuais = DadosPedidos.QuantidadeItensPedidosPorDia
    .SelectMany(lista => lista)
    .Count(n => n == 1);

Console.WriteLine($"A quentidade de pedidos individuais no mês foi: {pedidosIndividuais}");

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIO COM A RELAÇÃO NOME E CONTATO DOS CLIENTES");

var contatoENomeCliente = clientes.Select(c => new
{
    nomeCliente = c.Nome,
    contatoCliente = c.Telefone
});

foreach (var cliente in contatoENomeCliente)
{
    Console.WriteLine($"{cliente.nomeCliente} | {cliente.contatoCliente}");
}