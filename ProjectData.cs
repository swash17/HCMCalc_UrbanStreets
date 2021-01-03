using System;
using System.Collections.Generic;



namespace HCMCalc_UrbanStreets
{
    /// <summary>
    /// Type of analysis mode for an arterial. "Operations" uses full calculation such as platoon dispersion while "Planning" uses simple calculations.
    /// </summary>
    public enum AnalysisMode
    {
        Operations,
        Planning,
        //HCM2016,
        //LOSPLAN
    }

    /// <summary>
    /// The type of mode for a project.
    /// </summary>
    public enum ModeType
    {
        AutoOnly,
        Multimodal,
        SignalOnly
    }

    /// <summary>
    /// The type of analysis direction for a project.
    /// </summary>
    public enum DirectionType
    {
        PeakDirection,
        BothDirections
    }

    /// <summary>
    /// The study period for a project.
    /// </summary>
    public enum StudyPeriod
    {
        StandardK,
        Kother,
        PeakHour
    }
    
    /// <summary>
    /// The type of FFS calculation method to use for a project.
    /// </summary>
    public enum FFSCalcMethod
    {
        HCM2010,
        PostSpeed
    }
    /// <summary>
    /// The type of LOSCcriteria method to use for a project.
    /// </summary>
    public enum LevelServiceCriteria
    {
        HCM2000,
        HCM2010,
        FDOT2ClassLength,
        FDOT2ClassSpeed
    }
    
    #region Project Data
    /// <summary>
    /// Parameters necessary for creating a project.
    /// </summary>
    public class ProjectData
    {
        /**** Fields ****/
        string _filePath;
        string _fileName;
        DateTime _analysisDate;
        string _analystName;
        string _agency;
        string _userNotes;
        AnalysisMode _analMode;
        DirectionType _directionAnalysisMode;
        ModeType _mode;
        StudyPeriod _period;
        FFSCalcMethod _ffsCalc;
        LevelServiceCriteria _losCriteria;


        /**** Constructors ****/

        /// <summary>
        /// Empty constructor required for XML de/serialization.
        /// </summary>
        public ProjectData()
        {

        }

        /// <summary>
        /// Constructor required for creating a project.
        /// </summary>
        /// <param name="analMode"></param>
        public ProjectData(AnalysisMode analMode)
        {
            //_filePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            _fileName = "";
            _analystName = "";
            _agency = "";
            _analysisDate = System.DateTime.Now;
            _userNotes = "";
            //_analMode = AnalysisMode.HCM2016;
            _analMode = analMode;
            _directionAnalysisMode = DirectionType.PeakDirection;
            //_mode = ModeType.Multimodal;
            _mode = ModeType.AutoOnly;
            _period = StudyPeriod.StandardK;
            _ffsCalc = FFSCalcMethod.PostSpeed;
            _losCriteria = LevelServiceCriteria.FDOT2ClassSpeed;
        }

        /**** Properties ****/
        public string FilePath { get => _filePath; set => _filePath = value; }
        public string FileName { get => _fileName; set => _fileName = value; }        
        public DateTime AnalysisDate { get => _analysisDate; set => _analysisDate = value; }
        public string AnalystName { get => _analystName; set => _analystName = value; }
        public string Agency { get => _agency; set => _agency = value; }
        public string UserNotes { get => _userNotes; set => _userNotes = value; }
        public AnalysisMode AnalMode { get => _analMode; set => _analMode = value; }
        public DirectionType DirectionAnalysisMode { get => _directionAnalysisMode; set => _directionAnalysisMode = value; }
        public ModeType Mode { get => _mode; set => _mode = value; }
        public StudyPeriod Period { get => _period; set => _period = value; }
        public FFSCalcMethod FFSCalc { get => _ffsCalc; set => _ffsCalc = value; }
        public LevelServiceCriteria LOScriteria { get => _losCriteria; set => _losCriteria = value; }
    }
    #endregion
}
