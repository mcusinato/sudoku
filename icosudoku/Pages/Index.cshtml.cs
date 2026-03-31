using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace icosudoku_gui.Pages
{
    public class IndexModel : PageModel
    {
        public icosudokusolution.Position[] psol { get; set; }
        public string Message { get; set; }
        [BindProperty] public string A { get; set; }
        [BindProperty] public string B { get; set; }
        [BindProperty] public string C { get; set; }
        [BindProperty] public string D { get; set; }
        [BindProperty] public string E { get; set; }
        [BindProperty] public string F { get; set; }
        [BindProperty] public string G { get; set; }
        [BindProperty] public string H { get; set; }
        [BindProperty] public string I { get; set; }
        [BindProperty] public string J { get; set; }
        [BindProperty] public string K { get; set; }
        [BindProperty] public string L { get; set; }
        
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            psol = null;
            Message = "set values on vertexes and ...";
            A = "0";
            B = "0";
            C = "0";
            D = "0";
            E = "0";
            F = "0";
            G = "0";
            H = "0";
            I = "0";
            J = "0";
            K = "0";
            L = "0";
        }

        public void OnPost()
        {
            psol = null;
            icosudokusolution.Triangle[] t;
            icosudokusolution.Vertex[] v;
            icosudokusolution.Position[] p;

            A = A is null ? "0" : A;
            B = B is null ? "0" : B;
            C = C is null ? "0" : C;
            D = D is null ? "0" : D;
            E = E is null ? "0" : E;
            F = F is null ? "0" : F;
            G = G is null ? "0" : G;
            H = H is null ? "0" : H;
            I = I is null ? "0" : I;
            J = J is null ? "0" : J;
            K = K is null ? "0" : K;
            L = L is null ? "0" : L;

            string[] inputs = new string[icosudokusolution.Program.N_V];
            inputs[0] = A;
            inputs[1] = B;
            inputs[2] = C;
            inputs[3] = D;
            inputs[4] = E;
            inputs[5] = F;
            inputs[6] = G;
            inputs[7] = H;
            inputs[8] = I;
            inputs[9] = J;
            inputs[10] = K;
            inputs[11] = L;

            if (!icosudokusolution.Program.Main(inputs, out t, out v, out p))
            {
                Message = "ERROR: input not correct! redo ...";
            }
            else
            { 
                Message = "Solved! do it again ...";
                psol = p;
            }
        }
    }

    
}
