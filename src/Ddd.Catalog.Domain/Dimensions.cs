using Ddd.Core.DomainObjects;

namespace Ddd.Catalog.Domain;

public class Dimensions
{
    public decimal Height { get; set; }
    public decimal Width { get; set; }
    public decimal Depth { get; set; }

    public Dimensions(decimal height, decimal width, decimal depth)
    {
        AssertionConcern.ValidateIfLessThanOrEqual(height, 0, "Height cannot be negative");
        AssertionConcern.ValidateIfLessThanOrEqual(width, 0, "Width cannot be negative");
        AssertionConcern.ValidateIfLessThanOrEqual(depth, 0, "Depth cannot be negative");
        
        Height = height;
        Width = width;
        Depth = depth;
    }
    
     
    
    public override string ToString()
    {
        return $"{Height} x {Width} x {Depth}";
    }
}