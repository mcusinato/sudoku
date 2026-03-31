using _100.Pages;
using System.Collections.Generic;
using System.Linq;

namespace _100.Pages
{
    public static class solver
    {
        public static int solve(_100c game)
        {
            game.minus1 = 0;
            game.minus2 = 0;
            game.minus3 = 0;

            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    if ((i + j) % 3 == 0)
                    {
                        game.matrix[i][j] = -1;
                        game.minus1 ++;
                    }
                    if ((i + j) % 3 == 1)
                    {
                        game.matrix[i][j] = -2;
                        game.minus2++;
                    }
                    if ((i + j) % 3 == 2)
                    {
                        game.matrix[i][j] = -3;
                        game.minus3++;
                    }
                }

            game.matrix[game.row][game.col] = 1;
            if ((game.row + game.col) % 3 == 0)
            {
                game.actual = -1;
                game.minus1--;
            }
            if ((game.row + game.col) % 3 == 1)
            {
                game.actual = -2;
                game.minus2--;
            }
            if ((game.row + game.col) % 3 == 2)
            {
                game.actual = -3;
                game.minus3--;
            }

            return solverec(game.row, game.col, game.matrix[game.row][game.col] + 1, game.actual, game);
        }

        public static bool check(int row, int col, int actual, _100c game)
        {
            if (row < 0 || row > 9 || col < 0 || col > 9)
                return false;
            return (actual == 0 ? game.matrix[row][col] < 0 : game.matrix[row][col] == actual);
        }
        public static int solverec(int row, int col, int value, int actual, _100c game)
        {
            if (value == 101)
                return 0;

            int nextrow;
            int nextcol;

            if (actual == -1 && game.minus1 == 0)
                actual = 0;
            if (actual == -2 && game.minus2 == 0)
                actual = 0;
            if (actual == -3 && game.minus3 == 0)
                actual = 0;

            //right
            nextrow = row;
            nextcol = col + 3;
            if (check(nextrow, nextcol, actual, game))
            {
                int newactual = game.matrix[nextrow][nextcol];
                
                game.matrix[nextrow][nextcol] = value;
                if (newactual == -1)
                    game.minus1--;
                if (newactual == -2)
                    game.minus2--;
                if (newactual == -3)
                    game.minus3--;

                if (solverec(nextrow, nextcol, value + 1, newactual, game) == 0)
                        return 0;
                
                game.matrix[nextrow][nextcol] = newactual;
                if (newactual == -1)
                    game.minus1++;
                if (newactual == -2)
                    game.minus2++;
                if (newactual == -3)
                    game.minus3++;
            }

            //bottom
            nextrow = row+3;
            nextcol = col;
            if (check(nextrow, nextcol, actual, game))
            {
                int newactual = game.matrix[nextrow][nextcol];

                game.matrix[nextrow][nextcol] = value;
                if (newactual == -1)
                    game.minus1--;
                if (newactual == -2)
                    game.minus2--;
                if (newactual == -3)
                    game.minus3--;

                if (solverec(nextrow, nextcol, value + 1, newactual, game) == 0)
                    return 0;

                game.matrix[nextrow][nextcol] = newactual;
                if (newactual == -1)
                    game.minus1++;
                if (newactual == -2)
                    game.minus2++;
                if (newactual == -3)
                    game.minus3++;
            }

            //left
            nextrow = row;
            nextcol = col - 3;
            if (check(nextrow, nextcol, actual, game))
            {
                int newactual = game.matrix[nextrow][nextcol];

                game.matrix[nextrow][nextcol] = value;
                if (newactual == -1)
                    game.minus1--;
                if (newactual == -2)
                    game.minus2--;
                if (newactual == -3)
                    game.minus3--;

                if (solverec(nextrow, nextcol, value + 1, newactual, game) == 0)
                    return 0;

                game.matrix[nextrow][nextcol] = newactual;
                if (newactual == -1)
                    game.minus1++;
                if (newactual == -2)
                    game.minus2++;
                if (newactual == -3)
                    game.minus3++;
            }

            //up
            nextrow = row - 3;
            nextcol = col;
            if (check(nextrow, nextcol, actual, game))
            {
                int newactual = game.matrix[nextrow][nextcol];

                game.matrix[nextrow][nextcol] = value;
                if (newactual == -1)
                    game.minus1--;
                if (newactual == -2)
                    game.minus2--;
                if (newactual == -3)
                    game.minus3--;

                if (solverec(nextrow, nextcol, value + 1, newactual, game) == 0)
                    return 0;

                game.matrix[nextrow][nextcol] = newactual;
                if (newactual == -1)
                    game.minus1++;
                if (newactual == -2)
                    game.minus2++;
                if (newactual == -3)
                    game.minus3++;
            }

            //bottomleft
            nextrow = row + 2;
            nextcol = col - 2;
            if (check(nextrow, nextcol, actual, game))
            {
                int newactual = game.matrix[nextrow][nextcol];

                game.matrix[nextrow][nextcol] = value;
                if (newactual == -1)
                    game.minus1--;
                if (newactual == -2)
                    game.minus2--;
                if (newactual == -3)
                    game.minus3--;

                if (solverec(nextrow, nextcol, value + 1, newactual, game) == 0)
                    return 0;

                game.matrix[nextrow][nextcol] = newactual;
                if (newactual == -1)
                    game.minus1++;
                if (newactual == -2)
                    game.minus2++;
                if (newactual == -3)
                    game.minus3++;
            }

            //upleft
            nextrow = row - 2;
            nextcol = col - 2;
            if (check(nextrow, nextcol, actual, game))
            {
                int newactual = game.matrix[nextrow][nextcol];

                game.matrix[nextrow][nextcol] = value;
                if (newactual == -1)
                    game.minus1--;
                if (newactual == -2)
                    game.minus2--;
                if (newactual == -3)
                    game.minus3--;

                if (solverec(nextrow, nextcol, value + 1, newactual, game) == 0)
                    return 0;

                game.matrix[nextrow][nextcol] = newactual;
                if (newactual == -1)
                    game.minus1++;
                if (newactual == -2)
                    game.minus2++;
                if (newactual == -3)
                    game.minus3++;
            }

            //bottomright
            nextrow = row + 2;
            nextcol = col + 2;
            if (check(nextrow, nextcol, actual, game))
            {
                int newactual = game.matrix[nextrow][nextcol];

                game.matrix[nextrow][nextcol] = value;
                if (newactual == -1)
                    game.minus1--;
                if (newactual == -2)
                    game.minus2--;
                if (newactual == -3)
                    game.minus3--;

                if (solverec(nextrow, nextcol, value + 1, newactual, game) == 0)
                    return 0;

                game.matrix[nextrow][nextcol] = newactual;
                if (newactual == -1)
                    game.minus1++;
                if (newactual == -2)
                    game.minus2++;
                if (newactual == -3)
                    game.minus3++;
            }

            //upright
            nextrow = row - 2;
            nextcol = col + 2;
            if (check(nextrow, nextcol, actual, game))
            {
                int newactual = game.matrix[nextrow][nextcol];

                game.matrix[nextrow][nextcol] = value;
                if (newactual == -1)
                    game.minus1--;
                if (newactual == -2)
                    game.minus2--;
                if (newactual == -3)
                    game.minus3--;

                if (solverec(nextrow, nextcol, value + 1, newactual, game) == 0)
                    return 0;

                game.matrix[nextrow][nextcol] = newactual;
                if (newactual == -1)
                    game.minus1++;
                if (newactual == -2)
                    game.minus2++;
                if (newactual == -3)
                    game.minus3++;
            }

            return 1;
        }
    }
}
