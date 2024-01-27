using Application.Funcions.Category.Queries.List;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Application.Helpers;
using System.Security.Claims;
using Azure;
using Application.Funcions.Category.Commands.AddStarredCategory;
using Application.Funcions.Category.Commands.RemoveStarredCategory;
using Application.Services.Interfaces;
using Domain.Consts;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IUserService _user;

        public List<CategoryListDto> Categories { get; set; }

        public IndexModel(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _user = userService;

            Categories = [];
        }

        public async Task OnGetAsync()
        {
            var response = await _mediator.Send(new CategoryListQuery());
            Categories = response.Data;

            if (!response.Success)
                TempData["Alert"] = response.Alert?.ToTempData();
        }

        public async Task<ActionResult> OnGetAddStarredCategoryAsync(int id)
        {
            if (id != 0 && _user.IsAuthenticated)
            {
                var response = await _mediator.Send(new AddStarredCategoryCommand(id));
                TempData["Alert"] = response.Alert?.ToTempData();
            }

            return RedirectToPage(PageConsts.Index);
        }

        public async Task<ActionResult> OnGetRemoveStarredCategoryAsync(int id)
        {
            if (id != 0 && _user.IsAuthenticated)
            {
                var response = await _mediator.Send(new RemoveStarredCategoryCommand(id));
                TempData["Alert"] = response.Alert?.ToTempData();
            }

            return RedirectToPage(PageConsts.Index);
        }
    }
}
