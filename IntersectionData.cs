using System.Xml.Serialization;
using System.Collections.Generic;



namespace HCMCalc_UrbanStreets
{
    /// <summary>
    /// Parameters for intersection results.
    /// </summary>
    public class ResultsIntersectionData
    {
        float _vcRatio;
        float _ctrlDelay;
        string _los;

        /// <summary>
        /// Constructor for creating intersection results.
        /// </summary>
        public ResultsIntersectionData()
        {

        }

        public float vcRatio { get => _vcRatio; set => _vcRatio = value; }
        public float ControlDelay { get => _ctrlDelay; set => _ctrlDelay = value; }
        public string LOS { get => _los; set => _los = value; }
    }

    /// <summary>
    /// Parameters for approach results.
    /// </summary>
    public class ResultsIntersectionApproachData
    {
        float _vcRatio;
        float _ctrlDelay;
        string _los;
        static int _analysisPeriodTime = 300;                               // Maximum number of steps per cycle
        float[,] _arrivalFlowProfile;                                       // 1st sub; 1 = UpLeft, 2 = UpThru, 3 = UpRight, 4 = Mid-Block Entry

        /// <summary>
        /// Empty constructor that assumes cycle length is equal to an analysis period.
        /// </summary>
        public ResultsIntersectionApproachData()
        {
            _arrivalFlowProfile = new float[4, _analysisPeriodTime];
        }
        /// <summary>
        /// Main constructor for creating approach results.
        /// </summary>
        /// <param name="cycleLength"></param>
        public ResultsIntersectionApproachData(int cycleLength)
        {
            _arrivalFlowProfile = new float[4, cycleLength];
        }

        public float vcRatio { get => _vcRatio; set => _vcRatio = value; }
        public float ControlDelay { get => _ctrlDelay; set => _ctrlDelay = value; }
        public string LOS { get => _los; set => _los = value; }
        public static int AnalysisPeriodTime { get => _analysisPeriodTime; set => _analysisPeriodTime = value; }
        [XmlIgnore]
        public float[,] ArrivalFlowProfile { get => _arrivalFlowProfile; set => _arrivalFlowProfile = value; }
    }

    /// <summary>
    /// Parameters for lane group results.
    /// </summary>
    public class ResultsIntersectionLaneGroupData
    {
        SaturationFlowRateValues _satFlowRate;
        SignalControlParameters _sigControl;
        float _capacityPerLane;
        float _vcRatio;
        bool _overCap;
        float _queStorageRatio;
        string _los;
        float _dischargeVolume;

        /// <summary>
        /// Empty constructor for creating lane group resoults.
        /// </summary>
        public ResultsIntersectionLaneGroupData()
        {

        }

        public SaturationFlowRateValues SatFlowRate { get => _satFlowRate; set => _satFlowRate = value; }
        public SignalControlParameters SignalControlParms { get => _sigControl; set => _sigControl = value; }
        public float CapacityPerLane { get => _capacityPerLane; set => _capacityPerLane = value; }
        public float vcRatio { get => _vcRatio; set => _vcRatio = value; }
        public bool OverCap { get => _overCap; set => _overCap = value; }
        public float QueStorageRatio { get => _queStorageRatio; set => _queStorageRatio = value; }
        public string LOS { get => _los; set => _los = value; }
        public float DischargeVolume { get => _dischargeVolume; set => _dischargeVolume = value; }
    }

    /// <summary>
    /// NEMA movement numbers for each approach plus movement combination.
    /// </summary>
    public enum NemaMovementNumbers
    {
        EBLeft = 5,
        EBThru = 2,
        EBRight = 12,
        WBLeft = 1,
        WBThru = 6,
        WBRight = 16,
        NBLeft = 3,
        NBThru = 8,
        NBRight = 18,
        SBLeft = 7,
        SBThru = 4,
        SBRight = 14
    }

    /// <summary>
    /// Allowed movements for a lane.
    /// </summary>
    public enum LaneMovementsAllowed
    {
        LeftOnly,
        ThruOnly,
        RightOnly,
        ThruLeftShared,
        ThruRightShared,
        ThruAndLeftTurnBay,
        ThruAndRightTurnBay,
        LeftRightShared,
        All
    }

