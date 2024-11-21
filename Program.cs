using SerenattoEnsaio.Dados;
using SerenattoEnsaio.Modelos;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

IEnumerable<Cliente> clientes = DadosClientes.GetClientes().ToList();
IEnumerable<string> formasPagamento = DadosFormaDePagamento.FormasDePagamento;

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