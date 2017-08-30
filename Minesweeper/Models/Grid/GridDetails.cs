using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minesweeper.Models.Grid
{
    public class GridDetails
    {
        private static readonly int[,] config = { { 8, 8, 10 },
                                                  { 16, 16, 40},
                                                  { 16, 36, 99 } };   //Grid_length,Grid_width,no_of_mines
        public int Grid_length;
        public int Grid_width;
        public int No_of_mines;
        private int[,,] Grid;
        private static readonly int[] p = { -1, 0, 1 };
        public void config_set(int diff) {
            Grid_length = config[diff - 1, 0];
            Grid_width = config[diff - 1, 1];
            No_of_mines = config[diff - 1, 2];
            Create_Grid();
            
            //Checking Grid output
            string[] w = new string[Grid_length];
            string q,t;
            for(int i = 0; i < Grid_length; i++) {
                q = "";
                for(int j = 0; j < Grid_width; j++) {
                    t = Grid[i, j, 0].ToString();
                    if(t.Length == 1)
                        q += "000"; 
                    q += t + " ";
                }
                w[i] = q;
            }
            System.IO.File.WriteAllLines(@"./w.txt", w);
            
        }
        private bool InBound(int a, int b) {
            if(a < 0 || a >= Grid_length)
                return false;
            if(b < 0 || b >= Grid_width)
                return false;
            return true;
        }
        private void Create_Grid() {
            //Blank Grid Creation
            Grid = new int[Grid_length, Grid_width, 2];
            for (int i = 0; i < Grid_length; i++) {
                for (int j = 0; j < Grid_width; j++) {
                    Grid[i, j, 0] = 0;
                    Grid[i, j, 1] = 0;
                }
            }
            //Placing bomb and numbers
            Random r = new Random();
            int x, y;
            for(int i = 0; i < No_of_mines; i++) {
                x = r.Next(Grid_length);
                y = r.Next(Grid_width);
                if (Grid[x, y, 0] >= 0) {
                    for (int j = 0; j < 3; j++) {
                        for (int k = 0; k < 3; k++) {
                            if (j == 1 && k == 1)
                                Grid[x, y, 0] = -999;
                            else if (InBound(x + p[j], y + p[k]))
                                ++Grid[x + p[j], y + p[k], 0];
                        }
                    }
                }
                else
                    i--;
            }
        }
        public List<string> cell_click(string cell) {
            int x,y;
            List<string> result = new List<string>();
            string[] temp = cell.Split('A');

            x = Convert.ToInt32(temp[0]);
            y = Convert.ToInt32(temp[1]);
            if (Grid[x, y, 0] < 0)
                result.Add("A");
            else if (Grid[x, y, 0] > 0) {
                result.Add(cell + "A" + Grid[x, y, 0].ToString());
                Grid[x, y, 1] = 1;
            }
            else {
                List<int[]> q = new List<int[]>();
                q.Add(new int[] { x, y });
                while (q.Count > 0) {
                    x = q[0][0];
                    y = q[0][1];
                    for (int i = 0; i < 3; i++) {
                        for (int j = 0; j < 3; j++) {
                            if (InBound(x + p[i], y + p[j]) && Grid[x + p[i], y + p[j], 1] == 0) {
                                if(Grid[x + p[i], y + p[j],0] < 0) {
                                    continue;
                                }
                                else if (Grid[x + p[i], y + p[j], 0] > 0) {
                                    result.Add((x + p[i]).ToString() + "A" + (y + p[j]).ToString() + "A" + Grid[x + p[i], y + p[j], 0].ToString());
                                    Grid[x + p[i], y + p[j], 1] = 1;
                                }
                                else {
                                    q.Add(new int[] { x + p[i], y + p[j] });
                                    result.Add((x + p[i]).ToString() + "A" + (y + p[j]).ToString() + "A0");
                                    Grid[x + p[i], y + p[j], 1] = 1;
                                }
                            }
                        }
                    }
                    q.RemoveAt(0);
                }
            }
            return result;
        }
    }
}
