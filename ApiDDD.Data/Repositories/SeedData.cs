using Microsoft.EntityFrameworkCore;
using ApiDDD.Data.Context;
using SalesAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using SalesAPI.Domain.ValueObjects;

namespace ApiDDD.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Verifica se já existem dados
                if (context.Sales.Any())
                {
                    return; // Banco de dados já foi populado
                }

                // Adiciona vendas de exemplo
                var sale1 = new Sale("SALE001", new CustomerInfo(Guid.NewGuid(), "Cliente 1", "cliente1@email.com", "123456789"),
                    new BranchInfo(Guid.NewGuid(), "Filial 1", "Rua A, 123", "Cidade X", "Estado Y"))
                {
                    Items = new List<SaleItem>
                    {
                        new SaleItem(new ProductInfo(Guid.NewGuid(), "Produto 1", "Descrição do Produto 1", "Categoria A", "Marca X"), 2, 50.00m),
                        new SaleItem(new ProductInfo(Guid.NewGuid(), "Produto 2", "Descrição do Produto 2", "Categoria B", "Marca Y"), 2, 25.25m)
                    }
                };

                var sale2 = new Sale("SALE002", new CustomerInfo(Guid.NewGuid(), "Cliente 2", "cliente2@email.com", "987654321"),
                    new BranchInfo(Guid.NewGuid(), "Filial 2", "Rua B, 456", "Cidade Y", "Estado Z"))
                {
                    Items = new List<SaleItem>
                    {
                        new SaleItem(new ProductInfo(Guid.NewGuid(), "Produto 3", "Descrição do Produto 3", "Categoria C", "Marca Z"), 3, 25.25m),
                    }
                };

                context.Sales.AddRange(sale1, sale2);
                context.SaveChanges();
            }
        }
    }
}
