using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RM.BusinessLayer;

namespace RealManagement.Controllers
{
    public class SupplierController : Controller
    {
        private IBusinessLayer _businessLayer = null;
        //
        // GET: /Supplier/
        public ActionResult Index()
        {
            _businessLayer = new SupplierBuinessLayer();
            //var supplierList = _businessLayer.
            return View();
        }
	}
}