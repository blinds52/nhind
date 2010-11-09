using System;
using System.Linq;
using System.Web.Mvc;

using AdminMvc.Models;
using AdminMvc.Models.Repositories;

using AutoMapper;

using Health.Direct.Config.Store;

using MvcContrib.Pagination;

namespace AdminMvc.Controllers
{
    public class AddressesController : ControllerBase<Address, AddressModel, IAddressRepository>
    {
        public AddressesController(IAddressRepository repository) : base(repository)
        {
        }

        protected override void SetStatus(Address item, EntityStatus status)
        {
            item.Status = status;
        }

        public ActionResult Index(long domainID, int? page)
        {
            ViewData["Domain"] = Mapper.Map<Domain,DomainModel>(new DomainRepository().Get(domainID));

            return View(
                (from address in Repository.FindAll()
                 where address.DomainID == domainID
                 select Mapper.Map<Address,AddressModel>(address))
                    .AsPagination(page ?? 1, DefaultPageSize));
        }

        public ActionResult Add(long domainID)
        {
            return View(new AddressModel {DomainID = domainID});
        }

        [HttpPost]
        public ActionResult Add(FormCollection formValues)
        {
            var model = Mapper.Map<Address, AddressModel>(new Address());

            if (TryUpdateModel(model))
            {
                Repository.Add(Mapper.Map<AddressModel, Address>(model));
                return RedirectToAction("Index", new { domainID = model.DomainID });
            }

            return View(model);
        }

        public override ActionResult Details(long id)
        {
            var address = Repository.Get(id);
            if (address == null) return View("NotFound");

            return Json(Mapper.Map<Address, AddressModel>(address), "text/json", JsonRequestBehavior.AllowGet);
        }
    }
}