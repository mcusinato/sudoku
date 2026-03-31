using System;

namespace icosudokusolution
{
    public class Vertex
    {
        public int Value { get; set; }
        public char Name { get; set; }
        public int SolValue { get; set; }
        public int Tassigned { get; set; }
    }
    public class Triangle: ICloneable
    {
        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Value3 { get; set; }
        public int used { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public bool Rotable()
        {
            return !(Value1 == Value2 && Value2 == Value3);
        }
        public void Rotate()
        {
            int temp=Value1;
            Value1 = Value3;
            Value3 = Value2;
            Value2 = temp;
        }
    }
    public class Position
    {
        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }
        public Vertex V3 { get; set; }
        public Triangle T { get; set; }
        public int rotation { get; set; }
    }

    class Program
    {
        public const int N_T = 14;
        public const int N_V = 12;
        public const int N_P = 20;

        public static string ico = @"
           /A\         /A\         /A\         /A\         /A\     
          /*  \       /*  \       /*  \       /*  \       /*  \    
         /     \     /     \     /     \     /     \     /     \   
        /  P00  \   /  P01  \   /  P02  \   /  P03  \   /  P04  \  
      B/*_______*\C/*_______*\D/*_______*\E/*_______*\F/*_______*\B
     / \*       */ \*       */ \*       */ \*       */ \*       */ 
    /*  \  P06  /*  \  P08  /*  \  P10  /*  \  P12  /*  \  P14  /  
   /     \     /     \     /     \     /     \     /     \     /   
  /  P05  \*  /  P07  \*  /  P09  \*  /  P11  \*  /  P13  \*  /    
G/*_______*\H/*_______*\I/*_______*\J/*_______*\K/*_______*\G/     
 \*       */ \*       */ \*       */ \*       */ \*       */       
  \  P15  /   \  P16  /   \  P17  /   \  P18  /   \  P19  /        
   \     /     \     /     \     /     \     /     \     /         
    \*  /       \*  /       \*  /       \*  /       \*  /          
     \L/         \L/         \L/         \L/         \L/           
";
        public static string icoformat = @"
           /A\         /A\         /A\         /A\         /A\     
          /{0}  \       /{3}  \       /{6}  \       /{9}  \       /{12}  \    
         /     \     /     \     /     \     /     \     /     \   
        /  P00  \   /  P01  \   /  P02  \   /  P03  \   /  P04  \  
      B/{1}_______{2}\C/{4}_______{5}\D/{7}_______{8}\E/{10}_______{11}\F/{13}_______{14}\B
     / \{18}       {20}/ \{24}       {26}/ \{30}       {32}/ \{36}       {38}/ \{42}       {44}/ 
    /{15}  \  P06  /{21}  \  P08  /{27}  \  P10  /{33}  \  P12  /{39}  \  P14  /  
   /     \     /     \     /     \     /     \     /     \     /   
  /  P05  \{19}  /  P07  \{25}  /  P09  \{31}  /  P11  \{37}  /  P13  \{43}  /    
G/{16}_______{17}\H/{22}_______{23}\I/{28}_______{29}\J/{34}_______{35}\K/{40}_______{41}\G/     
 \{45}       {47}/ \{48}       {50}/ \{51}       {53}/ \{54}       {56}/ \{57}       {59}/       
  \  P15  /   \  P16  /   \  P17  /   \  P18  /   \  P19  /        
   \     /     \     /     \     /     \     /     \     /         
    \{46}  /       \{49}  /       \{52}  /       \{55}  /       \{58}  /          
     \L/         \L/         \L/         \L/         \L/           
";
        static void populatet(Triangle[] t)
        {
            int i = 0;

            t[i] = new Triangle();
            t[i].Value1 = 0;
            t[i].Value2 = 0;
            t[i].Value3 = 0;
            t[i].used = 1;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 0;
            t[i].Value2 = 0;
            t[i].Value3 = 1;
            t[i].used = 1;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 0;
            t[i].Value2 = 0;
            t[i].Value3 = 2;
            t[i].used = 1;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 0;
            t[i].Value2 = 0;
            t[i].Value3 = 3;
            t[i].used = 1;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 0;
            t[i].Value2 = 1;
            t[i].Value3 = 2;
            t[i].used = 3;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 0;
            t[i].Value2 = 2;
            t[i].Value3 = 1;
            t[i].used = 3;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 0;
            t[i].Value2 = 1;
            t[i].Value3 = 1;
            t[i].used = 1;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 0;
            t[i].Value2 = 2;
            t[i].Value3 = 2;
            t[i].used = 1;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 0;
            t[i].Value2 = 3;
            t[i].Value3 = 3;
            t[i].used = 1;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 1;
            t[i].Value2 = 1;
            t[i].Value3 = 1;
            t[i].used = 1;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 1;
            t[i].Value2 = 2;
            t[i].Value3 = 3;
            t[i].used = 2;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 1;
            t[i].Value2 = 3;
            t[i].Value3 = 2;
            t[i].used = 2;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 2;
            t[i].Value2 = 2;
            t[i].Value3 = 2;
            t[i].used = 1;
            i++;
            t[i] = new Triangle();
            t[i].Value1 = 3;
            t[i].Value2 = 3;
            t[i].Value3 = 3;
            t[i].used = 1;
            i++;
        }
        static void populatev(Vertex[] v)
        {
            v[0] = new Vertex();
            v[0].Name = 'A';
            v[0].SolValue = 0;
            v[0].Tassigned = 0;
            for (int i = 1; i < v.Length; i++)
            {
                v[i] = new Vertex();
                v[i].Name = (char)(v[i - 1].Name + 1);
                v[i].SolValue = 0;
                v[i].Tassigned = 0;
            }

        }
        static void populatep(Position[] p, Vertex[] v)
        {
            int i = 0;

            p[i] = new Position();
            p[i].V1 = v[0];
            p[i].V2 = v[1];
            p[i].V3 = v[2];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[0];
            p[i].V2 = v[2];
            p[i].V3 = v[3];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[0];
            p[i].V2 = v[3];
            p[i].V3 = v[4];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[0];
            p[i].V2 = v[4];
            p[i].V3 = v[5];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[0];
            p[i].V2 = v[5];
            p[i].V3 = v[1];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[1];
            p[i].V2 = v[6];
            p[i].V3 = v[7];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[1];
            p[i].V2 = v[7];
            p[i].V3 = v[2];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[2];
            p[i].V2 = v[7];
            p[i].V3 = v[8];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[2];
            p[i].V2 = v[8];
            p[i].V3 = v[3];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[3];
            p[i].V2 = v[8];
            p[i].V3 = v[9];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[3];
            p[i].V2 = v[9];
            p[i].V3 = v[4];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[4];
            p[i].V2 = v[9];
            p[i].V3 = v[10];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[4];
            p[i].V2 = v[10];
            p[i].V3 = v[5];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[5];
            p[i].V2 = v[10];
            p[i].V3 = v[6];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[5];
            p[i].V2 = v[6];
            p[i].V3 = v[1];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[6];
            p[i].V2 = v[11];
            p[i].V3 = v[7];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[7];
            p[i].V2 = v[11];
            p[i].V3 = v[8];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[8];
            p[i].V2 = v[11];
            p[i].V3 = v[9];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[9];
            p[i].V2 = v[11];
            p[i].V3 = v[10];
            p[i].T = null;
            i++;
            p[i] = new Position();
            p[i].V1 = v[10];
            p[i].V2 = v[11];
            p[i].V3 = v[6];
            p[i].T = null;
            i++;
        }
        static bool checkinput(Vertex[] v)
        {
            var v1 = (Vertex [])v.Clone(); 
            Array.Sort(v1,
                delegate (Vertex x, Vertex y) { return x.Value.CompareTo(y.Value); });

            for (int i = 0; i < v.Length; i++)
            { 
                if(v1[i].Value!=i+1)
                    return false;
            }
            return true;
        }
        static bool getinput(Vertex[] v)
        {
            for (int i = 0; i < v.Length; i++)
            {
                Console.Write("Value for vertex {0} [1-12]: ", v[i].Name);
                v[i].Value = int.Parse(Console.ReadLine());
            }
            if (!checkinput(v))
            {
                Console.WriteLine("ERROR: Input incorrect!");
                return false;
            }
            for (int i = 0; i < v.Length; i++)
            {
                ico = ico.Replace(v[i].Name, v[i].Value.ToString("x").ToCharArray()[0]);
            }
            Console.WriteLine(ico);
            return true;
        }
        static void printsol(Vertex[] v, Position[] p)
        {
            object[] values = new object[60];

            Console.Write("\nSolution:");
            for (int i = 0; i < v.Length; i++)
            {
                icoformat = icoformat.Replace(v[i].Name, v[i].Value.ToString("x").ToCharArray()[0]);
                Console.Write(" {0}-{1}/{2}",v[i].Name,v[i].Value,v[i].SolValue);
            }

            for (int i = 0; i < p.Length; i++)
            {
                if (p[i].T is null)
                {
                    values[i * 3 + 0] = 9;
                    values[i * 3 + 1] = 9;
                    values[i * 3 + 2] = 9;
                }
                else
                {
                    values[i * 3 + 0] = p[i].T.Value1;
                    values[i * 3 + 1] = p[i].T.Value2;
                    values[i * 3 + 2] = p[i].T.Value3;
                }
            }
            Console.WriteLine(icoformat, values);
        }
        static bool solok(Vertex[] v, Triangle[] t, Position[] p, int pos)
        {
            if (p[pos].V1.SolValue > p[pos].V1.Value)
                return false;
            if (p[pos].V2.SolValue > p[pos].V2.Value)
                return false;
            if (p[pos].V3.SolValue > p[pos].V3.Value)
                return false;
            if (p[pos].V1.Tassigned == 5 && p[pos].V1.SolValue != p[pos].V1.Value)
                return false;
            if (p[pos].V2.Tassigned == 5 && p[pos].V2.SolValue != p[pos].V2.Value)
                return false;
            if (p[pos].V3.Tassigned == 5 && p[pos].V3.SolValue != p[pos].V3.Value)
                return false;
            if ((5 - p[pos].V1.Tassigned) * 3 + p[pos].V1.SolValue < p[pos].V1.Value)
                return false;
            if ((5 - p[pos].V2.Tassigned) * 3 + p[pos].V2.SolValue < p[pos].V2.Value)
                return false;
            if ((5 - p[pos].V3.Tassigned) * 3 + p[pos].V3.SolValue < p[pos].V3.Value)
                return false;

            int i;
            if (p[pos].V1.Tassigned == 4)
            {
                int missing = p[pos].V1.Value - p[pos].V1.SolValue;
                for (i = 0; i < t.Length; i++)
                {
                    if (t[i].used>0 && (t[i].Value1 == missing || t[i].Value2 == missing || t[i].Value3 == missing))
                        break;
                }
                if (i==t.Length)
                {
                    return false;
                }
            }

            if (p[pos].V2.Tassigned == 4)
            {
                int missing = p[pos].V2.Value - p[pos].V2.SolValue;
                for (i = 0; i < t.Length; i++)
                {
                    if (t[i].used>0 && (t[i].Value1 == missing || t[i].Value2 == missing || t[i].Value3 == missing))
                        break;
                }
                if (i == t.Length)
                {
                    return false;
                }
            }
            
            if (p[pos].V3.Tassigned == 4)
            {
                int missing = p[pos].V3.Value - p[pos].V3.SolValue;
                for (i = 0; i < t.Length; i++)
                {
                    if (t[i].used>0 && (t[i].Value1 == missing || t[i].Value2 == missing || t[i].Value3 == missing))
                        break;
                }
                if (i == t.Length)
                {
                    return false;
                }
            }

            return true;
        }
        static bool FindSol(Vertex[] v, Triangle[] t, Position[] p, int pos)
        {
            //Console.Write("\r{0,2}", pos);

            if (pos == p.Length)
                return true;

            for (int i = 0; i < t.Length; i++) //all triangles
            {
                if (t[i].used>0)
                {
                    Triangle t_temp = (Triangle)(t[i].Clone());
                    p[pos].T = t_temp;
                    for (int j = 0; j < (t_temp.Rotable() ? 3 : 1); j++) //all rotations
                    {
                        p[pos].V1.SolValue += t_temp.Value1;
                        p[pos].V1.Tassigned++;
                        p[pos].V2.SolValue += t_temp.Value2;
                        p[pos].V2.Tassigned++;
                        p[pos].V3.SolValue += t_temp.Value3;
                        p[pos].V3.Tassigned++;
                        t[i].used--;
                        if (solok(v,t,p,pos))
                        {
                            if (FindSol(v, t, p, pos + 1))
                                return true;
                        }
                        t[i].used++;
                        p[pos].V1.SolValue -= t_temp.Value1;
                        p[pos].V1.Tassigned--;
                        p[pos].V2.SolValue -= t_temp.Value2;
                        p[pos].V2.Tassigned--;
                        p[pos].V3.SolValue -= t_temp.Value3;
                        p[pos].V3.Tassigned--;
                        t_temp.Rotate();
                    }
                    p[pos].T = null;
                }
            }
            return false;
        }
        static public bool Main(string[] inputs, out Triangle[] t, out Vertex[] v, out Position[] p)
        {
//            Console.WriteLine("Isosudoku solver");
//            Console.WriteLine("{0}", ico);

            t = new Triangle[N_T];
            populatet(t);

            v = new Vertex[N_V];
            populatev(v);
            
            p = new Position[N_P];
            populatep(p, v);

            for (int i = 0; i < N_V; i++)
                v[i].Value = int.Parse(inputs[i]);
            
            if (!checkinput(v))
                return false;
            
            FindSol(v, t, p, 0);

            return true;
            
            //printsol(v, p);
        }

    }
}
