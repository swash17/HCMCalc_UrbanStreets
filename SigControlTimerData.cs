using System;
using System.Collections.Generic;


namespace HCMCalc_UrbanStreets
{
    public class TimerData
    {
        float _startTime;                       //Relative to system master 0, s
        float _endTime;                         //Relative to system master 0, s
        float _duration;
        float[] _mvmtSatFlow;                   //Sat-flow rate of each movement (0=L, 1=T, 2=R)
        float _maxAllowHdwy;
        float _maxGreen;
        float _greenExtension;                  //Extension of green due to arriving traffic, s
        float _probPhaseCall;                   //probability of timer being called by conflicting traffic
        float _probMaxOut;                      //probability of phase termination by max out
        float _maxQueueClearTime;
        float _queueServeTime;
        float _queueClearTime;                  // Critical Green Queue Clear Time (s) (CNB: Might be incorrect?)
        float _greenFlow;
        float _redFlow;
        float _endWindow;
        float _startWindow;
        float _intergreen;
        float _greenTime; // CNB: Might just be effective green. Normally Splits - Intergreen

        //  Permissive Left-Turn Characteristics
        bool _isPermittedTimer;
        float _permEffGreen;                    //g_p duration of permissive green for permissive left-turn movements, s
        float _permServeTime;                   //g_u duration of permissive green that is not blocked by an opposing queue,s
        float _permL1_LT;                       //l_1p start-up lost time for permissive left operation (adjusted for phase seq.), s
        float _permEU_LT;                       //e_p end use for permissive left operation (adjusted for phase sequence), s
        float _permQueServeTime;                //g_ps queue service time during permissive green, s
        float _pctLeftsInsideLane;
        float _pctRightsOutsideLane;
        float _timeToFirstSharedBlock;          //Time before the first left-turn vehicle arrives and blockes the shared lane
        float _queueServeTimeBeforeBlk;         //Time during g_f that left queue is served
        float _callingFlow;
        float[] _portionFree = new float[3];    // L - T - R
        float[] _unbunchedFlow = new float[3];  // L - T - R
        float[] _delta = new float[3];          // L - T - R
        float[] _phaseFlowRate = new float[3];  // L - T - R
        float[] _bunchFactor = new float[3];    // L - T - R

        TimerResults _Results = new TimerResults();
        SignalPhaseParms _phaseParms = new SignalPhaseParms();

        public TimerData()
        {

        }

        public TimerData(float duration, float changePeriod, float startTime, float endTime, float maxHdwy, float maxGreen, float maxQueueClear, float greenExtension, float probPhaseCall, float probMaxOut)
        {
            _duration = duration;
            _startTime = startTime;
            _endTime = endTime;
            _intergreen = changePeriod;
            _maxAllowHdwy = maxHdwy;
            _maxGreen = maxGreen;
            _maxQueueClearTime = maxQueueClear;
            _greenExtension = greenExtension;
            _probPhaseCall = probPhaseCall;
            _probMaxOut = probMaxOut;
            _mvmtSatFlow = new float[3];
        }

        public float MaxGreen { get => _maxGreen; set => _maxGreen = value; }
        public float GreenExtension { get => _greenExtension; set => _greenExtension = value; }
        public float ProbOfPhaseCall { get => _probPhaseCall; set => _probPhaseCall = value; }
        public float ProbMaxOut { get => _probMaxOut; set => _probMaxOut = value; }
        public float PermEffGreen { get => _permEffGreen; set => _permEffGreen = value; }
        public float PermServeTime { get => _permServeTime; set => _permServeTime = value; }
        public float PermL1_LT { get => _permL1_LT; set => _permL1_LT = value; }
        public float PermEU_LT { get => _permEU_LT; set => _permEU_LT = value; }
        public float PermQueServeTime { get => _permQueServeTime; set => _permQueServeTime = value; }
        public float TimeToFirstBlk { get => _timeToFirstSharedBlock; set => _timeToFirstSharedBlock = value; }
        public float QueServeTimeBeforeBlk { get => _queueServeTimeBeforeBlk; set => _queueServeTimeBeforeBlk = value; }
        public float StartTime { get => _startTime; set => _startTime = value; }
        public float EndTime { get => _endTime; set => _endTime = value; }
        public float MaxAllowHdwy { get => _maxAllowHdwy; set => _maxAllowHdwy = value; }
        public float[] MvmtSatFlow { get => _mvmtSatFlow; set => _mvmtSatFlow = value; }
        public float PctLeftsInsideLane { get => _pctLeftsInsideLane; set => _pctLeftsInsideLane = value; }
        public float PctRightsOutsideLane { get => _pctRightsOutsideLane; set => _pctRightsOutsideLane = value; }
        public float Duration { get => _duration; set => _duration = value; }
        public float MaxQueueClearTime { get => _maxQueueClearTime; set => _maxQueueClearTime = value; }
        public float IntergreenTimeSec { get => _intergreen; set => _intergreen = value; }
        public bool IsPermittedTimer { get => _isPermittedTimer; set => _isPermittedTimer = value; }
        public float QueueServeTime { get => _queueServeTime; set => _queueServeTime = value; }
        public TimerResults Results { get => _Results; set => _Results = value; }
        public float EndWindow { get => _endWindow; set => _endWindow = value; }
        public float StartWindow { get => _startWindow; set => _startWindow = value; }
        public float CallingFlow { get => _callingFlow; set => _callingFlow = value; }
        public float[] PortionFree { get => _portionFree; set => _portionFree = value; }
        public float[] UnbunchedFlow { get => _unbunchedFlow; set => _unbunchedFlow = value; }
        public float[] Delta { get => _delta; set => _delta = value; }
        public float[] PhaseFlowRate { get => _phaseFlowRate; set => _phaseFlowRate = value; }
        public float[] BunchFactor { get => _bunchFactor; set => _bunchFactor = value; }
        public float GreenFlow { get => _greenFlow; set => _greenFlow = value; }
        public float RedFlow { get => _redFlow; set => _redFlow = value; }
        public float GreenTime { get => _greenTime; set => _greenTime = value; }
        public float QueueClearTime { get => _queueClearTime; set => _queueClearTime = value; }
        public SignalPhaseParms PhaseParms { get => _phaseParms; set => _phaseParms = value; }

