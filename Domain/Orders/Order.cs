using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flunt.Validations;

namespace IWantApp.Domain.Orders
{
    public class Order : Entity
    {
        public string ClientId { get; private set; }
        public List<Product> Products { get; private set; }
        public decimal Total { get; private set; }
        public string DeliveryAddress { get; private set; }

        public Order() { }

        public Order(string clientId, string clientName, List<Product> products, string deliveryAddress)
        {
            ClientId = clientId;
            Products = products;
            DeliveryAddress = deliveryAddress;
            CreatedBy = clientName;
            EditedBy = clientName;
            CreatedOn = DateTime.Now;
            EditeOn = DateTime.Now;

            Total = 0;
            foreach (var item in Products)
            {
                Total += item.Price;
            }

            Validate();
        }

        private void Validate()
        {
            var contract = new Contract<Order>()
                .IsNotNull(ClientId, "Client")
                .IsTrue(Products != null && Products.Any(), "Products")
                .IsNotNullOrEmpty(DeliveryAddress, "DeliveryAddress");
            AddNotifications(contract);
        }

    }
}