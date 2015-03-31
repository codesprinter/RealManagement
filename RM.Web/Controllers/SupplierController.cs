using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RM.BusinessLayer;
using RM.DomainModel;
using RM.Web.ViewModel;

namespace RM.Web.Controllers
{
    public class SupplierController : Controller
    {
        private ISupplierBusinessLayer _supplierBussiness = null;
        //
        // GET: /Supplier/
        
        public SupplierController(ISupplierBusinessLayer supplierBussiness)
        {
            _supplierBussiness = supplierBussiness;
        }
        public ActionResult Index()
        {
            //ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            var suppliers = _supplierBussiness.GetAllSuppliers();
            return View(suppliers);
        }
        public ActionResult AddSupplier()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSupplier(Supplier supplier)
        {
            //ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            supplier.EntityState = EntityState.Added;
            _supplierBussiness.AddSupplier(supplier);
            return RedirectToAction("Index");
        }
        public ActionResult EditSupplier(long id)
        {
            //ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            var supplier = _supplierBussiness.GetSupplierByID(id);
            return View(supplier);
        }
        [HttpPost]
        public ActionResult EditSupplier(Supplier supplier)
        {
            ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            supplier.EntityState = EntityState.Modified;
            businessLayer.UpdateSupplier(supplier);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteSupplier(int id)
        {
            ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            Supplier[] supplierList = new Supplier[1];
            supplierList[0] = businessLayer.GetSupplierByID(id);

            if (supplierList[0].SupplierContactDetails.Count > 0) {
                SupplierContactDetail[] contactList = new SupplierContactDetail[supplierList[0].SupplierContactDetails.Count];
                int i = 0;
                foreach (SupplierContactDetail contact in supplierList[0].SupplierContactDetails) {
                    contact.EntityState = EntityState.Deleted;
                    contactList[i++] = contact;
                }
                businessLayer.RemoveSupplierContactDetails(contactList);
            }
           
            supplierList[0].EntityState = EntityState.Deleted;
            businessLayer.RemoveSupplier(supplierList);
            return RedirectToAction("Index");
        }
        public ActionResult Details(long id)
        {
            ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            var supplier = businessLayer.GetSupplierByID(id);
            SupplierContactViewModel supplierViewModel = new SupplierContactViewModel(supplier);
            return View(supplierViewModel);
        }
        public ActionResult ContactList(int id)
        {
            ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            SupplierContactViewModel supplierContact = new SupplierContactViewModel();
            var contactList = businessLayer.GetSupplierContactsBySupplierID(id);
            supplierContact.SupplierId = id;
            supplierContact.ContactList = contactList;
            return View(supplierContact);
        }
        public ActionResult AddSupplierContactDetails(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSupplierContactDetails(SupplierContactDetail contactDetail, int id)
        {
            ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            contactDetail.EntityState = EntityState.Added;
            contactDetail.SupplierID = id;
            SupplierContactDetail[] contactList = new SupplierContactDetail[1];
            contactList[0] = contactDetail;
            businessLayer.AddSupplierContactDetails(contactList);
            return RedirectToAction("ContactList", new { id = id});
        }
        public ActionResult EditSupplierContact(int id)
        {
            ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            var supplierContact = businessLayer.GetSupplierContactByID(id);
            return View(supplierContact);
        }
        [HttpPost]
        public ActionResult EditSupplierContact(SupplierContactDetail contactDetail, int id)
        {
            ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            contactDetail.EntityState = EntityState.Modified;
            SupplierContactDetail[] contactList = new SupplierContactDetail[1];
            contactList[0] = contactDetail;
            businessLayer.AddSupplierContactDetails(contactList);
            return RedirectToAction("ContactList", new { id = id });
        }
        public ActionResult DeleteSupplierContact(int id)
        {
            ISupplierBusinessLayer businessLayer = new SupplierBuinessLayer();
            var supplierContact = businessLayer.GetSupplierContactByID(id);
            supplierContact.EntityState = EntityState.Deleted;
            long supplierId = supplierContact.SupplierID;
            SupplierContactDetail[] contactList = new SupplierContactDetail[1];
            contactList[0] = supplierContact;
            businessLayer.RemoveSupplierContactDetails(contactList);
            return RedirectToAction("ContactList", new { id = supplierId });
        }
	}
}