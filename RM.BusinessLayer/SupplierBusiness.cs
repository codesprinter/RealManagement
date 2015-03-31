using System.Collections.Generic;
using RM.DomainModel;
using RM.DataAccessLayer;
using System.Data.Entity;

namespace RM.BusinessLayer
{
    public interface IDiposeContext
    {
        void DisposeContext();
    }
    public interface ISupplierBusinessLayer: IDiposeContext
    {
        IList<Supplier> GetAllSuppliers();
        Supplier GetSupplierByName(string supplierName);
        Supplier GetSupplierByID(long supplierId);
        void AddSupplier(params Supplier[] supplier);
        void UpdateSupplier(params Supplier[] supplier);
        void RemoveSupplier(params Supplier[] supplier);

        IList<SupplierContactDetail> GetSupplierContactsBySupplierID(int supplierId);
        SupplierContactDetail GetSupplierContactByID(int contactId);
        void AddSupplierContactDetails(SupplierContactDetail[] supplierContacts);
        void UpdateSupplierContactDetails(SupplierContactDetail[] supplierContacts);
        void RemoveSupplierContactDetails(SupplierContactDetail[] supplierContacts);
    }

    public class SupplierBuinessLayer : ISupplierBusinessLayer
    {
        private IUnitOfWork _unitOfWork;
        private readonly IGenericDataRepository<Supplier> _supplierRepository;
        private readonly IGenericDataRepository<SupplierContactDetail> _supplierContactDetailsRepository;
        private string _contextName = string.Empty;

        public SupplierBuinessLayer()
        {
            DbContext context = new RMContext();
            _unitOfWork = new UnitOfWork(context);
            _supplierRepository = _unitOfWork.RepositoryFor<Supplier>();
            _supplierContactDetailsRepository = _unitOfWork.RepositoryFor<SupplierContactDetail>();
        }

        public SupplierBuinessLayer(string contextName)
        {
            _contextName = contextName;
            DbContext context = new RMContext(_contextName);
            _unitOfWork = new UnitOfWork(context);
            _supplierRepository = _unitOfWork.RepositoryFor<Supplier>();
            _supplierContactDetailsRepository = _unitOfWork.RepositoryFor<SupplierContactDetail>();
        }

        //public SupplierBuinessLayer(ISupplierRepository supplierRepository,
        //    ISupplierContactDetailsRepository supplierContactDetailsRepository)
        //{
        //    _supplierRepository = supplierRepository;
        //    _supplierContactDetailsRepository = supplierContactDetailsRepository;
        //}

        public IList<Supplier> GetAllSuppliers()
        {
            return _supplierRepository.GetAll();
        }

        public Supplier GetSupplierByName(string supplierName)
        {
            return _supplierRepository.GetSingle(
                s => s.Name.Equals(supplierName), 
                s => s.SupplierContactDetails); //include related contact details
        }
        public Supplier GetSupplierByID(long supplierId)
        {
            return _supplierRepository.GetSingle(
                s => s.SupplierID== supplierId,
                s => s.SupplierContactDetails); //include related employees
        }
        public void AddSupplier(params Supplier[] supplier)
        {
            /* Validation and error handling omitted */
            _supplierRepository.Add(supplier);
            _unitOfWork.SaveChanges();
        }

        public void UpdateSupplier(params Supplier[] supplier)
        {
            /* Validation and error handling omitted */
            _supplierRepository.Update(supplier);
        }
        public void RemoveSupplier(params Supplier[] supplier)
        {
            /* Validation and error handling omitted */
            _supplierRepository.Remove(supplier);
        }
        public SupplierContactDetail GetSupplierContactByID(int contactId)
        {
            return _supplierContactDetailsRepository.GetSingle(p => p.ContactID == contactId);
        }
        public IList<SupplierContactDetail> GetSupplierContactsBySupplierID(int supplierId)
        {
            return _supplierContactDetailsRepository.GetList(e => e.SupplierID == supplierId);
        }

        public void AddSupplierContactDetails(SupplierContactDetail[] supplierContacts)
        {
            /* Validation and error handling omitted */
            _supplierContactDetailsRepository.Add(supplierContacts);
        }

        public void UpdateSupplierContactDetails(SupplierContactDetail[] supplierContacts)
        {
            /* Validation and error handling omitted */
            _supplierContactDetailsRepository.Update(supplierContacts);
        }

        public void RemoveSupplierContactDetails(SupplierContactDetail[] supplierContacts)
        {
            /* Validation and error handling omitted */
            _supplierContactDetailsRepository.Remove(supplierContacts);
        }
        public void DisposeContext()
        {
            _unitOfWork.Dispose();
        }
    }
}