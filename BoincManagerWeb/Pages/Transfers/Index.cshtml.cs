﻿using BoincManagerWeb.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace BoincManagerWeb.Pages.Transfers
{
    public class IndexModel : PageModel
    {
        private readonly BoincManager.Manager _manager;
        private readonly ViewDataProcessor _viewDataProcessor;

        public string CurrentFilter { get; set; }

        public List<TransferViewModel> Transfers { get; set; }

        public IndexModel(BoincManager.Manager manager, ViewDataProcessor viewDataProcessor)
        {
            _manager = manager;
            _viewDataProcessor = viewDataProcessor;
        }

        public void OnGet(string searchString)
        {
            CurrentFilter = searchString;
            Transfers = _viewDataProcessor.GetTransfers(_manager.GetAllHostStates(), searchString);
        }

    }
}
