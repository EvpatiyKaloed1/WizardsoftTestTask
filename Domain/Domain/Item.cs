using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;
public class Item
{
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
    public Guid Id { get; set; }
    public List<Item?> ChildItems { get; set; } = [];
    public virtual Item Parent { get; set; }    

    //for EF
    private Item() { }
    public Item(string name, List<Item?> childItems, Guid? parentId = null,Guid? id= null)
    {
        Name = name;
        ParentId = parentId;
        Id = id??Guid.NewGuid();
        ChildItems = childItems;
        UpdateParentForChildren();
    }
   
    public void UpdateParentForChildren()
    {
        foreach (var child in ChildItems)
        {
            child.ParentId = this.Id; 
        }
    }
}
