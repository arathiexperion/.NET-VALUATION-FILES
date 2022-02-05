using Bill.Models;
using Bill.viewmodel;
using BillRepo.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Bill.Repository
{
    public class BillRepository:IBillRepository
    {
        private readonly billContext _context;

        public BillRepository(billContext context)
        {
            _context = context;

        }

        public async Task<int> AddCategory(Category category)
        {
            if (_context != null)
            {
                await _context.Category.AddAsync(category);
                await _context.SaveChangesAsync();
                return category.CategoryId;
            }
            return 0;
        }

        public  async Task<int> AddGst(Gst gst)
        {
            if (_context != null)
            {
                await _context.Gst.AddAsync(gst);
                await _context.SaveChangesAsync();
                return gst.GstId;
            }
            return 0;

        }

        public  async Task<int> AddProduct(Product product)
        {
            if (_context != null)
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
                return product.ProductCode;
            }
            return 0;
        }

        public  async Task<int> DeleteCategory(int? id)
        {
            int result = 0;
            if (_context != null)
            {
                var category = await _context.Category.FirstOrDefaultAsync(cat => cat.CategoryId == id);
                //check condition
                if (category != null)
                {
                    //delete
                    _context.Category.Remove(category);

                    //commit
                    result = await _context.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<int> DeleteGst(int? id)
        {
            int result = 0;
            if (_context != null)
            {
                var gst = await _context.Gst.FirstOrDefaultAsync(gst => gst.GstId == id);
                //check condition
                if (gst != null)
                {
                    //delete
                    _context.Gst.Remove(gst);

                    //commit
                    result = await _context.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<int> DeleteProduct(int? id)
        {
            int result = 0;
            if (_context != null)
            {
                var product = await _context.Product.FirstOrDefaultAsync(pro => pro.CategoryId == id);
                //check condition
                if (product != null)
                {
                    //delete
                    _context.Product.Remove(product);

                    //commit
                    result = await _context.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<List<Category>> GetCategory()
        {
            if (_context != null)
            {
                return await _context.Category.ToListAsync();
            }
            return null;
        }

        public  async Task<List<Gst>> GetGst()
        {

            if (_context != null)
            {
                return await _context.Gst.ToListAsync();
            }
            return null;

        }

        public async  Task<List<Product>> GetProduct()
        {
            if (_context != null)
            {
                return await _context.Product.ToListAsync();
            }
            return null;
        }

        public async Task<List<productgst>> Getproductgstdetails()
        {
            if (_context != null)
            {
                //linq
                //join post and category
                return await (from p in _context.Product
                              join c in _context.Category
                              on p.CategoryId equals c.CategoryId
                              join g in _context.Gst
                            on c.CategoryId equals g.CategoryId
                            where c.CategoryName.Contains("mobile_phone" ) || c.CategoryName.Contains("hard_disk")
                              select new productgst { 
                              category_id = c.CategoryId,
                              gst_value = (float)g.GstValue,
                              product_code = p.ProductCode,
                              qty = (int)p.Qty,
                              rate_per_unit = (int)p.RatePerUnit,
                              Net_Rate = (double)(g.GstValue * p.RatePerUnit) 
                              }

                              ).OrderBy(x=> x.gst_value).ToListAsync();

            }
            return null;
        }

        public async Task UpdateCategory(Category category)
        {
            if (_context != null)
            {
                _context.Entry(category).State = EntityState.Modified;
                _context.Category.Update(category);
                await _context.SaveChangesAsync();

            }
        }

        public async Task UpdateGst(Gst gst)
        {
            if (_context != null)
            {
                _context.Entry(gst).State = EntityState.Modified;
                _context.Gst.Update(gst);
                await _context.SaveChangesAsync();

            }

        }

        public async Task UpdateProduct(Product product)
        {
            if (_context != null)
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.Product.Update(product);
                await _context.SaveChangesAsync();

            }
        }
    }
}
