using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace s09p23
{
    public partial class Form1 : Form
    {

        Nodo nodo;
        Arbol arbol;

        Point pa;
        Point pb;

        List<Point> la;
        List<Point> lb;

        int n;
        String n1;
        String n2;

        SolidBrush brush1;
        SolidBrush brush2;

        FontFamily fontFamily = new FontFamily("Consolas");
        Font font;
        Pen pincel;

        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Inicializar();
            
        }

        private void Inicializar() {

            font = new Font(fontFamily, 15, FontStyle.Bold, GraphicsUnit.Pixel);
            brush1 = new SolidBrush(Color.Blue);
            brush2 = new SolidBrush(Color.White);
            pincel = new Pen(Color.Red);
            pincel.Width = 2;

            n = 0;
            n1 = "00";

            arbol = new Arbol();
            nodo=  arbol.Insertar(n1, null);

            la = new List<Point>();
            lb = new List<Point>();

            pa = new Point(0, 0);
            pb = new Point(this.Width / 2, 50);

            la.Add(pa);
            lb.Add(pb);

            AgregarPaneles();
            panel1.Invalidate();
            timer1.Enabled = false;
        }


        private void AgregarPaneles() {

            panel1.Controls.Clear();

            for (int i = 0; i < lb.Count; i++) {
                Point p1 = new Point(lb[i].X - 20, lb[i].Y - 20);
                Panel pn = new Panel();
                pn.Location = p1;
                pn.Name = "p_" + i.ToString("00");
                //pn.BorderStyle = BorderStyle.FixedSingle;
                pn.Size = new Size(40, 40);
                pn.BackColor = Color.FromArgb(0, 0, 0, 0);
                pn.Click += new System.EventHandler(this.panel_Click);
                panel1.Controls.Add(pn);
            }
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g;

            //g = e.Graphics;


        }

        object aux;

        private void panel_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;

            if (me.Button == MouseButtons.Left) { 
                AgregarNodo(sender);
                
            }

            if (me.Button == MouseButtons.Right) {
                Panel p1 = (sender as Panel);
                aux = p1;
                Point ptLowerLeft = new Point(0, p1.Height);
                ptLowerLeft = p1.PointToScreen(ptLowerLeft);
                contextMenuStrip1.Show(ptLowerLeft);

            }


        }

        private void esHojaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EsHoja(aux);
        }


        private void EsHoja(object sender)
        {
            Panel p1 = (sender as Panel);
            Point p = panel1.PointToClient(Cursor.Position);

            n1 = p1.Name.Split('_')[1];

            Console.WriteLine();
            Console.WriteLine(n1 + "\t" + p.X.ToString() + "\t" + p.Y.ToString());

            Nodo encontrado = arbol.Buscar(n1, nodo);

            encontrado = encontrado.hijo;

            string res = "No es hoja";

            if (encontrado == null)
            {
                res = "Si es hoja";
            }
           


            int VisibleTime = 1000;
            ToolTip tt = new ToolTip();
            tt.Show(res, p1, 0, 0, VisibleTime);

        }



        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MostrarGrado(aux);
        }

        private void MostrarGrado(object sender)
        {
            Panel p1 = (sender as Panel);
            Point p = panel1.PointToClient(Cursor.Position);

            n1 = p1.Name.Split('_')[1];

            Console.WriteLine();
            Console.WriteLine(n1 + "\t" + p.X.ToString() + "\t" + p.Y.ToString());

            Nodo encontrado = arbol.Buscar(n1, nodo);

            encontrado = encontrado.hijo;

            int k = 0;

            if (encontrado == null)
            {
                k = 0;
            }
            else {
                k = 1;
                while (encontrado.hermano != null)
                {
                    k++;
                    encontrado = encontrado.hermano;
                }
            }


            Console.WriteLine("encontrado===============");
            Console.WriteLine(k);


            int VisibleTime = 1000;
            ToolTip tt = new ToolTip();
            tt.Show(k.ToString(), p1, 0, 0, VisibleTime);

        }


        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgregarNodo(aux);
        }


        private void AgregarNodo(object sender) {
            Panel p1 = (sender as Panel);
            Point p = panel1.PointToClient(Cursor.Position);

            n1 = p1.Name.Split('_')[1];

            Console.WriteLine();
            Console.WriteLine(n1 + "\t" + p.X.ToString() + "\t" + p.Y.ToString());

            pa = new Point(p1.Location.X + 20, p1.Location.Y + 20);
            n = n + 1;

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point p = panel1.PointToClient(Cursor.Position);
            pb = p;
            panel1.Invalidate();
        }

        private void panel1_Click(object sender, EventArgs e)
        {

            if (timer1.Enabled) {

                timer1.Stop();

                la.Add(pa);
                lb.Add(pb);

                panel1.Invalidate();
                AgregarPaneles();

                n2 = n.ToString("00");
                Console.WriteLine(n2 + "\t" + pb.X.ToString() + "\t" + pb.Y.ToString());

                Nodo encontrado = arbol.Buscar(n1, nodo);
                arbol.Insertar(n2, encontrado);
            }

        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape) {
                Inicializar();
            }
            if (e.KeyData == Keys.F5) {
                panel1.Invalidate();
            }
            if (e.KeyData == Keys.F4)
            {
                Console.WriteLine("========================================");
                arbol.TransversaPreO(nodo);
                Console.WriteLine("========================================");
            }
        }

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

            if (timer1.Enabled) {
                e.Graphics.DrawLine(pincel, pa,pb);
            }

            
            if (lb.Count>1)
            {
                for (int i = 1; i < lb.Count; i++)
                    e.Graphics.DrawLine(pincel, la[i], lb[i]);
            }

            for (int i = 0; i < lb.Count; i++) {
                e.Graphics.FillEllipse(brush1, lb[i].X - 20, lb[i].Y - 20, 40, 40);
                e.Graphics.DrawEllipse(pincel, lb[i].X - 20, lb[i].Y - 20, 40, 40);
                Point tp = new Point(lb[i].X - 10, lb[i].Y - 10);
                e.Graphics.DrawString(i.ToString("00"), font, brush2, tp);
            }

        }

       
    }
}
