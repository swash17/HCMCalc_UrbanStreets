using System;
using System.Collections.Generic;


namespace HCMCalc_UrbanStreets
{
    class ThresholdData
    {
        int[] _Speed;
        int[] _Delay;

        public ThresholdData()
        {
            _Speed = new int[5];
            _Delay = new int[5];
        }

        public ThresholdData(AreaType SerVolAreaType, int baseFreeFlowSpeed)
        {
            _Speed = new int[5];
            _Delay = new int[5];

            Speed = GetLOSBoundaries_Speed(baseFreeFlowSpeed);
            Delay = GetLOSBoundaries_Delay(SerVolAreaType);
        }



        public int[] Speed { get => _Speed; set => _Speed = value; }
        public int[] Delay { get => _Delay; set => _Delay = value; }

        public int[] GetLOSBoundaries_Speed(float baseFreeFlowSpeed, AnalysisMode AnalMode = AnalysisMode.Planning)
        {
            //40, 31, 23, 18, 15 for posted speed >= 40
            //28, 22, 17, 13, 10 for posted speed < 40

            if (AnalMode == AnalysisMode.Planning)
            {
                if (baseFreeFlowSpeed >= 40)
                {
                    Speed[0] = 40;
                    Speed[1] = 31;
                    Speed[2] = 23;
                    Speed[3] = 18;
                    Speed[4] = 15;
                }
                else if (baseFreeFlowSpeed < 40)
                {
                    Speed[0] = 28;
                    Speed[1] = 22;
                    Speed[2] = 17;
                    Speed[3] = 13;
                    Speed[4] = 10;
                }
            }
            else
            {
                if (baseFreeFlowSpeed == 55)
                {
                    Speed[0] = 44;
                    Speed[1] = 37;
                    Speed[2] = 28;
                    Speed[3] = 22;
                    Speed[4] = 17;
                }
                else if (baseFreeFlowSpeed == 50)
                {
                    Speed[0] = 40;
                    Speed[1] = 34;
                    Speed[2] = 25;
                    Speed[3] = 20;
                    Speed[4] = 15;
                }
                else if (baseFreeFlowSpeed == 45)
                {
                    Speed[0] = 36;
                    Speed[1] = 30;
                    Speed[2] = 23;
                    Speed[3] = 18;
                    Speed[4] = 14;
                }
                else if (baseFreeFlowSpeed == 40)
                {
                    Speed[0] = 32;
                    Speed[1] = 27;
                    Speed[2] = 20;
                    Speed[3] = 16;
                    Speed[4] = 12;
                }
                else if (baseFreeFlowSpeed == 35)
                {
                    Speed[0] = 28;
                    Speed[1] = 23;
                    Speed[2] = 18;
                    Speed[3] = 14;
                    Speed[4] = 11;
                }
                else if (baseFreeFlowSpeed == 30)
                {
                    Speed[0] = 24;
                    Speed[1] = 20;
                    Speed[2] = 15;
                    Speed[3] = 12;
                    Speed[4] = 9;
                }
                else if (baseFreeFlowSpeed == 25)
                {
                    Speed[0] = 20;
                    Speed[1] = 17;
                    Speed[2] = 13;
                    Speed[3] = 10;
                    Speed[4] = 8;
                }
            }
            return Speed;
        }

        public int[] GetLOSBoundaries_Delay(AreaType area)
        {
            //Set Control Delay boundaries for LOS A through E, based on Class and Area Type
            if (area == AreaType.RuralDeveloped)
            {
                Delay[0] = 5;
                Delay[1] = 10;
                Delay[2] = 15;
                Delay[3] = 25;
                Delay[4] = 40;
            }
            else
            {
                Delay[0] = 10;
                Delay[1] = 20;
                Delay[2] = 35;
                Delay[3] = 55;
                Delay[4] = 80;
            }
            return Delay;
        }
    }
}
