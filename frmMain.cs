using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;  //for file path methods
using System.ComponentModel;  //for Background Worker


namespace HCMCalc_UrbanStreets
{
    public partial class frmMain : Form
    {
        ProjectData Project;
        //ArterialData[] Art = new ArterialData[1];
        ArterialData Art;
        List<SerVolTablesByClass> SerVolTables = new List<SerVolTablesByClass>();

        FileInputOutput FileIO = new FileInputOutput();

        string InputFilesFolder = "";
        string OutputFilesFolder = "";

        BackgroundWorker bgwReadBatchFiles;
        BackgroundWorker bgwRunLOSanalysis;
        public static BackgroundWorker bgwRunServiceVolsCalcs;



        public frmMain()
        {
            InitializeComponent();
            //this.cboTestParameter.SelectedItem = "None";
            this.cboTestParameter.SelectedIndex = 0;
            CheckForIllegalCrossThreadCalls = false;
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            //from HCM-CALC
            /*
                string FileListingTitle = ProjectData.SelectFacilityNameString(Module) + " Files";
                string RegistryDirectory = SwashWare_FileManagement.RegistryPathsHCMcalc.SelectMostRecentlyUsedDirectoryRegistryPath((SwashWare_FileManagement.FacilityType)Module);
                string RegistryPathForStoredFilenames = SwashWare_FileManagement.RegistryPathsHCMcalc.SelectMostRecentlyUsedFilesRegistryPath((SwashWare_FileManagement.FacilityType)Module);
                string Filter = SwashWare_FileManagement.RegistryPathsHCMcalc.SelectFileExtensionFilter((SwashWare_FileManagement.FacilityType)Module);
                string InitialDirectory = System.Windows.Forms.Application.StartupPath;

                string Filename = SwashWare_FileManagement.Main.GetFilename(FileListingTitle, RegistryDirectory, RegistryPathForStoredFilenames, Filter, InitialDirectory);

                if (Filename != "")
                    ReadFileData(Filename);
            */

            //-------------------------------

            SwashWare_FileManagement.frmSelectFile SelectFile = new SwashWare_FileManagement.frmSelectFile("HCM-CALC Urban Streets Project Files", "Software\\SwashWare\\HCMCalc\\UrbanStreets", "Software\\SwashWare\\HCMCalc\\UrbanStreets\\MostRecentlyUsedFiles");
            SelectFile.ShowDialog();

            if (SelectFile.CancelWasPressed)
            {
                //FileOpenError = true;
                return;
            }
            
            if (SelectFile.SelectedMru.Length > 0)
            {
                if (!File.Exists(SelectFile.SelectedMru))
                {
                    DialogResult result = MessageBox.Show(SelectFile.SelectedMru + "\r\nCannot be found\r\n\r\nDo you want to remove this filename from the Most recently Used File list?", "File does not exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                        SelectFile.Mrus.DeleteFilePath(SelectFile.SelectedMru);
                    //FileOpenError = true;
                    return;
                }
                else
                {
                    SelectFile.Mrus.ReportFileBeingOpenedOrSavedOrClosed(SelectFile.SelectedMru);
                    LoadProject(SelectFile.SelectedMru);
                }
            }
            else
            {
                string Mrd = SwashWare_FileManagement.MostRecentlyUsedDirectory.GetMostRecentlyUsedDirectory("Software\\SwashWare\\HCMCalc\\UrbanStreets");
                string InitialDirectory;
                if (string.IsNullOrEmpty(Mrd))
                    InitialDirectory = System.Windows.Forms.Application.StartupPath;   // + "\\Example";
                else
                    InitialDirectory = Mrd;

                string Filename = GetFilenameFromOpenDialog(InitialDirectory);

                if (Filename != "No Filename")
                {
                    SwashWare_FileManagement.MostRecentlyUsedDirectory.SetMostRecentlyUsedDirectory(Path.GetDirectoryName(Filename));
                    SelectFile.Mrus.ReportFileBeingOpenedOrSavedOrClosed(Filename);
                    LoadProject(Filename);
                    Project.FileName = Filename;
                }
                else
                {
                    //give warning message
                    return;
                }
            }
            
            //tstFilename.Text = Inputs.Project.FileName;                       

            //Filename = @"X:\OneDrive\Software Projects\HCM-CALC\_DataFiles\HCM\Urban Streets\UrbanStreetsTest1.xus";
            //Filename = @"X:\OneDrive\Software Projects\HCM-CALC\_DataFiles\HCM\Urban Streets\Scenario1.xus";            
            //FileIO.ReadXmlFile(Filename, Project, Art, Ints, Segs);

            //Filename = @"X:\OneDrive\Software Projects\HCM-CALC\_DataFiles\HCM\Urban Streets\ArterialTest.xml";
            //Art = FileInputOutput2.DeserializeArterialData(Filename);
        }

        private string GetFilenameFromOpenDialog(string initialDirectory)
        {
            String Filename = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (Directory.Exists(initialDirectory))
                openFileDialog.InitialDirectory = initialDirectory;
            else
                openFileDialog.InitialDirectory = System.Windows.Forms.Application.StartupPath;

            //openFileDialog.InitialDirectory = initialDirectory;
            //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            
            //openFileDialog.Filter = "HCM-CALC Urban Streets Project Files (*.xus)|*.xus|All Files (*.*)|*.*";
            openFileDialog.Filter = "HCM-CALC Urban Streets Project Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                Filename = openFileDialog.FileName;
            }
            else
                Filename = "No Filename";

            return Filename;
        }

