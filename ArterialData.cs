using System;
using System.Collections.Generic;



namespace HCMCalc_UrbanStreets
{
    /// <summary>
    /// The area type for an Arterial.
    /// </summary>
    public enum AreaType
    {
        /// <summary>
        /// Area with a population &gt; 1 million.
        /// </summary>
        LargeUrbanized = 0,

        /// <summary>
        /// Area with a population &gt; 50,000 and &lt; 1 million.
        /// </summary>
        OtherUrbanized = 1,

        /// <summary>
        /// Area with a population &gt; 5,000 and &lt; 50,000.
        /// </summary>
        Transitioning = 2,

        /// <summary>
        /// Area with a population &lt; 5,000.
        /// </summary>
        RuralDeveloped = 3

        // Future Area Types:
        // Rural
        // Rural Town
        // Suburban
        // Urban
        // Urban Core
    }

    /// <summary>
    /// The travel direction for multiple objects; Index 0 is North and then iterates clockwise.
    /// </summary>
    public enum TravelDirection
    {
        /*, Southbound = 0
        Westbound = 1,
        Northbound = 2,        
        Eastbound = 3*/

        // Clockwise of compass, starting at 12' (N)
        Northbound = 0,
        Eastbound = 1,
        Southbound = 2,
        Westbound = 3
    }

    public enum TerrainType
    {
        Level = 0,
        Rolling = 1
    }


    /// <summary>
    /// Handles the parameters for an arterial.
    /// </summary>
    public class ArterialData
    {
        //public static float RTadj = 0.0;        
        string _artName;              //arterial name
        string _from;
        string _to;
        TravelDirection _dir;         //direction label (e.g., northbound)
        AreaType _area;
        ArterialClass _class;
        float _Kfactor;
        float _Dfactor;        
        int _maxSerVol;
        int _totalInts;
        int _totalSegs;
        int _testSerVol;
        ThresholdData _thresholds;
        float _lengthMiles;     //total length of arterial, in miles
        bool _overCapacity;
        List<SegmentData> _segments;
        ResultsArterialData _results;


        /**** Constructors ****/

        /// <summary>
        /// Parameterless constructor needed for XML de/serialization. Do not use.
        /// </summary>
        public ArterialData()
        {
            //Parameterless constructor needed for XML de/serialization
        }

        /// <summary>
        /// Constructor for creating an Arterial without using Service Volumes.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="classification"></param>
        /// <param name="analysisTravelDir"></param>
        /// <param name="segments"></param>
        public ArterialData(AreaType area, ArterialClass classification, TravelDirection analysisTravelDir, List<SegmentData> segments)
        {
            //SetDefaultValues();

            //_segments = new List<SegmentData>();
            
            _artName = "";
            _from = "";
            _to = "";
            _lengthMiles = 0;
            _dir = analysisTravelDir;
            _area = area;
            _class = classification;
            _segments = segments;
            _Dfactor = 0.5f;
            _Kfactor = 0.1f;
            _thresholds = new ThresholdData();
            _results = new ResultsArterialData();

            //_class = ParmRanges.ArtClassDefault[Convert.ToInt32(_area)];
            //_Kfactor = ParmRanges.KfactDefault[Convert.ToInt32(_area)];
            //_Dfactor = ParmRanges.DfactDefault[Convert.ToInt32(_area)];            
            //_maxSerVol = ParmRanges.MaxSerVol[Convert.ToInt32(_area)];  //for other urbanized
        }

        /// <summary>
        /// Constructor for creating an Arterial for use with Service Volumes.
        /// </summary>
        /// <param name="area"></param>
        /// <param name="classification"></param>
        /// <param name="analysisTravelDir"></param>
        /// <param name="segments"></param>
        /// <param name="KFactor"></param>
        /// <param name="DFactor"></param>
        public ArterialData(AreaType area, ArterialClass classification, TravelDirection analysisTravelDir, List<SegmentData> segments, float KFactor, float DFactor)
        {
            //SetDefaultValues();

            //_segments = new List<SegmentData>();

            _artName = "";
            _from = "";
            _to = "";
            _lengthMiles = 0;
            _dir = analysisTravelDir;
            _area = area;
            _class = classification;
            _segments = segments;
            _Dfactor = DFactor;
            _Kfactor = KFactor;
            _thresholds = new ThresholdData();
            _results = new ResultsArterialData();
        }

        public ArterialData(ServiceVolumeTableFDOT Inputs, List<SegmentData> segments)
        {
            _artName = "";
            _from = "";
            _to = "";
            _lengthMiles = Inputs.Roadway.FacilityLengthMiles;
            _dir = Inputs.Roadway.AnalysisTravelDir;
            _area = Inputs.SerVolAreaType;
            _class = Inputs.Class;
            _segments = segments;
            _Dfactor = Inputs.Traffic.DFactor;
            _Kfactor = Inputs.Traffic.KFactor;
            _thresholds = new ThresholdData();
            _results = new ResultsArterialData();
        }

        /**** Properties ****/

        public int TotalInts { get => _totalInts; set => _totalInts = value; }
        public int TotalSegs { get => _totalSegs; set => _totalSegs = value; }
        public string ArtName { get => _artName; set => _artName = value; }
        public string From { get => _from; set => _from = value; }
        public string To { get => _to; set => _to = value; }
        public float LengthMiles { get => _lengthMiles; set => _lengthMiles = value; }
        public TravelDirection AnalysisTravelDir { get => _dir; set => _dir = value; }
        public AreaType Area { get => _area; set => _area = value; }
        public ArterialClass Classification { get => _class; set => _class = value; }
        public float Kfactor { get => _Kfactor; set => _Kfactor = value; }
        public float Dfactor { get => _Dfactor; set => _Dfactor = value; }
        public int MaxSerVol { get => _maxSerVol; set => _maxSerVol = value; }
        public ResultsArterialData Results { get => _results; set => _results = value; }
        public List<SegmentData> Segments { get => _segments; set => _segments = value; }
        public bool OverCapacity { get => _overCapacity; set => _overCapacity = value; }
        public int TestSerVol { get => _testSerVol; set => _testSerVol = value; }
        internal ThresholdData Thresholds { get => _thresholds; set => _thresholds = value; }
    }
}
