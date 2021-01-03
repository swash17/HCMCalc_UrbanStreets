using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace HCMCalc_UrbanStreets
{

    public class ServiceVolumeTableFDOT
    {
        ArterialClass _class;
        bool _multiLane;
        AreaType _serVolAreaType;
        RoadwayData _roadway;
        TrafficData _traffic;
        SignalData _signal;

        public ServiceVolumeTableFDOT()
        { }

        public ServiceVolumeTableFDOT(AreaType SerVolAreaType, ArterialClass ArtClass, bool MultiLane)
        {
            _class = ArtClass;
            _serVolAreaType = SerVolAreaType;
            _multiLane = MultiLane;
        }

        public static ServiceVolumeTableFDOT ChangeTestParameterValue(ServiceVolumeTableFDOT Table, string testParameter, float testParameterValue)
        {
            if (testParameter == "g / C")
                Table.Signal.EffGreenToCycleLengthRatio = testParameterValue;
            else if (testParameter == "Posted Speed")
                Table.Roadway.PostedSpeedMPH = (byte)testParameterValue;
            else if (testParameter == "Pct. Heavy Vehicles")
                Table.Traffic.PctHeavyVeh = testParameterValue;
            else if (testParameter == "Base Sat Flow")
                Table.Traffic.BaseSatFlow = (int)testParameterValue;
            else if (testParameter == "Segment Length")
            {
                Table.Roadway.SegmentLengthFt = (int)testParameterValue;
                Table.Roadway.ArterialLenghtFt = Table.Roadway.NumSegments * Table.Roadway.SegmentLengthFt;
                Table.Roadway.FacilityLengthMiles = Table.Roadway.ArterialLenghtFt / 5280;
            }
            return Table;
        }

        public ArterialClass Class { get => _class; set => _class = value; }
        [XmlAttribute("Multi-Lane")]
        public bool MultiLane { get => _multiLane; set => _multiLane = value; }
        public RoadwayData Roadway { get => _roadway; set => _roadway = value; }
        public TrafficData Traffic { get => _traffic; set => _traffic = value; }
        public SignalData Signal { get => _signal; set => _signal = value; }
        public AreaType SerVolAreaType { get => _serVolAreaType; set => _serVolAreaType = value; }
    }

    public class SerVolTablesByMultiLane
    {
        ArterialClass _artClass;
        List<ServiceVolumeTableFDOT> _serVolTables = new List<ServiceVolumeTableFDOT>();

        public SerVolTablesByMultiLane()
        { }

        public SerVolTablesByMultiLane(ArterialClass artClass, ServiceVolumeTableFDOT singleLaneTable, ServiceVolumeTableFDOT multiLaneTable)
        {
            _artClass = artClass;
            SerVolTables.Add(singleLaneTable);
            SerVolTables.Add(multiLaneTable);
        }

        [XmlAttribute("Class")]
        public ArterialClass ArtClass { get => _artClass; set => _artClass = value; }
        public List<ServiceVolumeTableFDOT> SerVolTables { get => _serVolTables; set => _serVolTables = value; }
    }

    public class SerVolTablesByClass
    {
        AreaType _artAreaType;
        List<SerVolTablesByMultiLane> _serVolTablesMultiLane = new List<SerVolTablesByMultiLane>();

        public SerVolTablesByClass()
        { }

        public SerVolTablesByClass(AreaType ArtAreaType)
        {
            _artAreaType = ArtAreaType;
        }

        [XmlAttribute("AreaType")]
        public AreaType ArtAreaType { get => _artAreaType; set => _artAreaType = value; }
        public List<SerVolTablesByMultiLane> SerVolTablesMultiLane { get => _serVolTablesMultiLane; set => _serVolTablesMultiLane = value; }
    }

    public class RoadwayData
    {
        byte _postedSpeedMPH;
        byte _freeFlowSpeedMPH;
        MedianType _median;
        TerrainType _terrain;
        bool _exclusiveLeftTurns;
        bool _exclusiveRightTurns;
        float _facilityLengthMiles;

        byte _numSegments;
        byte _numIntersections;
        float _segmentLengthFt;
        byte _accessPointsPerMile;
        float _propCurbRightSide;
        int _turnBayLeftLengthFeet;
        float _arterialLenghtFt;
        TravelDirection _analysisTravelDir;

        public RoadwayData()
        { }

        public RoadwayData(byte postedSpeedMPH, byte freeFlowSpeedMPH, MedianType median, TerrainType terrain, bool exclusiveLeftTurns, bool exclusiveRightTurns, float facilityLengthMiles, byte numIntersections, byte accessPointsPerMile)
        {
            _postedSpeedMPH = postedSpeedMPH;
            _freeFlowSpeedMPH = freeFlowSpeedMPH;
            _median = median;
            _terrain = terrain;
            _exclusiveLeftTurns = exclusiveLeftTurns;
            _exclusiveRightTurns = exclusiveRightTurns;
            _facilityLengthMiles = facilityLengthMiles;

            _numSegments = (byte)(numIntersections - 1);
            _numIntersections = numIntersections;
            _segmentLengthFt = facilityLengthMiles / (numIntersections-1) * 5280;
            _accessPointsPerMile = accessPointsPerMile;
            _propCurbRightSide = 0.94f;
            _turnBayLeftLengthFeet = 235;
            _analysisTravelDir = TravelDirection.Eastbound;
            _arterialLenghtFt = facilityLengthMiles * 5280;
        }

        public byte NumSegments { get => _numSegments; set => _numSegments = value; }
        public float SegmentLengthFt { get => _segmentLengthFt; set => _segmentLengthFt = value; }
        public byte PostedSpeedMPH { get => _postedSpeedMPH; set => _postedSpeedMPH = value; }
        public byte AccessPointsPerMile { get => _accessPointsPerMile; set => _accessPointsPerMile = value; }
        public byte NumIntersections { get => _numIntersections; set => _numIntersections = value; }
        public MedianType Median { get => _median; set => _median = value; }
        public byte FreeFlowSpeedMPH { get => _freeFlowSpeedMPH; set => _freeFlowSpeedMPH = value; }
        public float FacilityLengthMiles { get => _facilityLengthMiles; set => _facilityLengthMiles = value; }
        public TerrainType Terrain { get => _terrain; set => _terrain = value; }
        public bool ExclusiveLeftTurns { get => _exclusiveLeftTurns; set => _exclusiveLeftTurns = value; }
        public bool ExclusiveRightTurns { get => _exclusiveRightTurns; set => _exclusiveRightTurns = value; }
        public float PropCurbRightSide { get => _propCurbRightSide; set => _propCurbRightSide = value; }
        public int TurnBayLeftLengthFeet { get => _turnBayLeftLengthFeet; set => _turnBayLeftLengthFeet = value; }
        public TravelDirection AnalysisTravelDir { get => _analysisTravelDir; set => _analysisTravelDir = value; }
        public float ArterialLenghtFt { get => _arterialLenghtFt; set => _arterialLenghtFt = value; }
    }

    public class TrafficData
    {
        float _PHF;
        float _pctHeavyVeh;
        float _pctLeftTurns;
        float _pctRightTurns;
        float _kFactor;
        float _dFactor;
        int _baseSatFlow;

        public TrafficData()
        { }

        public TrafficData(float kFactor, float dFactor, float peakHourFact, int baseSatFlow, float pctHV, float pctLeftTurns, float pctRightTurns)
        {
            _kFactor = kFactor;
            _dFactor = dFactor;
            _PHF = peakHourFact;
            _baseSatFlow = baseSatFlow;
            _pctHeavyVeh = pctHV;
            _pctLeftTurns = pctLeftTurns;
            _pctRightTurns = pctRightTurns;
        }

        public float PHF { get => _PHF; set => _PHF = value; }
        public float PctLeftTurns { get => _pctLeftTurns; set => _pctLeftTurns = value; }
        public float PctRightTurns { get => _pctRightTurns; set => _pctRightTurns = value; }
        public float KFactor { get => _kFactor; set => _kFactor = value; }
        public float DFactor { get => _dFactor; set => _dFactor = value; }
        public int BaseSatFlow { get => _baseSatFlow; set => _baseSatFlow = value; }
        public float PctHeavyVeh { get => _pctHeavyVeh; set => _pctHeavyVeh = value; }
    }

    public class SignalData
    {
        float _gC;
        byte _arvType;
        int _cycleLengthSec;
        float _effGreen;
        float _effGreenLeft;
        SigControlType _sigType;
        byte _numSignals;

        public SignalData()
        {

        }

        public SignalData(byte numSignals, byte arrivalType, SigControlType sigType, int cycleLength, float gCratio)
        {
            _numSignals = numSignals;
            _arvType = arrivalType;
            _sigType = sigType;
            _cycleLengthSec = cycleLength;
            _gC = gCratio;
            _effGreen = CalcEffectiveGreen(gCratio, cycleLength);
            _effGreenLeft = CalcEffectiveGreen(0.1f, cycleLength);
        }

        public float CalcEffectiveGreen(float gC, int cycleLengthSec)
        {
            return (gC * cycleLengthSec);
        }

        public float EffGreenToCycleLengthRatio { get => _gC; set => _gC = value; }
        public int CycleLengthSec { get => _cycleLengthSec; set => _cycleLengthSec = value; }
        public byte ArvType { get => _arvType; set => _arvType = value; }
        public SigControlType SigType { get => _sigType; set => _sigType = value; }
        public byte NumSignals { get => _numSignals; set => _numSignals = value; }
        [XmlIgnore]
        public float EffGreen { get => _effGreen; set => _effGreen = value; }
        [XmlIgnore]
        public float EffGreenLeft { get => _effGreenLeft; set => _effGreenLeft = value; }
    }

    class UrbanStreetParameters
    {
        float _startUpLostTime;         // s
        float _greenExtension;          // s
        float _anaylsisTimePeriod;      // h
        float _criticalMergeHeadway;    // s
        float _apDecelRate;             // ft/s^2
        float _apRightTurnSpeed;        // ft/s
        float _signalDecelRate;         // ft/s^2
        float _accelRate;               // ft/s^2
        float _bunchedHeadway;          // s/veh
        float _platoonMaxHeadway;       // s/veh
        float _stopThreshSpeed;         // mph
        float _storedLaneLength;        // ft
        float _calculationIterations;   // unitless
        float _apLeftTurnBayLength;     // ft
        float _sneakersPerCycle;        // veh
        float _distBetweenStoredVeh;    // ft
        float _critHeadway;             // s, left from major
        float _followUpHeadway;         // s, left from major
        float _apRightTurnEquivalency;  // unitless
        float _averageSpeedtoFFSRatio;  // unitless

        // Ignored:
        // Base Sat Flow
        // Right Turn Equivalency (Signal)
        // Left Turn Equivalency (Signal)

        public UrbanStreetParameters()
        {
            _startUpLostTime = 2;               // s
            _greenExtension = 2;                // s
            _anaylsisTimePeriod = 0.25f;        // h
            _criticalMergeHeadway = 3.7f;       // s
            _apDecelRate = 6.7f;                // ft/s^2
            _apRightTurnSpeed = 20;             // ft/s
            _signalDecelRate = 4;               // ft/s^2
            _accelRate = 3.5f;                  // ft/s^2
            _bunchedHeadway = 1.5f;             // s/veh
            _platoonMaxHeadway = 3.6f;          // s/veh
            _stopThreshSpeed = 5;               // mph
            _storedLaneLength = 25;             // ft
            _calculationIterations = 15;        // unitless
            _apLeftTurnBayLength = 250;         // ft
            _sneakersPerCycle = 2;              // veh
            _distBetweenStoredVeh = 8;          // ft
            _critHeadway = 4.1f;                // s, left from major
            _followUpHeadway = 2.2f;            // s, left from major
            _apRightTurnEquivalency = 2.2f;     // unitless
            _averageSpeedtoFFSRatio = 0.9f;     // unitless


        }

        public float StartUpLostTime { get => _startUpLostTime; set => _startUpLostTime = value; }
        public float GreenExtension { get => _greenExtension; set => _greenExtension = value; }
        public float AnaylsisTimePeriod { get => _anaylsisTimePeriod; set => _anaylsisTimePeriod = value; }
        public float CriticalMergeHeadway { get => _criticalMergeHeadway; set => _criticalMergeHeadway = value; }
        public float ApDecelRate { get => _apDecelRate; set => _apDecelRate = value; }
        public float ApRightTurnSpeed { get => _apRightTurnSpeed; set => _apRightTurnSpeed = value; }
        public float SignalDecelRate { get => _signalDecelRate; set => _signalDecelRate = value; }
        public float AccelRate { get => _accelRate; set => _accelRate = value; }
        public float BunchedHeadway { get => _bunchedHeadway; set => _bunchedHeadway = value; }
        public float PlatoonMaxHeadway { get => _platoonMaxHeadway; set => _platoonMaxHeadway = value; }
        public float StopThreshSpeed { get => _stopThreshSpeed; set => _stopThreshSpeed = value; }
        public float StoredLaneLength { get => _storedLaneLength; set => _storedLaneLength = value; }
        public float CalculationIterations { get => _calculationIterations; set => _calculationIterations = value; }
        public float ApLeftTurnBayLength { get => _apLeftTurnBayLength; set => _apLeftTurnBayLength = value; }
        public float SneakersPerCycle { get => _sneakersPerCycle; set => _sneakersPerCycle = value; }
        public float DistBetweenStoredVeh { get => _distBetweenStoredVeh; set => _distBetweenStoredVeh = value; }
        public float CritHeadway { get => _critHeadway; set => _critHeadway = value; }
        public float FollowUpHeadway { get => _followUpHeadway; set => _followUpHeadway = value; }
        public float ApRightTurnEquivalency { get => _apRightTurnEquivalency; set => _apRightTurnEquivalency = value; }
        public float AverageSpeedtoFFSRatio { get => _averageSpeedtoFFSRatio; set => _averageSpeedtoFFSRatio = value; }
    }
    

}