        private void LoadProject(string Filename)
        {
            Project = new ProjectData(AnalysisMode.Operations);
            Art = new ArterialData();

            Art = FileInputOutput2.DeserializeArterialData(Filename);
            Project.FileName = Filename;
        }

        private void LoadSerVolsProject(string Filename)
        {
            Project = new ProjectData(AnalysisMode.Operations);
            Art = new ArterialData();

            SerVolTables = FileInputOutput2.DeserializeSerVolTables(Filename);
            Project.FileName = Filename;
        }

        private void btnCalcResults_Click(object sender, EventArgs e)
        {
            CalcsArterial.CalcResults(Project, ref Art);
            MessageBox.Show("Analysis finished!");
        }

        private void btnWriteFile_Click(object sender, EventArgs e)
        {
            //Project.FileName = @"X:\OneDrive\Software Projects\HCM-CALC\_DataFiles\HCM\Urban Streets\ArterialTest.xml";
            //FileIO.WriteXmlFile(Filename, false, Project, Art, Ints, Segs, null, false, true);

            if (Project.FileName != "No Filename" && Project.FileName != "")
            {
                FileInputOutput2.SerializeArterialData(Project.FileName, Art);
            }
        }

        private void btnCreateArterial_Click(object sender, EventArgs e)
        {
            Project = new ProjectData();
            //ArterialData newArterial = CreateArterial.NewArterial();

            //Art = CreateArterial2.NewArterial();
            Art = CreateArterial_HCMExample1.NewArterial(Project.AnalMode, 100);

            //Change file path/name as appropriate
            //Project.FileName = @"X:\OneDrive\Software Projects\HCM-CALC\_DataFiles\HCM\Urban Streets\HCMExample1.xml";
            Project.FileName = @"C:\Users\Christian\source\repos\HCMCalc_UrbanStreets\bin\Output\HCMExample1.xml";
            FileInputOutput2.SerializeArterialData(Project.FileName, Art);
        }

        private void btnSelectDataFolder_Click(object sender, EventArgs e)
        {
            //lblStatus.Text = "";
            //this.dlgFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgFolderBrowser.RootFolder = Environment.SpecialFolder.Desktop;  // MyComputer;  'MyComputer' does not show network drives
            dlgFolderBrowser.ShowDialog();
            //SourceDir = @"C:\My Documents\Projects\NCHRP 3-87\CapacityDataProcessor\Toronto Data for PLM Pilot";
            InputFilesFolder = dlgFolderBrowser.SelectedPath;
            txtInputFilesFolder.Text = InputFilesFolder;

            //System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(InputFilesFolder);
            //int totalFiles = dir.GetFiles().Length;  
        }

