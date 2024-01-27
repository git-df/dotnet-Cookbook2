using Application.Funcions.User.Commands.SignOut;
using Domain.Consts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Helpers;

namespace Web.Pages.User
{
    public class SignOutModel : PageModel
    {
        private readonly IMediator _mediator;

        public SignOutModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> OnGetAsync()
        {
            var response = await _mediator.Send(new SignOutCommand());
            TempData["Alert"] = response.Alert?.ToTempData();

            return RedirectToPage(PageConsts.Index);
        }
    }
}
