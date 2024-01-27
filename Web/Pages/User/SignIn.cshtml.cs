using Application.Funcions.User.Commands.SignIn;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Helpers;
using Domain.Consts;

namespace Web.Pages.User
{
    public class SignInModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public SignInCommand SignInCommand { get; set; } = new SignInCommand();

        public SignInModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult> OnPostAsync(CancellationToken ct = default)
        {
            var response = await _mediator.Send(SignInCommand, ct);
            TempData["Alert"] = response.Alert?.ToTempData();

            if (response.Success)
                return RedirectToPage(PageConsts.Index);

            ModelState.Clear();
            response.ValidationResult?.AddToModelState(ModelState, nameof(SignInCommand));

            return Page();
        }
    }
}