        private void btnSelectOutputFolder_Click(object sender, EventArgs e)
        {
            this.dlgFolderBrowser.RootFolder = Environment.SpecialFolder.Desktop;
            dlgFolderBrowser.ShowDialog();
            //SourceDir = @"C:\My Documents\Projects\NCHRP 3-87\CapacityDataProcessor\Toronto Data for PLM Pilot";
            OutputFilesFolder = dlgFolderBrowser.SelectedPath;
            txtReportFilesFolder.Text = OutputFilesFolder;
        }

        private void txtInputFilesFolder_TextChanged(object sender, EventArgs e)
        {
            InputFilesFolder = txtInputFilesFolder.Text;
        }

        private void txtReportFilesFolder_TextChanged(object sender, EventArgs e)
        {
            OutputFilesFolder = txtReportFilesFolder.Text;
        }



        // Service volume calculations -------------------------------------------------------------------------------------

        private void btnCalcServiceVols_Click(object sender, EventArgs e)
        {
            bgwRunServiceVolsCalcs = new BackgroundWorker();
            bgwRunServiceVolsCalcs.DoWork += bgwRunServiceVolsCalcs_DoWork;
            bgwRunServiceVolsCalcs.RunWorkerCompleted += bgwRunServiceVolsCalcs_RunWorkerCompleted;
            bgwRunServiceVolsCalcs.ProgressChanged += bgwRunServiceVolsCalcs_ProgressChanged;
            bgwRunServiceVolsCalcs.WorkerReportsProgress = true;
            bgwRunServiceVolsCalcs.WorkerSupportsCancellation = true;

            if (!bgwRunServiceVolsCalcs.IsBusy)
                bgwRunServiceVolsCalcs.RunWorkerAsync();
            
        }

        /*private void bgwRunServiceVolsCalcs_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Project = new ProjectData(AnalysisMode.Planning);
            
            string testParameter = this.cboTestParameter.Text;
            float[] testParameterBoundaries = ServiceVolumeArterialInputs.GetTestParameterValues(testParameter);
            List<ServiceVolumes> serviceVolResults = new List<ServiceVolumes>();
            
            for (float iterateBoundary = testParameterBoundaries[0]; iterateBoundary <= testParameterBoundaries[1]; iterateBoundary += testParameterBoundaries[2])
            {
                ServiceVolumes SerVols = new ServiceVolumes(iterateBoundary, testParameter);
                ServiceVolumeArterialInputs InputsOneLane = new ServiceVolumeArterialInputs(false, testParameter, iterateBoundary);
                ServiceVolumeArterialInputs InputsMultiLane = new ServiceVolumeArterialInputs(true, testParameter, iterateBoundary);
                this.lblIteration.Text = "Iteration: " + Math.Round(iterateBoundary,2).ToString();
                CalcsServiceVolumes.ServiceVolsAuto(Project, SerVols, InputsOneLane, InputsMultiLane);
                serviceVolResults.Add(SerVols);
            }
            //e.Cancel = true;
            ServiceVolResultsOutput.WriteServiceVolResultsData("Service Volume Outputs.csv", serviceVolResults);

            //ServiceVolumeArterialInputs InputsOneLane = new ServiceVolumeArterialInputs(false, TestParameter, 0);
            //ServiceVolumeArterialInputs InputsMultiLane = new ServiceVolumeArterialInputs(true, TestParameter, 0);
            //CalcsServiceVolumes.ServiceVolsAuto(Project, SerVols, InputsOneLane, InputsMultiLane);
        }*/

