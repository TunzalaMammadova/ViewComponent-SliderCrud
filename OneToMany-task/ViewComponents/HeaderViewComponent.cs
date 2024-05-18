using System;
using Microsoft.AspNetCore.Mvc;
using OneToMany_task.Data;
using OneToMany_task.Services.Interfaces;

namespace OneToMany_task.ViewComponents
{
	public class HeaderViewComponent :ViewComponent
	{
        private readonly ISettingService _settingService;

        public HeaderViewComponent(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View(await _settingService.GetAllAsync()));
        }
    }
}

