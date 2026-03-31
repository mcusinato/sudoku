using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _100.Pages
{
    public class _100c
    {
        public int[][] matrix { get; set; }
        public int row { get; set; }
        public int col { get; set; }

        public int minus1 { get; set; }
        public int minus2 { get; set; }
        public int minus3 { get; set; }
        public int actual { get; set; }

        public _100c(int row, int col)
        {
            this.row = row;
            this.col = col;
            this.matrix = new int[10][];
            for (int i = 0; i < 10; i++)
                this.matrix[i] = new int[10];
        }
    }
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Message { get; set; }
        public _100c game { get; set; }
        [BindProperty] public int row { get; set; }
        [BindProperty] public int col { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Message = "Select starting box";
        }

        public void OnPost()
        {
            if (row < 0 || row > 9)
                row = 0;

            if (col < 0 || col > 9)
                col = 0;
            
            game = new _100c(row, col);

            var retval = solver.solve(game);

            if (retval == 0)
                Message = "Solved! Select starting box";
            else
                Message = "NOT solved! Select starting box";
        }
    }
}
