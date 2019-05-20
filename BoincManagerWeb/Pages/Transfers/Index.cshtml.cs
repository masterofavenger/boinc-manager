﻿using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BoincManagerWeb.Pages.Transfers
{
    public class IndexModel : PageModel
    {
        public readonly BoincManager.Manager _manager;

        public IndexModel(BoincManager.Manager manager)
        {
            _manager = manager;
            _manager.CurrentUpdateScope = BoincManager.Manager.UpdateScope.Transfers;
        }

        public void OnGet()
        {
            
        }

    }
}
