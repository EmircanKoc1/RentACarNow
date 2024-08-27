using Microsoft.EntityFrameworkCore;
using RentACarNow.APIs.WriteAPI.Application.Repositories.Write.EfCore;
using RentACarNow.APIs.WriteAPI.Domain.Entities.EfCoreEntities;
using RentACarNow.APIs.WriteAPI.Persistence.Contexts;
using RentACarNow.APIs.WriteAPI.Persistence.Repositories.Base;
namespace RentACarNow.APIs.WriteAPI.Persistence.Repositories.Write.EfCore
{
    public class EfCoreClaimWriteRepository : EfCoreBaseWriteRepository<Claim>, IEfCoreClaimWriteRepository
    {
        public EfCoreClaimWriteRepository(RentalACarNowDbContext context) : base(context)
        {
        }

        //public async Task<Claim> AddClaimToAdminAsync(Guid claimId, Guid adminId)
        //{
        //    var claim = await _context.Claims.FindAsync(claimId);

        //    //error
        //    //claim.Admins.Add(new Admin
        //    //{
        //    //    Id = adminId,
        //    //});

        //    var admin = await _context.Admins.FindAsync(adminId);

        //    admin.Claims.Add(claim);

        //    await _context.SaveChangesAsync();

        //    return claim;


        //}

        //public async Task<Claim> AddClaimToCustomerAsync(Guid claimId, Guid customerId)
        //{
        //    var claim = await _context.Claims.FindAsync(claimId);

        //    var customer = await _context.Admins.FindAsync(customerId);

        //    customer.Claims.Add(claim);

        //    await _context.SaveChangesAsync();

        //    return claim;
        //}

        //public async Task<Claim> AddClaimToEmployeeAsync(Guid claimId, Guid employeeId)
        //{
        //    var claim = await _context.Claims.FindAsync(claimId);

        //    var employee = await _context.Admins.FindAsync(employeeId);


        //    employee.Claims.Add(claim);

        //    await _context.SaveChangesAsync();

        //    return claim;
        //}

        //public async Task<bool> DeleteClaimFromEmployeeAsync(Guid claimId, Guid employeeId)
        //{
        //    List<Employee> customers = await _context.Employees
        //       .Include(c => c.Claims.Where(c => c.Id == claimId))
        //       .Where(c => c.Id == employeeId)
        //       .ToListAsync();

        //    var employee = customers.FirstOrDefault();

        //    if (employee is null || employee.Claims is null)
        //        return false;


        //    var claim = employee.Claims.FirstOrDefault();

        //    employee.Claims.Remove(claim);

        //    _context.SaveChanges();

        //    return true;

        //}

        //public async Task<bool> DeleteClaimFromCustomerAsync(Guid claimId, Guid customerId)
        //{

        //    List<Customer> customers = await _context.Customers
        //        .Include(c => c.Claims.Where(c => c.Id == claimId))
        //        .Where(c => c.Id == customerId)
        //        .ToListAsync();

        //    var customer = customers.FirstOrDefault();

        //    if (customer is null || customer.Claims is null)
        //        return false;


        //    var claim = customer.Claims.FirstOrDefault();

        //    customer.Claims.Remove(claim);

        //    _context.SaveChanges();

        //    return true;
        //}

        //public async Task<bool> DeleteClaimFromAdminAsync(Guid claimId, Guid adminId)
        //{
        //    List<Admin> customers = await _context.Admins
        //       .Include(c => c.Claims.Where(c => c.Id == claimId))
        //       .Where(c => c.Id == adminId)
        //       .ToListAsync();

        //    var admin = customers.FirstOrDefault();

        //    if (admin is null || admin.Claims is null)
        //        return false;


        //    var claim = admin.Claims.FirstOrDefault();

        //    admin.Claims.Remove(claim);

        //    _context.SaveChanges();

        //    return true;
        //}


    }



}
