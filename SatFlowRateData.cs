using System;
using System.Collections.Generic;



namespace HCMCalc_UrbanStreets
{
    /// <summary>
    /// Parameters that cover all adjustment factors required for saturation flow rate analysis.
    /// </summary>
    public class SatFlowRateAdjFactors
    {        
        float _fw;        
        float _fHVg;
        float _fp;
        float _fbb;
        float _fa;
        float _fLU;
        float _fLT;
        float _fRT;
        float _fLpb;
        float _fRpb;
        float _fwz;
        float _fms;
        float _fsp;
        float _e_L;
        float _e_R;

        //LOSPLAN factors
        float _fHV;
        float _fN;
        float _fAP;
        float _fTP;
        float _fSL;
        float _vol;
        float _pop;
        //float _fRT;
        float _fbay;
        float _fmedian;
        //float _baseValue;
        //float _adjValue;
        int _widthOuterLane;
        int _widthInnerLane;
        float _factLaneWidth;
        //float _localAdjFactor;
        float _pctRTMultiplier;
        float _permSatFlow;
        float _sharedSatFlow;
        float[] _areaPopulationDefault; // Area population in millions

        /// <summary>
        /// Constructor for assigning saturation flow rate adjustment factors.
        /// </summary>
        public SatFlowRateAdjFactors()
        {
            _fw = 1;
            _fHVg = 1;
            _fp = 1;
            _fbb = 1;
            _fa = 1;
            _fLU = 1;
            _fLT = 1;
            _fRT = 1;
            _fLpb = 1;
            _fRpb = 1;
            _fwz = 1;
            _fms = 1;
            _fsp = 1;  
            _e_L = 1 / 0.95f; 
            _e_R = 1 / 0.85f;
            _areaPopulationDefault = new float[4] { 1.5f, 0.4f, 0.03f, 0.003f };
        }

        public float LaneWidth { get => _fw; set => _fw = value; }
        public float HeavyVehGrade { get => _fHVg; set => _fHVg = value; }
        public float Parking { get => _fp; set => _fp = value; }
        public float BusBlockage { get => _fbb; set => _fbb = value; }
        public float AreaType { get => _fa; set => _fa = value; }
        public float LaneUtilization { get => _fLU; set => _fLU = value; }
        public float LeftTurn { get => _fLT; set => _fLT = value; }
        public float RightTurn { get => _fRT; set => _fRT = value; }
        public float PedLeftTurn { get => _fLpb; set => _fLpb = value; }
        public float PedRightTurn { get => _fRpb; set => _fRpb = value; }
        public float WorkZone { get => _fwz; set => _fwz = value; }
        public float LaneBlockage { get => _fms; set => _fms = value; }
        public float Spillback { get => _fsp; set => _fsp = value; }
        public float LeftTurnEquivalency { get => _e_L; set => _e_L = value; }
        public float RightTurnEquivalency { get => _e_R; set => _e_R = value; }

        // LOS Plan
        public float HeavyVeh { get => _fHV; set => _fHV = value; }
        public float fN { get => _fN; set => _fN = value; }
        public float fAreaPopulation { get => _fAP; set => _fAP = value; }
        public float fTP { get => _fTP; set => _fTP = value; }
        public float fSL { get => _fSL; set => _fSL = value; }
        public float Vol { get => _vol; set => _vol = value; }
        public float Pop { get => _pop; set => _pop = value; }
        //public float RightTurn { get => _fRT; set => _fRT = value; }
        public float fbay { get => _fbay; set => _fbay = value; }
        public float fmedian { get => _fmedian; set => _fmedian = value; }
        public int WidthOuterLane { get => _widthOuterLane; set => _widthOuterLane = value; }
        public int WidthInnerLane { get => _widthInnerLane; set => _widthInnerLane = value; }
        public float FactLaneWidth { get => _factLaneWidth; set => _factLaneWidth = value; }
        //public float LocalAdjFactor { get => _localAdjFactor; set => _localAdjFactor = value; }
        public float PctRTMultiplier { get => _pctRTMultiplier; set => _pctRTMultiplier = value; }
        public float PermSatFlow { get => _permSatFlow; set => _permSatFlow = value; }
        public float SharedSatFlow { get => _sharedSatFlow; set => _sharedSatFlow = value; }
        public float[] AreaPopulationDefault { get => _areaPopulationDefault; set => _areaPopulationDefault = value; }
        //public float BaseValue { get => _baseValue; set => _baseValue = value; }
        //public float AdjValue { get => _adjValue; set => _adjValue = value; }
    }

