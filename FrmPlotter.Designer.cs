
namespace Plotter
{
    partial class FrmPlotter
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPlot = new System.Windows.Forms.Button();
            this.pbGraph = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinX = new System.Windows.Forms.TextBox();
            this.txtMinY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaxX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMaxY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gbCoordinates = new System.Windows.Forms.GroupBox();
            this.chkParametric = new System.Windows.Forms.CheckBox();
            this.rbPolar = new System.Windows.Forms.RadioButton();
            this.rbRectangular = new System.Windows.Forms.RadioButton();
            this.txtMinT = new System.Windows.Forms.TextBox();
            this.lblMinT = new System.Windows.Forms.Label();
            this.txtMaxT = new System.Windows.Forms.TextBox();
            this.lblMaxT = new System.Windows.Forms.Label();
            this.txtExpression2 = new System.Windows.Forms.TextBox();
            this.lblFunc2 = new System.Windows.Forms.Label();
            this.lblFunc1 = new System.Windows.Forms.Label();
            this.txtExpression1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).BeginInit();
            this.gbCoordinates.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPlot
            // 
            this.btnPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlot.Location = new System.Drawing.Point(617, 80);
            this.btnPlot.Name = "btnPlot";
            this.btnPlot.Size = new System.Drawing.Size(73, 23);
            this.btnPlot.TabIndex = 1;
            this.btnPlot.Text = "Plot";
            this.btnPlot.UseVisualStyleBackColor = true;
            this.btnPlot.Click += new System.EventHandler(this.btnPlot_Click);
            // 
            // pbGraph
            // 
            this.pbGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbGraph.Location = new System.Drawing.Point(3, 1);
            this.pbGraph.Name = "pbGraph";
            this.pbGraph.Size = new System.Drawing.Size(500, 500);
            this.pbGraph.TabIndex = 4;
            this.pbGraph.TabStop = false;
            this.pbGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.pbGraph_Paint);
            this.pbGraph.Resize += new System.EventHandler(this.pbGraph_Resize);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(509, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Min X";
            // 
            // txtMinX
            // 
            this.txtMinX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinX.Location = new System.Drawing.Point(553, 223);
            this.txtMinX.Name = "txtMinX";
            this.txtMinX.Size = new System.Drawing.Size(243, 23);
            this.txtMinX.TabIndex = 6;
            // 
            // txtMinY
            // 
            this.txtMinY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinY.Location = new System.Drawing.Point(553, 252);
            this.txtMinY.Name = "txtMinY";
            this.txtMinY.Size = new System.Drawing.Size(243, 23);
            this.txtMinY.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(509, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Min Y";
            // 
            // txtMaxX
            // 
            this.txtMaxX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxX.Location = new System.Drawing.Point(553, 281);
            this.txtMaxX.Name = "txtMaxX";
            this.txtMaxX.Size = new System.Drawing.Size(243, 23);
            this.txtMaxX.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(509, 284);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Max X";
            // 
            // txtMaxY
            // 
            this.txtMaxY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxY.Location = new System.Drawing.Point(553, 310);
            this.txtMaxY.Name = "txtMaxY";
            this.txtMaxY.Size = new System.Drawing.Size(243, 23);
            this.txtMaxY.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(509, 313);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 13;
            this.label4.Text = "Max Y";
            // 
            // gbCoordinates
            // 
            this.gbCoordinates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCoordinates.Controls.Add(this.chkParametric);
            this.gbCoordinates.Controls.Add(this.rbPolar);
            this.gbCoordinates.Controls.Add(this.rbRectangular);
            this.gbCoordinates.Location = new System.Drawing.Point(509, 344);
            this.gbCoordinates.Name = "gbCoordinates";
            this.gbCoordinates.Size = new System.Drawing.Size(286, 99);
            this.gbCoordinates.TabIndex = 17;
            this.gbCoordinates.TabStop = false;
            this.gbCoordinates.Text = "Coordenadas";
            // 
            // chkParametric
            // 
            this.chkParametric.AutoSize = true;
            this.chkParametric.Location = new System.Drawing.Point(6, 72);
            this.chkParametric.Name = "chkParametric";
            this.chkParametric.Size = new System.Drawing.Size(89, 19);
            this.chkParametric.TabIndex = 19;
            this.chkParametric.Text = "Paramétrica";
            this.chkParametric.UseVisualStyleBackColor = true;
            this.chkParametric.CheckedChanged += new System.EventHandler(this.chkParametric_CheckedChanged);
            // 
            // rbPolar
            // 
            this.rbPolar.AutoSize = true;
            this.rbPolar.Location = new System.Drawing.Point(6, 47);
            this.rbPolar.Name = "rbPolar";
            this.rbPolar.Size = new System.Drawing.Size(63, 19);
            this.rbPolar.TabIndex = 18;
            this.rbPolar.Text = "Polares";
            this.rbPolar.UseVisualStyleBackColor = true;
            this.rbPolar.CheckedChanged += new System.EventHandler(this.rbPolar_CheckedChanged);
            // 
            // rbRectangular
            // 
            this.rbRectangular.AutoSize = true;
            this.rbRectangular.Checked = true;
            this.rbRectangular.Location = new System.Drawing.Point(6, 22);
            this.rbRectangular.Name = "rbRectangular";
            this.rbRectangular.Size = new System.Drawing.Size(93, 19);
            this.rbRectangular.TabIndex = 17;
            this.rbRectangular.TabStop = true;
            this.rbRectangular.Text = "Retangulares";
            this.rbRectangular.UseVisualStyleBackColor = true;
            this.rbRectangular.CheckedChanged += new System.EventHandler(this.rbRectangular_CheckedChanged);
            // 
            // txtMinT
            // 
            this.txtMinT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinT.Location = new System.Drawing.Point(553, 449);
            this.txtMinT.Name = "txtMinT";
            this.txtMinT.Size = new System.Drawing.Size(243, 23);
            this.txtMinT.TabIndex = 19;
            this.txtMinT.Visible = false;
            // 
            // lblMinT
            // 
            this.lblMinT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMinT.AutoSize = true;
            this.lblMinT.Location = new System.Drawing.Point(509, 452);
            this.lblMinT.Name = "lblMinT";
            this.lblMinT.Size = new System.Drawing.Size(37, 15);
            this.lblMinT.TabIndex = 18;
            this.lblMinT.Text = "Min T";
            this.lblMinT.Visible = false;
            // 
            // txtMaxT
            // 
            this.txtMaxT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMaxT.Location = new System.Drawing.Point(553, 478);
            this.txtMaxT.Name = "txtMaxT";
            this.txtMaxT.Size = new System.Drawing.Size(243, 23);
            this.txtMaxT.TabIndex = 21;
            this.txtMaxT.Visible = false;
            // 
            // lblMaxT
            // 
            this.lblMaxT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMaxT.AutoSize = true;
            this.lblMaxT.Location = new System.Drawing.Point(509, 481);
            this.lblMaxT.Name = "lblMaxT";
            this.lblMaxT.Size = new System.Drawing.Size(36, 15);
            this.lblMaxT.TabIndex = 20;
            this.lblMaxT.Text = "MaxT";
            this.lblMaxT.Visible = false;
            // 
            // txtExpression2
            // 
            this.txtExpression2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpression2.Location = new System.Drawing.Point(537, 33);
            this.txtExpression2.Name = "txtExpression2";
            this.txtExpression2.Size = new System.Drawing.Size(259, 23);
            this.txtExpression2.TabIndex = 0;
            // 
            // lblFunc2
            // 
            this.lblFunc2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFunc2.Location = new System.Drawing.Point(508, 33);
            this.lblFunc2.Name = "lblFunc2";
            this.lblFunc2.Size = new System.Drawing.Size(22, 23);
            this.lblFunc2.TabIndex = 15;
            this.lblFunc2.Text = "y=";
            this.lblFunc2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFunc1
            // 
            this.lblFunc1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFunc1.Location = new System.Drawing.Point(509, 4);
            this.lblFunc1.Name = "lblFunc1";
            this.lblFunc1.Size = new System.Drawing.Size(22, 23);
            this.lblFunc1.TabIndex = 23;
            this.lblFunc1.Text = "x=";
            this.lblFunc1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFunc1.Visible = false;
            // 
            // txtExpression1
            // 
            this.txtExpression1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpression1.Location = new System.Drawing.Point(537, 5);
            this.txtExpression1.Name = "txtExpression1";
            this.txtExpression1.Size = new System.Drawing.Size(259, 23);
            this.txtExpression1.TabIndex = 22;
            this.txtExpression1.Visible = false;
            // 
            // FrmPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 503);
            this.Controls.Add(this.lblFunc1);
            this.Controls.Add(this.txtExpression1);
            this.Controls.Add(this.txtMaxT);
            this.Controls.Add(this.lblMaxT);
            this.Controls.Add(this.txtMinT);
            this.Controls.Add(this.lblMinT);
            this.Controls.Add(this.gbCoordinates);
            this.Controls.Add(this.lblFunc2);
            this.Controls.Add(this.txtMaxY);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtMaxX);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMinY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMinX);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbGraph);
            this.Controls.Add(this.btnPlot);
            this.Controls.Add(this.txtExpression2);
            this.DoubleBuffered = true;
            this.Name = "FrmPlotter";
            this.Text = "Plotter";
            this.Load += new System.EventHandler(this.FrmPlotter_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbGraph)).EndInit();
            this.gbCoordinates.ResumeLayout(false);
            this.gbCoordinates.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnPlot;
        private System.Windows.Forms.PictureBox pbGraph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMinX;
        private System.Windows.Forms.TextBox txtMinY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaxX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMaxY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbCoordinates;
        private System.Windows.Forms.RadioButton rbPolar;
        private System.Windows.Forms.RadioButton rbRectangular;
        private System.Windows.Forms.TextBox txtMinT;
        private System.Windows.Forms.Label lblMinT;
        private System.Windows.Forms.TextBox txtMaxT;
        private System.Windows.Forms.Label lblMaxT;
        private System.Windows.Forms.CheckBox chkParametric;
        private System.Windows.Forms.TextBox txtExpression2;
        private System.Windows.Forms.Label lblFunc2;
        private System.Windows.Forms.Label lblFunc1;
        private System.Windows.Forms.TextBox txtExpression1;
    }
}

