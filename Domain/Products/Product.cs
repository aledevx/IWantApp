using System.Diagnostics.Contracts;
using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Product : Entity
{
    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string Description { get; private set; }
    public bool HasStock { get; private set; }
    public bool Active { get; private set; } = true;
    public decimal Price { get; private set; }

    public Product(string name, Category category, string description, bool hasStock, decimal price, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;
        Price = price;

        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditeOn = DateTime.Now;

        Validate();
    }

    //Preciso desse construtor vazio pois se não o construtor com parâmetros
    // não consegue bindar o category, não sei o motivo kkk
    public Product()
    {

    }

    public void EditInfo(string name, Category category, string description, bool hasStock, bool active, decimal price, string editedBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;
        Active = active;
        Price = price;

        EditedBy = editedBy;
        EditeOn = DateTime.Now;

        Validate();
    }

    private void Validate()
    {
        var contract = new Contract<Product>()
            .IsNotNullOrEmpty(Name, "Name")
            .IsGreaterOrEqualsThan(Name, 3, "Name")
            .IsNotNull(Category, "Category", "Category not found")
            .IsNotNullOrEmpty(Description, "Description")
            .IsGreaterOrEqualsThan(Description, 3, "Description")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy")
            .IsNotNullOrEmpty(EditedBy, "EditedBy");
        AddNotifications(contract);
    }
}