    /// <summary>
    /// Parameters relevant to both the base and adjusted saturation flow rate values, as well as the adjustment factors.
    /// </summary>
    public class SaturationFlowRateValues
    {
        float _baseValue;
        float _adjustedValue;        
        SatFlowRateAdjFactors _adjFact;

        /// <summary>
        /// Empty constructor required for XML de/serialization.
        /// </summary>
        public SaturationFlowRateValues()
        {
            _baseValue = 1950;
            _adjFact = new SatFlowRateAdjFactors();
        }

        /// <summary>
        /// Constructor for creating saturation flow rate values.
        /// </summary>
        /// <param name="baseSatFlow"></param>
        public SaturationFlowRateValues(int baseSatFlow)
        {
            _baseValue = baseSatFlow;
            _adjFact = new SatFlowRateAdjFactors();
        }

        public float BaseValuePCHrLane { get => _baseValue; set => _baseValue = value; }
        public float AdjustedValueVehHrLane { get => _adjustedValue; set => _adjustedValue = value; }
        public SatFlowRateAdjFactors AdjFact { get => _adjFact; set => _adjFact = value; }
    }

    /// <summary>
    /// Handles calculations related to finding the adjusted saturation flow rate.
    /// </summary>
    public static class SatFlowRateCalculations
    {
        /// <summary>
        /// Calculates the adjusted saturation flow rate using the HCM 2016 methodology.
        /// </summary>
        /// <param name="Area"></param>
        /// <param name="numLanes"></param>
        /// <param name="laneWidth"></param>
        /// <param name="pctHeavyVeh"></param>
        /// <param name="type"></param>
        /// <param name="PctLeftTurns"></param>
        /// <param name="PctRightTurns"></param>
        /// <param name="PctGrade"></param>
        /// <param name="baseSatFlow"></param>
        /// <returns></returns>
        public static SaturationFlowRateValues HCM2016(AreaType Area, int numLanes, float laneWidth, float pctHeavyVeh, LaneMovementsAllowed type, float PctLeftTurns, float PctRightTurns, float PctGrade, int baseSatFlow)
        {

            //See Equation 19-8 of HCM 2016

            SaturationFlowRateValues SatFlowRate = new SaturationFlowRateValues(baseSatFlow);

            //Lane width, Exhibit 19-20
            if (laneWidth < 10)
                SatFlowRate.AdjFact.LaneWidth = 0.96f;
            else if (laneWidth >= 10 && laneWidth <= 12.9)
                SatFlowRate.AdjFact.LaneWidth = 1.0f;
            else
                SatFlowRate.AdjFact.LaneWidth = 1.04f;


            //Heavy vehicles, Eqs. 19-9, 19-10
            if (PctGrade < 0)
                SatFlowRate.AdjFact.HeavyVehGrade = (float)(100 - 0.79 * pctHeavyVeh - 2.07 * PctGrade) / 100;
            else
                SatFlowRate.AdjFact.HeavyVehGrade = (float)(100 - 0.78 * pctHeavyVeh - 0.31 * Math.Pow(PctGrade, 2)) / 100;

            //Parking
            //To be implemented, leave at default value (1.0)

            //Bus blocking
            //To be implemented, leave at default value (1.0)

            //Area type
            //To be implemented, leave at default value (1.0)
           

            //Lane utilization (default value = 1.0)
            //Exhibit 19-15
            if (type == LaneMovementsAllowed.ThruOnly)
            {
                if (numLanes == 1)
                    SatFlowRate.AdjFact.LaneUtilization = 1.0f;
                else if (numLanes == 2)
                    SatFlowRate.AdjFact.LaneUtilization = 0.952f;
                else
                    SatFlowRate.AdjFact.LaneUtilization = 0.908f;
            }
            else if (type == LaneMovementsAllowed.LeftOnly)
            {
                if (numLanes == 1)
                    SatFlowRate.AdjFact.LaneUtilization = 1.0f;                
                else
                    SatFlowRate.AdjFact.LaneUtilization = 0.971f;
            }
            else if (type == LaneMovementsAllowed.RightOnly)
            {
                if (numLanes == 1)
                    SatFlowRate.AdjFact.LaneUtilization = 1.0f;
                else
                    SatFlowRate.AdjFact.LaneUtilization = 0.885f;
            }

            float FactorResult = SatFlowRate.AdjFact.LaneWidth * SatFlowRate.AdjFact.HeavyVehGrade * SatFlowRate.AdjFact.Parking * SatFlowRate.AdjFact.BusBlockage * SatFlowRate.AdjFact.AreaType * SatFlowRate.AdjFact.LaneUtilization;
            float SatFlowRateThruOnly = SatFlowRate.BaseValuePCHrLane * FactorResult;
            SatFlowRate.AdjustedValueVehHrLane = SatFlowRateThruOnly;


            if (type == LaneMovementsAllowed.ThruOnly)
            {
                //SatFlowRate.AdjFact.RightTurn = 1;
                //SatFlowRate.AdjustedValueVehHrLane *= SatFlowRate.AdjFact.RightTurn;
            }
            else if (type == LaneMovementsAllowed.ThruAndRightTurnBay || type == LaneMovementsAllowed.RightOnly)            
            {
                //Equation 19-13 (Exclusive or shared right turn lane, protected phase)                
                SatFlowRate.AdjFact.RightTurn = 1 / SatFlowRate.AdjFact.RightTurnEquivalency;
                SatFlowRate.AdjustedValueVehHrLane *= SatFlowRate.AdjFact.RightTurn;
            }            
            else if (type == LaneMovementsAllowed.ThruRightShared)
            {
                //Equation 31-105 (shared lane)
                float PropRightTurns = PctRightTurns / 100;  //this will be an input, based on simulation results, rather than per HCM Chapter 31, Section 2
                float PedBikeAdj = 1.0f;                

                SatFlowRate.AdjustedValueVehHrLane = SatFlowRateThruOnly / (1 + PropRightTurns * (SatFlowRate.AdjFact.RightTurnEquivalency / PedBikeAdj - 1));
            }            
            else if (type == LaneMovementsAllowed.ThruAndLeftTurnBay || type == LaneMovementsAllowed.LeftOnly)
            {
                //Equation 19-14 (Exclusive or shared left turn lane, protected phase)                
                SatFlowRate.AdjFact.LeftTurn = 1 / SatFlowRate.AdjFact.LeftTurnEquivalency;
                SatFlowRate.AdjustedValueVehHrLane *= SatFlowRate.AdjFact.LeftTurn;
            }
            else if (type == LaneMovementsAllowed.ThruLeftShared)
            {
                //Chapter 31 (starting on p. 52)
                
            }

            return SatFlowRate;
        }

