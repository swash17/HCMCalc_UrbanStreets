using System.Collections.Generic;
using System.Xml.Serialization;

namespace HCMCalc_UrbanStreets
{
    /// <summary>
    /// The type of median for a segment.
    /// </summary>
    public enum MedianType
    {
        None,
        Nonrestrictive,
        Restrictive
    }

    /// <summary>
    /// The type of outer lane width design.
    /// </summary>
    public enum OutLaneWidth
    {
        Narrow,
        Typical,
        Wide,
        Custom
    }

    /// <summary>
    /// The level of parking activity.
    /// </summary>
    public enum ParkingActivityLevel
    {
        NotApplicable,
        Low,
        Medium,
        High
    }
    /// <summary>
    /// Design direction for the segment.
    /// </summary>
    public enum SegmentDirection
    {
        EBNB = 0,
        WBSB = 1
    }

    /// <summary>
    /// Parameters for creating a segment.
    /// </summary>
    [XmlRoot(ElementName = "Segment")]
    public class SegmentData
    {
        int _id;
        float _lengthFt;
        float _upstreamIntersectionWidth;
        LinkData _link;
        IntersectionData _intersection;
        ResultsSegmentData _results;
        ThresholdData _thresholds;

        /// <summary>
        /// Parameterless constructor needed for XML de/serialization. Do not use.
        /// </summary>
        public SegmentData()
        {
            //Parameterless constructor needed for XML de/serialization
            _thresholds = new ThresholdData();
            _results = new ResultsSegmentData();
        }

        /// <summary>
        /// Constructor for creating a segment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="link"></param>
        /// <param name="intersection"></param>
        public SegmentData(int id, LinkData link, IntersectionData intersection, int upstreamIntersectionWidth)
        {
            _id = id;
            _link = link;
            _intersection = intersection;
            _thresholds = new ThresholdData();
            _results = new ResultsSegmentData();
            _upstreamIntersectionWidth = upstreamIntersectionWidth;
            _lengthFt = link.LengthFt + upstreamIntersectionWidth;
        }

        public SegmentData(int id, LinkData link, IntersectionData intersection, AreaType SerVolAreaType, int BaseFFS, int upstreamIntersectionWidth)
        {
            _id = id;
            _link = link;
            _intersection = intersection;
            _thresholds = new ThresholdData(SerVolAreaType, BaseFFS);
            _results = new ResultsSegmentData();
            _upstreamIntersectionWidth = upstreamIntersectionWidth;
            _lengthFt = link.LengthFt + upstreamIntersectionWidth;

        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("ID")]
        public int Id { get => _id; set => _id = value; }
        public float LengthFt { get => _lengthFt; set => _lengthFt = value; }
        public float UpstreamIntersectionWidth { get => _upstreamIntersectionWidth; set => _upstreamIntersectionWidth = value; }
        /// <summary>
        /// 
        /// </summary>
        public LinkData Link { get => _link; set => _link = value; }
        /// <summary>
        /// 
        /// </summary>
        public IntersectionData Intersection { get => _intersection; set => _intersection = value; }
        public ResultsSegmentData Results { get => _results; set => _results = value; }
        internal ThresholdData Thresholds { get => _thresholds; set => _thresholds = value; }
    }

    /// <summary>
    /// Parameters required for creating a link, which is a property of a Segment.
    /// </summary>
    [XmlRoot(ElementName = "Link")]
    public class LinkData
    {
        /**** Fields ****/
        int _id;
        float _lengthFt;
        long _AADT;
        float _ddhv;
        float _adjDDHV;    //segment demand after accounting for reduced demand from upstream intersection due to left turn spillover
        
        int _numLanes;
        int _numThruLanes;
        int _postSpeedMPH;
        float _freeFlowSpeedMPH;
        float _pctGrade;
        float _propCurbRightSide;
        MedianType _medType;
        float _propRestrictMedian;
        OutLaneWidth _outsideLaneWidth;
        bool _onStreetParkingExists;
        ParkingActivityLevel _parkingActivity;
        float _pctOnStreetParking;
        //int _numAccessPtsSubjectDir;
        //int _numAccessPtsOppDir;
        int[] _numAccessPts;
        SegmentDirection _travelDirection;
        //List<AccessPointData[]> _accessPoints;
        List<AccessPointData> _accessPoints;
        ResultsLinkData _results;


        /**** Constructors ****/

        /// <summary>
        /// Parameterless constructor needed for XML de/serialization.
        /// </summary>
        public LinkData()
        {

        }

