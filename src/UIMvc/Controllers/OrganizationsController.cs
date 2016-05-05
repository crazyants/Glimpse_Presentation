using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Extras.NLog;
using Kiehl.App.Business.Models.Organizations;
using MediatR;
using NExtensions;

namespace Kiehl.App.UIMvc.Controllers
{
    public class OrganizationsController : Controller
    {
        private readonly IMediator mediator;
        public ILogger Logger { get; set; }

        public OrganizationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<ActionResult> Index(int? id)
        {
            Logger.Trace("Index::{0}", id);

            var query = new OrganizationDetailsQuery(organizationId: id ?? 1);
            var result = await mediator.SendAsync(query);

            if (result == null)
                return new HttpNotFoundResult("An Organization with id {0} was not found".FormatWith(id));

            return View(result);
        }

        public async Task<ActionResult> Create(int? id)
        {
            Logger.Trace("Create::Get::{0}", id);

            var query = new OrganizationEditorFormQuery(parentOrganizationId: id);
            var form = await mediator.SendAsync(query);
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrganizationEditorForm form)
        {
            Logger.Trace("Create::Post::{0}", form.ParentOrganizationId);

            if (ModelState.IsValid)
            {
                var result = await mediator.SendAsync(new AddOrEditOrganizationCommand(form, ModelState));

                if (result.IsSuccess)
                    return RedirectToAction("Index", new { Id = result.Result });

                ModelState.AddModelError("", result.Result.ToString());
            }

            return View(form);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            Logger.Trace("Edit::Get::{0}", id);

            if (!id.HasValue)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var query = new OrganizationEditorFormQuery(organizationId: id);
            var form = await mediator.SendAsync(query);
            return View(form);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OrganizationEditorForm form)
        {
            Logger.Trace("Edit::Post::{0}", form.OrganizationId);

            if (ModelState.IsValid)
            {
                var result = await mediator.SendAsync(new AddOrEditOrganizationCommand(form, ModelState));

                if (result.IsSuccess)
                    return RedirectToAction("Index", new { Id = result.Result });

                ModelState.AddModelError("", result.Result.ToString());
            }

            return View(form);
        }
    }
}