    /// <summary>
    /// Parameters relevant to a lane.
    /// </summary>
    [XmlRoot(ElementName = "Lane")]
    public class LaneData
    {
        int _id;  //outermost lane is ID 1, innermost lane ID = number of lanes on approach
        LaneMovementsAllowed _allowedMovements;

        public LaneData()
        {
            //Parameterless constructor needed for XML de/serialization
        }
        
        public LaneData(int id, LaneMovementsAllowed allowedMvmts)
        {
            _id = id;
            _allowedMovements = allowedMvmts;
        }

        [XmlAttribute("ID")]
        public int Id { get => _id; set => _id = value; }
        public LaneMovementsAllowed AllowedMovements { get => _allowedMovements; set => _allowedMovements = value; }
    }

    /// <summary>
    /// Parameters for creation of a lane group.
    /// </summary>
    [XmlRoot(ElementName = "LaneGroup")]
    public class LaneGroupData
    {
        int _id;
        string _label;
        TravelDirection _travelDir;
        LaneMovementsAllowed _type;
        NemaMovementNumbers _nemaMvmtID;
        List<LaneData> _lanes;
        int _numLanes;
        int _numLeftLanes;
        int _turnBayLeftLengthFeet;
        int _turnBayRightLengthFeet;
        float _pctLT;
        float _pctRT;
        float _pctTurnsExcLane;
        float _demandVolumeVehPerHr;
        float _peakHourFactor;
        float _pctHeavyVehicles;
        float _analysisFlowRate;
        float _initialQue;
        float _platoonRatio;
        float _laneFilterFactor;
        float _dischargeVolume;
        float _portionOnGreen; //proportion of vehicles that arrive on green    
        float _portionOnGreenOfSharedLane; //proportion of vehicles that arrive on green  
        float _detectorLength;
        float[] _demandVolumeAdjByMovement;
        int _arvType;
        SignalPhaseData _signalPhase;  //circular reference, as SignalPhaseData contains a field for LaneGroupData
        [XmlIgnore]
        int _baseSatFlow = 1950;



        ResultsIntersectionLaneGroupData _results;
        /// <summary>
        /// Parameterless constructor needed for XML de/serialization. Do not use
        /// </summary>
        public LaneGroupData()
        {
            //Parameterless constructor needed for XML de/serialization

            _nemaMvmtID = NemaMovementNumbers.EBThru;  //SSW: Temporary, otherwise serialization fails due to LaneGroupData instantiation in TimerData constructor
        }

        /// <summary>
        /// Creates a lane group for an approach.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="label"></param>
        /// <param name="type"></param>
        /// <param name="nemaMvmtId"></param>
        /// <param name="travDir"></param>
        /// <param name="lanes"></param>
        /// <param name="signalPhasing"></param>
        /// <param name="pctLeftTurns"></param>
        /// <param name="pctRightTurns"></param>
        /// <param name="arvType"></param>
        public LaneGroupData(int id, string label, LaneMovementsAllowed type, NemaMovementNumbers nemaMvmtId, TravelDirection travDir, List<LaneData> lanes, SignalPhaseData signalPhasing, float pctLeftTurns = 0, float pctRightTurns = 0, int arvType = 3)
        {
            signalPhasing.AssociatedLaneGroup = this;

            _id = id;
            _label = label;
            _type = type;
            _nemaMvmtID = nemaMvmtId;
            _travelDir = travDir;
            _peakHourFactor = 1.0f;  
            _lanes = lanes;
            _numLanes = lanes.Count;
            _signalPhase = signalPhasing;
            _arvType = arvType;
            _pctLT = pctLeftTurns;
            _pctRT = pctRightTurns;
            float[] PlatoonRatioValues = new float[] { 0.333f, 0.667f, 1.0f, 1.333f, 1.667f, 2.0f };
            _platoonRatio = PlatoonRatioValues[arvType - 1];
            _results = new ResultsIntersectionLaneGroupData();
            _demandVolumeAdjByMovement = new float[3];

            //_peakHourFactor = ParmRanges.PHFDefault[Convert.ToInt32(_area)];
            //_pctHeavyVeh = ParmRanges.PctHVDefault[Convert.ToInt32(_area)];
        }

