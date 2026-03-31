using sudoku_gui.Pages;
using System.Collections.Generic;
using System.Linq;

namespace sudoku_gui
{
    class line {
        public int value;
        public int x;
        public int y;
        public List<int> possiblevalues;

        public line()
        { 
        }
        public line(int _value, int _x, int _y)
        { 
            value = _value;
            x = _x;
            y = _y;
            possiblevalues = new List<int>();
        }
    }
    public static class solver
    {
        public static int solve(sudoku game)
        {
            if(!isoksudoku(game))
                return -1;

            if (!solve_rec(game))
            {
                return -2;
            }
            return 0;
        }

        static bool findvalues(sudoku game, line spot)
        {
            int initvalue = spot.value;

            for (int i = 1; i <= 9; i++)
            {
                game.matrix[spot.x][spot.y] = i;
                if (isokintset(getrow(game, spot.x)) && isokintset(getcol(game, spot.y)) && isokintset(getsquare(game, (spot.x / 3) * 3 + (spot.y / 3))))
                    spot.possiblevalues.Add(i);
            }
            game.matrix[spot.x][spot.y] = initvalue;

            if (spot.possiblevalues.Count == 0)
                return false;

            return true;
        }
        static bool solve_rec(sudoku game)
        {
            List<line> emptyspots = new List<line>();

            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    if (game.matrix[i][j] <= 0 || game.matrix[i][j] > 9)
                        emptyspots.Add(new line(game.matrix[i][j], i, j));
            
            if (emptyspots.Count == 0)
                return true;

            for(int i=0; i<emptyspots.Count; i++)
                if (!findvalues(game, emptyspots[i]))
                    return false;

            emptyspots = emptyspots.OrderBy(o => o.possiblevalues.Count).ToList();

            foreach (var val in emptyspots[0].possiblevalues)
            {
                int initvalue = game.matrix[emptyspots[0].x][emptyspots[0].y];
                game.matrix[emptyspots[0].x][emptyspots[0].y] = val;
                if (solve_rec(game))
                    return true;
                game.matrix[emptyspots[0].x][emptyspots[0].y] = initvalue;
            }

            return false;
        }

        static line[] getrow(sudoku game, int i)
        {
            line[] retval = new line[9];
            for (int j = 0; j < 9; j++)
                retval[j] = new line();
            
            for (int j = 0; j < 9; j++)
            {
                retval[j].x = i;
                retval[j].y = j;
                retval[j].value = game.matrix[retval[j].x][retval[j].y];
            }
            return retval;
        }
        static line[] getcol(sudoku game, int i)
        {
            line[] retval = new line[9];
            for (int j = 0; j < 9; j++)
                retval[j] = new line();
            
            for (int j = 0; j < 9; j++)
            {
                retval[j].x = j;
                retval[j].y = i;
                retval[j].value = game.matrix[retval[j].x][retval[j].y];
            }
            return retval;
        }
        static line[] getsquare(sudoku game, int i)
        {
            line[] retval = new line[9];
            for (int j = 0; j < 9; j++)
                retval[j] = new line();

            for (int j = 0; j < 3; j++)
                for (int k = 0; k < 3; k++)
                {
                    retval[j].x = (i / 3) * 3 + j;
                    retval[j].y = (i % 3) * 3 + k;
                    retval[j*3+k].value = game.matrix[retval[j].x][retval[j].y];
                }
            return retval;
        }

        static bool isokintset(line[] intset)
        {
            var valueList = new HashSet<int>();
            foreach (var v in intset)
            {
                if (v.value > 0 && v.value < 10 && valueList.Contains(v.value))
                    return false;
                else if (v.value > 0 && v.value < 10)
                    valueList.Add(v.value);
            }
            return true;
        }

        static bool isoksudoku(sudoku game)
        {
            for (int i = 0; i < 9; i++)
            {
                if (!isokintset(getrow(game, i)))
                    return false;
                if (!isokintset(getcol(game, i)))
                    return false;
                if (!isokintset(getsquare(game, i)))
                    return false;
            }

            return true;
        }


        











    }
}
