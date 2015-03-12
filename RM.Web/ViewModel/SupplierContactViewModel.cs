using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RM.DomainModel;

namespace RM.Web.ViewModel
{
    public class SupplierContactViewModel
    {
        public long SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IList<SupplierContactDetail> ContactList { get; set; }
        public SupplierContactViewModel() { }
        public SupplierContactViewModel(Supplier src)
        {
            this.SupplierId = src.SupplierID;
            this.SupplierCode = src.SupplierCode;
            this.Name = src.Name;
            this.Address = src.Address;
            this.ContactList = new List<SupplierContactDetail>();
            foreach (SupplierContactDetail contact in src.SupplierContactDetails) {
                SupplierContactDetail item = new SupplierContactDetail();
                item.ContactID = contact.ContactID;
                item.ContactPerson = contact.ContactPerson;
                item.Email = contact.Email;
                item.ContactNo = contact.ContactNo;
                item.Fax = contact.Fax;
                this.ContactList.Add(item);
            }
        }
    }
}