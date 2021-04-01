using System;
using System.Collections.Generic;
using System.Xml.Serialization;



namespace HCMCalc_UrbanStreets
{

    public enum UnsignalControlType
    {
        TwoWayStop,
        AllWayStop
    }
    public enum MovementDirection
    {
        Left = 0,
        Through = 1,
        Right = 2
    }
    public enum MovementDirectionWithMidBlock
    {
        Left = 0,
        Through = 1,
        Right = 2,
        MidBlock = 3
    }
    public enum PhasingType
    {
        None,
        Protected,
        Permitted,
        ProtPerm,
        Split,
        ThruAndRight
    }
    public enum PhaseColor
    {
        Green,
        Yellow,
        AllRed
    }
    public enum SequenceType
    {
        Lead,
        Lag,
        None
    }
    public enum SigControlType
    {
        Pretimed = 0,
        CoordinatedActuated = 1,  //actuation on cross-street, excess time goes to major street
        FullyActuated = 2   //all approaches actuated, no coordination
    }

    public class SignalCycleData
    {
        int _cycleLengthSec;
        bool _forceMode;
        SigControlType _controlType;    // type of signal control (e.g., pretimed)
        int _referencePhaseID;
        int _referencePoint;
        int _offsetSec;
        List<SignalPhaseData> _phases;

        public SignalCycleData()
        {
            _controlType = SigControlType.Pretimed;
            //_sigControl = ParmRanges.SigControlDefault[Convert.ToInt32(_area), Convert.ToInt32(_class)];
            //_sigControl = ParmRanges.SigControlDefault[Convert.ToInt32(_class)];
        }

        public SignalCycleData(SigControlType controlType, int cycleLengthSec, List<SignalPhaseData> Phases)
        {
            _controlType = controlType;
            _cycleLengthSec = cycleLengthSec;
            _referencePhaseID = 1;
            _offsetSec = 0;
            _phases = Phases;
        }
        public SignalCycleData(SigControlType controlType, int cycleLengthSec)
        {
            _controlType = controlType;
            _cycleLengthSec = cycleLengthSec;
            _referencePhaseID = 1;
            _offsetSec = 0;
        }

        public int CycleLengthSec { get => _cycleLengthSec; set => _cycleLengthSec = value; }
        public SigControlType ControlType { get => _controlType; set => _controlType = value; }
        public int ReferencePhaseID { get => _referencePhaseID; set => _referencePhaseID = value; }
        public int OffsetSec { get => _offsetSec; set => _offsetSec = value; }
        public bool ForceMode { get => _forceMode; set => _forceMode = value; }
        public int ReferencePoint { get => _referencePoint; set => _referencePoint = value; }
        public List<SignalPhaseData> Phases { get => _phases; set => _phases = value; }
    }

    public class SignalPhaseData
    {
        byte _nemaPhaseId;
        NemaMovementNumbers _nemaMvmtId;
        PhasingType _phasing;
        SequenceType _sequence;

        TimerData _timer;
        LaneGroupData _associatedLaneGroup;  //circular reference, as LaneGroupData contains a field for SignalPhaseData

        float _yellowSec;
        float _allRedSec;
        float _effectiveGreenSec;
        float _effectiveRedSec;
        float _gC;
        float _minGreenIntervalSec;
        float _splits; // s
        float _startUpLostTimeSec;
        float _endUseSec; // average end use of intergreen period
        float _passTime;
        bool _recallMinMode;
        bool _recallMaxMode;
        bool _gapOutMode;
        bool _dualEntryMode;
        bool _dallasPhasingMode;


        public SignalPhaseData()
        {
            
        }

        public SignalPhaseData(byte nemaPhaseId, NemaMovementNumbers nemaMvmtId, PhasingType phaseType, float greenTime, float yellowTime, float allRedTime, float startUpLostTime, TimerData timer)
        {
            _nemaPhaseId = nemaPhaseId;
            _nemaMvmtId = nemaMvmtId;
            _phasing = phaseType;
            _yellowSec = yellowTime;
            _allRedSec = allRedTime;
            _startUpLostTimeSec = startUpLostTime;
            _timer = timer;
            _endUseSec = 2;
        }

        public byte NemaPhaseId { get => _nemaPhaseId; set => _nemaPhaseId = value; }
        public PhasingType Phasing { get => _phasing; set => _phasing = value; }
        public float YellowSec { get => _yellowSec; set => _yellowSec = value; }
        public float AllRedSec { get => _allRedSec; set => _allRedSec = value; }
        public float GreenEffectiveSec { get => _effectiveGreenSec; set => _effectiveGreenSec = value; }
        public float gC { get => _gC; set => _gC = value; }
        public SequenceType Sequence { get => _sequence; set => _sequence = value; }
        public bool RecallMinMode { get => _recallMinMode; set => _recallMinMode = value; }
        public bool RecallMaxMode { get => _recallMaxMode; set => _recallMaxMode = value; }
        public bool GapOutMode { get => _gapOutMode; set => _gapOutMode = value; }
        public bool DualEntryMode { get => _dualEntryMode; set => _dualEntryMode = value; }
        public bool DallasPhasingMode { get => _dallasPhasingMode; set => _dallasPhasingMode = value; }
        public float MinGreenIntervalSec { get => _minGreenIntervalSec; set => _minGreenIntervalSec = value; }
        public float StartUpLostTimeSec { get => _startUpLostTimeSec; set => _startUpLostTimeSec = value; }
        public float EndUseSec { get => _endUseSec; set => _endUseSec = value; }
        public float Splits { get => _splits; set => _splits = value; }
        public TimerData Timer { get => _timer; set => _timer = value; }
        public float PassTime { get => _passTime; set => _passTime = value; }
        public float RedEffectiveSec { get => _effectiveRedSec; set => _effectiveRedSec = value; }

        //[XmlIgnore]  //commenting this line exposed the circular reference between SignalPhaseData and LaneGroupData
        public LaneGroupData AssociatedLaneGroup { get => _associatedLaneGroup; set => _associatedLaneGroup = value; }
        public NemaMovementNumbers NemaMvmtId { get => _nemaMvmtId; set => _nemaMvmtId = value; }
    }
    
}