        /// <summary>
        /// Calculates the adjusted saturation flow rate using the direct calculations.
        /// </summary>
        /// <param name="Mode"></param>
        /// <param name="Area"></param>
        /// <param name="OutsideLaneWidth"></param>
        /// <param name="NumOutsideLaneWidth"></param>
        /// <param name="ThruVol"></param>
        /// <param name="PctHeavyVeh"></param>
        /// <param name="NumLanes"></param>
        /// <param name="Median"></param>
        /// <param name="PostSpeed"></param>
        /// <param name="LTBayYN"></param>
        /// <param name="RTBayYN"></param>
        /// <param name="PctLT"></param>
        /// <param name="PctRT"></param>
        /// <param name="CycleLengthSec"></param>
        /// <param name="PctGrade"></param>
        /// <param name="BaseSatFlow"></param>
        /// <returns></returns>
        public static SaturationFlowRateValues Planning(ModeType Mode, AreaType Area, OutLaneWidth OutsideLaneWidth, int NumOutsideLaneWidth, float ThruVol, float PctHeavyVeh, float NumLanes, MedianType Median, float PostSpeed, bool LTBayYN, bool RTBayYN, float PctLT, float PctRT, int CycleLengthSec, float PctGrade, int BaseSatFlow)
        {

            //See Equation 19-8 of HCM 2016

            //float AdjSatFlow;
            SaturationFlowRateValues SatFlowRateFact = new SaturationFlowRateValues(BaseSatFlow);
            
            //SatFlowRateFact.BaseValue = 1950;
            SatFlowRateFact.AdjFact.fN = (float)(NumLanes / (NumLanes + 0.03));

            //Population should be > 0.0005 and < 2.0
            //Currently constrained to this range by Area Type list box
            SatFlowRateFact.AdjFact.Pop = SatFlowRateFact.AdjFact.AreaPopulationDefault[Convert.ToInt32(Area)];

            SatFlowRateFact.AdjFact.fAreaPopulation = (float)Math.Pow(SatFlowRateFact.AdjFact.Pop, 0.018);

            SatFlowRateFact.AdjFact.Vol = ThruVol;
            SatFlowRateFact.AdjFact.Vol = SatFlowRateFact.AdjFact.Vol * CycleLengthSec / (NumLanes * 3600);
            if (SatFlowRateFact.AdjFact.Vol > 30)
                SatFlowRateFact.AdjFact.Vol = 30;       //upper boundary specified by Bonneson
            SatFlowRateFact.AdjFact.fTP = (float)(1 / (1 - 0.0032 * (SatFlowRateFact.AdjFact.Vol - 20)));

            //PostSpeed -= 5;
            if (PostSpeed < 30)
                PostSpeed = 30;     //lower boundary specified by Bonneson, see p. 54 of Bonneson report
            if (PostSpeed > 55)
                PostSpeed = 55;     //upper boundary specified by Bonneson, see p. 54 of Bonneson report
            SatFlowRateFact.AdjFact.fSL = (float)(1 / (1 - 0.0066 * (PostSpeed - 50)));

            //Equation 19-13
            if (RTBayYN == true)
            {
                //fRT = 1;

                if (NumLanes > 1)
                {
                    if (PctRT < 2.5)
                        SatFlowRateFact.AdjFact.PctRTMultiplier = 0;
                    else if (PctRT > 30)
                        SatFlowRateFact.AdjFact.PctRTMultiplier = 0.140f;
                    else
                        SatFlowRateFact.AdjFact.PctRTMultiplier = (float)(0.00007 * Math.Pow(PctRT, 2) + 0.0004 * PctRT + 0.0611);
                    SatFlowRateFact.AdjFact.RightTurn = (float)(1 - (SatFlowRateFact.AdjFact.PctRTMultiplier * PctRT / 12));
                }
                else
                {
                    if (PctRT < 2.5)
                        SatFlowRateFact.AdjFact.PctRTMultiplier = 0;
                    else if (PctRT > 30)
                        SatFlowRateFact.AdjFact.PctRTMultiplier = 0.13f;
                    else
                        SatFlowRateFact.AdjFact.PctRTMultiplier = (float)(0.0001 * Math.Pow(PctRT, 2) + 0.0004 * PctRT + 0.0253);
                }
                SatFlowRateFact.AdjFact.RightTurn = (float)(1 - (SatFlowRateFact.AdjFact.PctRTMultiplier * PctRT / 12));
            }
            else
                SatFlowRateFact.AdjFact.RightTurn = (float)(1 / (1 + (PctRT / 100 * 0.07)));


            SatFlowRateFact.AdjFact.HeavyVeh = (float)(1 / (1 + (PctHeavyVeh / 100) * (2.3 - 1)));  //value of 2.3 comes from "Impact of Trucks on Arterial LOS" FDOT project, BD-545-51; previous E_T value was 1.74, based on Bonneson FDOT project

            SatFlowRateFact.AdjFact.fbay = 1;
            if (LTBayYN == false & PctLT != 0)  //do not apply penalty for no left turn lane if there is no left turning vehicles (e.g., a T-intersection)
                SatFlowRateFact.AdjFact.fbay = 0.8f;

            SatFlowRateFact.AdjFact.fmedian = 1;
            if (Median == MedianType.None)
                SatFlowRateFact.AdjFact.fmedian = 0.95f;

            //Find lane width factor for each segment

            //For auto only analysis, use normal lane width
            if (Mode == ModeType.AutoOnly)
                OutsideLaneWidth = OutLaneWidth.Typical;

            if (OutsideLaneWidth == OutLaneWidth.Narrow)
            {
                SatFlowRateFact.AdjFact.WidthOuterLane = 10;
                SatFlowRateFact.AdjFact.WidthInnerLane = 10;
            }
            else if (OutsideLaneWidth == OutLaneWidth.Typical)
            {
                SatFlowRateFact.AdjFact.WidthOuterLane = 12;
                SatFlowRateFact.AdjFact.WidthInnerLane = 12;
            }
            else if (OutsideLaneWidth == OutLaneWidth.Wide)
            {
                SatFlowRateFact.AdjFact.WidthOuterLane = 14;
                SatFlowRateFact.AdjFact.WidthInnerLane = 12;
            }
            else // (OutsideLaneWidth == OutLaneWidth.Custom)
            {
                SatFlowRateFact.AdjFact.WidthOuterLane = NumOutsideLaneWidth;
                if (SatFlowRateFact.AdjFact.WidthOuterLane >= 12)
                    SatFlowRateFact.AdjFact.WidthInnerLane = 12;
                else
                    SatFlowRateFact.AdjFact.WidthInnerLane = SatFlowRateFact.AdjFact.WidthOuterLane;
            }

            SatFlowRateFact.AdjFact.FactLaneWidth = 0;
            for (int j = 1; j <= Math.Floor(NumLanes + 0.5); j++)
            {
                if (j != Math.Floor(NumLanes + 0.5))
                    SatFlowRateFact.AdjFact.FactLaneWidth = SatFlowRateFact.AdjFact.FactLaneWidth + SatFlowRateFact.AdjFact.WidthInnerLane;
                else
                    SatFlowRateFact.AdjFact.FactLaneWidth = SatFlowRateFact.AdjFact.FactLaneWidth + SatFlowRateFact.AdjFact.WidthOuterLane;
            }
            SatFlowRateFact.AdjFact.FactLaneWidth = (float)(SatFlowRateFact.AdjFact.FactLaneWidth / Math.Floor(NumLanes + 0.5));
            SatFlowRateFact.AdjFact.FactLaneWidth = 1 + ((SatFlowRateFact.AdjFact.FactLaneWidth - 12) / 30);


            float LocalAdjFactor = SatFlowRateFact.AdjFact.fN * SatFlowRateFact.AdjFact.fAreaPopulation * SatFlowRateFact.AdjFact.fTP * SatFlowRateFact.AdjFact.fSL * SatFlowRateFact.AdjFact.RightTurn;
            SatFlowRateFact.AdjustedValueVehHrLane = LocalAdjFactor * SatFlowRateFact.AdjFact.HeavyVeh * SatFlowRateFact.AdjFact.fmedian * SatFlowRateFact.AdjFact.fbay * SatFlowRateFact.AdjFact.FactLaneWidth * SatFlowRateFact.BaseValuePCHrLane;
            return SatFlowRateFact;
        }

        public static float SupplementalPlatoonAdjFact(int arvtype)
        {
            float[] Fp = new float[6] { 1.0f, 0.93f, 1.0f, 1.15f, 1.0f, 1.0f };
            return Fp[arvtype - 1];   //values are indexed starting at zero
        }
    }
}
