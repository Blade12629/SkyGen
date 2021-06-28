
namespace SKMapGenerator
{
    partial class FormMain
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
            this.TBHeightmap = new System.Windows.Forms.TextBox();
            this.TBTilemap = new System.Windows.Forms.TextBox();
            this.TBHeightmapCS = new System.Windows.Forms.TextBox();
            this.TBTilemapCS = new System.Windows.Forms.TextBox();
            this.TBOutputDir = new System.Windows.Forms.TextBox();
            this.NUMMapIndex = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BTNGenerateMap = new System.Windows.Forms.Button();
            this.RTBOutput = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.BTNHeightmap = new System.Windows.Forms.Button();
            this.BTNHeightmapCS = new System.Windows.Forms.Button();
            this.BTNTilemap = new System.Windows.Forms.Button();
            this.BTNTilemapCS = new System.Windows.Forms.Button();
            this.BTNOutputDir = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.CBUseTilemap = new System.Windows.Forms.CheckBox();
            this.CBUseHeightmap = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TBStatics = new System.Windows.Forms.TextBox();
            this.BTNStatics = new System.Windows.Forms.Button();
            this.CBStatics = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.NUMMapIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // TBHeightmap
            // 
            this.TBHeightmap.BackColor = System.Drawing.Color.Black;
            this.TBHeightmap.ForeColor = System.Drawing.Color.Aqua;
            this.TBHeightmap.Location = new System.Drawing.Point(12, 25);
            this.TBHeightmap.Name = "TBHeightmap";
            this.TBHeightmap.Size = new System.Drawing.Size(226, 21);
            this.TBHeightmap.TabIndex = 0;
            this.TBHeightmap.Text = "heightmap.bmp";
            // 
            // TBTilemap
            // 
            this.TBTilemap.BackColor = System.Drawing.Color.Black;
            this.TBTilemap.ForeColor = System.Drawing.Color.Aqua;
            this.TBTilemap.Location = new System.Drawing.Point(12, 147);
            this.TBTilemap.Name = "TBTilemap";
            this.TBTilemap.Size = new System.Drawing.Size(226, 21);
            this.TBTilemap.TabIndex = 1;
            this.TBTilemap.Text = "tilemap.bmp";
            // 
            // TBHeightmapCS
            // 
            this.TBHeightmapCS.BackColor = System.Drawing.Color.Black;
            this.TBHeightmapCS.ForeColor = System.Drawing.Color.Aqua;
            this.TBHeightmapCS.Location = new System.Drawing.Point(12, 78);
            this.TBHeightmapCS.Name = "TBHeightmapCS";
            this.TBHeightmapCS.Size = new System.Drawing.Size(226, 21);
            this.TBHeightmapCS.TabIndex = 2;
            this.TBHeightmapCS.Text = "heightmap.colorstore";
            // 
            // TBTilemapCS
            // 
            this.TBTilemapCS.BackColor = System.Drawing.Color.Black;
            this.TBTilemapCS.ForeColor = System.Drawing.Color.Aqua;
            this.TBTilemapCS.Location = new System.Drawing.Point(12, 200);
            this.TBTilemapCS.Name = "TBTilemapCS";
            this.TBTilemapCS.Size = new System.Drawing.Size(226, 21);
            this.TBTilemapCS.TabIndex = 3;
            this.TBTilemapCS.Text = "tilemap.colorstore";
            // 
            // TBOutputDir
            // 
            this.TBOutputDir.BackColor = System.Drawing.Color.Black;
            this.TBOutputDir.ForeColor = System.Drawing.Color.Aqua;
            this.TBOutputDir.Location = new System.Drawing.Point(12, 337);
            this.TBOutputDir.Name = "TBOutputDir";
            this.TBOutputDir.Size = new System.Drawing.Size(226, 21);
            this.TBOutputDir.TabIndex = 4;
            // 
            // NUMMapIndex
            // 
            this.NUMMapIndex.BackColor = System.Drawing.Color.Black;
            this.NUMMapIndex.ForeColor = System.Drawing.Color.Aqua;
            this.NUMMapIndex.Location = new System.Drawing.Point(246, 387);
            this.NUMMapIndex.Name = "NUMMapIndex";
            this.NUMMapIndex.Size = new System.Drawing.Size(84, 21);
            this.NUMMapIndex.TabIndex = 5;
            this.NUMMapIndex.ValueChanged += new System.EventHandler(this.NUMMapIndex_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Heightmap (.bmp)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tilemap (.bmp)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(172, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Heightmap colorstore (.colorstore)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Tilemap colorstore (.colorstore)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(83, 321);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Output directory";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(257, 371);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Map Index";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // BTNGenerateMap
            // 
            this.BTNGenerateMap.BackColor = System.Drawing.Color.Black;
            this.BTNGenerateMap.Location = new System.Drawing.Point(12, 387);
            this.BTNGenerateMap.Name = "BTNGenerateMap";
            this.BTNGenerateMap.Size = new System.Drawing.Size(226, 23);
            this.BTNGenerateMap.TabIndex = 12;
            this.BTNGenerateMap.Text = "Generate Map";
            this.BTNGenerateMap.UseVisualStyleBackColor = false;
            this.BTNGenerateMap.Click += new System.EventHandler(this.BTNGenerateMap_Click);
            // 
            // RTBOutput
            // 
            this.RTBOutput.BackColor = System.Drawing.Color.Black;
            this.RTBOutput.ForeColor = System.Drawing.Color.Aqua;
            this.RTBOutput.Location = new System.Drawing.Point(336, 25);
            this.RTBOutput.Name = "RTBOutput";
            this.RTBOutput.ReadOnly = true;
            this.RTBOutput.Size = new System.Drawing.Size(295, 385);
            this.RTBOutput.TabIndex = 13;
            this.RTBOutput.Text = "";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(336, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Output";
            // 
            // BTNHeightmap
            // 
            this.BTNHeightmap.Location = new System.Drawing.Point(244, 25);
            this.BTNHeightmap.Name = "BTNHeightmap";
            this.BTNHeightmap.Size = new System.Drawing.Size(86, 21);
            this.BTNHeightmap.TabIndex = 15;
            this.BTNHeightmap.Text = "Select Folder";
            this.BTNHeightmap.UseVisualStyleBackColor = true;
            this.BTNHeightmap.Click += new System.EventHandler(this.BTNHeightmap_Click);
            // 
            // BTNHeightmapCS
            // 
            this.BTNHeightmapCS.Location = new System.Drawing.Point(244, 78);
            this.BTNHeightmapCS.Name = "BTNHeightmapCS";
            this.BTNHeightmapCS.Size = new System.Drawing.Size(86, 21);
            this.BTNHeightmapCS.TabIndex = 16;
            this.BTNHeightmapCS.Text = "Select Folder";
            this.BTNHeightmapCS.UseVisualStyleBackColor = true;
            this.BTNHeightmapCS.Click += new System.EventHandler(this.BTNHeightmapCS_Click);
            // 
            // BTNTilemap
            // 
            this.BTNTilemap.Location = new System.Drawing.Point(244, 146);
            this.BTNTilemap.Name = "BTNTilemap";
            this.BTNTilemap.Size = new System.Drawing.Size(86, 21);
            this.BTNTilemap.TabIndex = 17;
            this.BTNTilemap.Text = "Select Folder";
            this.BTNTilemap.UseVisualStyleBackColor = true;
            this.BTNTilemap.Click += new System.EventHandler(this.BTNTilemap_Click);
            // 
            // BTNTilemapCS
            // 
            this.BTNTilemapCS.Location = new System.Drawing.Point(244, 200);
            this.BTNTilemapCS.Name = "BTNTilemapCS";
            this.BTNTilemapCS.Size = new System.Drawing.Size(86, 21);
            this.BTNTilemapCS.TabIndex = 18;
            this.BTNTilemapCS.Text = "Select Folder";
            this.BTNTilemapCS.UseVisualStyleBackColor = true;
            this.BTNTilemapCS.Click += new System.EventHandler(this.BTNTilemapCS_Click);
            // 
            // BTNOutputDir
            // 
            this.BTNOutputDir.Location = new System.Drawing.Point(244, 336);
            this.BTNOutputDir.Name = "BTNOutputDir";
            this.BTNOutputDir.Size = new System.Drawing.Size(86, 21);
            this.BTNOutputDir.TabIndex = 19;
            this.BTNOutputDir.Text = "Select Folder";
            this.BTNOutputDir.UseVisualStyleBackColor = true;
            this.BTNOutputDir.Click += new System.EventHandler(this.BTNOutputDir_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // CBUseTilemap
            // 
            this.CBUseTilemap.AutoSize = true;
            this.CBUseTilemap.Checked = true;
            this.CBUseTilemap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBUseTilemap.Location = new System.Drawing.Point(12, 228);
            this.CBUseTilemap.Name = "CBUseTilemap";
            this.CBUseTilemap.Size = new System.Drawing.Size(81, 17);
            this.CBUseTilemap.TabIndex = 20;
            this.CBUseTilemap.Text = "Use tilemap";
            this.CBUseTilemap.UseVisualStyleBackColor = true;
            this.CBUseTilemap.CheckedChanged += new System.EventHandler(this.CBUseTilemap_CheckedChanged);
            // 
            // CBUseHeightmap
            // 
            this.CBUseHeightmap.AutoSize = true;
            this.CBUseHeightmap.Checked = true;
            this.CBUseHeightmap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBUseHeightmap.Location = new System.Drawing.Point(12, 105);
            this.CBUseHeightmap.Name = "CBUseHeightmap";
            this.CBUseHeightmap.Size = new System.Drawing.Size(97, 17);
            this.CBUseHeightmap.TabIndex = 21;
            this.CBUseHeightmap.Text = "Use heightmap";
            this.CBUseHeightmap.UseVisualStyleBackColor = true;
            this.CBUseHeightmap.CheckedChanged += new System.EventHandler(this.CBUseHeightmap_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(70, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(126, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Static Apply Array (.saa)";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TBStatics
            // 
            this.TBStatics.BackColor = System.Drawing.Color.Black;
            this.TBStatics.ForeColor = System.Drawing.Color.Aqua;
            this.TBStatics.Location = new System.Drawing.Point(12, 271);
            this.TBStatics.Name = "TBStatics";
            this.TBStatics.Size = new System.Drawing.Size(226, 21);
            this.TBStatics.TabIndex = 22;
            this.TBStatics.Text = "statics.saa";
            // 
            // BTNStatics
            // 
            this.BTNStatics.Location = new System.Drawing.Point(244, 271);
            this.BTNStatics.Name = "BTNStatics";
            this.BTNStatics.Size = new System.Drawing.Size(86, 21);
            this.BTNStatics.TabIndex = 24;
            this.BTNStatics.Text = "Select Folder";
            this.BTNStatics.UseVisualStyleBackColor = true;
            this.BTNStatics.Click += new System.EventHandler(this.BTNStatics_Click);
            // 
            // CBStatics
            // 
            this.CBStatics.AutoSize = true;
            this.CBStatics.Location = new System.Drawing.Point(12, 298);
            this.CBStatics.Name = "CBStatics";
            this.CBStatics.Size = new System.Drawing.Size(88, 17);
            this.CBStatics.TabIndex = 25;
            this.CBStatics.Text = "Apply Statics";
            this.CBStatics.UseVisualStyleBackColor = true;
            this.CBStatics.CheckedChanged += new System.EventHandler(this.CBStatics_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(648, 426);
            this.Controls.Add(this.CBStatics);
            this.Controls.Add(this.BTNStatics);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TBStatics);
            this.Controls.Add(this.CBUseHeightmap);
            this.Controls.Add(this.CBUseTilemap);
            this.Controls.Add(this.BTNOutputDir);
            this.Controls.Add(this.BTNTilemapCS);
            this.Controls.Add(this.BTNTilemap);
            this.Controls.Add(this.BTNHeightmapCS);
            this.Controls.Add(this.BTNHeightmap);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.RTBOutput);
            this.Controls.Add(this.BTNGenerateMap);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NUMMapIndex);
            this.Controls.Add(this.TBOutputDir);
            this.Controls.Add(this.TBTilemapCS);
            this.Controls.Add(this.TBHeightmapCS);
            this.Controls.Add(this.TBTilemap);
            this.Controls.Add(this.TBHeightmap);
            this.ForeColor = System.Drawing.Color.Aqua;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.Text = "SkyGen";
            ((System.ComponentModel.ISupportInitialize)(this.NUMMapIndex)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBHeightmap;
        private System.Windows.Forms.TextBox TBTilemap;
        private System.Windows.Forms.TextBox TBHeightmapCS;
        private System.Windows.Forms.TextBox TBTilemapCS;
        private System.Windows.Forms.TextBox TBOutputDir;
        private System.Windows.Forms.NumericUpDown NUMMapIndex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BTNGenerateMap;
        private System.Windows.Forms.RichTextBox RTBOutput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BTNHeightmap;
        private System.Windows.Forms.Button BTNHeightmapCS;
        private System.Windows.Forms.Button BTNTilemap;
        private System.Windows.Forms.Button BTNTilemapCS;
        private System.Windows.Forms.Button BTNOutputDir;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox CBUseTilemap;
        private System.Windows.Forms.CheckBox CBUseHeightmap;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TBStatics;
        private System.Windows.Forms.Button BTNStatics;
        private System.Windows.Forms.CheckBox CBStatics;
    }
}

