using Application.Funcions.User.Commands.SignUp;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Helpers;
using Domain.Consts;

namespace Web.Pages.User
{
    public class SignUpModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public SignUpCommand SignUpCommand { get; set; }

        public SignUpModel(IMediator mediator)
        {
            _mediator = mediator;

            SignUpCommand = new SignUpCommand();
        }

        public async Task<ActionResult> OnPostAsync(CancellationToken ct = default)
        {
            var response = await _mediator.Send(SignUpCommand, ct);
            TempData["Alert"] = response.Alert?.ToTempData();

            if (response.Success)
                return RedirectToPage(PageConsts.UserSignIn);

            ModelState.Clear();
            response.ValidationResult?.AddToModelState(ModelState, nameof(SignUpCommand));

            return Page();
        }
    }
}