        /// <summary>
        /// Constructor required for creating a link.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lengthFt"></param>
        /// <param name="aadt"></param>
        /// <param name="ddhv"></param>
        /// <param name="numLanes"></param>
        /// <param name="numThruLanes"></param>
        /// <param name="postSpeed"></param>
        /// <param name="ffs"></param>
        /// <param name="propCurbRightSide"></param>
        /// <param name="medType"></param>
        /// <param name="propRestrictMedian"></param>
        /// <param name="onStreetParkingExists"></param>
        /// <param name="parkingActivity"></param>
        /// <param name="pctOnStreetParking"></param>
        public LinkData(float lengthFt, int numLanes, int postSpeed, float propCurbRightSide, MedianType medType)
        {
            _id = 1;
            _lengthFt = lengthFt;
            _numLanes = numLanes;
            _postSpeedMPH = postSpeed;
            _freeFlowSpeedMPH = postSpeed + 5;
            _propCurbRightSide = propCurbRightSide;   
            _medType = medType;
            _propRestrictMedian = 0;            
            _onStreetParkingExists = false;
            _pctOnStreetParking = 0;
            _parkingActivity = ParkingActivityLevel.NotApplicable;
            _numAccessPts = new int[2] { 0, 0 };
            //_numAccessPtsOppDir = 0;
            //to-do: change access points to list data structure
            //_accessPoints = new List<AccessPointData[]>();  //index is for direction identifier
            _accessPoints = new List<AccessPointData>();  //index is for direction identifier


            //SetDefaultValuesArtPlan(area, artClass);
        }

        /**** Properties ****/

        /// <summary>
        /// The ID of the link.
        /// </summary>
        [XmlAttribute("ID")]
        public int Id { get => _id; set => _id = value; }
        /// <summary>
        /// The length in ft.
        /// </summary>
        public float LengthFt { get => _lengthFt; set => _lengthFt = value; }
        /// <summary>
        /// Annual average daily traffic in veh/day.
        /// </summary>
        public long AADT { get => _AADT; set => _AADT = value; }
        /// <summary>
        /// Directional design hourly volume in veh/h.
        /// </summary>
        public float DDHV { get => _ddhv; set => _ddhv = value; }
        /// <summary>
        /// Adjusted directional design hourly volume in veh/h.
        /// </summary>
        public float AdjDDHV { get => _adjDDHV; set => _adjDDHV = value; }
        /// <summary>
        /// The number of lanes in the link.
        /// </summary>
        public int NumLanes { get => _numLanes; set => _numLanes = value; }
        /// <summary>
        /// Posted speed of the roadway in mi/h.
        /// </summary>
        public int PostSpeedMPH { get => _postSpeedMPH; set => _postSpeedMPH = value; }
        /// <summary>
        /// Free flow speed in mi/h.
        /// </summary>
        public float FreeFlowSpeedMPH { get => _freeFlowSpeedMPH; set => _freeFlowSpeedMPH = value; }
        /// <summary>
        /// Proportion of curb that uses the right side, entered as a decimal (0.0-1.0).
        /// </summary>
        public float PropCurbRightSide { get => _propCurbRightSide; set => _propCurbRightSide = value; }
        /// <summary>
        /// Type of median for the segment, using the enumeration <a href="https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.MedianType.html">MedianType</a>.
        /// </summary>
        public MedianType MedType { get => _medType; set => _medType = value; }
        /// <summary>
        /// The proportion of the road with a restricted median, entered as a decimal (0.0-1.0).
        /// </summary>
        public float PropRestrictMedian { get => _propRestrictMedian; set => _propRestrictMedian = value; }
        /// <summary>
        /// Whether or not on street parking exists.
        /// </summary>
        public bool OnStreetParkingExists { get => _onStreetParkingExists; set => _onStreetParkingExists = value; }
        /// <summary>
        /// Level of parking activity, using the enumeration <a href="https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ParkingActivityLevel.html">ParkingActivityLevel</a>.
        /// </summary>
        public ParkingActivityLevel ParkingActivity { get => _parkingActivity; set => _parkingActivity = value; }
        /// <summary>
        /// Percentage of on street parking, entered as a percentage (0-100).
        /// </summary>
        public float PctOnStreetParking { get => _pctOnStreetParking; set => _pctOnStreetParking = value; }
        /// <summary>
        /// The classification of width for the outer-lane design, using the enumeration <a href="https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.OutLaneWidth.html">OutLaneWidth</a>.
        /// </summary>
        public OutLaneWidth OutsideLaneWidth { get => _outsideLaneWidth; set => _outsideLaneWidth = value; }
        /// <summary>
        /// The incline of the link, entered as a percentage (+/- 0-100).
        /// </summary>
        public float PctGrade { get => _pctGrade; set => _pctGrade = value; }        
        /// <summary>
        /// The amount of access points, with the first bound [0] being the analysis direction and the second bound [1] being the opposing direction.
        /// </summary>
        public int[] NumAccessPts { get => _numAccessPts; set => _numAccessPts = value; }
        /// <summary>
        /// The access points of the segment, using a list of <a href="https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html">AccessPointData</a>.
        /// </summary>
        public List<AccessPointData> AccessPoints { get => _accessPoints; set => _accessPoints = value; }
        /// <summary>
        /// Results for the link, using the class <a href="https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ResultsLinkData.html">ResultsLinkData</a>.
        /// </summary>
        public ResultsLinkData Results { get => _results; set => _results = value; }
        /// <summary>
        /// Travel direction, using the enumeration <a href="https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SegmentDirection.html">TravelDirection</a>.
        /// </summary>
        public SegmentDirection TravelDirection { get => _travelDirection; set => _travelDirection = value; }
        /// <summary>
        /// The number of lanes with a through movement.
        /// </summary>
        public int NumThruLanes { get => _numThruLanes; set => _numThruLanes = value; }
    }

