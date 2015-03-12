using System.Collections.Generic;
using RM.DomainModel;
using RM.DataAccessLayer;

namespace RM.BusinessLayer
{
    public interface IBusinessLayer
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

    public class SupplierBuinessLayer : IBusinessLayer
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierContactDetailsRepository _supplierContactDetailsRepository;

        public SupplierBuinessLayer()
        {
            _supplierRepository = new SupplierRepository();
            _supplierContactDetailsRepository = new SupplierContactDetailsRepository();
        }

        public SupplierBuinessLayer(ISupplierRepository supplierRepository,
            ISupplierContactDetailsRepository supplierContactDetailsRepository)
        {
            _supplierRepository = supplierRepository;
            _supplierContactDetailsRepository = supplierContactDetailsRepository;
        }

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
    }
}