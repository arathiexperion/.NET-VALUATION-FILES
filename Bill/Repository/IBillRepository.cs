using Bill.Models;
using Bill.viewmodel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillRepo.Repository
{
    public interface IBillRepository
    {
        //crud operation method declaration for category
        Task<List<Category>> GetCategory();

        Task<int> AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task<int> DeleteCategory(int? id);

        //crud operation method declaration for Product

        Task<List<Product>> GetProduct();
        
        Task<int> AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task<int> DeleteProduct(int? id);

        //crud operation method declaration for Gst

        Task<List<Gst>> GetGst();

        Task<int> AddGst(Gst gst);
        Task UpdateGst(Gst gst);
        Task<int> DeleteGst(int? id);

        Task<List<productgst>> Getproductgstdetails();
        
       // Task<productgst> Getincreasinggstorder(int? postId);


    }
}
