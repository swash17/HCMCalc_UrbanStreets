using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace HCMCalc_UrbanStreets
{
    /// <summary>
    /// Parameters for creating the results for a segment.
    /// </summary>
    public class ResultsSegmentData
    {
        //Calculated Values        
        float _travelTime;        
        float _avgSpeed;
        string _los;
        float _totalRunningTime;

        /// <summary>
        /// Constructor for creating the results for a segment.
        /// </summary>
        public ResultsSegmentData()
        {
            _travelTime = 0;
            _avgSpeed = 0;
            _los = "";
            _totalRunningTime = 0;
        }

        public float TravelTime { get => _travelTime; set => _travelTime = value; }
        public float AverageSpeed { get => _avgSpeed; set => _avgSpeed = value; }
        public string LOS { get => _los; set => _los = value; }
        public float TotalRunningTime { get => _totalRunningTime; set => _totalRunningTime = value; }
    }

    /// <summary>
    /// Parameters for creating the results for an arterial.
    /// </summary>
    public class ResultsArterialData
    {
        //Calculated Values        
        float _travelTime;
        float _avgSpeed;
        float _critSpeed;
        string _los;
        float _totalRunningTime;

        /// <summary>
        /// Constructor for creating the results for an arterial.
        /// </summary>
        public ResultsArterialData()
        {
            _travelTime = 0;
            _avgSpeed = 0;
            _critSpeed = 100; //lowest segment speed along arterial, intialize to high value
            _los = "";
            _totalRunningTime = 0;
        }

        public float TravelTime { get => _travelTime; set => _travelTime = value; }
        public float AverageSpeed { get => _avgSpeed; set => _avgSpeed = value; }
        public string LOS { get => _los; set => _los = value; }
        public float TotalRunningTime { get => _totalRunningTime; set => _totalRunningTime = value; }
        public float CritSpeed { get => _critSpeed; set => _critSpeed = value; }
    }

    /// <summary>
    /// Parameters for creating the results for a link.
    /// </summary>
    public class ResultsLinkData
    {
        //Calculated Values

        LinkRunningTimeCalcParameters _runningTimeCalcParms;
        float _runTime;
        float _baseFreeFlowTravelTime;  //travel time based on base free-flow speed
        float _freeFlowTravelTime;  //travel time based on free-flow speed
        float _baseFreeFlowSpeed;
        float _freeFlowSpeed;
        float _speedRatio;     //Avg Speed/Base Free-Flow Speed        
        float _runSpeed;        
        string _los;
        float[,] _dischargeFlowProfile;
        float[,] _projectedFlowProfile;
        float _smoothingFactor;
        int _platoonArrivalTime;
        float _midblockEnteringVolVehPerHr;
        float _midblockExitingVolVehPerHr;

        /// <summary>
        /// Constructor required for creating the results for a link.
        /// </summary>
        /// <param name="cycleLength"></param>
        public ResultsLinkData(int cycleLength)
        {            
            //_avgSegLength = _lengthMiles / _totalSegs;
            
            _baseFreeFlowSpeed = 0;
            _speedRatio = 0;
            _los = "";
            _dischargeFlowProfile = new float[4, cycleLength];
            _projectedFlowProfile = new float[4, cycleLength];
        }

        /// <summary>
        /// Empty constructor required for XML de/serialization. Do not use.
        /// </summary>
        public ResultsLinkData()
        {
            //_avgSegLength = _lengthMiles / _totalSegs;

            _baseFreeFlowSpeed = 0;
            _speedRatio = 0;
            _los = "";
            _dischargeFlowProfile = new float[4, 100];
            _projectedFlowProfile = new float[4, 100];
        }


        public LinkRunningTimeCalcParameters RunningTimeCalcParms { get => _runningTimeCalcParms; set => _runningTimeCalcParms = value; }
        public float RunTimeSec { get => _runTime; set => _runTime = value; }
        public float BaseFreeFlowSpeed { get => _baseFreeFlowSpeed; set => _baseFreeFlowSpeed = value; }
        public float FreeFlowSpeedMPH { get => _freeFlowSpeed; set => _freeFlowSpeed = value; }
        public float BaseFreeFlowTravelTime { get => _baseFreeFlowTravelTime; set => _baseFreeFlowTravelTime = value; }
        public float FreeFlowTravelTime { get => _freeFlowTravelTime; set => _freeFlowTravelTime = value; }
        public float SpeedRatio { get => _speedRatio; set => _speedRatio = value; }        
        public float RunSpeedMPH { get => _runSpeed; set => _runSpeed = value; }        
        public string LOS { get => _los; set => _los = value; }
        [XmlIgnore]
        public float[,] DischargeFlowProfile { get => _dischargeFlowProfile; set => _dischargeFlowProfile = value; }
        [XmlIgnore]
        public float[,] ProjectedFlowProfile { get => _projectedFlowProfile; set => _projectedFlowProfile = value; }
        public float SmoothingFactor { get => _smoothingFactor; set => _smoothingFactor = value; }
        public int PlatoonArrivalTime { get => _platoonArrivalTime; set => _platoonArrivalTime = value; }
        /// <summary>
        /// The volume entering the midblock, in veh/h.
        /// </summary>
        public float MidblockEnteringVolVehPerHr { get => _midblockEnteringVolVehPerHr; set => _midblockEnteringVolVehPerHr = value; }
        /// <summary>
        /// The volume exiting the midblock, in veh/h.
        /// </summary>
        public float MidblockExitingVolVehPerHr { get => _midblockExitingVolVehPerHr; set => _midblockExitingVolVehPerHr = value; }
    }
}
