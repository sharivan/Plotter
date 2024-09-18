using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Plotter
{
    public partial class FrmPlotter : Form
    {
        private const float MINX = -3;
        private const float MINY = -3;
        private const float MAXX = 3;
        private const float MAXY = 3;

        private const float MINT = 0;
        private const float MAXT = (float)(2 * Math.PI);

        private const float STEPX = 0.1F;
        private const float STEPT = 0.01F;

        private readonly StackMachine machine1;
        private readonly StackMachine machine2;
        private readonly ExpressionCompiler compiler;

        private float minX = MINX;
        private float minY = MINY;
        private float maxX = MAXX;
        private float maxY = MAXY;

        private float minT = MINT;
        private float maxT = MAXT;

        private readonly float stepX = STEPX;
        private readonly float stepT = STEPT;

        private int savedTop;

        private bool parametric;
        private bool polar;

        public FrmPlotter()
        {
            machine1 = new StackMachine();
            machine2 = new StackMachine();
            compiler = new ExpressionCompiler();

            machine1.GenerateExceptionObject = false;
            machine2.GenerateExceptionObject = false;

            InitializeComponent();
        }

        private PointF PolarTransform(float r, float theta)
        {
            float x = (float)(r * Math.Cos(theta));
            float y = (float)(r * Math.Sin(theta));

            return new PointF(x, y);
        }

        private PointF TransformPoint(float x, float y)
        {
            if (polar)
            {
                float r = y;
                float theta = x;
                PointF p = PolarTransform(r, theta);
                x = p.X;
                y = p.Y;
            }

            float scaleX = pbGraph.ClientSize.Width / (maxX - minX);
            float scaleY = pbGraph.ClientSize.Height / (maxY - minY);
            return new PointF(x * scaleX + pbGraph.ClientSize.Width / 2, pbGraph.ClientSize.Height / 2 - y * scaleY);
        }

        private PointF TransformPoint(PointF p) => TransformPoint(p.X, p.Y);

        private void btnPlot_Click(object sender, EventArgs e)
        {
            try
            {
                minX = float.Parse(txtMinX.Text, System.Globalization.CultureInfo.InvariantCulture);
                minY = float.Parse(txtMinY.Text, System.Globalization.CultureInfo.InvariantCulture);
                maxX = float.Parse(txtMaxX.Text, System.Globalization.CultureInfo.InvariantCulture);
                maxY = float.Parse(txtMaxY.Text, System.Globalization.CultureInfo.InvariantCulture);

                minT = float.Parse(txtMinT.Text, System.Globalization.CultureInfo.InvariantCulture);
                maxT = float.Parse(txtMaxT.Text, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            machine1.Clear();
            machine2.Clear();

            string expression = txtExpression2.Text;
            if (expression == "")
            {
                MessageBox.Show("Expressão vazia!");
                return;
            }

            try
            {
                compiler.Compile(expression, machine2);
            }
            catch (Exception ex)
            {
                machine2.Clear();

                MessageBox.Show(ex.Message);

                return;
            }

            if (chkParametric.Checked)
            {
                parametric = true;

                expression = txtExpression1.Text;
                if (expression == "")
                {
                    machine2.Clear();

                    MessageBox.Show("Expressão vazia!");
                    return;
                }

                try
                {
                    compiler.Compile(expression, machine1);
                }
                catch (Exception ex)
                {
                    machine1.Clear();
                    machine2.Clear();

                    MessageBox.Show(ex.Message);

                    return;
                }
            }
            else
                parametric = false;

            polar = rbPolar.Checked;

            if (parametric || polar)
            {
                machine2.SetVar("t", 0);

                if (parametric)
                    machine1.SetVar("t", 0);
            }
            else
                machine2.SetVar("x", 0);

            pbGraph.Invalidate();
        }

        private void pbGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            using (var pen = new Pen(Color.Red))
            {
                g.DrawLine(pen, 0, pbGraph.ClientSize.Height / 2, pbGraph.ClientSize.Width, pbGraph.ClientSize.Height / 2);
                g.DrawLine(pen, pbGraph.ClientSize.Width / 2, 0, pbGraph.ClientSize.Width / 2, pbGraph.ClientSize.Height);
            }

            if (machine2.Size > 0 && (parametric ? (machine1.Size > 0) : true))
            {
                float t0;
                float t1;
                float dt;
                int varIndex1;
                int varIndex2;
                if (polar || parametric)
                {
                    t0 = minT;
                    t1 = maxT;
                    dt = stepT;
                    varIndex1 = machine1.GetVarIndex("t");
                    varIndex2 = machine2.GetVarIndex("t");
                }
                else
                {
                    t0 = minX;
                    t1 = maxX;
                    dt = stepX;
                    varIndex1 = machine1.GetVarIndex("x");
                    varIndex2 = machine2.GetVarIndex("x");
                }

                float y;
                float x;

                float x0 = 0;
                float y0 = 0;

                float width = t1 - t0;
                float height = maxY - minY;

                bool first = true;
                for (float t = t0; t <= t1; t += dt)
                {
                    if (parametric)
                    {
                        machine1.SetVar(varIndex1, t);
                        x = machine1.Eval();
                        if (machine1.Exception)
                        {
                            first = true;
                            continue;
                        }
                    }
                    else
                        x = t;

                    machine2.SetVar(varIndex2, t);
                    y = machine2.Eval();
                    if (machine2.Exception || !first && (Math.Abs(x - x0) >= width || Math.Abs(y - y0) >= height))
                    {
                        first = true;
                        continue;
                    }

                    if (!first)
                    {
                        using (var pen = new Pen(Color.Blue))
                        {
                            PointF p0 = TransformPoint(x0, y0);
                            PointF p = TransformPoint(x, y);

                            try
                            {
                                g.DrawLine(pen, p0, p);
                            }
                            catch (ArithmeticException)
                            {
                                first = true;
                                continue;
                            }
                        }
                    }
                    else
                        first = false;

                    x0 = x;
                    y0 = y;
                }
            }
        }

        private void FrmPlotter_Load(object sender, EventArgs e)
        {
            txtMinX.Text = minX.ToString(System.Globalization.CultureInfo.InvariantCulture);
            txtMinY.Text = minY.ToString(System.Globalization.CultureInfo.InvariantCulture);
            txtMaxX.Text = maxX.ToString(System.Globalization.CultureInfo.InvariantCulture);
            txtMaxY.Text = maxY.ToString(System.Globalization.CultureInfo.InvariantCulture);

            txtMinT.Text = minT.ToString(System.Globalization.CultureInfo.InvariantCulture);
            txtMaxT.Text = maxT.ToString(System.Globalization.CultureInfo.InvariantCulture);

            lblFunc1.Visible = false;
            txtExpression1.Visible = false;

            savedTop = lblFunc2.Top;
            lblFunc2.Top = lblFunc1.Top;
            txtExpression2.Top = txtExpression1.Top;
        }

        private void pbGraph_Resize(object sender, EventArgs e) => pbGraph.Invalidate();

        private void rbRectangular_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRectangular.Checked)
            {
                lblFunc1.Text = "x=";
                lblFunc2.Text = "y=";
                lblMinT.Visible = chkParametric.Checked;
                lblMaxT.Visible = chkParametric.Checked;
                txtMinT.Visible = chkParametric.Checked;
                txtMaxT.Visible = chkParametric.Checked;
            }
        }

        private void rbPolar_CheckedChanged(object sender, EventArgs e)
        {
            if (rbPolar.Checked)
            {
                lblFunc1.Text = "θ=";
                lblFunc2.Text = "r=";
                lblMinT.Visible = true;
                lblMaxT.Visible = true;
                txtMinT.Visible = true;
                txtMaxT.Visible = true;
            }
        }

        private void chkParametric_CheckedChanged(object sender, EventArgs e)
        {
            if (chkParametric.Checked || rbPolar.Checked)
            {
                lblMinT.Visible = true;
                lblMaxT.Visible = true;
                txtMinT.Visible = true;
                txtMaxT.Visible = true;
            }
            else
            {
                lblMinT.Visible = false;
                lblMaxT.Visible = false;
                txtMinT.Visible = false;
                txtMaxT.Visible = false;
            }

            if (chkParametric.Checked)
            {
                lblFunc1.Visible = true;
                txtExpression1.Visible = true;

                lblFunc2.Top = savedTop;
                txtExpression2.Top = savedTop;
            }
            else
            {
                lblFunc1.Visible = false;
                txtExpression1.Visible = false;

                savedTop = lblFunc2.Top;
                lblFunc2.Top = lblFunc1.Top;
                txtExpression2.Top = txtExpression1.Top;
            }
        }
    }
}
