using System;
using System.Net;
using System.Web.Mvc;
using DAL.Interfaces;
using Domain.Identity;
using Microsoft.Owin.Security;
using NLog;

namespace Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserClaimsController : Controller
    {
        private readonly NLog.ILogger _logger;
        private readonly string _instanceId = Guid.NewGuid().ToString();
        private readonly IUOW _uow;

        private readonly ApplicationRoleManager _roleManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly ApplicationUserManager _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        public UserClaimsController(IUOW uow, ILogger logger, ApplicationRoleManager roleManager, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
        {
            _uow = uow;
            _logger = logger;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _authenticationManager = authenticationManager;

            _logger.Debug("InstanceId: " + _instanceId);
        }

        // GET: UserClaims
        public ActionResult Index()
        {
            return View(_uow.GetRepository<IUserClaimIntRepository>().AllIncludeUser());
        }

        // GET: UserClaims/Details/5
        public ActionResult Details(int id)
        {
            if (id == default(int))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userClaim = _uow.GetRepository<IUserClaimIntRepository>().GetById(id);
            if (userClaim == null)
            {
                return HttpNotFound();
            }
            return View(userClaim);
        }

        // GET: UserClaims/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(_uow.GetRepository<IUserIntRepository>().All, "Id", "Email");
            return View();
        }

        // POST: UserClaims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ClaimType,ClaimValue")] UserClaimInt userClaim)
        {
            if (ModelState.IsValid)
            {
                _uow.GetRepository<IUserClaimIntRepository>().Add(userClaim);
                _uow.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(_uow.UsersInt.All, "Id", "Email", userClaim.UserId);
            return View(userClaim);
        }

        // GET: UserClaims/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == default(int))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userClaim = _uow.GetRepository<IUserClaimIntRepository>().GetById(id);
            if (userClaim == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(_uow.GetRepository<IUserIntRepository>().All, "Id", "Email",
                userClaim.UserId);
            return View(userClaim);
        }

        // POST: UserClaims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ClaimType,ClaimValue")] UserClaimInt userClaim)
        {
            if (ModelState.IsValid)
            {
                _uow.GetRepository<IUserClaimIntRepository>().Update(userClaim);
                _uow.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(_uow.GetRepository<IUserIntRepository>().All, "Id", "Email",
                userClaim.UserId);
            return View(userClaim);
        }

        // GET: UserClaims/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == default(int))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userClaim = _uow.GetRepository<IUserClaimIntRepository>().GetById(id);
            if (userClaim == null)
            {
                return HttpNotFound();
            }
            return View(userClaim);
        }

        // POST: UserClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _uow.GetRepository<IUserClaimIntRepository>().Delete(id);
            _uow.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Debug("InstanceId: " + _instanceId);
            base.Dispose(disposing);
        }
    }
}