        [XmlAttribute("TYPE")]
        public LaneMovementsAllowed Type { get => _type; set => _type = value; }

        //[XmlAttribute("ID")]
        public int Id { get => _id; set => _id = value; }
        public string Label { get => _label; set => _label = value; }
        public TravelDirection TravelDir { get => _travelDir; set => _travelDir = value; }
        public float DemandVolumeVehPerHr { get => _demandVolumeVehPerHr; set => _demandVolumeVehPerHr = value; }
        public float PeakHourFactor { get => _peakHourFactor; set => _peakHourFactor = value; }
        public float AnalysisFlowRate { get => _analysisFlowRate; set => _analysisFlowRate = value; }
        public float PctHeavyVehicles { get => _pctHeavyVehicles; set => _pctHeavyVehicles = value; }
        public List<LaneData> Lanes { get => _lanes; set => _lanes = value; }
        public int NumLanes { get => _numLanes; set => _numLanes = value; }
        public int TurnBayLeftLengthFeet { get => _turnBayLeftLengthFeet; set => _turnBayLeftLengthFeet = value; }
        public int TurnBayRightLengthFeet { get => _turnBayRightLengthFeet; set => _turnBayRightLengthFeet = value; }
        public float PctLeftTurns { get => _pctLT; set => _pctLT = value; }
        public float PctRightTurns { get => _pctRT; set => _pctRT = value; }
        public int ArvType { get => _arvType; set => _arvType = value; }
        public SignalPhaseData SignalPhase { get => _signalPhase; set => _signalPhase = value; }
        public ResultsIntersectionLaneGroupData AnalysisResults { get => _results; set => _results = value; }
        public float InitialQue { get => _initialQue; set => _initialQue = value; }
        public float PlatoonRatio { get => _platoonRatio; set => _platoonRatio = value; }
        public float PortionOnGreen { get => _portionOnGreen; set => _portionOnGreen = value; }
        public float LaneFilterFactor { get => _laneFilterFactor; set => _laneFilterFactor = value; }
        public float DischargeVolume { get => _dischargeVolume; set => _dischargeVolume = value; }
        public int NumLeftLanes { get => _numLeftLanes; set => _numLeftLanes = value; }
        public NemaMovementNumbers NemaMvmtID { get => _nemaMvmtID; set => _nemaMvmtID = value; }
        public float PortionOnGreenOfSharedLane { get => _portionOnGreenOfSharedLane; set => _portionOnGreenOfSharedLane = value; }
        public float PctTurnsExcLane { get => _pctTurnsExcLane; set => _pctTurnsExcLane = value; }
        public int BaseSatFlow { get => _baseSatFlow; set => _baseSatFlow = value; }
        public float DetectorLength { get => _detectorLength; set => _detectorLength = value; }
        public float[] DemandVolumeVehPerHrSplit { get => _demandVolumeAdjByMovement; set => _demandVolumeAdjByMovement = value; }

        public float CalcAnalysisFlowRate(float demandVolume, float peakHourFactor)
        {
            return demandVolume * peakHourFactor;
        }
    }

    /// <summary>
    /// Parameters required for creating an approach.
    /// </summary>
    [XmlRoot(ElementName = "Approach")]
    public class ApproachData
    {
        TravelDirection _dir;
        int _id;
        string _label;
        float _demandVolume;
        float _pctLT;
        float _pctRT;
        float _pctGrade;
        bool _leftTurnBayExists;
        bool _rightTurnBayExists;
        List<LaneGroupData> _laneGroups;
        ResultsIntersectionApproachData _results;

        /// <summary>
        /// Parameterless constructor needed for XML de/serialization. Do not use.
        /// </summary>
        public ApproachData()
        {
            //Parameterless constructor needed for XML de/serialization
        }

        /// <summary>
        /// Constructor for creating an approach.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dir"></param>
        /// <param name="label"></param>
        /// <param name="pctGrade"></param>
        /// <param name="laneGroups"></param>
        public ApproachData(int id, TravelDirection dir, string label, float pctGrade, List<LaneGroupData> laneGroups)
        {
            //needs = demandDelayVehPerHr, demandStops
            _id = id;
            _dir = dir;
            _label = label;
            _pctGrade = pctGrade;
            _laneGroups = laneGroups;
        }

