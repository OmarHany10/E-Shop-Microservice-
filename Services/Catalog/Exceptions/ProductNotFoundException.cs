using BuildingBlocks.Exceptions;

namespace Catalog.Exceptions
{
    public class ProductNotFoundException: NotFoundException
    {
        public ProductNotFoundException(object key): base("Product", key)
        {
            
        }
    }
}
