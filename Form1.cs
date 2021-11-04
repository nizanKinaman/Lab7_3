using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace CG_Lab_6
{
    public partial class Form1 : Form
    {
        static Bitmap bmp = new Bitmap(800, 800);

        Graphics g = Graphics.FromImage(bmp);

        Pen myPen = new Pen(Color.Black);
        List<Line> lines;
        Polyhedron poly = new Polyhedron();

        int size = 70;

        Point3 moving_point = new Point3(0, 0, 0);
        Point3 moving_point_line = new Point3(0, 0, 0);
        Point3 centr;
        string Path = "points.txt";

        public Form1()
        {
            InitializeComponent();
            //lines = Hex(size);
            centr = new Point3(pictureBox1.Width / 2, pictureBox1.Height / 2, 0);
            poly = Tetr(size);
            var edges = poly.edges;
            foreach (var ed in edges)
                g.DrawPolygon(myPen, Position2d(ed));
            pictureBox1.Image = bmp;
        }
        public class Point3
        {
            public double X;
            public double Y;
            public double Z;
            public int ID;

            public Point3() { X = 0; Y = 0; Z = 0; ID = 0; }

            public Point3(double x, double y, double z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }
            public Point3(double x, double y, double z, int id)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.ID = id;
            }
        }

        public class Line
        {
            public Point3 p1;
            public Point3 p2;
            public int ID;

            public Line()
            {
                p1 = new Point3();
                p2 = new Point3();
            }

            public Line(Point3 p1, Point3 p2)
            {
                this.p1 = p1;
                this.p2 = p2;
            }

        }

        public class Edge
        {
            public List<Point3> points;

            public Edge()
            {
                this.points = new List<Point3>{};
            }
            public Edge(List<Point3> p)
            {
                this.points = p;
            }
        }

        public class Polyhedron
        {
            public List<Edge> edges;

            public Polyhedron()
            {
                this.edges = new List<Edge> { };
            }
            public Polyhedron(List<Edge> e)
            {
                this.edges = e;
            }
        }

        public Polyhedron Hex(int size)
        {
            var hc = size / 2;
            Polyhedron p = new Polyhedron();
            Edge e = new Edge();
            // 1-2-3-4
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(hc, hc, -hc), // 2
                new Point3(hc, -hc, -hc), // 3
                new Point3(-hc, -hc, -hc) // 4
            }; 
            p.edges.Add(e);
            e = new Edge();

            // 1-2-6-5
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(hc, hc, -hc), // 2
                new Point3(hc, hc, hc), // 6 
                new Point3(-hc, hc, hc) // 5
            }; 
            p.edges.Add(e);
            e = new Edge();

            // 5-6-7-8
            e.points = new List<Point3> {
                new Point3(-hc, hc, hc), // 5
                new Point3(hc, hc, hc), // 6 
                new Point3(hc, -hc, hc), // 7
                new Point3(-hc, -hc, hc) // 8
            };
            p.edges.Add(e);
            e = new Edge();

            // 6-2-3-7
            e.points = new List<Point3> {
                new Point3(hc, hc, hc), // 6 
                new Point3(hc, hc, -hc), // 2
                new Point3(hc, -hc, -hc), // 3
                new Point3(hc, -hc, hc) // 7
            };
            p.edges.Add(e);
            e = new Edge();

            // 5-1-4-8
            e.points = new List<Point3> {
                new Point3(-hc, hc, hc), // 5
                new Point3(-hc, hc, -hc), // 1
                new Point3(-hc, -hc, -hc), // 4
                new Point3(-hc, -hc, hc) // 8
            };
            p.edges.Add(e);
            e = new Edge();

            // 4-3-7-8
            e.points = new List<Point3> {
                new Point3(-hc, -hc, -hc), // 4
                new Point3(hc, -hc, -hc), // 3
                new Point3(hc, -hc, hc), // 7
                new Point3(-hc, -hc, hc) // 8
            };
            p.edges.Add(e);
            e = new Edge();

            return p;

        }

        public Polyhedron Tetr(int size)
        {
            //var tetr_centr = size / 2;
            //return new List<Line>
            //{
            //    new Line(new Point3(-tetr_centr, tetr_centr, -tetr_centr), new Point3(-tetr_centr, -tetr_centr, tetr_centr)), //1->2
            //    new Line(new Point3(-tetr_centr, tetr_centr, -tetr_centr), new Point3(-tetr_centr, tetr_centr, tetr_centr)), //1->4
            //    new Line(new Point3(-tetr_centr, tetr_centr, -tetr_centr), new Point3(tetr_centr, tetr_centr, tetr_centr)), //1->3
            //    new Line(new Point3(-tetr_centr, -tetr_centr, tetr_centr), new Point3(-tetr_centr, tetr_centr, tetr_centr)), //2->4
            //    new Line(new Point3(-tetr_centr, -tetr_centr, tetr_centr), new Point3(tetr_centr, tetr_centr, tetr_centr)), //2->3
            //    new Line(new Point3(tetr_centr, tetr_centr, tetr_centr), new Point3(-tetr_centr, tetr_centr, tetr_centr)) //3->4
            //};

            var hc = size / 2;
            Polyhedron p = new Polyhedron();
            Edge e = new Edge();
            // 1-2-3
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(-hc, -hc, hc), // 2
                new Point3(-hc, hc, hc), // 3
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-4-2
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(hc, hc, hc), // 4
                new Point3(-hc, -hc, hc), // 2 
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-3-4
            e.points = new List<Point3> {
                new Point3(-hc, hc, -hc), // 1
                new Point3(-hc, hc, hc), // 3
                new Point3(hc, hc, hc), // 4
            };
            p.edges.Add(e);
            e = new Edge();

            // 3-2-4
            e.points = new List<Point3> {
                new Point3(-hc, hc, hc), // 3 
                new Point3(-hc, -hc, hc), // 2
                new Point3(hc, hc, hc), // 4
            };
            p.edges.Add(e);

            return p;
        }

        public Polyhedron Oct(int size)
        {
            var hc = size / 2;
            Polyhedron p = new Polyhedron();
            Edge e = new Edge();
            // 1-2-3-4
            //e.points = new List<Point3> {
            //    new Point3(hc, 0, -hc), // 1
            //    new Point3(-hc, 0, -hc), // 2
            //    new Point3(-hc, 0, hc), // 3
            //    new Point3(hc, 0, hc) // 4
            //};
            //p.edges.Add(e);
            //e = new Edge();

            // 2-5-1
            e.points = new List<Point3> {
                new Point3(-hc, 0, -hc), // 2
                new Point3(0, hc, 0), // 5
                new Point3(hc, 0, -hc), // 1 
            };
            p.edges.Add(e);
            e = new Edge();

            // 2-5-3
            e.points = new List<Point3> {
                new Point3(-hc, 0, -hc), // 2
                new Point3(0, hc, 0), // 5
                new Point3(-hc, 0, hc), // 3
            };
            p.edges.Add(e);
            e = new Edge();

            // 3-5-4
            e.points = new List<Point3> {
                new Point3(-hc, 0, hc), // 3 
                new Point3(0, hc, 0), // 5
                new Point3(hc, 0, hc), // 4
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-5-4
            e.points = new List<Point3> {
                new Point3(hc, 0, -hc), // 1
                new Point3(0, hc, 0), // 5
                new Point3(hc, 0, hc), // 4
            };
            p.edges.Add(e);
            e = new Edge();
            ////////
            // 2-6-1
            e.points = new List<Point3> {
                new Point3(-hc, 0, -hc), // 2
                new Point3(0, -hc, 0), // 6
                new Point3(hc, 0, -hc), // 1 
            };
            p.edges.Add(e);
            e = new Edge();

            // 2-6-3
            e.points = new List<Point3> {
                new Point3(-hc, 0, -hc), // 2
                new Point3(0, -hc, 0), // 6
                new Point3(-hc, 0, hc), // 3
            };
            p.edges.Add(e);
            e = new Edge();

            // 3-6-4
            e.points = new List<Point3> {
                new Point3(-hc, 0, hc), // 3 
                new Point3(0, -hc, 0), // 6
                new Point3(hc, 0, hc), // 4
            };
            p.edges.Add(e);
            e = new Edge();

            // 1-6-4
            e.points = new List<Point3> {
                new Point3(hc, 0, -hc), // 1
                new Point3(0, -hc, 0), // 6
                new Point3(hc, 0, hc), // 4
            };
            p.edges.Add(e);
            e = new Edge();

            return p;
        }


        Point Position2d(Point3 p)
        {
            return new Point((int)p.X + (int)centr.X, (int)p.Y + (int)centr.Y);
        }

        Point[] Position2d(Edge e)
        {
            List<Point> p2D = new List<Point> { };
            foreach (var p3 in e.points)
            {
                p2D.Add(new Point((int)p3.X + (int)centr.X, (int)p3.Y + (int)centr.Y));
            }
            return p2D.ToArray();
        }

        public static double[,] MultiplyMatrix(double[,] m1, double[,] m2)
        {
            double[,] m = new double[1, 4];

            for (int i = 0; i < 4; i++)
            {
                var temp = 0.0;
                for (int j = 0; j < 4; j++)
                {
                    temp += m1[0, j] * m2[j, i];
                }
                m[0, i] = temp;
            }
            return m;
        }



        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                g.Clear(Color.White);
                poly = Tetr(size);
                var edges = poly.edges;
                foreach (var ed in edges)
                    g.DrawPolygon(myPen, Position2d(ed));
                pictureBox1.Image = bmp;
            }
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton8.Checked)
            {
                g.Clear(Color.White);
                poly = Hex(size);
                var edges = poly.edges;
                foreach (var ed in edges)
                    g.DrawPolygon(myPen, Position2d(ed));
                pictureBox1.Image = bmp;
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked)
            {
                g.Clear(Color.White);
                poly = Oct(size);
                var edges = poly.edges;
                foreach (var ed in edges)
                    g.DrawPolygon(myPen, Position2d(ed));
                pictureBox1.Image = bmp;
            }
        }

        

        // clear
        private void button7_Click(object sender, EventArgs e)
        {
            poly = new Polyhedron();
            moving_point = new Point3(0, 0, 0);
            moving_point_line = new Point3(0, 0, 0);
            //lines = Hex(size);
            g.Clear(Color.White);
            pictureBox1.Image = bmp;
        }

        // move
        private void button1_Click(object sender, EventArgs e)
        {
            var posx = double.Parse(textBox1.Text);
            var posy = double.Parse(textBox2.Text);
            var posz = double.Parse(textBox3.Text);


            g.Clear(Color.White);
            moving_point.X += posx;
            moving_point.Y -= posy;
            moving_point.Z += posz;
            List<Edge> newEdges = new List<Edge>();
            foreach (var edge in poly.edges)
            {
                Edge newPoints = new Edge();
                foreach (var point in edge.points)
                {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.X;
                    m[0, 1] = point.Y;
                    m[0, 2] = point.Z;
                    m[0, 3] = 1;

                    double[,] matr = new double[4, 4]
                {   { 1, 0, 0, 0},
                    { 0, 1, 0, 0 },
                    {0, 0, 1, 0 },
                    { posx, -posy, posz, 1 } };

                    var final_matrix = MultiplyMatrix(m, matr);

                    newPoints.points.Add(new Point3(final_matrix[0, 0], final_matrix[0, 1], final_matrix[0, 2]));
                }
                newEdges.Add(newPoints);

            }
            poly.edges = newEdges;
            DrawPol();
            pictureBox1.Image = bmp;
        }

        // rotate
        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            List<Edge> newEdges = new List<Edge>();
            foreach (var edge in poly.edges)
            {
                Edge newPoints = new Edge();
                foreach (var point in edge.points)
                {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.X - moving_point.X;
                    m[0, 1] = point.Y - moving_point.Y;
                    m[0, 2] = point.Z - moving_point.Z;
                    m[0, 3] = 1;

                    var angle = double.Parse(textBox6.Text) * Math.PI / 180;
                    double[,] matrx = new double[4, 4]
                {   { Math.Cos(angle), 0, Math.Sin(angle), 0},
                    { 0, 1, 0, 0 },
                    {-Math.Sin(angle), 0, Math.Cos(angle), 0 },
                    { 0, 0, 0, 1 } };

                    angle = double.Parse(textBox5.Text) * Math.PI / 180;
                    double[,] matry = new double[4, 4]
                    {  { 1, 0, 0, 0 },
                    { 0, Math.Cos(angle), -Math.Sin(angle), 0},
                    {0, Math.Sin(angle), Math.Cos(angle), 0 },
                    { 0, 0, 0, 1 } };

                    angle = double.Parse(textBox4.Text) * Math.PI / 180;
                    double[,] matrz = new double[4, 4]
                    {  { Math.Cos(angle), -Math.Sin(angle), 0, 0},
                    { Math.Sin(angle), Math.Cos(angle), 0, 0 },
                    { 0, 0, 1, 0 },
                    { 0, 0, 0, 1 } };

                    var final_matrix = MultiplyMatrix(m, matrx);
                    final_matrix = MultiplyMatrix(final_matrix, matry);
                    final_matrix = MultiplyMatrix(final_matrix, matrz);

                    newPoints.points.Add(new Point3(final_matrix[0, 0] + moving_point.X, final_matrix[0, 1] + moving_point.Y, final_matrix[0, 2] + moving_point.Z));
                }
                newEdges.Add(newPoints);
            }
            poly.edges = newEdges;
            DrawPol();
            pictureBox1.Image = bmp;
        }

        // scale
        private void button3_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            List<Edge> newEdges = new List<Edge>();
            var posx = double.Parse(textBox9.Text);
            var posy = double.Parse(textBox8.Text);
            var posz = double.Parse(textBox7.Text);
            foreach (var edge in poly.edges)
            {
                Edge newPoints = new Edge();
                foreach (var point in edge.points)
                {
                    double[,] m = new double[1, 4];
                    m[0, 0] = point.X - moving_point.X;
                    m[0, 1] = point.Y - moving_point.Y;
                    m[0, 2] = point.Z - moving_point.Z;
                    m[0, 3] = 1;

                    double[,] matr = new double[4, 4]
                {   { posx, 0, 0, 0 },
                    { 0, posy, 0, 0 },
                    { 0, 0, posz, 0 },
                    { 0, 0, 0, 1 } };

                    var final_matrix = MultiplyMatrix(m, matr);

                    newPoints.points.Add(new Point3(final_matrix[0, 0] + moving_point.X, final_matrix[0, 1] + moving_point.Y, final_matrix[0, 2] + moving_point.Z));
                }
                newEdges.Add(newPoints);

            }
            poly.edges = newEdges;
            DrawPol();

        }

        public void DrawAll()
        {
            foreach (var line in lines)
            {
                g.DrawLine(myPen, Position2d(line.p1), Position2d(line.p2));
            }
            pictureBox1.Image = bmp;
        }
        public void DrawPol()
        {
            g.Clear(Color.White);
            pictureBox1.Image = bmp;
            foreach (var edge in poly.edges)
                g.DrawPolygon(myPen, Position2d(edge));
            pictureBox1.Image = bmp;
        }


        private void button8_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(Path))
            {
                foreach(var edge in poly.edges)
                {
                    foreach(var point in edge.points)
                    {
                        writer.Write(""+point.X +" "+point.Y + " "+ point.Z +";");
                    }
                    writer.Write("\n");
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var readText = File.ReadAllLines(Path);
            List<Edge> Edges = new List<Edge>();
            foreach (var line in readText)
            {
                Edge points = new Edge();
                var pointsline = line.Split(';');
                foreach (var point in pointsline)
                {
                    var pointXYZ = point.Split();
                    if (pointXYZ.Length > 1)
                        points.points.Add(new Point3(double.Parse(pointXYZ[0]), double.Parse(pointXYZ[1]), double.Parse(pointXYZ[2])));
                }
                Edges.Add(points);
            }
            poly.edges = Edges;

            DrawPol();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var s = size / 2;
            poly = new Polyhedron();
            Polyhedron p = new Polyhedron();


            Func < double, double, double> func = (double x, double y) => { return 50 * (Math.Sin(x / 50) * Math.Cos(y / 50)); };
            if(comboBox1.Text == "sin(x)+cos(y)")
                func = (double x, double y) => { return 50 * (Math.Sin(x / 50) + Math.Cos(y / 50)); };
            //Func<double,double,double> func = (double x, double y) => { return 50*( (x/50) * (y/50)); };
            //Func<double, double, double> func = (double x, double y) => { return 50* Math.Sqrt(1 - (x / 50)* (x / 50) - (y / 50)* (y / 50)); };
            //Func<double, double, double> func = (double x, double y) => { return  1/((x / 50)* (x / 50) + (y / 50) * (y / 50)); };

            var x0 = int.Parse(textBoxX0.Text);
            var x1 = int.Parse(textBoxX1.Text);
            var y0 = int.Parse(textBoxY0.Text);
            var y1 = int.Parse(textBoxY1.Text);
            var splitx = int.Parse(textBoxSplitx.Text);
            var splity = int.Parse(textBoxSplity.Text);

            var stepx = (double)(x1 - x0) / splitx * 50;
            var stepy = (double)(y1 - y0) / splity * 50;

            var curr_x = 0.0;
            var curr_y = 0.0;

            for (int i = 0; i < splitx; i++)
            {
                for (int j = 0; j < splity; j++)
                {
                    poly.edges.Add(new Edge(new List<Point3> {
                    new Point3(curr_x        , curr_y        ,func(x0 + curr_x        ,y0+ curr_y)),
                    new Point3(curr_x + stepx, curr_y        ,func(x0 + curr_x + stepx,y0+ curr_y)), 
                    new Point3(curr_x + stepx, curr_y + stepy,func(x0 + curr_x + stepx,y0+ curr_y + stepy)), 
                    new Point3(curr_x        , curr_y + stepy,func(x0 + curr_x        ,y0+ curr_y + stepy))
                    }));
                    curr_x += stepx;
                }
                curr_x = 0.0;
                curr_y += stepy;

            }
            DrawPol();
        }
    }
}
