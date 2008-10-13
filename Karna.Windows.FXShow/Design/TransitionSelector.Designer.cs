//===============================================================================
// Copyright © Serhiy Perevoznyk.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

namespace Karna.Windows.FXShow.Design
{
    /// <summary>
    /// Transition selection form for property Style editor
    /// </summary>
    partial class TransitionSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.edtStyles = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.transition = new Karna.Windows.FXShow.FXTransition();
            this.SuspendLayout();
            // 
            // edtStyles
            // 
            this.edtStyles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.edtStyles.FormattingEnabled = true;
            this.edtStyles.Items.AddRange(new object[] {
            "Expand from right",
            "Expand from left",
            "Slide in from right",
            "Slide in from left",
            "Reveal from left",
            "Reveal from right",
            "Expand in from right",
            "Expand in from left",
            "Expand in to middle",
            "Expand out from middle",
            "Reveal out from middle",
            "Reveal in from sides",
            "Expand in from sides",
            "Unroll from left",
            "Unroll from right",
            "Build up from right",
            "Build up from left",
            "Expand from bottom",
            "Expand from top",
            "Slide in from bottom",
            "Slide in from top",
            "Reveal from top",
            "Reveal from bottom",
            "Expand in from bottom",
            "Expand in from top",
            "Expand in to middle (horiz)",
            "Expand out from middle (horiz)",
            "Reveal from middle (horiz)",
            "Slide in from top / bottom",
            "Expand in from top / bottom",
            "Unroll from top",
            "Unroll from bottom",
            "Expand from bottom",
            "Expand in from top",
            "Expand from bottom right",
            "Expand from top right",
            "Expand from top left",
            "Expand from bottom left",
            "Slide in from bottom right",
            "Slide in from top right",
            "Slide in from top left",
            "Slide in from bottom left",
            "Reveal from top left",
            "Reveal from bottom left",
            "Reveal from bottom right",
            "Reveal from top right",
            "Appear and Contract to top left",
            "Appear and Contract to bottom left",
            "Appear and Contract to bottom right",
            "Appear and Contract to top right",
            "Appear and Contract to middle",
            "Expand out from centre",
            "Reveal out from centre",
            "Reveal in to centre",
            "Quarters Reveal in to middle",
            "Quarters Expand to middle",
            "Quarters Slide in to middle",
            "Curved Reveal from left",
            "Curved Reveal from right",
            "Bars in from right",
            "Bars in from left",
            "Bars left then right",
            "Bars right then left",
            "Bars from both sides",
            "Uneven shred from right",
            "Uneven shred from left",
            "Uneven shred out from middle (horiz)",
            "Uneven shred in to middle (horiz)",
            "Curved Reveal from top",
            "Curved Reveal from bottom",
            "Bars from bottom",
            "Bars from top",
            "Bars top then bottom",
            "Bars bottom then top",
            "Bars from top and bottom",
            "Unven shred from bottom",
            "Uneven shred from top",
            "Uneven shred from horizon",
            "Uneven shred in to horizon",
            "Curved reveal from top left",
            "Curved reveal from top right",
            "Curved reveal from bottom left",
            "Curved reveal from bottom right",
            "Elliptic reveal out from centre",
            "Elliptic reveal in to centre",
            "Criss Cross reveal from bottom right",
            "Criss Cross reveal from top right",
            "Criss Cross reveal from bottom left",
            "Criss Cross reveal from top left",
            "Criss Cross reveal bounce from top left",
            "Criss Cross reveal bounce from bottom left",
            "Criss Cross reveal bounce from top right",
            "Criss Cross reveal bounce from bottom right",
            "Criss Cross reveal from right top and bottom",
            "Criss Cross reveal from left top and bottom",
            "Criss Cross reveal from left right and bottom",
            "Criss Cross reveal from left right and top",
            "Criss Cross reveal from top left right and bottom",
            "Criss Cross reveal from bottom left top right",
            "Uneven shred from bottom and right",
            "Uneven shred from top and right",
            "Uneven shred from bottom and left",
            "Uneven shred from top and left",
            "Uneven shred from horiz and right",
            "Uneven shred from horiz and left",
            "Uneven shred from bottom and vert middle",
            "Uneven shred from top and vert middle",
            "Uneven shred from centre",
            "Uneven shred to centre",
            "Reveal diagonal from top left",
            "Reveal diagonal from top right",
            "Reveal diagonal from bottom left",
            "Reveal diagonal from bottom right",
            "Diagonal sweep from top left bottom right anticlockwise",
            "Diagonal sweep from top left bottom right clockwise",
            "Starburst clockwise from center",
            "Starburst anticlockwise from center",
            "Triangular shred",
            "Fade",
            "Pivot from top left",
            "Pivot from bottom left",
            "Pivot from top right",
            "Pivot from bottom right",
            "Speckle appear from right",
            "Speckle appear from left",
            "Speckle appear from bottom",
            "Speckle appear from top",
            "Random squares appear",
            "Push right",
            "Push left",
            "Push and squeeze right",
            "Push and squeeze left",
            "Push down",
            "Push up",
            "Push and sqeeze down",
            "Push and sqeeze up",
            "Blind vertically",
            "Blind horizontally",
            "Uneven blind from left",
            "Uneven blind from right",
            "Uneven blind from top",
            "Uneven blind from bottom",
            "Rectangular shred",
            "Sweep clockwise",
            "Sweep anticlockwise",
            "Rectangles apear from left and disapear to right",
            "Rectangles apear from right and disapear to left",
            "Rectangles apear from up and disapear to bottom",
            "Rectangles apear from bottom and disapear to up",
            "Rotational rectangle in center",
            "Rotational star in center",
            "Spiral rectangle",
            "Circular shred",
            "Reveal V from left",
            "Reveal V from right",
            "Reveal V from top",
            "Reveal V from bottom",
            "Bow Tie Horizontal",
            "Bow Tie Vertical",
            "Diagonal Cross In",
            "Diagonal Cross Out",
            "Zigzag Horizontal",
            "Zigzag Vertical",
            "Diamond shred",
            "Reveal diamond out from centre",
            "Reveal diamond in to centre",
            "Diagonal Box Out",
            "Pixelate",
            "Dissolve",
            "Random Bars Horizontal",
            "Random Bars Vertical",
            "Channel Mix"});
            this.edtStyles.Location = new System.Drawing.Point(11, 12);
            this.edtStyles.Name = "edtStyles";
            this.edtStyles.Size = new System.Drawing.Size(268, 21);
            this.edtStyles.TabIndex = 0;
            this.edtStyles.SelectedIndexChanged += new System.EventHandler(this.edtStyles_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(124, 258);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(204, 258);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // transition
            // 
            this.transition.BackColor = System.Drawing.Color.Black;
            this.transition.Center = false;
            this.transition.Location = new System.Drawing.Point(45, 39);
            this.transition.Name = "transition";
            this.transition.Progress = 0;
            this.transition.Proportional = false;
            this.transition.Size = new System.Drawing.Size(200, 200);
            this.transition.Stretch = false;
            this.transition.Style = 51;
            this.transition.Surface = global::Karna.Windows.FXShow.Properties.Resources.styleA;
            this.transition.TabIndex = 3;
            this.transition.Text = "fxTransition1";
            this.transition.Texture = global::Karna.Windows.FXShow.Properties.Resources.styleB;
            // 
            // TransitionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(291, 293);
            this.Controls.Add(this.transition);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.edtStyles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransitionSelector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transition Selector";
            this.Load += new System.EventHandler(this.TransitionSelector_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private FXTransition transition;
        /// <summary>
        /// List of the available transition styles
        /// </summary>
        public System.Windows.Forms.ComboBox edtStyles;
    }
}