        private void bgwRunServiceVolsCalcs_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (SerVolTables.Count == 0)
            {
                MessageBox.Show("Please read in a Service Volumes Input File first.");
            }
            else
            {
                Project = new ProjectData(AnalysisMode.Planning);

                string testParameter = this.cboTestParameter.Text;
                float[] testParameterBoundaries = ServiceVolumeArterialInputs.GetTestParameterValues(testParameter);
                int numberOfTestParmsPerTable = (int)(((testParameterBoundaries[1] - testParameterBoundaries[0]) / testParameterBoundaries[2]) + 1);
                List<ServiceVolumes> serviceVolResults = new List<ServiceVolumes>();

                foreach (SerVolTablesByClass SerVolTablesForClasses in SerVolTables)
                {
                    foreach (SerVolTablesByMultiLane SerVolTablesForNumLanes in SerVolTablesForClasses.SerVolTablesMultiLane)
                    {
                        for (float iterateBoundary = testParameterBoundaries[0]; iterateBoundary <= testParameterBoundaries[1]; iterateBoundary += testParameterBoundaries[2])
                        {
                            ServiceVolumes SerVols = new ServiceVolumes(iterateBoundary, testParameter, SerVolTablesForClasses.ArtAreaType, SerVolTablesForNumLanes.ArtClass);

                            SerVolTablesForNumLanes.SerVolTables[0] = ServiceVolumeTableFDOT.ChangeTestParameterValue(SerVolTablesForNumLanes.SerVolTables[0], testParameter, iterateBoundary);
                            SerVolTablesForNumLanes.SerVolTables[1] = ServiceVolumeTableFDOT.ChangeTestParameterValue(SerVolTablesForNumLanes.SerVolTables[1], testParameter, iterateBoundary);
                            this.lblIteration.Text = "Iteration: " + Math.Round(iterateBoundary, 2).ToString();
                            CalcsServiceVolumes.ServiceVolsAutoNew(Project, SerVols, SerVolTablesForNumLanes.SerVolTables[0], SerVolTablesForNumLanes.SerVolTables[1]);
                            serviceVolResults.Add(SerVols);
                        }
                    }
                }

                ServiceVolResultsOutput.WriteServiceVolResultsDataNew("Service Volume Outputs.csv", serviceVolResults, numberOfTestParmsPerTable);
                MessageBox.Show("Calculations Finished! Please check out the \"Service Volume Outputs.csv\" file.");
            }
        }