        /// <summary>
        /// Returns standard TimerDatas for a signal with 8 phases. 
        /// </summary>
        public static List<TimerData> GetStandardTimers()
        {
            TimerData TimerWBL = new TimerData(15.9f, 3, 35.27f, 51.16f, 3.13f, 30.73f, 12.646f, 0.311f, 0.995f, 0);
            TimerData TimerEBL = new TimerData(16.1f, 3, 35.27f, 51.37f, 3.13f, 30.73f, 12.829f, 0.322f, 0.996f, 0);
            TimerData TimerEBT = new TimerData(52.84f, 4, 51.16f, 4f, 0, 0, 0, 0, 0, 0);
            TimerData TimerWBT = new TimerData(52.63f, 4, 51.37f, 4f, 0, 0, 0, 0, 0, 0);
            TimerData TimerNBL = new TimerData(9.13f, 3, 4, 13.13f, 3.13f, 17, 6.442f, 0.099f, 0.938f, 0);
            TimerData TimerSBL = new TimerData(9.13f, 3, 4, 13.13f, 3.13f, 17, 6.442f, 0.099f, 0.938f, 0);
            TimerData TimerSBT = new TimerData(22.13f, 4, 13.14f, 35.27f, 3.06f, 31.87f, 16.165f, 1.968f, 1, 0.016f);
            TimerData TimerNBT = new TimerData(22.13f, 4, 13.14f, 35.27f, 3.06f, 31.87f, 16.165f, 1.968f, 1, 0.016f);
            return new List<TimerData> { TimerWBL, TimerEBL, TimerEBT, TimerWBT, TimerNBL, TimerSBL, TimerSBT, TimerNBT };
        }

        public class TimerResults
        {
            int _movement;
            float _p_R;
            float _f_RT;
            float _weightedDischargeFlowRate;
            float _dischargeFlowRateVehPerSec;
            float _satFlowRateVehPerLanePerSec;
            float _redTimeFlow;
            float _greenTimeFlow;
            float _effectiveRedSec;
            float _queueRedOnly;
            int _cycleStart;
            int _queueServiceStart;
            int _phaseTotalRunningTime;
            int _effectiveRedStart;

            public TimerResults()
            {

            }

            public int Movement { get => _movement; set => _movement = value; }
            public float P_R { get => _p_R; set => _p_R = value; }
            public float F_RT { get => _f_RT; set => _f_RT = value; }
            public float WeightedDischargeFlowRate { get => _weightedDischargeFlowRate; set => _weightedDischargeFlowRate = value; }
            public float DischargeFlowRateVehPerSec { get => _dischargeFlowRateVehPerSec; set => _dischargeFlowRateVehPerSec = value; }
            public float SatFlowRateVehPerLanePerSec { get => _satFlowRateVehPerLanePerSec; set => _satFlowRateVehPerLanePerSec = value; }
            public float RedTimeFlow { get => _redTimeFlow; set => _redTimeFlow = value; }
            public float GreenTimeFlow { get => _greenTimeFlow; set => _greenTimeFlow = value; }
            public float EffectiveRedSec { get => _effectiveRedSec; set => _effectiveRedSec = value; }
            public float QueueRedOnly { get => _queueRedOnly; set => _queueRedOnly = value; }
            public int CycleStart { get => _cycleStart; set => _cycleStart = value; }
            public int QueueServiceStart { get => _queueServiceStart; set => _queueServiceStart = value; }
            public int PhaseTotalRunningTime { get => _phaseTotalRunningTime; set => _phaseTotalRunningTime = value; }
            public int EffectiveRedStart { get => _effectiveRedStart; set => _effectiveRedStart = value; }
        }
    }
}
