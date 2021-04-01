namespace HCMCalc_UrbanStreets
{
    partial class frmMain
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
            this.btnReadFile = new System.Windows.Forms.Button();
            this.btnCalcResults = new System.Windows.Forms.Button();
            this.btnWriteFile = new System.Windows.Forms.Button();
            this.btnCreateArterial = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBatchOutputFilesFolder = new System.Windows.Forms.TextBox();
            this.txtBatchInputFilesFolder = new System.Windows.Forms.TextBox();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.lblComplete = new System.Windows.Forms.Label();
            this.progBarBatch = new System.Windows.Forms.ProgressBar();
            this.btnSelectBatchOutputFilesFolder = new System.Windows.Forms.Button();
            this.btnProcessFiles = new System.Windows.Forms.Button();
            this.btnSelectBatchInputFilesFolder = new System.Windows.Forms.Button();
            this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCalcServiceVols = new System.Windows.Forms.Button();
            this.cboTestParameter = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnReadFileSerVols = new System.Windows.Forms.Button();
            this.btnWriteSerVolInputs = new System.Windows.Forms.Button();
            this.lblIteration = new System.Windows.Forms.Label();
            this.lblUpperBound = new System.Windows.Forms.Label();
            this.lblLowerBound = new System.Windows.Forms.Label();
            this.lblStep = new System.Windows.Forms.Label();
            this.progBarSerVols = new System.Windows.Forms.ProgressBar();
            this.lblPctSerVols = new System.Windows.Forms.Label();
            this.lblTestParm = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtOutputFilesFolder = new System.Windows.Forms.TextBox();
            this.btnOutputFilesFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCreateIntersection = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(24, 99);
            this.btnReadFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(146, 45);
            this.btnReadFile.TabIndex = 0;
            this.btnReadFile.Text = "Read File";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnCalcResults
            // 
            this.btnCalcResults.Location = new System.Drawing.Point(213, 33);
            this.btnCalcResults.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCalcResults.Name = "btnCalcResults";
            this.btnCalcResults.Size = new System.Drawing.Size(148, 45);
            this.btnCalcResults.TabIndex = 1;
            this.btnCalcResults.Text = "Calculate LOS";
            this.btnCalcResults.UseVisualStyleBackColor = true;
            this.btnCalcResults.Click += new System.EventHandler(this.btnCalcResults_Click);
            // 
            // btnWriteFile
            // 
            this.btnWriteFile.Location = new System.Drawing.Point(213, 99);
            this.btnWriteFile.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnWriteFile.Name = "btnWriteFile";
            this.btnWriteFile.Size = new System.Drawing.Size(148, 45);
            this.btnWriteFile.TabIndex = 2;
            this.btnWriteFile.Text = "Write Results to File";
            this.btnWriteFile.UseVisualStyleBackColor = true;
            this.btnWriteFile.Click += new System.EventHandler(this.btnWriteFile_Click);
            // 
            // btnCreateArterial
            // 
            this.btnCreateArterial.Location = new System.Drawing.Point(24, 33);
            this.btnCreateArterial.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCreateArterial.Name = "btnCreateArterial";
            this.btnCreateArterial.Size = new System.Drawing.Size(146, 45);
            this.btnCreateArterial.TabIndex = 3;
            this.btnCreateArterial.Text = "Create Arterial";
            this.btnCreateArterial.UseVisualStyleBackColor = true;
            this.btnCreateArterial.Click += new System.EventHandler(this.btnCreateArterial_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBatchOutputFilesFolder);
            this.groupBox1.Controls.Add(this.txtBatchInputFilesFolder);
            this.groupBox1.Controls.Add(this.lblPercentage);
            this.groupBox1.Controls.Add(this.lblComplete);
            this.groupBox1.Controls.Add(this.progBarBatch);
            this.groupBox1.Controls.Add(this.btnSelectBatchOutputFilesFolder);
            this.groupBox1.Controls.Add(this.btnProcessFiles);
            this.groupBox1.Controls.Add(this.btnSelectBatchInputFilesFolder);
            this.groupBox1.Location = new System.Drawing.Point(22, 453);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(807, 320);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Batch Processing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(541, 15);
            this.label1.TabIndex = 61;
            this.label1.Text = "Use buttons to select input and output folder(s), or copy and paste folder path d" +
    "irectly into textboxes.";
            // 
            // txtBatchOutputFilesFolder
            // 
            this.txtBatchOutputFilesFolder.Location = new System.Drawing.Point(255, 148);
            this.txtBatchOutputFilesFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtBatchOutputFilesFolder.Multiline = true;
            this.txtBatchOutputFilesFolder.Name = "txtBatchOutputFilesFolder";
            this.txtBatchOutputFilesFolder.Size = new System.Drawing.Size(527, 48);
            this.txtBatchOutputFilesFolder.TabIndex = 60;
            this.txtBatchOutputFilesFolder.Validating += new System.ComponentModel.CancelEventHandler(this.txtBatchOutputFilesFolder_Validating);
            this.txtBatchOutputFilesFolder.Validated += new System.EventHandler(this.txtBatchOutputFilesFolder_Validated);
            // 
            // txtBatchInputFilesFolder
            // 
            this.txtBatchInputFilesFolder.Location = new System.Drawing.Point(255, 73);
            this.txtBatchInputFilesFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtBatchInputFilesFolder.Multiline = true;
            this.txtBatchInputFilesFolder.Name = "txtBatchInputFilesFolder";
            this.txtBatchInputFilesFolder.Size = new System.Drawing.Size(527, 48);
            this.txtBatchInputFilesFolder.TabIndex = 59;
            this.txtBatchInputFilesFolder.Validating += new System.ComponentModel.CancelEventHandler(this.txtBatchInputFilesFolder_Validating);
            this.txtBatchInputFilesFolder.Validated += new System.EventHandler(this.txtBatchInputFilesFolder_Validated);
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPercentage.Location = new System.Drawing.Point(262, 284);
            this.lblPercentage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(25, 15);
            this.lblPercentage.TabIndex = 58;
            this.lblPercentage.Text = "0%";
            // 
            // lblComplete
            // 
            this.lblComplete.AutoSize = true;
            this.lblComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblComplete.Location = new System.Drawing.Point(306, 284);
            this.lblComplete.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblComplete.Name = "lblComplete";
            this.lblComplete.Size = new System.Drawing.Size(60, 15);
            this.lblComplete.TabIndex = 57;
            this.lblComplete.Text = "Complete";
            // 
            // progBarBatch
            // 
            this.progBarBatch.Location = new System.Drawing.Point(255, 219);
            this.progBarBatch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progBarBatch.Name = "progBarBatch";
            this.progBarBatch.Size = new System.Drawing.Size(527, 48);
            this.progBarBatch.TabIndex = 56;
            this.progBarBatch.UseWaitCursor = true;
            // 
            // btnSelectBatchOutputFilesFolder
            // 
            this.btnSelectBatchOutputFilesFolder.Location = new System.Drawing.Point(24, 148);
            this.btnSelectBatchOutputFilesFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectBatchOutputFilesFolder.Name = "btnSelectBatchOutputFilesFolder";
            this.btnSelectBatchOutputFilesFolder.Size = new System.Drawing.Size(197, 48);
            this.btnSelectBatchOutputFilesFolder.TabIndex = 53;
            this.btnSelectBatchOutputFilesFolder.Text = "Select Output Folder...";
            this.btnSelectBatchOutputFilesFolder.UseVisualStyleBackColor = true;
            this.btnSelectBatchOutputFilesFolder.Click += new System.EventHandler(this.btnSelectBatchOutputFilesFolder_Click);
            // 
            // btnProcessFiles
            // 
            this.btnProcessFiles.Location = new System.Drawing.Point(24, 219);
            this.btnProcessFiles.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnProcessFiles.Name = "btnProcessFiles";
            this.btnProcessFiles.Size = new System.Drawing.Size(197, 48);
            this.btnProcessFiles.TabIndex = 52;
            this.btnProcessFiles.Text = "Process Input Files";
            this.btnProcessFiles.UseVisualStyleBackColor = true;
            this.btnProcessFiles.Click += new System.EventHandler(this.btnProcessBatchFiles_Click);
            // 
            // btnSelectBatchInputFilesFolder
            // 
            this.btnSelectBatchInputFilesFolder.Location = new System.Drawing.Point(24, 73);
            this.btnSelectBatchInputFilesFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSelectBatchInputFilesFolder.Name = "btnSelectBatchInputFilesFolder";
            this.btnSelectBatchInputFilesFolder.Size = new System.Drawing.Size(197, 48);
            this.btnSelectBatchInputFilesFolder.TabIndex = 51;
            this.btnSelectBatchInputFilesFolder.Text = "Select Input Files Folder...";
            this.btnSelectBatchInputFilesFolder.UseVisualStyleBackColor = true;
            this.btnSelectBatchInputFilesFolder.Click += new System.EventHandler(this.btnSelectBatchInputFilesFolder_Click);
            // 
            // btnCalcServiceVols
            // 
            this.btnCalcServiceVols.Location = new System.Drawing.Point(195, 37);
            this.btnCalcServiceVols.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCalcServiceVols.Name = "btnCalcServiceVols";
            this.btnCalcServiceVols.Size = new System.Drawing.Size(166, 45);
            this.btnCalcServiceVols.TabIndex = 5;
            this.btnCalcServiceVols.Text = "Calc Service Volumes";
            this.btnCalcServiceVols.UseVisualStyleBackColor = true;
            this.btnCalcServiceVols.Click += new System.EventHandler(this.btnCalcServiceVols_Click);
            // 
            // cboTestParameter
            // 
            this.cboTestParameter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTestParameter.FormattingEnabled = true;
            this.cboTestParameter.Items.AddRange(new object[] {
            "None",
            "g / C",
            "Pct. Heavy Vehicles",
            "Base Sat Flow",
            "Segment Length",
            "Posted Speed"});
            this.cboTestParameter.Location = new System.Drawing.Point(391, 40);
            this.cboTestParameter.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboTestParameter.Name = "cboTestParameter";
            this.cboTestParameter.Size = new System.Drawing.Size(140, 23);
            this.cboTestParameter.TabIndex = 6;
            this.cboTestParameter.SelectedValueChanged += new System.EventHandler(this.cboTestParameter_Changed);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnReadFileSerVols);
            this.groupBox2.Controls.Add(this.btnWriteSerVolInputs);
            this.groupBox2.Controls.Add(this.lblIteration);
            this.groupBox2.Controls.Add(this.lblUpperBound);
            this.groupBox2.Controls.Add(this.lblLowerBound);
            this.groupBox2.Controls.Add(this.lblStep);
            this.groupBox2.Controls.Add(this.progBarSerVols);
            this.groupBox2.Controls.Add(this.lblPctSerVols);
            this.groupBox2.Controls.Add(this.lblTestParm);
            this.groupBox2.Controls.Add(this.cboTestParameter);
            this.groupBox2.Controls.Add(this.btnCalcServiceVols);
            this.groupBox2.Location = new System.Drawing.Point(22, 268);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(688, 168);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Service Volume Calculations";
            // 
            // btnReadFileSerVols
            // 
            this.btnReadFileSerVols.Location = new System.Drawing.Point(22, 92);
            this.btnReadFileSerVols.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnReadFileSerVols.Name = "btnReadFileSerVols";
            this.btnReadFileSerVols.Size = new System.Drawing.Size(166, 45);
            this.btnReadFileSerVols.TabIndex = 8;
            this.btnReadFileSerVols.Text = "Read Service Volume Input Data";
            this.btnReadFileSerVols.UseVisualStyleBackColor = true;
            this.btnReadFileSerVols.Click += new System.EventHandler(this.buttonReadSerVols);
            // 
            // btnWriteSerVolInputs
            // 
            this.btnWriteSerVolInputs.Location = new System.Drawing.Point(22, 37);
            this.btnWriteSerVolInputs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnWriteSerVolInputs.Name = "btnWriteSerVolInputs";
            this.btnWriteSerVolInputs.Size = new System.Drawing.Size(166, 45);
            this.btnWriteSerVolInputs.TabIndex = 8;
            this.btnWriteSerVolInputs.Text = "Write Service Volume Input Data";
            this.btnWriteSerVolInputs.UseVisualStyleBackColor = true;
            this.btnWriteSerVolInputs.Click += new System.EventHandler(this.btnWriteSerVolInputs_Click);
            // 
            // lblIteration
            // 
            this.lblIteration.AutoSize = true;
            this.lblIteration.Location = new System.Drawing.Point(546, 96);
            this.lblIteration.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIteration.Name = "lblIteration";
            this.lblIteration.Size = new System.Drawing.Size(97, 15);
            this.lblIteration.TabIndex = 64;
            this.lblIteration.Text = "Current Iteration:";
            // 
            // lblUpperBound
            // 
            this.lblUpperBound.AutoSize = true;
            this.lblUpperBound.Location = new System.Drawing.Point(560, 37);
            this.lblUpperBound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUpperBound.Name = "lblUpperBound";
            this.lblUpperBound.Size = new System.Drawing.Size(80, 15);
            this.lblUpperBound.TabIndex = 63;
            this.lblUpperBound.Text = "Upper Bound:";
            // 
            // lblLowerBound
            // 
            this.lblLowerBound.AutoSize = true;
            this.lblLowerBound.Location = new System.Drawing.Point(560, 18);
            this.lblLowerBound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLowerBound.Name = "lblLowerBound";
            this.lblLowerBound.Size = new System.Drawing.Size(80, 15);
            this.lblLowerBound.TabIndex = 62;
            this.lblLowerBound.Text = "Lower Bound:";
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(608, 55);
            this.lblStep.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(33, 15);
            this.lblStep.TabIndex = 61;
            this.lblStep.Text = "Step:";
            // 
            // progBarSerVols
            // 
            this.progBarSerVols.Location = new System.Drawing.Point(227, 114);
            this.progBarSerVols.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progBarSerVols.Name = "progBarSerVols";
            this.progBarSerVols.Size = new System.Drawing.Size(418, 29);
            this.progBarSerVols.TabIndex = 60;
            // 
            // lblPctSerVols
            // 
            this.lblPctSerVols.AutoSize = true;
            this.lblPctSerVols.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPctSerVols.Location = new System.Drawing.Point(652, 120);
            this.lblPctSerVols.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPctSerVols.Name = "lblPctSerVols";
            this.lblPctSerVols.Size = new System.Drawing.Size(25, 15);
            this.lblPctSerVols.TabIndex = 59;
            this.lblPctSerVols.Text = "0%";
            // 
            // lblTestParm
            // 
            this.lblTestParm.AutoSize = true;
            this.lblTestParm.Location = new System.Drawing.Point(387, 18);
            this.lblTestParm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTestParm.Name = "lblTestParm";
            this.lblTestParm.Size = new System.Drawing.Size(84, 15);
            this.lblTestParm.TabIndex = 7;
            this.lblTestParm.Text = "Test Parameter";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnReadFile);
            this.groupBox3.Controls.Add(this.btnCalcResults);
            this.groupBox3.Controls.Add(this.btnCreateArterial);
            this.groupBox3.Controls.Add(this.btnWriteFile);
            this.groupBox3.Location = new System.Drawing.Point(22, 90);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(405, 168);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Arterial Level of Service";
            // 
            // txtOutputFilesFolder
            // 
            this.txtOutputFilesFolder.Location = new System.Drawing.Point(250, 33);
            this.txtOutputFilesFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOutputFilesFolder.Multiline = true;
            this.txtOutputFilesFolder.Name = "txtOutputFilesFolder";
            this.txtOutputFilesFolder.Size = new System.Drawing.Size(617, 34);
            this.txtOutputFilesFolder.TabIndex = 61;
            this.txtOutputFilesFolder.Validating += new System.ComponentModel.CancelEventHandler(this.txtOutputFilesFolder_Validating);
            this.txtOutputFilesFolder.Validated += new System.EventHandler(this.txtOutputFilesFolder_Validated);
            // 
            // btnOutputFilesFolder
            // 
            this.btnOutputFilesFolder.Location = new System.Drawing.Point(26, 33);
            this.btnOutputFilesFolder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOutputFilesFolder.Name = "btnOutputFilesFolder";
            this.btnOutputFilesFolder.Size = new System.Drawing.Size(197, 34);
            this.btnOutputFilesFolder.TabIndex = 60;
            this.btnOutputFilesFolder.Text = "Select Output Files Folder...";
            this.btnOutputFilesFolder.UseVisualStyleBackColor = true;
            this.btnOutputFilesFolder.Click += new System.EventHandler(this.btnOutputFilesFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(500, 15);
            this.label2.TabIndex = 62;
            this.label2.Text = "Use button to select folder for output files, or copy and paste folder path direc" +
    "tly into textbox.";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnCreateIntersection);
            this.groupBox4.Location = new System.Drawing.Point(456, 90);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(447, 168);
            this.groupBox4.TabIndex = 63;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Isolated Signal Level of Service";
            // 
            // btnCreateIntersection
            // 
            this.btnCreateIntersection.Location = new System.Drawing.Point(27, 33);
            this.btnCreateIntersection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCreateIntersection.Name = "btnCreateIntersection";
            this.btnCreateIntersection.Size = new System.Drawing.Size(146, 45);
            this.btnCreateIntersection.TabIndex = 4;
            this.btnCreateIntersection.Text = "Create Intersection";
            this.btnCreateIntersection.UseVisualStyleBackColor = true;
            this.btnCreateIntersection.Click += new System.EventHandler(this.btnCreateIntersection_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 785);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOutputFilesFolder);
            this.Controls.Add(this.btnOutputFilesFolder);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMain";
            this.Text = "HCM-CALC: Urban Streets Analysis Methodology (Ver: 4/1/21)";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnReadFile;
        private System.Windows.Forms.Button btnCalcResults;
        private System.Windows.Forms.Button btnWriteFile;
        private System.Windows.Forms.Button btnCreateArterial;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Label lblComplete;
        private System.Windows.Forms.ProgressBar progBarBatch;
        private System.Windows.Forms.Button btnSelectBatchOutputFilesFolder;
        private System.Windows.Forms.Button btnProcessFiles;
        private System.Windows.Forms.Button btnSelectBatchInputFilesFolder;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        private System.Windows.Forms.TextBox txtBatchOutputFilesFolder;
        private System.Windows.Forms.TextBox txtBatchInputFilesFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCalcServiceVols;
        private System.Windows.Forms.ComboBox cboTestParameter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTestParm;
        private System.Windows.Forms.ProgressBar progBarSerVols;
        private System.Windows.Forms.Label lblPctSerVols;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Label lblUpperBound;
        private System.Windows.Forms.Label lblLowerBound;
        private System.Windows.Forms.Label lblIteration;
        private System.Windows.Forms.Button btnWriteSerVolInputs;
        private System.Windows.Forms.Button btnReadFileSerVols;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtOutputFilesFolder;
        private System.Windows.Forms.Button btnOutputFilesFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnCreateIntersection;
    }
}

