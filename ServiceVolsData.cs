using System;
using System.Collections.Generic;



namespace HCMCalc_UrbanStreets
{
    public enum ArterialClass
    {
        Class_I = 0,
        Class_II = 1
    }
    /// <summary>
    /// Contains the volume, in both directions or the peak direction, and the AADT for all LOS levels and number of lanes for an arterial.
    /// </summary>
    public class ServiceVolumes
    {
        /**** Fields ****/
        int[,] _pkDirVol = new int[5, 5]; //6 and 7
        int[,] _bothDirVol = new int[5, 5];
        int[,] _aadt = new int[5, 5];
        bool[,] _found = new bool[5, 5];
        float _testParameterValue;
        string _testParameterLabel;
        AreaType _serVolAreaType;
        ArterialClass _class;

        /**** Constructors ****/
        public ServiceVolumes()
        {

        }

        public ServiceVolumes(float testParameterValue, string testParameterLabel, AreaType serVolAreaType, ArterialClass artClass)
        {
            _testParameterValue = testParameterValue;
            _testParameterLabel = testParameterLabel;
            _serVolAreaType = serVolAreaType;
            _class = artClass;
    }

        /**** Properties ****/
        public int[,] PkDirVol
        {
            get { return _pkDirVol; }
            set { _pkDirVol = value; }
        }
        public int[,] BothDirVol
        {
            get { return _bothDirVol; }
            set { _bothDirVol = value; }
        }
        public int[,] AADT
        {
            get { return _aadt; }
            set { _aadt = value; }
        }
        public bool[,] Found
        {
            get { return _found; }
            set { _found = value; }
        }

        public float TestParameterValue { get => _testParameterValue; set => _testParameterValue = value; }
        public string TestParameterLabel { get => _testParameterLabel; set => _testParameterLabel = value; }
        public AreaType SerVolAreaType { get => _serVolAreaType; set => _serVolAreaType = value; }
        public ArterialClass Class { get => _class; set => _class = value; }
    }

    /// <summary>
    /// Variables for creating an arterial for service volume calculations. The variables may dynamically change depending on the chosen test parameter.
    /// </summary>
    public class ServiceVolumeArterialInputs
    {
        int _numIntersections;
        int _postSpeedMPH;
        int _cycleLengthSec;
        int _segmentLength;
        int _turnBayLeftLengthFeet;
        int _turnBayRightLengthFeet;
        ArterialClass _arterialClass;
        int _baseSatFlow;
        float _kFactor;
        float _dFactor;
        float _arterialLengthFeet;
        float _effGreen;
        float _effGreenLeft;
        float _pctHeavyVehicles;
        float _propCurbRightSide;
        float _gC;
        float _gCLeft;
        
        MedianType _medType = MedianType.None;
        TravelDirection _analysisTravelDir;
        AreaType _areaType;

        public ServiceVolumeArterialInputs(bool isMultiLane, string testParameter, float testParameterValue)
        {
            // Changeable Values
            _gC = 0.44f;
            _postSpeedMPH = 45;
            _pctHeavyVehicles = 1;
            _baseSatFlow = 1950;
            _segmentLength = 660;
            if (testParameter == "g / C")
                _gC = testParameterValue;
            else if (testParameter == "Posted Speed")
                _postSpeedMPH = (int)testParameterValue;
            else if (testParameter == "Pct. Heavy Vehicles")
                _pctHeavyVehicles = testParameterValue;
            else if (testParameter == "Base Sat Flow")
                _baseSatFlow = (int)testParameterValue;
            else if (testParameter == "Segment Length")
                _segmentLength = (int)testParameterValue;

            // Default Values
            _numIntersections = 5;
            _arterialLengthFeet = 2 * 5280;
            _cycleLengthSec = 120;
            _kFactor = 0.097f;
            _dFactor = 0.55f;
            _effGreen = (_gC * _cycleLengthSec);
            _effGreenLeft = (0.1f * _cycleLengthSec);
            _medType = MedianType.None;
            _turnBayLeftLengthFeet = 235;
            _arterialClass = ArterialClass.Class_I;
            _analysisTravelDir = TravelDirection.Eastbound;
            _areaType = AreaType.LargeUrbanized;
            _propCurbRightSide = 0.94f;

            if (isMultiLane)
            {
                _postSpeedMPH += 5;
                _cycleLengthSec += 30;
                _dFactor += 0.01f;
                _effGreen = (_gC + 0.01f) * _cycleLengthSec;
                _medType = MedianType.Restrictive;
            }

        }
        /// <summary>
        /// Returns a float array based on an input parameter. For the float array, the first index is minimum, the second index is maximum, and the third index is the step.
        /// </summary>
        /// <param name="testParameter"></param>
        /// <returns></returns>
        public static float[] GetTestParameterValues(string testParameter)
        {
            if (testParameter == "g / C")
                return new float[3] { 0.35f, 0.50f, 0.01f };
            else if (testParameter == "Pct. Heavy Vehicles")
                return new float[3] { 0, 2, 1 };
            else if (testParameter == "Posted Speed")
                return new float[3] { 30, 50, 2 };
            else if (testParameter == "Base Sat Flow")
                return new float[3] { 1600, 1950, 50 };
            else if (testParameter == "Segment Length")
                return new float[3] { 660, 5280, 660 };
            else // "None"
                return new float[3] { 0, 0, 1 };
        }

        public int NumIntersections { get => _numIntersections; set => _numIntersections = value; }
        public float ArterialLengthFeet { get => _arterialLengthFeet; set => _arterialLengthFeet = value; }
        public int PostSpeedMPH { get => _postSpeedMPH; set => _postSpeedMPH = value; }
        public int CycleLengthSec { get => _cycleLengthSec; set => _cycleLengthSec = value; }
        public float KFactor { get => _kFactor; set => _kFactor = value; }
        public float DFactor { get => _dFactor; set => _dFactor = value; }
        public float EffGreen { get => _effGreen; set => _effGreen = value; }
        public float EffGreenLeft { get => _effGreenLeft; set => _effGreenLeft = value; }
        public MedianType MedType { get => _medType; set => _medType = value; }
        public float PctHeavyVehicles { get => _pctHeavyVehicles; set => _pctHeavyVehicles = value; }
        public int TurnBayLeftLengthFeet { get => _turnBayLeftLengthFeet; set => _turnBayLeftLengthFeet = value; }
        public int TurnBayRightLengthFeet { get => _turnBayRightLengthFeet; set => _turnBayRightLengthFeet = value; }
        public ArterialClass ArterialClass { get => _arterialClass; set => _arterialClass = value; }
        public TravelDirection AnalysisTravelDir { get => _analysisTravelDir; set => _analysisTravelDir = value; }
        public AreaType AreaType { get => _areaType; set => _areaType = value; }
        public float PropCurbRightSide { get => _propCurbRightSide; set => _propCurbRightSide = value; }
        public float GC { get => _gC; set => _gC = value; }
        public float GCLeft { get => _gCLeft; set => _gCLeft = value; }
        public int BaseSatFlow { get => _baseSatFlow; set => _baseSatFlow = value; }
        public int SegmentLength { get => _segmentLength; set => _segmentLength = value; }
    }
}