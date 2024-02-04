using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IWantApp.Endpoints.Products;
using IWantApp.Infra.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace IWantApp.Endpoints.Orders
{
    public class OrderGet
    {
        public static string Template => "/orders/{id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        [Authorize]
        public static async Task<IResult> Action(HttpContext http, ApplicationDbContext context, [FromRoute] Guid id)
        {
            var order = await context.Orders.Include(o => o.Products)
                .ThenInclude(p => p.Category).FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return Results.BadRequest("Pedido não encontrado");
            }

            var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var ehFuncionario = http.User.Claims.Any(c => c.Type == "EmployeeCode");
            //  var clientNome = context.UserClaims.Where(c => c.UserId == order.ClientId).First(c => c.ClaimType == "Name").ClaimValue;
            // var clientCpf = context.UserClaims.Where(c => c.UserId == order.ClientId).First(c => c.ClaimType == "Cpf").ClaimValue;
            var clientNomeeCpf = await context.UserClaims.Where(c => c.UserId == order.ClientId).ToListAsync();
            var nomeCliente = clientNomeeCpf.First(c => c.ClaimType == "Name").ClaimValue;
            var cpfCliente = clientNomeeCpf.First(c => c.ClaimType == "Cpf").ClaimValue;

            var listaProdutos = order.Products.Select(p => new ProductResponse(p.Name,
                p.Category.Name,
                p.Description,
                p.HasStock,
                p.Price,
                p.Active)).ToList();

            if (userId != order.ClientId && !ehFuncionario)
            {
                return Results.BadRequest("Você não tem permissão para acessar esse pedido");
            }

            var result = new OrderResponse(order.Id, nomeCliente, cpfCliente, order.Total, order.DeliveryAddress, order.CreatedOn, listaProdutos);

            return Results.Ok(result);
        }
    }
}