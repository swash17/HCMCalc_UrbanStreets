using System;
using System.Collections.Generic;



namespace HCMCalc_UrbanStreets
{
    public class FDOTplanning_ParmRanges
    {
        //index 0 = Large Urbanized, 1 = Other Urbanized 2 = Transitioning, 3 = Rural Developed (Large Urbanized and Other Urbanized use the same default values)

        //arterial values
        public float[] AreaPopulationDefault = new float[4] { 1.5f, 0.4f, 0.03f, 0.003f };  //area population in millions
        //public  int[] ArtClassDefault = new int[4] { 2, 2, 2, 1 };
        public int[] ArtClassDefault = new int[4] { 1, 1, 1, 1 };
        public long[] AADTDefault = new long[4] { 34000, 30000, 25000, 10000 };
        public float[] KfactDefault = new float[4] { 0.09f, 0.09f, 0.09f, 0.095f };  // { 9.0, 9.0, 9.0, 9.5 }
        public float[] DfactDefault = new float[4] { 0.565f, 0.565f, 0.57f, 0.57f };  //{ 56.5, 56.5, 57.0, 57.0 }
        public float[] PHFDefault = new float[4] { 1.0f, 1.0f, 1.0f, 1.0f };   // { 0.925, 0.925, 0.910, 0.895 }
        public float[] PctHVDefault = new float[4] { 2.0f, 2.0f, 3.0f, 3.0f };
        public int[] MaxSerVol = new int[4] { 1000, 950, 900, 850 };

        //segment values
        //public  int[] SegLengthDefault = new int[4] { 1760, 1760, 1760, 2640 };
        public  int[] SegLengthDefault = new int[2] { 3500, 1000 }; //Class 1, Class 2        
        public  float[] PropSegWithCurbDefault = new float[4] { 1.0f, 1.0f, 0.5f, 0.0f };
        public  int[] MidBlockPctTurnsDefault = new int[4] { 7, 5, 3, 2 }; // { 8, 6, 4, 2 };
        public  int[] NumThruLanesDefault = new int[4] { 2, 2, 2, 1 };
        public  int[] PostedSpeedDefault = new int[2] { 45, 30 };  //45 for class 1, 30 for class 2
        public  int[] FFSpeedDefault = new int[2] { 50, 35 };
        public  MedianType[] MedianDefault = new MedianType[4] { MedianType.Restrictive, MedianType.Restrictive, MedianType.Restrictive, MedianType.Restrictive };

        //intersection values
        //public  SigControlType[,] SigControlDefault = new SigControlType[4, 5];     //First dimension is area type; Second dimension is class, which is numbered 1-4, so element 0 in array is ignored.
        public  SigControlType[] SigControlDefault = new SigControlType[3];     //Based on arterial class, which is numbered 1-2, so element 0 in array is ignored.
        //public  int[] CycleLengthDefault = new int[4] { 150, 150, 120, 90 };
        public  int[,] CycleLengthDefault = new int[4, 3];  //First dimension is area type; Second dimension is class, which is numbered 1-2, so element 0 in array is ignored.
        //public  int[] ArrTypeDefault = new int[4] { 4, 4, 4, 3 };
        //public  int[,] ArrTypeDefault = new int[4, 5];    //First dimension is area type; Second dimension is class, which is numbered 1-4, so element 0 in array is ignored.
        public  int[] ArrTypeDefault = new int[3] { 5, 4, 3 };  //function of control type (1-Pretimed, 2-CoordActuated, 3-FullyActuated)
        //public  float[] gCThruDefault = new float[4] { 0.44, 0.44, 0.44, 0.44 };
        public  float[] gCLeftDefault = new float[4] { 0.15f, 0.15f, 0.15f, 0.15f };
        public  int[] PctLTDefault = new int[4] { 12, 12, 12, 12 };
        public  int[] PctRTDefault = new int[4] { 12, 12, 12, 12 };
        public  bool[] ExclLTlaneDefault = new bool[4] { true, true, true, true };
        public  PhasingType LTphasing = PhasingType.Protected;
        public  int[] NumLTLanesDefault = new int[4] { 1, 1, 1, 1 };
        public  int[] LTbayLenDefault = new int[4] { 235, 235, 235, 235 };
        public  bool[] ExclRTlaneDefault = new bool[4] { false, false, false, false };
        public  int[] IntWidthDefault = new int[4] { 60, 60, 36, 24 };
        public  int[] NumRightTurnIslandsDefault = new int[4] { 0, 0, 0, 0 };  //{ 2, 1, 1, 0 }

        //multimodal values
        public  int SidePathSepDefault = 20;
        public  int[] BusFreqDefault = new int[4] { 2, 2, 0, 0 };
        public  int[] BusSpanServDefault = new int[4] { 15, 15, 0, 0 };
        public  float[] PassLoadFactor = new float[4] { 0.8f, 0.6f, 0.4f, 0.2f };
        //public  AmenitiesLevel[] AmenitiesDefault = new AmenitiesLevel[4] { AmenitiesLevel.Excellent, AmenitiesLevel.Good, AmenitiesLevel.Fair, AmenitiesLevel.Poor };