    /// <summary>
    /// Parameters required for creating an access point.
    /// </summary>
    public class AccessPointData
    {
        float _rightTurnEquivalency;                                        //ratio of through to right-turn sat flow rate at access points
        float _decelRate;                                                   //average constant decel rate in response to turning veh., ft/s/s
        float[,] _arrivalFlowRate;                                             //computed arrival flow rate at access point
        //List<float[]> _arrivalFlowRate; if we need to read this item from file, then we can switch to this data structure; multi-dimensional arrays cannot be deserialized
        //I have not tried it, but supposedly can also use jagged array structure (e.g., float[][]); could do conversion from one to the other
        int[] _blockTime;
        float[] _portionTimeBlocked;                                         //computed portion time blocked at access point
        float _probInsideLaneBlocked;                                       //prob. of left-turn queue in inside through lane
        float _thruDelay;                                                   //computed delay to major thrus due to turns at acc. pts., s/veh
        float[] _volume;                                                      //input active access point volume, veh/h
        float[] _lanes;                                                       //input active access point lanes
        float _location;                                                    //input active access point location

        /// <summary>
        /// Parameterless constructor needed for XML de/serialization. Do not use.
        /// </summary>
        public AccessPointData()
        {
            _arrivalFlowRate = new float[2,0];
        }

        /// <summary>
        /// Constructor required for creating an access point.
        /// </summary>
        /// <param name="rightTurnEquivalency"></param>
        /// <param name="decelRate"></param>
        /// <param name="arrivalFlowRate"></param>
        /// <param name="blockTime"></param>
        /// <param name="portionTimeBlocked"></param>
        /// <param name="probInsideLaneBlocked"></param>
        /// <param name="thruDelay"></param>
        /// <param name="location"></param>
        public AccessPointData(float rightTurnEquivalency, float decelRate, float[,] arrivalFlowRate, int[] blockTime, float[] portionTimeBlocked, float probInsideLaneBlocked, float thruDelay, float location)
        {
            _rightTurnEquivalency = rightTurnEquivalency;
            _decelRate = decelRate;
            _arrivalFlowRate = arrivalFlowRate;
            _blockTime = blockTime;
            _portionTimeBlocked = portionTimeBlocked;
            _probInsideLaneBlocked = probInsideLaneBlocked;
            _thruDelay = thruDelay;
            _location = location;
        }

        public float RightTurnEquivalency { get => _rightTurnEquivalency; set => _rightTurnEquivalency = value; }
        public float DecelRate { get => _decelRate; set => _decelRate = value; }
        [XmlIgnore]
        public float[,] ArrivalFlowRate { get => _arrivalFlowRate; set => _arrivalFlowRate = value; }
        public float[] PortionTimeBlocked { get => _portionTimeBlocked; set => _portionTimeBlocked = value; }
        public float ProbInsideLaneBlocked { get => _probInsideLaneBlocked; set => _probInsideLaneBlocked = value; }
        public float ThruDelay { get => _thruDelay; set => _thruDelay = value; }
        public float[] Volume { get => _volume; set => _volume = value; }
        public float[] Lanes { get => _lanes; set => _lanes = value; }
        public float Location { get => _location; set => _location = value; }
        public int[] BlockTime { get => _blockTime; set => _blockTime = value; }
    }
}
