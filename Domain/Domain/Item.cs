namespace Domain;

public class Item
{
    public string Name { get; private set; }
    public Guid? ParentId { get; private set; }
    public Guid Id { get; private set; }
    public List<Item> ChildItems { get; private set; } = [];
    public virtual Item Parent { get; private set; }

    //for EF
    private Item()
    { }

    public Item(string name, List<Item> childItems, Guid? parentId = null, Guid? id = null)
    {
        Name = name;
        ParentId = parentId;
        Id = id ?? Guid.NewGuid();
        ChildItems = childItems;

        UpdateChilds();
    }

    public void Update(string? name, List<Item>? childItems, Guid? parentId)
    {
        Name = name ?? Name;
        ChildItems = childItems?.Count > 0 ? childItems : ChildItems;
        ParentId = parentId ?? ParentId;

        UpdateChilds();
    }

    public List<Item> GetAll()
    {
        var allItems = new List<Item>
        {
            this
        };

        foreach (var child in ChildItems)
        {
            allItems.AddRange(child.GetAll());
        }

        return allItems;
    }

    private void UpdateChilds()
    {
        foreach (var child in ChildItems)
        {
            child.Id = child.Id == Guid.Empty ? Guid.NewGuid() : child.Id;
            child.ParentId = Id;
        }
    }
}