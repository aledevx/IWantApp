using IWantApp.Endpoints.Products;

namespace IWantApp.Endpoints.Orders;

public record OrderResponse(Guid OrderId, string ClientName, string ClientCpf, decimal Total, string DeliveryAddress, DateTime CreateOn, List<ProductResponse> Products);
