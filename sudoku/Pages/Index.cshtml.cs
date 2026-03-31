using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudoku_gui.Pages
{
    public class sudoku
    {
        public int[][] matrix { get; set; }
        
        public sudoku()
        {
            this.matrix = new int[9][];
            for (int i = 0; i < 9; i++)
                this.matrix[i] = new int[9];
        }
    }
    public class IndexModel : PageModel
    {
        public string Message { get; set; }
        public sudoku gamein { get; set; }
        [BindProperty] public sudoku game { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            game = new sudoku();
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    game.matrix[i][j] = -1;
            
            Message = "Set data and (all but 1-9 is considered an empty spot) ...";
        }

        public void OnPost()
        {
            gamein = new sudoku();
            gamein.matrix = (int[][])game.matrix.Clone();
            for (int i = 0; i < 9; i++)
                gamein.matrix[i] = (int [])game.matrix[i].Clone();
            var retval = solver.solve(game);
            if(retval==-1)
                Message = "ERROR: input not valid. (all but 1-9 is considered an empty spot) ...";
            else if(retval==-2)
                Message = "ERROR: Unsolvable. (all but 1-9 is considered an empty spot) ...";
            else
                Message = "Solved. (all but 1-9 is considered an empty spot) ...";
        }
    }   
}
