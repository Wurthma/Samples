using Dapper;
using DapperComSQL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperComSQL
{
    /// <summary>
    /// Exemplo do uso do Dapper com SQL Server
    /// </summary>
    /// <remarks>
    /// Utilizando o banco de dados de exemplo da MS (Northwind): https://docs.microsoft.com/pt-br/dotnet/framework/data/adonet/sql/linq/downloading-sample-databases
    /// Github: https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs
    /// </remarks>
    class Program
    {
        static void Main(string[] args)
        {
            //ExemploDapper1();
            //ExemploDapper2();
            //ExemploDapper3(40);
            //ExemploDapper4(1);
            //ExemploDapper5();
            ExemploDapper6();
        }

        /// <summary>
        /// Exemplo básico do uso do Dapper realizando a leitura de clientes e exibindo na tela.
        /// </summary>
        /// <remarks>Link com exemplo: http://www.macoratti.net/15/12/adn_dapper1.htm</remarks>
        private static void ExemploDapper1()
        {
            string strConexao = ConfigurationManager.ConnectionStrings["conexaoNorthwind"].ConnectionString;

            //Todos os métodos de extensão do Dapper assumem que a conexão já esta aberta. Se a conexão estiver fechada o código vai falhar
            //Você é responsável por abrir e fechar as conexões
            SqlConnection conexaoBD = new SqlConnection(strConexao);
            conexaoBD.Open();
            var resultado = conexaoBD.Query("Select * from Customers");

            //Retorna uma lista de objetos dinâmicos  eliminando a necessidade de definir objeto DTO e simplificando o código.
            Console.WriteLine("{0} - {1} - {2} ", "Código", "Nome do Contato", "Endereco do Cliente");
            foreach (dynamic cliente in resultado)
            {
                Console.WriteLine("{0} - {1} - {2} ", cliente.CustomerID, cliente.ContactName, cliente.Address);
            }
            conexaoBD.Close();
            Console.ReadLine();
        }

        /// <summary>
        /// Retornando uma lista tipada de objeto (IEnumerable<Produto>).
        /// <see cref="Produto"/>.
        /// </summary>
        private static void ExemploDapper2()
        {
            string strConexao = ConfigurationManager.ConnectionStrings["conexaoNorthwind"].ConnectionString;
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                IEnumerable<Produto> produtos = conexaoBD.Query<Produto>("Select * from Products");
                Console.WriteLine("{0} - {1} - {2} ", "Código", "Nome do Produto", "Preço do Produto");
                foreach (Produto produto in produtos)
                {
                    Console.WriteLine("{0} - {1} - {2}", produto.ProductId, produto.ProductName, produto.UnitPrice);
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Exemplo de consulta com parametros usando Dapper.
        /// Retorna a lista de produtos acima do valor passador no parametro.
        /// </summary>
        /// <param name="preco">Valor a ser utilizado para consultador produtos (acima desse valor específicado)</param>
        private static void ExemploDapper3(int preco)
        {
            string strConexao = ConfigurationManager.ConnectionStrings["conexaoNorthwind"].ConnectionString;
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                IEnumerable<Produto> produtos = conexaoBD.Query<Produto>
                    (
                      "Select ProductID, ProductName, UnitPrice from Products Where UnitPrice > @Preco", new { Preco = preco }
                    );
                Console.WriteLine("{0} - {1} - {2} ", "Código", "Nome do Produto", "Preço do Produto");
                foreach (Produto produto in produtos)
                {
                    Console.WriteLine("{0} - {1} - {2}", produto.ProductId, produto.ProductName, produto.UnitPrice);
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Retorno de multiplos objetos na consulta.
        /// Retorna a lista de fornecedores e a lista de produtos que tenham o IdFornecedor passado no método.
        /// </summary>
        /// <param name="idFornecedor">Id do fornecedor para consulta a lista de fornecedores e produtos fornecidos por estes.</param>
        private static void ExemploDapper4(int idFornecedor)
        {
            string strConexao = ConfigurationManager.ConnectionStrings["conexaoNorthwind"].ConnectionString;
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                var consulta = @"SELECT * FROM dbo.Suppliers WHERE SupplierID = @Id
                                 SELECT * FROM dbo.Products WHERE SupplierID = @Id";

                using (var resultado = conexaoBD.QueryMultiple(consulta, new { Id = 1 }))
                {
                    var fornecedor = resultado.Read().Single();
                    var produtos = resultado.Read().ToList();
                    Console.WriteLine("Fornecedor  - {0} ", fornecedor.CompanyName);
                    Console.WriteLine(string.Format("Total de Produtos {0}", produtos.Count));
                    foreach (dynamic produto in produtos)
                    {
                        Console.WriteLine("{0} - {1} - {2}", produto.ProductID, produto.ProductName, produto.UnitPrice);
                    }
                }
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Exemplo de inserção no banco de dados usando Dapper.
        /// </summary>
        private static void ExemploDapper5()
        {
            string strConexao = ConfigurationManager.ConnectionStrings["conexaoNorthwind"].ConnectionString;
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                var fornecedor = new Fornecedor()
                {
                    Address = "Rua Projetada 100",
                    CompanyName = "JcmSoft Inc."
                };
                conexaoBD.Execute(@"insert Suppliers(CompanyName, Address)
                                    values (@CompanyName, @Address)", fornecedor);
                Console.WriteLine("Informações do fornecedor incluídas com sucesso.");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Exemplo de atualização de dados (update) no banco de dados utilizando Dapper.
        /// </summary>
        private static void ExemploDapper6()
        {
            string strConexao = ConfigurationManager.ConnectionStrings["conexaoNorthwind"].ConnectionString;
            using (var conexaoBD = new SqlConnection(strConexao))
            {
                var atualizarBD = @"Update Products Set UnitPrice = @UnitPrice Where ProductId = @ProductId";
                conexaoBD.Execute(atualizarBD, new
                {
                    UnitPrice = 99.9m,
                    ProductId = 50
                });
                Console.WriteLine("Informações do fornecedor atualizadas com sucesso.");
                Console.ReadLine();
            }
        }
    }
}