        private void bgwRunServiceVolsCalcs_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progBarSerVols.Value = e.ProgressPercentage; 
            this.lblPctSerVols.Text = e.ProgressPercentage.ToString("0") + "%";
        }

        private void bgwRunServiceVolsCalcs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //update UI once background worker has completed work
            //frmProgress.SetStatusLabelBefore = "Finished";

            //BackgroundWorkerWriteFiles.Dispose();
            bgwRunServiceVolsCalcs = null;
        }
        

        

        // Batch file processing -------------------------------------------------------------------------------------

        private void btnProcessBatchFiles_Click(object sender, EventArgs e)
        {
            if (!bgwReadBatchFiles.IsBusy)
                bgwReadBatchFiles.RunWorkerAsync();
        }

        private void backgroundWorkerReadBatchFiles_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ProcessBatchFiles();  // (backgroundWorkerReadFiles);
        }

        private void ProcessBatchFiles()  //(BackgroundWorker backWorker)
        {
            if (InputFilesFolder != "")
            {
                string FilenameExtension = "";
                string[] fileEntries = Directory.GetFiles(InputFilesFolder);  // "*.*", SearchOption.AllDirectories);
                int totalFiles = 0; // = fileEntries.Length;
                int processingFile = 0;

                foreach (string fileName in fileEntries)
                {
                    FilenameExtension = Path.GetExtension(fileName);
                    if (FilenameExtension == ".xml")
                    {
                        totalFiles++;
                    }
                }

                foreach (string fileName in fileEntries)
                {
                    FilenameExtension = Path.GetExtension(fileName);
                    if (FilenameExtension == ".xml")
                    {
                        LoadProject(fileName);
                        CalcsArterial.CalcResults(Project, ref Art);
                        FileInputOutput2.SerializeArterialData(Project.FileName, Art);

                        processingFile++;
                        int PctProgress = (int)((Single)processingFile / totalFiles * 100);
                        bgwReadBatchFiles.ReportProgress(PctProgress);
                    }
                }
            }
            else
            {
                MessageBox.Show("The input and/or output source path is empty.", "Folder Path Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void backgroundWorkerReadFiles_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progBarBatch.Value = e.ProgressPercentage;
            this.lblPercentage.Text = e.ProgressPercentage.ToString("0") + "%";
        }

        private void backgroundWorkerReadFiles_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //update UI once background worker has completed work
            //frmProgress.SetStatusLabelBefore = "Finished";

            //BackgroundWorkerWriteFiles.Dispose();
            bgwReadBatchFiles = null;
        }

        private void cboTestParameter_Changed(object sender, EventArgs e)
        {
            string testParameter = this.cboTestParameter.Text;
            float[] testParameterBoundaries = ServiceVolumeArterialInputs.GetTestParameterValues(testParameter);
            this.lblLowerBound.Text = "Lower Bound: " + testParameterBoundaries[0].ToString();
            this.lblUpperBound.Text = "Upper Bound: " + testParameterBoundaries[1].ToString();
            this.lblStep.Text = "Step: " + testParameterBoundaries[2].ToString();
        }

        private void btnWriteSerVolInputs_Click(object sender, EventArgs e)
        {
            CalcsServiceVolumes.WriteFDOTServiceVolInputFile();
        }

        private void buttonReadSerVols(object sender, EventArgs e)
        {
            SwashWare_FileManagement.frmSelectFile SelectFile = new SwashWare_FileManagement.frmSelectFile("HCM-CALC Urban Streets Service Volume Input File", "Software\\SwashWare\\HCMCalc\\UrbanStreets", "Software\\SwashWare\\HCMCalc\\UrbanStreets\\MostRecentlyUsedFiles");
            SelectFile.ShowDialog();

            if (SelectFile.CancelWasPressed)
                return;

            if (SelectFile.SelectedMru.Length > 0)
            {
                if (!File.Exists(SelectFile.SelectedMru))
                {
                    DialogResult result = MessageBox.Show(SelectFile.SelectedMru + "\r\nCannot be found\r\n\r\nDo you want to remove this filename from the Most recently Used File list?", "File does not exist", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                    if (result == DialogResult.Yes)
                        SelectFile.Mrus.DeleteFilePath(SelectFile.SelectedMru);

                    return;
                }
                else
                {
                    SelectFile.Mrus.ReportFileBeingOpenedOrSavedOrClosed(SelectFile.SelectedMru);
                    LoadSerVolsProject(SelectFile.SelectedMru);
                }
            }
            else
            {
                string Mrd = SwashWare_FileManagement.MostRecentlyUsedDirectory.GetMostRecentlyUsedDirectory("Software\\SwashWare\\HCMCalc\\UrbanStreets");
                string InitialDirectory;
                if (string.IsNullOrEmpty(Mrd))
                    InitialDirectory = System.Windows.Forms.Application.StartupPath;   // + "\\Example";
                else
                    InitialDirectory = Mrd;

                string Filename = GetFilenameFromOpenDialog(InitialDirectory);

                if (Filename != "No Filename")
                {
                    SwashWare_FileManagement.MostRecentlyUsedDirectory.SetMostRecentlyUsedDirectory(Path.GetDirectoryName(Filename));
                    SelectFile.Mrus.ReportFileBeingOpenedOrSavedOrClosed(Filename);
                    LoadSerVolsProject(Filename);
                    Project.FileName = Filename;
                }
                else
                {
                    //give warning message
                    return;
                }
            }
        }
    }
}