        //Minimum and Maximum values
        //intersection data
        public const int CycleMin = 15;
        public const int CycleMax = 240;
        public const float gCthruMin = 0.10f;
        public const float gCthruMax = 1.0f;
        public const float gCleftMin = 0.01f;
        public const float gCleftMax = 0.25f;
        public const int NumIntThruLanesMin = 1;
        public const int NumIntThruLanesMax = 4;
        public const int LTBayLenMin = 50;
        public const int LTBayLenMax = 1600;
        public const int PctLTMin = 0;
        public const int PctLTMax = 45;
        public const int PctRTMin = 0;
        public const int PctRTMax = 45;
        //segment data
        public const float KfactStdMin = 9;   //0.09;
        public const float KfactStdMax = 15;   //0.15;
        public const float KfactOtherMin = 1;   //0.01;
        public const float KfactOtherMax = 15;   //0.15;
        public const float DfactMin = 50.1f;   //0.501;
        public const float DfactMax = 100;    //1.0;
        public const float PHFMin = 0.75f;
        public const float PHFMax = 1.0f;
        public const int LengthMin = 100;
        public const int LengthMax = 15840;    //3 miles
        public const int AADTmin = 1000;
        public const int AADTmax = 100000;
        public const int DDHVmin = 5;
        public const int DDHVMax = 15000;
        public const int FFSmin = 25;
        public const int FFSmax = 60;
        //multimodal values
        public const int LaneWidthMin = 8;
        public const int LaneWidthMax = 16;
        public const int SidePathSepMin = 1;
        public const int SidePathSepMax = 40;
        public const float PassLoadFactMin = 0;
        public const float PassLoadFactMax = 3;
        public const int BusFreqMin = 0;
        public const int BusFreqMax = 65;
        public const int BusServSpanMin = 0;
        public const int BusServSpanMax = 24;
        public const int PedSubsegPctMin = 0;
        public const int PedSubsegPctMax = 100;

        public const string VersionDate = "6/29/2020";
        //public  string VersionDate = DateTime.Now.ToShortDateString();

        public  int[] ThreshDelay = new int[6];
        public  int[] ThreshSpeed = new int[5];
        public  float[] ThreshBikePed = new float[6];


        public void GetLOSBoundariesHCM2016(float baseFreeFlowSpeed)
        {
            if (baseFreeFlowSpeed == 55)
            {
                ThreshSpeed[0] = 44;
                ThreshSpeed[1] = 37;
                ThreshSpeed[2] = 28;
                ThreshSpeed[3] = 22;
                ThreshSpeed[4] = 17;
            }
            else if (baseFreeFlowSpeed == 50)
            {
                ThreshSpeed[0] = 40;
                ThreshSpeed[1] = 34;
                ThreshSpeed[2] = 25;
                ThreshSpeed[3] = 20;
                ThreshSpeed[4] = 15;
            }
            else if (baseFreeFlowSpeed == 45)
            {
                ThreshSpeed[0] = 36;
                ThreshSpeed[1] = 30;
                ThreshSpeed[2] = 23;
                ThreshSpeed[3] = 18;
                ThreshSpeed[4] = 14;
            }
            else if (baseFreeFlowSpeed == 40)
            {
                ThreshSpeed[0] = 32;
                ThreshSpeed[1] = 27;
                ThreshSpeed[2] = 20;
                ThreshSpeed[3] = 16;
                ThreshSpeed[4] = 12;
            }
            else if (baseFreeFlowSpeed == 35)
            {
                ThreshSpeed[0] = 28;
                ThreshSpeed[1] = 23;
                ThreshSpeed[2] = 18;
                ThreshSpeed[3] = 14;
                ThreshSpeed[4] = 11;
            }
            else if (baseFreeFlowSpeed == 30)
            {
                ThreshSpeed[0] = 24;
                ThreshSpeed[1] = 20;
                ThreshSpeed[2] = 15;
                ThreshSpeed[3] = 12;
                ThreshSpeed[4] = 9;
            }
            else if (baseFreeFlowSpeed == 25)
            {
                ThreshSpeed[0] = 20;
                ThreshSpeed[1] = 17;
                ThreshSpeed[2] = 13;
                ThreshSpeed[3] = 10;
                ThreshSpeed[4] = 8;
            }
        }


