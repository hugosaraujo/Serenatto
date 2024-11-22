using SerenattoEnsaio.Dados;
using SerenattoEnsaio.Modelos;
using SerenattoPreGravacao.Dados;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

IEnumerable<Cliente> clientes = DadosClientes.GetClientes().ToList();
IEnumerable<string> formasPagamento = DadosFormaDePagamento.FormasDePagamento;
IEnumerable<Produto> cardapioLoja = DadosCardapio.GetProdutos().ToList();
IEnumerable<Produto> cardapioDelivery = DadosCardapio.CardapioDelivery();
IEnumerable<int> totalPedidosMes = DadosPedidos.QuantidadeItensPedidosPorDia.SelectMany(lista => lista);


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

foreach (var pedido in totalPedidosMes)
{
    Console.Write($"{pedido} ");
}

Console.WriteLine();

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIO QUANTIDADE DE PEDIDOS INDIVIDUAIS MÊS");

var pedidosIndividuais = totalPedidosMes
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

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIO PEDIDOS COM QUANTIDADE DIFERENTES");

var totalPedidosDiferentes = totalPedidosMes.Distinct();

foreach (var itemPedido  in totalPedidosDiferentes)
{
    Console.Write($"{itemPedido} ");
}

Console.WriteLine();
Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIO PRODUTOS EXCLUSIVOS LOJA");

var produtosLoja = cardapioLoja.Select(p => p.Nome);
var produtosDelivery = cardapioDelivery.Select(p => p.Nome);

var produtosExclusivosLoja = produtosLoja.Except(produtosDelivery).ToList();

foreach (var produto in produtosExclusivosLoja)
{
    Console.WriteLine(produto);
}

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIO PRODUTOS LOJA E DELIVERY");

var listaProdutosCardapioEDelivey = produtosLoja.Intersect(produtosDelivery).ToList(); 

foreach (var produto in listaProdutosCardapioEDelivey)
{
    Console.WriteLine(produto);
}

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIO PRODUTOS OFERECIDOS PELA LOJA");

var listaGeralProdutos = listaProdutosCardapioEDelivey.Union(produtosDelivery).ToList();

foreach (var produto in listaGeralProdutos)
{
    Console.WriteLine(produto);
}


Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIO PRODUTOS OFERECIDOS PELA LOJA");

var cardapioOrdenado = cardapioLoja
    .OrderBy(p => p.Nome)
    .ThenBy(p => p.Preco);

foreach (var produto in cardapioOrdenado)
{
    Console.WriteLine($"{produto.Nome} | R$ {produto.Preco},00");
}

Console.WriteLine("-------------------------------------------------------------------------------");
Console.WriteLine("RELATÓRIO NOMES E ENDERECO CLIENTES PROMOÇÃO");

var clientePromocao = clientes
    .OrderBy(c => c.Nome)
    .ThenBy(c => c.Endereco)
    .ToList();

foreach(var cliente in clientePromocao)
{
    Console.WriteLine($"{cliente.Nome} | {cliente.Endereco}");
}