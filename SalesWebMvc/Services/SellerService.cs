using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services

{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }   

        public async Task <List<Seller>> FindAllAsync() 
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj) //inserindo novo vendedor
        {
            _context.Add(obj);
            _context.SaveChangesAsync();
        }

        public async Task <Seller> FindByIdAsyn(int id)
        {
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException("Não é possível deletar o vendedor, pois ele possui vendas.");
            }
        }

        public async Task UpdateAsyn(Seller obj)
        {
            bool hasAny = _context.Seller.Any(x => x.Id == obj.Id);
            if (hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {

                _context.Update(obj);
                await _context.SaveChangesAsync();
            } 
            catch (DbUpdateConcurrencyException e) 
            {
                throw new DbConcurrencyException(e.Message);
            }
        }

        //internal Task<string?> FindByIdAsync(int value)
        //{
        //    throw new NotImplementedException();
        //}

        //internal Task FindByIdAsync(Seller seller)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller
                .Include(s => s.Department) // se Seller tiver Department
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        internal Task FindByIdAsync(Seller seller)
        {
            throw new NotImplementedException();
        }
    }
}
