using qlbb2.AppService.Services.Interface;
using qlbb2.Data.Entities;
using qlbb2.Infrastructure.Repositories.Interface;

namespace qlbb2.AppService.Services
{
    public class SupplierService : GenericService<TblSupplier>, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public SupplierService(ISupplierRepository supplierRepository) : base(supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
    }
}
