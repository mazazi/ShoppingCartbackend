namespace Tatweer.Core.Common;

/*
This will serve as common fields for domain
This means, every entity will have below props by default
*/
public abstract class EntityBase
{
    public int Id { get; set; }
    //Below Properties are Audit properties
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}