        public void GetLOSBoundaries(AreaType area, int artClass)
        {
            //Set Control Delay boundaries for LOS A through E, based on Class and Area Type
            if (area == AreaType.RuralDeveloped)
            {
                ThreshDelay[0] = 5;
                ThreshDelay[1] = 10;
                ThreshDelay[2] = 15;
                ThreshDelay[3] = 25;
                ThreshDelay[4] = 40;
            }
            else
            {
                ThreshDelay[0] = 10;
                ThreshDelay[1] = 20;
                ThreshDelay[2] = 35;
                ThreshDelay[3] = 55;
                ThreshDelay[4] = 80;
            }

            //Set Speed boundaries for LOS A through E, based on Class
            if (artClass == 1)  //(Posted Speed >= 40)
            {
                ThreshSpeed[0] = 40;
                ThreshSpeed[1] = 31;
                ThreshSpeed[2] = 23;
                ThreshSpeed[3] = 18;
                ThreshSpeed[4] = 15;
            }
            else // (artClass == 2)  //(Posted Speed < 40)
            {
                ThreshSpeed[0] = 28;
                ThreshSpeed[1] = 22;
                ThreshSpeed[2] = 17;
                ThreshSpeed[3] = 13;
                ThreshSpeed[4] = 10;
            }

            /*
            if (LOSCrit == LOSCcriteria.HCM2010)
            {
                if (SpeedRatio > 0.85)
                    LOS = "A";
                else if (SpeedRatio > 0.67)
                    LOS = "B";
                else if (SpeedRatio > 0.50)
                    LOS = "C";
                else if (SpeedRatio > 0.40)
                    LOS = "D";
                else if (SpeedRatio > 0.30)
                    LOS = "E";
                else
                    LOS = "F";
            }
            */
            /*
             * FDOT 2 Class using speed ratio
                //Class 1
                if (SpeedRatio > 0.80)
                    LOS = "A";
                else if (SpeedRatio > 0.65)
                    LOS = "B";
                else if (SpeedRatio > 0.50)
                    LOS = "C";
                else if (SpeedRatio > 0.40)
                    LOS = "D";
                else if (SpeedRatio > 0.30)
                    LOS = "E";
                else
                    LOS = "F";
            }
            else
            {
                //Class 2
                if (SpeedRatio > 0.60)
                    LOS = "A";
                else if (SpeedRatio > 0.45)
                    LOS = "B";
                else if (SpeedRatio > 0.40)
                    LOS = "C";
                else if (SpeedRatio > 0.30)
                    LOS = "D";
                else if (SpeedRatio > 0.20)
                    LOS = "E";
                else
                    LOS = "F";
            } */
            /*
            if (LOSCrit == LOSCcriteria.FDOT2ClassLength)    //FDOT 2-Class thresholds
            {
                if (Class == 1)   //(AvgSegLength > 1760)   //3.0 signals per mile (5280/3 = 1760, or 0.333 miles)      //2.5 signals per mile (5280/2.5 = 2112 ft, or 0.4 miles)
                {
                    ThreshSpeed[1] = 36;
                    ThreshSpeed[2] = 31;
                    ThreshSpeed[3] = 21;
                    ThreshSpeed[4] = 16;
                    ThreshSpeed[5] = 12;                    
                }
                else  //Class 2
                {
                    ThreshSpeed[1] = 25;
                    ThreshSpeed[2] = 18;
                    ThreshSpeed[3] = 15;
                    ThreshSpeed[4] = 11;
                    ThreshSpeed[5] = 7;                    
                }                    
            }            
            */

            ThreshBikePed[1] = 2.0f;
            ThreshBikePed[2] = 2.75f;
            ThreshBikePed[3] = 3.5f;
            ThreshBikePed[4] = 4.25f;
            ThreshBikePed[5] = 5.0f;
        }

        public  void GetLOSBoundariesHCM2000(AreaType area, int artClass)
        {
            //Set Control Delay boundaries for LOS A through E, based on Class and Area Type
            if (area == AreaType.RuralDeveloped)
            {
                ThreshDelay[1] = 5;
                ThreshDelay[2] = 10;
                ThreshDelay[3] = 15;
                ThreshDelay[4] = 25;
                ThreshDelay[5] = 40;
            }
            else
            {
                ThreshDelay[1] = 10;
                ThreshDelay[2] = 20;
                ThreshDelay[3] = 35;
                ThreshDelay[4] = 55;
                ThreshDelay[5] = 80;
            }

            //Set Speed boundaries for LOS A through E, based on Class and Area Type
            if (area == AreaType.RuralDeveloped || artClass == 1)
            {
                ThreshSpeed[1] = 42;
                ThreshSpeed[2] = 34;
                ThreshSpeed[3] = 27;
                ThreshSpeed[4] = 21;
                ThreshSpeed[5] = 16;
            }
            else if (artClass == 2)
            {
                ThreshSpeed[1] = 35;
                ThreshSpeed[2] = 28;
                ThreshSpeed[3] = 22;
                ThreshSpeed[4] = 17;
                ThreshSpeed[5] = 13;
            }
            else if (artClass == 3)
            {
                ThreshSpeed[1] = 30;
                ThreshSpeed[2] = 24;
                ThreshSpeed[3] = 18;
                ThreshSpeed[4] = 14;
                ThreshSpeed[5] = 10;
            }
            else if (artClass == 4)
            {
                ThreshSpeed[1] = 25;
                ThreshSpeed[2] = 19;
                ThreshSpeed[3] = 13;
                ThreshSpeed[4] = 9;
                ThreshSpeed[5] = 7;
            }
        }



    }
}