        [XmlAttribute("DIR")]
        public TravelDirection Dir { get => _dir; set => _dir = value; }
        public int Id { get => _id; set => _id = value; }
        public string Label { get => _label; set => _label = value; }
        public float DemandVolume { get => _demandVolume; set => _demandVolume = value; }
        public float PctLeftTurns { get => _pctLT; set => _pctLT = value; }
        public float PctRightTurns { get => _pctRT; set => _pctRT = value; }
        public float PctGrade { get => _pctGrade; set => _pctGrade = value; }
        public List<LaneGroupData> LaneGroups { get => _laneGroups; set => _laneGroups = value; }
        public ResultsIntersectionApproachData Results { get => _results; set => _results = value; }
        public bool LeftTurnBayExists { get => _leftTurnBayExists; set => _leftTurnBayExists = value; }
        public bool RightTurnBayExists { get => _rightTurnBayExists; set => _rightTurnBayExists = value; }
    }

    /// <summary>
    /// Parameters required for making an intersection.
    /// </summary>
    [XmlRoot(ElementName = "Intersection")]
    public class IntersectionData
    {
        /**** Fields ****/
        //Input Values
        int _id;
        string _label;
        string _crossStreetName;
        SignalCycleData _signal;
        int _crossStreetWidth;
        //float _numThLanes;  //for ARTPLAN        
        float _demandVolume;
        float _speedLimit;
        float _detectorLength;
        float _laneGradeHVAreaFactors;  //fw x fg x fHV x fa
        ResultsIntersectionData _results;
        List<ApproachData> _approaches;


        /**** Constructors ****/

        /// <summary>
        /// Parameterless constructor needed for XML de/serialization.
        /// </summary>
        public IntersectionData()
        {
            //Parameterless constructor needed for XML de/serialization
        }

        /// <summary>
        /// Constructor for creating an intersection.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="area"></param>
        /// <param name="artClass"></param>
        /// <param name="approaches"></param>
        /// <param name="signalData"></param>
        public IntersectionData(int id, AreaType area, ArterialClass artClass, List<ApproachData> approaches, SignalCycleData signalData, AnalysisMode ProjAnalMode = AnalysisMode.Planning, int crossStreetWidth = 0)   //create segment with default parameter values
        {
            _id = id;
            _crossStreetName = "";
            _signal = signalData;

            _approaches = approaches;

            if (ProjAnalMode == AnalysisMode.Planning)
            {
                if (area == AreaType.LargeUrbanized)
                    _crossStreetWidth = 60;
                if (area == AreaType.OtherUrbanized)
                    _crossStreetWidth = 60;
                if (area == AreaType.Transitioning)
                    _crossStreetWidth = 36;
                if (area == AreaType.RuralDeveloped)
                    _crossStreetWidth = 24;
            }
            else
                _crossStreetWidth = crossStreetWidth; // CNB: Have it set explicitly

            //SetDefaultValues(area, artClass, sigControl);
        }


        /**** Properties ****/
        [XmlAttribute("ID")]
        public int Id { get => _id; set => _id = value; }
        public string Label { get => _label; set => _label = value; }
        public string CrossStreetName { get => _crossStreetName; set => _crossStreetName = value; }
        public int CrossStreetWidth { get => _crossStreetWidth; set => _crossStreetWidth = value; }
        public SignalCycleData Signal { get => _signal; set => _signal = value; }
        public List<ApproachData> Approaches { get => _approaches; set => _approaches = value; }
        public float DemandVolumeVehPerHr { get => _demandVolume; set => _demandVolume = value; }
        public ResultsIntersectionData Results { get => _results; set => _results = value; }
        public float SpeedLimit { get => _speedLimit; set => _speedLimit = value; }
        public float DetectorLength { get => _detectorLength; set => _detectorLength = value; }
        public float LaneGradeHVAreaFactors { get => _laneGradeHVAreaFactors; set => _laneGradeHVAreaFactors = value; }

    }
}
