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
            this.txtReportFilesFolder = new System.Windows.Forms.TextBox();
            this.txtInputFilesFolder = new System.Windows.Forms.TextBox();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.lblComplete = new System.Windows.Forms.Label();
            this.progBarBatch = new System.Windows.Forms.ProgressBar();
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.btnProcessFiles = new System.Windows.Forms.Button();
            this.btnSelectDataFolder = new System.Windows.Forms.Button();
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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadFile
            // 
            this.btnReadFile.Location = new System.Drawing.Point(53, 88);
            this.btnReadFile.Name = "btnReadFile";
            this.btnReadFile.Size = new System.Drawing.Size(142, 39);
            this.btnReadFile.TabIndex = 0;
            this.btnReadFile.Text = "Read File";
            this.btnReadFile.UseVisualStyleBackColor = true;
            this.btnReadFile.Click += new System.EventHandler(this.btnReadFile_Click);
            // 
            // btnCalcResults
            // 
            this.btnCalcResults.Location = new System.Drawing.Point(242, 22);
            this.btnCalcResults.Name = "btnCalcResults";
            this.btnCalcResults.Size = new System.Drawing.Size(142, 39);
            this.btnCalcResults.TabIndex = 1;
            this.btnCalcResults.Text = "Calculate LOS";
            this.btnCalcResults.UseVisualStyleBackColor = true;
            this.btnCalcResults.Click += new System.EventHandler(this.btnCalcResults_Click);
            // 
            // btnWriteFile
            // 
            this.btnWriteFile.Location = new System.Drawing.Point(242, 88);
            this.btnWriteFile.Name = "btnWriteFile";
            this.btnWriteFile.Size = new System.Drawing.Size(142, 39);
            this.btnWriteFile.TabIndex = 2;
            this.btnWriteFile.Text = "Write Results to File";
            this.btnWriteFile.UseVisualStyleBackColor = true;
            this.btnWriteFile.Click += new System.EventHandler(this.btnWriteFile_Click);
            // 
            // btnCreateArterial
            // 
            this.btnCreateArterial.Location = new System.Drawing.Point(54, 22);
            this.btnCreateArterial.Name = "btnCreateArterial";
            this.btnCreateArterial.Size = new System.Drawing.Size(142, 39);
            this.btnCreateArterial.TabIndex = 3;
            this.btnCreateArterial.Text = "Create Arterial";
            this.btnCreateArterial.UseVisualStyleBackColor = true;
            this.btnCreateArterial.Click += new System.EventHandler(this.btnCreateArterial_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtReportFilesFolder);
            this.groupBox1.Controls.Add(this.txtInputFilesFolder);
            this.groupBox1.Controls.Add(this.lblPercentage);
            this.groupBox1.Controls.Add(this.lblComplete);
            this.groupBox1.Controls.Add(this.progBarBatch);
            this.groupBox1.Controls.Add(this.btnSelectOutputFolder);
            this.groupBox1.Controls.Add(this.btnProcessFiles);
            this.groupBox1.Controls.Add(this.btnSelectDataFolder);
            this.groupBox1.Location = new System.Drawing.Point(12, 174);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(692, 277);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Batch Processing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(478, 13);
            this.label1.TabIndex = 61;
            this.label1.Text = "Use buttons to select input and output folder(s), or copy and paste folder path d" +
    "irectly into textboxes.";
            // 
            // txtReportFilesFolder
            // 
            this.txtReportFilesFolder.Location = new System.Drawing.Point(219, 128);
            this.txtReportFilesFolder.Multiline = true;
            this.txtReportFilesFolder.Name = "txtReportFilesFolder";
            this.txtReportFilesFolder.Size = new System.Drawing.Size(452, 42);
            this.txtReportFilesFolder.TabIndex = 60;
            this.txtReportFilesFolder.TextChanged += new System.EventHandler(this.txtReportFilesFolder_TextChanged);
            // 
            // txtInputFilesFolder
            // 
            this.txtInputFilesFolder.Location = new System.Drawing.Point(219, 63);
            this.txtInputFilesFolder.Multiline = true;
            this.txtInputFilesFolder.Name = "txtInputFilesFolder";
            this.txtInputFilesFolder.Size = new System.Drawing.Size(452, 42);
            this.txtInputFilesFolder.TabIndex = 59;
            this.txtInputFilesFolder.TextChanged += new System.EventHandler(this.txtInputFilesFolder_TextChanged);
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercentage.Location = new System.Drawing.Point(225, 246);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(25, 15);
            this.lblPercentage.TabIndex = 58;
            this.lblPercentage.Text = "0%";
            // 
            // lblComplete
            // 
            this.lblComplete.AutoSize = true;
            this.lblComplete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComplete.Location = new System.Drawing.Point(262, 246);
            this.lblComplete.Name = "lblComplete";
            this.lblComplete.Size = new System.Drawing.Size(60, 15);
            this.lblComplete.TabIndex = 57;
            this.lblComplete.Text = "Complete";
            // 
            // progBarBatch
            // 
            this.progBarBatch.Location = new System.Drawing.Point(219, 190);
            this.progBarBatch.Name = "progBarBatch";
            this.progBarBatch.Size = new System.Drawing.Size(452, 42);
            this.progBarBatch.TabIndex = 56;
            this.progBarBatch.UseWaitCursor = true;
            // 
            // btnSelectOutputFolder
            // 
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(21, 128);
            this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(169, 42);
            this.btnSelectOutputFolder.TabIndex = 53;
            this.btnSelectOutputFolder.Text = "Select Output Folder...";
            this.btnSelectOutputFolder.UseVisualStyleBackColor = true;
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);
            // 
            // btnProcessFiles
            // 
            this.btnProcessFiles.Location = new System.Drawing.Point(21, 190);
            this.btnProcessFiles.Name = "btnProcessFiles";
            this.btnProcessFiles.Size = new System.Drawing.Size(169, 42);
            this.btnProcessFiles.TabIndex = 52;
            this.btnProcessFiles.Text = "Process Input Files";
            this.btnProcessFiles.UseVisualStyleBackColor = true;
            this.btnProcessFiles.Click += new System.EventHandler(this.btnProcessBatchFiles_Click);
            // 
            // btnSelectDataFolder
            // 
            this.btnSelectDataFolder.Location = new System.Drawing.Point(21, 63);
            this.btnSelectDataFolder.Name = "btnSelectDataFolder";
            this.btnSelectDataFolder.Size = new System.Drawing.Size(169, 42);
            this.btnSelectDataFolder.TabIndex = 51;
            this.btnSelectDataFolder.Text = "Select Input Files Folder...";
            this.btnSelectDataFolder.UseVisualStyleBackColor = true;
            this.btnSelectDataFolder.Click += new System.EventHandler(this.btnSelectDataFolder_Click);
            // 
            // btnCalcServiceVols
            // 
            this.btnCalcServiceVols.Location = new System.Drawing.Point(167, 32);
            this.btnCalcServiceVols.Name = "btnCalcServiceVols";
            this.btnCalcServiceVols.Size = new System.Drawing.Size(142, 39);
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
            this.cboTestParameter.Location = new System.Drawing.Point(335, 35);
            this.cboTestParameter.Name = "cboTestParameter";
            this.cboTestParameter.Size = new System.Drawing.Size(121, 21);
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
            this.groupBox2.Location = new System.Drawing.Point(423, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 146);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Service Volume Calculations";
            // 
            // btnReadFileSerVols
            // 
            this.btnReadFileSerVols.Location = new System.Drawing.Point(19, 80);
            this.btnReadFileSerVols.Name = "btnReadFileSerVols";
            this.btnReadFileSerVols.Size = new System.Drawing.Size(142, 39);
            this.btnReadFileSerVols.TabIndex = 8;
            this.btnReadFileSerVols.Text = "Read Service Volume Input Data";
            this.btnReadFileSerVols.UseVisualStyleBackColor = true;
            this.btnReadFileSerVols.Click += new System.EventHandler(this.buttonReadSerVols);
            // 
            // btnWriteSerVolInputs
            // 
            this.btnWriteSerVolInputs.Location = new System.Drawing.Point(19, 32);
            this.btnWriteSerVolInputs.Name = "btnWriteSerVolInputs";
            this.btnWriteSerVolInputs.Size = new System.Drawing.Size(142, 39);
            this.btnWriteSerVolInputs.TabIndex = 8;
            this.btnWriteSerVolInputs.Text = "Write Service Volume Input Data";
            this.btnWriteSerVolInputs.UseVisualStyleBackColor = true;
            this.btnWriteSerVolInputs.Click += new System.EventHandler(this.btnWriteSerVolInputs_Click);
            // 
            // lblIteration
            // 
            this.lblIteration.AutoSize = true;
            this.lblIteration.Location = new System.Drawing.Point(468, 83);
            this.lblIteration.Name = "lblIteration";
            this.lblIteration.Size = new System.Drawing.Size(85, 13);
            this.lblIteration.TabIndex = 64;
            this.lblIteration.Text = "Current Iteration:";
            // 
            // lblUpperBound
            // 
            this.lblUpperBound.AutoSize = true;
            this.lblUpperBound.Location = new System.Drawing.Point(480, 32);
            this.lblUpperBound.Name = "lblUpperBound";
            this.lblUpperBound.Size = new System.Drawing.Size(73, 13);
            this.lblUpperBound.TabIndex = 63;
            this.lblUpperBound.Text = "Upper Bound:";
            // 
            // lblLowerBound
            // 
            this.lblLowerBound.AutoSize = true;
            this.lblLowerBound.Location = new System.Drawing.Point(480, 16);
            this.lblLowerBound.Name = "lblLowerBound";
            this.lblLowerBound.Size = new System.Drawing.Size(73, 13);
            this.lblLowerBound.TabIndex = 62;
            this.lblLowerBound.Text = "Lower Bound:";
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(521, 48);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(32, 13);
            this.lblStep.TabIndex = 61;
            this.lblStep.Text = "Step:";
            // 
            // progBarSerVols
            // 
            this.progBarSerVols.Location = new System.Drawing.Point(195, 99);
            this.progBarSerVols.Name = "progBarSerVols";
            this.progBarSerVols.Size = new System.Drawing.Size(358, 25);
            this.progBarSerVols.TabIndex = 60;
            // 
            // lblPctSerVols
            // 
            this.lblPctSerVols.AutoSize = true;
            this.lblPctSerVols.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPctSerVols.Location = new System.Drawing.Point(559, 104);
            this.lblPctSerVols.Name = "lblPctSerVols";
            this.lblPctSerVols.Size = new System.Drawing.Size(25, 15);
            this.lblPctSerVols.TabIndex = 59;
            this.lblPctSerVols.Text = "0%";
            // 
            // lblTestParm
            // 
            this.lblTestParm.AutoSize = true;
            this.lblTestParm.Location = new System.Drawing.Point(332, 16);
            this.lblTestParm.Name = "lblTestParm";
            this.lblTestParm.Size = new System.Drawing.Size(79, 13);
            this.lblTestParm.TabIndex = 7;
            this.lblTestParm.Text = "Test Parameter";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 460);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCreateArterial);
            this.Controls.Add(this.btnWriteFile);
            this.Controls.Add(this.btnCalcResults);
            this.Controls.Add(this.btnReadFile);
            this.Controls.Add(this.groupBox2);
            this.Name = "frmMain";
            this.Text = "HCM-CALC: Urban Streets Analysis Methodology";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.Button btnProcessFiles;
        private System.Windows.Forms.Button btnSelectDataFolder;
        private System.Windows.Forms.FolderBrowserDialog dlgFolderBrowser;
        private System.Windows.Forms.TextBox txtReportFilesFolder;
        private System.Windows.Forms.TextBox txtInputFilesFolder;
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
    }
}

