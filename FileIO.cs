using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;

namespace HCMCalc_UrbanStreets
{
    class FileInputOutput
    {
        // Illegal XML characters: < (less than), > (greater than), & (ampersand), ' (apostrophe), " (quotation mark)

        public void ReadXmlFile(string filename, ProjectData project, ArterialData Art, List<IntersectionData> ints, List<LinkData> segs)
        {
            XmlTextReader xtr = new XmlTextReader(filename);
            int Index = 0;      //index for intersection number
            //int SubSegNum = 0;  //index for ped subsegment number
            int TotInts = 1;    //total number of intersections

            IntersectionData newInt = new IntersectionData();  // (Art.Area, Art.Classification, Art.SigControl);  //first intersection is "dummy" intersection
            ints.Add(newInt);

            LinkData newSeg = new LinkData();  // (Art.Area, Art.Classification);    //first segment is "dummy" segment
            segs.Add(newSeg);            

            while (xtr.Read())
            {
                if (xtr.NodeType == XmlNodeType.Whitespace)
                    continue;

                if (xtr.IsStartElement("PROJECT"))
                {
                    while (xtr.Read())
                    {
                        if (xtr.NodeType == XmlNodeType.EndElement)
                            if (xtr.Name == "PROJECT")
                                break;

                        switch (xtr.Name)
                        {
                            //Project Data Elements
                            case "FileName":
                                break;
                            case "Analyst":
                                project.AnalystName = xtr.ReadElementContentAsString();
                                break;
                            case "Date":
                                //project.AnalDate = xtr.ReadElementContentAsDateTime();
                                project.AnalysisDate = Convert.ToDateTime(xtr.ReadElementContentAsString());
                                break;
                            case "Agency":
                                project.Agency = xtr.ReadElementContentAsString();
                                break;
                            case "AgencyName":  //for compatibality with Artplan 2007 files
                                project.Agency = xtr.ReadElementContentAsString();
                                break;
                            case "Comment":
                                project.UserNotes = xtr.ReadElementContentAsString();
                                break;
                            case "PeriodID":
                                string period = xtr.ReadElementContentAsString();
                                if (period == "Standard K" || period == "K100")
                                    project.Period = StudyPeriod.StandardK;
                                else if (period == "Kother")
                                    project.Period = StudyPeriod.Kother;
                                else if (period == "Dir Hr Demand Vol")
                                    project.Period = StudyPeriod.PeakHour;
                                break;
                            case "AnalysisType":
                                string AnalType = xtr.ReadElementContentAsString();
                                if (AnalType == "Peak Direction")
                                    project.DirectionAnalysisMode = DirectionType.PeakDirection;
                                else if (AnalType == "Peak and Off-Peak Directions")
                                    project.DirectionAnalysisMode = DirectionType.BothDirections;
                                break;
                            case "ModalAnalysis":
                                string Mode = xtr.ReadElementContentAsString();
                                if (Mode == "Multimodal")
                                    project.Mode = ModeType.Multimodal;
                                else if (Mode == "Auto Only")
                                    project.Mode = ModeType.AutoOnly;
                                else if (Mode == "Isolated Signal")
                                    project.Mode = ModeType.SignalOnly;
                                break;
                            case "MultiModal":  //for compatibality with Artplan 2007 files
                                string Mode2 = xtr.ReadElementContentAsString();
                                if (Mode2 == "True")
                                    project.Mode = ModeType.Multimodal;
                                else
                                    project.Mode = ModeType.AutoOnly;
                                //signal only analysis was handled through Class field in Artplan 2007
                                //signal only analysis files are not handled because all the inputs were handled through the general facility data screen (i.e., ArterialInfo), which no longer exists
                                break;
                        }
                    }
                }
                else if (xtr.IsStartElement("ARTERIALINFO"))
                {
                    while (xtr.Read())
                    {
                        if (xtr.NodeType == XmlNodeType.EndElement)
                            if (xtr.Name == "ARTERIALINFO")
                                break;

                        switch (xtr.Name)
                        {
                            //Arterial Values
                            case "ArterialName":
                                Art.ArtName = xtr.ReadElementContentAsString();
                                break;
                            case "From":
                                Art.From = xtr.ReadElementContentAsString();
                                break;
                            case "To":
                                Art.To = xtr.ReadElementContentAsString();
                                break;
                            case "FwdDirection":
                                string peakDir = xtr.ReadElementContentAsString();
                                if (peakDir == "Northbound")
                                    Art.AnalysisTravelDir = TravelDirection.Northbound;
                                else if (peakDir == "Southbound")
                                    Art.AnalysisTravelDir = TravelDirection.Southbound;
                                else if (peakDir == "Eastbound")
                                    Art.AnalysisTravelDir = TravelDirection.Eastbound;
                                else if (peakDir == "Westbound")
                                    Art.AnalysisTravelDir = TravelDirection.Westbound;
                                break;
                            case "AreaType":
                                string areaType = xtr.ReadElementContentAsString();
                                if (areaType == "Large Urbanized")
                                    Art.Area = AreaType.LargeUrbanized;
                                else if (areaType == "Other Urbanized")
                                    Art.Area = AreaType.OtherUrbanized;
                                else if (areaType == "Transitioning/Urban")
                                    Art.Area = AreaType.Transitioning;
                                else if (areaType == "Rural Developed")
                                    Art.Area = AreaType.RuralDeveloped;
                                break;
                            case "ArterialClass_HCM":
                                Art.Classification = (ArterialClass)xtr.ReadElementContentAsInt();
                                if ((int)Art.Classification > 2)   //code to handle legacy projects that used 4 classes
                                    Art.Classification = ArterialClass.Class_II;
                                break;
                            case "ArtLength":
                                Art.LengthMiles = xtr.ReadElementContentAsFloat();
                                break;
                            case "KFactor_PLN":
                                Art.Kfactor = xtr.ReadElementContentAsFloat();
                                break;
                            case "DFactor_PLN":
                                Art.Dfactor = xtr.ReadElementContentAsFloat();
                                break;
                            case "PHF":
                                //Art.PHF = xtr.ReadElementContentAsFloat();
                                break;
                            case "BaseSatFlowPerLane":
                                //Art.BaseSatFlow = xtr.ReadElementContentAsInt();
                                break;
                            case "NumberOfIntersections":
                                TotInts = xtr.ReadElementContentAsInt();
                                break;
                        }
                    }
                }

                else if (xtr.IsStartElement("ARTERIALDIR")) // || xtr.IsStartElement("APPROACH")) //The 'APPROACH ' tag is to provide support to Artplan 2007 version
                {
                    while (xtr.Read())
                    {
                        if (xtr.NodeType == XmlNodeType.EndElement)
                            if (xtr.Name == "ARTERIALDIR") // || xtr.Name == "APPROACH")
                                break;

                        switch (xtr.Name)
                        {
                            //Direction specific approach items
                            case "HVPct":
                                //Art.PctHeavyVeh = xtr.ReadElementContentAsFloat();
                                break;
                        }
                    }
                }
                
                else if (xtr.IsStartElement("INTERSECTIONINFO"))
                {
                    while (xtr.Read())
                    {
                        if (xtr.NodeType == XmlNodeType.EndElement)
                            if (xtr.Name == "INTERSECTIONINFO")
                                break;

                        newInt = new IntersectionData(1, Art.Area, Art.Classification, null, new SignalCycleData());
                        ints.Add(newInt);
                        newSeg = new LinkData();
                        segs.Add(newSeg);  

                        switch (xtr.Name)
                        {
                            //Intersection Data Elements
                            case "CrossStreetName":
                                Index++;    //increment intersection counter
                                ints[Index].CrossStreetName = xtr.ReadElementContentAsString();
                                break;
                        }
                    }
                }
                else if (xtr.IsStartElement("CONTROLLER"))
                {
                    while (xtr.Read())
                    {
                        if (xtr.NodeType == XmlNodeType.EndElement)
                            if (xtr.Name == "CONTROLLER")
                                break;

                        switch (xtr.Name)
                        {
                            case "ControlMode":
                                string controlMode = xtr.ReadElementContentAsString();
                                //if (controlMode == "Pretimed")
                                //    Art.SigControl = SigControlType.Pretimed;
                                //else if (controlMode == "CoordinatedActuated")
                                //    Art.SigControl = SigControlType.CoordinatedActuated;
                                //else if (controlMode == "FullyActuated")
                                //    Art.SigControl = SigControlType.FullyActuated;
                                break;
                            case "CycleLength":
                                //ints[Index].CycleLen = xtr.ReadElementContentAsInt();
                                break;
                            case "LeftTurnPhasing":
                                string LTphasing = xtr.ReadElementContentAsString();
                                //if (LTphasing == "Protected")
                                //    ints[Index].LTphasing = PhasingType.Protect;
                                //else if (LTphasing == "ProtPerm")
                                //    ints[Index].LTphasing = PhasingType.ProtPerm;
                                //else
                                //    ints[Index].LTphasing = PhasingType.None;
                                break;
                        }
                    }
                }
                else if (xtr.IsStartElement("LANEGROUP"))
                {
                    while (xtr.Read())
                    {
                        if (xtr.NodeType == XmlNodeType.EndElement)

                            if (xtr.Name == "LANEGROUP")
                            {
                                //SubSegNum = 0;  //reset subsegment counter for next segment
                                break;
                            }

                        switch (xtr.Name)
                        {
                            case "GCRatio":
                                ints[Index].Approaches[0].LaneGroups[0].SignalPhase.gC = xtr.ReadElementContentAsFloat();
                                break;
                            case "LeftTurnBayYN":
                                string LTBay = xtr.ReadElementContentAsString();
                                //if (LTBay == "No")
                                //    ints[Index].isLTbay = false;
                                //else
                                //    ints[Index].isLTbay = true;
                                break;
                            case "NumberLTlanes":
                                string LTlanes = xtr.ReadElementContentAsString();
                                //if (LTlanes != "N/A")
                                //    ints[Index].NumLTlanes = Convert.ToInt32(LTlanes);
                                break;
                            case "LTBayLength":
                                string LTbayLen = xtr.ReadElementContentAsString();
                                //if (LTbayLen != "N/A")
                                //    ints[Index].LTbayLen = Convert.ToInt32(LTbayLen);
                                break;
                            case "GCRatioLT":
                                string LTgCratio = xtr.ReadElementContentAsString();
                                //if (LTgCratio != "N/A")
                                //    ints[Index].gClt = Convert.ToSingle(LTgCratio);
                                break;
                            case "RightTurnBayYN":
                                string RTBay = xtr.ReadElementContentAsString();
                                //if (RTBay == "No")
                                //    ints[Index].isRTbay = false;
                                //else
                                //    ints[Index].isRTbay = true;
                                break;
                            case "PctTurn_Left":
                                //ints[Index].PctLT = xtr.ReadElementContentAsInt();
                                break;
                            case "PctTurn_Right":
                                //nts[Index].PctRT = xtr.ReadElementContentAsInt();
                                break;
                            case "ArrivalType":
                                //ints[Index].ArvType = xtr.ReadElementContentAsInt();
                                break;
                            case "NumberOfLanes_INT":
                                //ints[Index].NumThLanes = xtr.ReadElementContentAsFloat();
                                break;
                            //Segment Data Elements
                            case "LinkLength":
                                //subtract 1 from index since there is one less segment than # of intersections
                                float SegLength = xtr.ReadElementContentAsFloat();
                                if (SegLength < 10)
                                    segs[Index - 1].LengthFt = SegLength * 5280;
                                else
                                    segs[Index - 1].LengthFt = SegLength;
                                break;
                            case "AADT":
                                segs[Index - 1].AADT = xtr.ReadElementContentAsLong();
                                break;
                            case "DDHV":
                                segs[Index - 1].DDHV = xtr.ReadElementContentAsFloat();
                                break;
                            case "NumberOfLanes_SEG":
                                segs[Index - 1].NumLanes = xtr.ReadElementContentAsInt();
                                break;
                            case "PostedSpeed":
                                segs[Index - 1].PostSpeedMPH = xtr.ReadElementContentAsInt();
                                break;
                            case "FreeFlowSpeed":
                                //segs[Index - 1].FFSpeed = xtr.ReadElementContentAsFloat();
                                break;
                            case "MedianType":
                                string MedType = xtr.ReadElementContentAsString();
                                if (MedType == "None")
                                    segs[Index - 1].MedType = MedianType.None;
                                else if (MedType == "Non-Restrictive")
                                    segs[Index - 1].MedType = MedianType.Nonrestrictive;
                                else if (MedType == "Restrictive")
                                    segs[Index - 1].MedType = MedianType.Restrictive;
                                //SegNum++;
                                break;
                            case "OnStreetParkingYN":
                                string OnStreetParkingYN = xtr.ReadElementContentAsString();
                                if (OnStreetParkingYN == "No")
                                    segs[Index - 1].OnStreetParkingExists = false;
                                else
                                    segs[Index - 1].OnStreetParkingExists = true;
                                break;
                            case "ParkingActivity":
                                string ParkingActivity = xtr.ReadElementContentAsString();
                                if (ParkingActivity == "Low")
                                    segs[Index - 1].ParkingActivity = ParkingActivityLevel.Low;
                                else if (ParkingActivity == "Medium")
                                    segs[Index - 1].ParkingActivity = ParkingActivityLevel.Medium;
                                else if (ParkingActivity == "High")
                                    segs[Index - 1].ParkingActivity = ParkingActivityLevel.High;
                                else  // N/A (on-street parking not present)
                                    segs[Index - 1].ParkingActivity = ParkingActivityLevel.NotApplicable;
                                break;

                            //Multimodal Segment Data Elements
                            case "OutsideLnWidth":
                                string LaneWidth = xtr.ReadElementContentAsString();
                                if (LaneWidth == "Narrow")
                                    segs[Index - 1].OutsideLaneWidth = OutLaneWidth.Narrow;
                                else if (LaneWidth == "Typical")
                                    segs[Index - 1].OutsideLaneWidth = OutLaneWidth.Typical;
                                else if (LaneWidth == "Wide")
                                    segs[Index - 1].OutsideLaneWidth = OutLaneWidth.Wide;
                                else  //custom
                                {
                                    segs[Index - 1].OutsideLaneWidth = OutLaneWidth.Custom;
                                    //Seg[Index - 1].NumOutsideLaneWidth = Convert.ToInt32(LaneWidth);
                                }
                                break;
                            case "PavementCondition":
                                string PaveCond = xtr.ReadElementContentAsString();
                                //if (PaveCond == "Desirable")
                                //    Seg[Index - 1].PaveCond = PavementCondition.Desirable;
                                //else if (PaveCond == "Typical")
                                //    Seg[Index - 1].PaveCond = PavementCondition.Typical;
                                //else if (PaveCond == "Undesirable")
                                //    Seg[Index - 1].PaveCond = PavementCondition.Undesirable;
                                break;
                            case "BikeLnYN":
                                string BikeLaneYN = xtr.ReadElementContentAsString();
                                //if (BikeLaneYN == "No")
                                //    SegMM[Index - 1].BikeLaneExists = false;
                                //else
                                //    SegMM[Index - 1].BikeLaneExists = true;
                                break;                                                        
                            case "SidewalkYN":
                                string SidewalkYN = xtr.ReadElementContentAsString();
                                //if (SidewalkYN == "No")
                                //    SegMM[Index - 1].SidewalkExists = false;
                                //else
                                //    SegMM[Index - 1].SidewalkExists = true;
                                break;                            
                                                        

                        }
                    }
                }                
            }
            /*
            if (Index == TotInts + 1)
            {
                xtr.Close();
                Art.TotalInts = Index;
                Art.TotalSegs = Index - 1;
                return;
            }
            */
            Art.TotalInts = Index;
            Art.TotalSegs = Index - 1;
            xtr.Close();
        }


        public void WriteXmlFile(string filename, bool previewMode, ProjectData Project, ArterialData Art, List<IntersectionData> Int, List<LinkData> Seg, ServiceVolumes SerVol, bool overVCMessage, bool writeResults)
        {

            XmlTextWriter xtw = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
            //indent tags by 2 characters
            xtw.Formatting = Formatting.Indented;
            xtw.Indentation = 2;
            //enclose attributes' values in float quotes
            xtw.QuoteChar = '"';
            xtw.WriteStartDocument(true);
            if (previewMode == true)
            {
                string XSLfileName = XSLfileName = Application.StartupPath + "\\APauto1.xsl";                
                string InstructionText = "type=\"text/xsl\" href=\"" + XSLfileName + "\"";
                xtw.WriteProcessingInstruction("xml-stylesheet", InstructionText);
            }
            //write <TMML> information
            xtw.WriteStartElement("TMML");
            xtw.WriteAttributeString("Facility", "Arterial");
            //write <PROJECT> information
            xtw.WriteStartElement("PROJECT");
            xtw.WriteElementString("FileName", Project.FileName);  //("FileName", filename)
            xtw.WriteElementString("Analyst", Project.AnalystName);
            xtw.WriteElementString("Date", Project.AnalysisDate.ToString());
            xtw.WriteElementString("Agency", Project.Agency);
            xtw.WriteElementString("Comment", Project.UserNotes);

            if (Project.Mode == ModeType.AutoOnly)
                xtw.WriteElementString("ModalAnalysis", "Auto Only");
            else if (Project.Mode == ModeType.SignalOnly)
                xtw.WriteElementString("ModalAnalysis", "Isolated Signal");

            if (Project.DirectionAnalysisMode == DirectionType.PeakDirection)
                xtw.WriteElementString("AnalysisType", "Peak Direction");
            else if (Project.DirectionAnalysisMode == DirectionType.BothDirections)
                xtw.WriteElementString("AnalysisType", "Peak and Off-Peak Directions");

            if (Project.Period == StudyPeriod.StandardK)
                xtw.WriteElementString("PeriodID", "Standard K");
            else if (Project.Period == StudyPeriod.Kother)
                xtw.WriteElementString("PeriodID", "Kother");
            else if (Project.Period == StudyPeriod.PeakHour)
                xtw.WriteElementString("PeriodID", "Dir Hr Demand Vol");

            xtw.WriteElementString("Program", "ARTPLAN 2012");
            xtw.WriteElementString("Version", FDOTplanning_ParmRanges.VersionDate);

            xtw.WriteEndElement();  //close the <PROJECT> tag

            //write <AnalysisParamters> information
            //** maybe not include since this information is never used (maybe by HCS)

            //write <ARTERIAL> information
            xtw.WriteStartElement("ARTERIAL");
            xtw.WriteAttributeString("id", "1");
            //write <ARTERIALINFO> information
            xtw.WriteStartElement("ARTERIALINFO");
            xtw.WriteElementString("ArterialName", Art.ArtName);
            xtw.WriteElementString("From", Art.From);
            xtw.WriteElementString("To", Art.To);

            if (Art.AnalysisTravelDir == TravelDirection.Northbound)
                xtw.WriteElementString("FwdDirection", "Northbound");
            else if (Art.AnalysisTravelDir == TravelDirection.Southbound)
                xtw.WriteElementString("FwdDirection", "Southbound");
            else if (Art.AnalysisTravelDir == TravelDirection.Westbound)
                xtw.WriteElementString("FwdDirection", "Westbound");
            else if (Art.AnalysisTravelDir == TravelDirection.Eastbound)
                xtw.WriteElementString("FwdDirection", "Eastbound");

            if (Art.Area == AreaType.LargeUrbanized)
                xtw.WriteElementString("AreaType", "Large Urbanized");
            else if (Art.Area == AreaType.OtherUrbanized)
                xtw.WriteElementString("AreaType", "Other Urbanized");
            else if (Art.Area == AreaType.Transitioning)
                xtw.WriteElementString("AreaType", "Transitioning/Urban");
            else if (Art.Area == AreaType.RuralDeveloped)
                xtw.WriteElementString("AreaType", "Rural Developed");

            xtw.WriteElementString("ArterialClass_HCM", Art.Classification.ToString());
            xtw.WriteElementString("KFactor_PLN", Art.Kfactor.ToString());
            xtw.WriteElementString("DFactor_PLN", Art.Dfactor.ToString());
            //xtw.WriteElementString("PHF", Art.PHF.ToString());
            //xtw.WriteElementString("BaseSatFlowPerLane", Art.Segments[0].Intersection.Approaches[0].LaneGroups[0].BaseSaturationFlowRate.ToString());
            xtw.WriteElementString("NumberOfIntersections", Convert.ToString(Art.TotalInts - 1));
            if (Project.Mode != ModeType.SignalOnly)
                xtw.WriteElementString("ArtLength", Art.LengthMiles.ToString("0.0000"));
            else
                xtw.WriteElementString("ArtLength", "N/A");
            xtw.WriteElementString("MaxSerVol", Art.MaxSerVol.ToString());
            xtw.WriteEndElement();  //close the <ARTERIALINFO> tag

            //write <APPROACH> information
            xtw.WriteStartElement("ARTERIALDIR");
            xtw.WriteAttributeString("id", "forward");
            //xtw.WriteElementString("NumberOfLanes", Art.);
            //xtw.WriteElementString("HVPct", Art.PctHeavyVeh.ToString());

            //only write results to file if results are currently displayed
            if (writeResults == true)
            {
                //write <MOEGROUP> information
                xtw.WriteStartElement("MOEGROUP");
                if (Project.Mode != ModeType.SignalOnly)
                {
                    xtw.WriteElementString("TotalTravelTime", Art.Results.TravelTime.ToString("0.00"));
                    //if (Art.WtdgC <= 0.50)
                    //    xtw.WriteElementString("WtdgC", Art.WtdgC.ToString("0.00"));
                    //else
                    //    xtw.WriteElementString("WtdgC", "##");
                    //xtw.WriteElementString("FFSDelay", Art.Results.FFSdelay.ToString("0.00"));
                    //xtw.WriteElementString("ThresholdDelay", Art.Results.ThreshDelay.ToString("0.00"));
                    if (overVCMessage == false)
                    {
                        xtw.WriteElementString("AvgTravelSpeed", Art.Results.AverageSpeed.ToString("0.00"));
                        //xtw.WriteElementString("BaseFreeFlowSpeed", Art.Results.BaseFreeFlowSpeed.ToString("0.00"));
                        xtw.WriteElementString("ArtLOS", Art.Results.LOS.ToString());
                    }
                    else
                    {
                        xtw.WriteElementString("AvgTravelSpeed", "###");
                        xtw.WriteElementString("BaseFreeFlowSpeed", "###");
                        xtw.WriteElementString("ArtLOS", "###");
                    }                    
                }
                else  //signal only
                {
                    xtw.WriteElementString("TotalTravelTime", "N/A");
                    xtw.WriteElementString("WtdgC", "N/A");
                    xtw.WriteElementString("AvgTravelSpeed", "N/A");
                    xtw.WriteElementString("FFSDelay", "N/A");
                    xtw.WriteElementString("ThresholdDelay", "N/A");
                    xtw.WriteElementString("ArtLOS", "N/A");
                }
                xtw.WriteEndElement();  //close the <MOEGROUP> tag
            }

            xtw.WriteEndElement();  //close the <APPROACH> tag

            if (Project.DirectionAnalysisMode == DirectionType.BothDirections)
            {
                //write <APPROACH> information
                xtw.WriteStartElement("APPROACH");
                xtw.WriteAttributeString("id", "reverse");
                //xtw.WriteElementString("NumberOfLanes", Art.);
                //xtw.WriteElementString("HVPct", Art[1].PctHeavyVeh.ToString());

                //write <MOEGROUP> information
                //xtw.WriteStartElement("MOEGROUP");

                xtw.WriteEndElement();  //close the <Approach> tag
            }

            //write <INTERSECTIONS> information
            xtw.WriteStartElement("INTERSECTIONS");
            for (int id = 1; id <= Art.TotalInts; id++)
            {
                //write a new <INTERSECTION id="nnn"> element
                xtw.WriteStartElement("INTERSECTION");
                xtw.WriteAttributeString("id", id.ToString());

                //write <INTERSECTIONINFO> information
                xtw.WriteStartElement("INTERSECTIONINFO");
                xtw.WriteElementString("CrossStreetName", Int[id].CrossStreetName);
                xtw.WriteEndElement();  //close the <INTERSECTIONINFO> tag

                //write <CONTROLLER> information
                xtw.WriteStartElement("CONTROLLER");
                //if (Art.SigControl == SigControlType.Pretimed)
                //    xtw.WriteElementString("ControlMode", "Pretimed");
                //else if (Art.SigControl == SigControlType.CoordinatedActuated)
                //    xtw.WriteElementString("ControlMode", "CoordinatedActuated");
                //else if (Art.SigControl == SigControlType.FullyActuated)
                //    xtw.WriteElementString("ControlMode", "FullyActuated");
                //xtw.WriteElementString("CycleLength", Int[id].CycleLen.ToString());
                //if (Int[id].LTphasing == PhasingType.Protect)
                //    xtw.WriteElementString("LeftTurnPhasing", "Protected");
                //else if (Int[id].LTphasing == PhasingType.ProtPerm)
                //    xtw.WriteElementString("LeftTurnPhasing", "ProtPerm");
                //else
                //    xtw.WriteElementString("LeftTurnPhasing", "None");
                xtw.WriteEndElement();  //close the <CONTROLLER> tag

                if (id == 1 && Project.DirectionAnalysisMode == DirectionType.PeakDirection) // && Project.Mode != ModeType.SignalOnly)  //if peak direction analysis and not signal-only analysis, do not write remaining items for 1st intersection
                {
                    xtw.WriteEndElement();  //close the <INTERSECTION> tag
                    continue;
                }

                //write <APPROACH> information
                xtw.WriteStartElement("APPROACH");
                xtw.WriteAttributeString("id", "forward");

                //write <LANEGROUP> information
                xtw.WriteStartElement("LANEGROUP");     //do not include id info, as all lane group info is currently grouped together

                xtw.WriteElementString("Section", Convert.ToString(id - 1));
                //Intersection Data
                //unit extension
                //xtw.WriteElementString("GCRatio", Int[id].gCthru.ToString());
                //if (Int[id].isLTbay == true)
                //{
                //    xtw.WriteElementString("LeftTurnBayYN", "Yes");
                //    xtw.WriteElementString("NumberLTlanes", Int[id].NumLTlanes.ToString());
                //    xtw.WriteElementString("LTBayLength", Int[id].LTbayLen.ToString());
                //    xtw.WriteElementString("GCRatioLT", Int[id].gClt.ToString("0.00"));
                //}
                //else  //false
                //{
                //    xtw.WriteElementString("LeftTurnBayYN", "No");
                //    xtw.WriteElementString("NumberLTlanes", "N/A");
                //    xtw.WriteElementString("LTBayLength", "N/A");
                //    xtw.WriteElementString("GCRatioLT", "N/A");
                //}

                //if (Int[id].isRTbay == true)
                //    xtw.WriteElementString("RightTurnBayYN", "Yes");
                //else  //false
                //    xtw.WriteElementString("RightTurnBayYN", "No");
                //xtw.WriteElementString("PctTurn_Left", Int[id].PctLT.ToString());
                //xtw.WriteElementString("PctTurn_Right", Int[id].PctRT.ToString());
                //xtw.WriteElementString("ArrivalType", Int[id].ArvType.ToString());
                //xtw.WriteElementString("NumberOfLanes_INT", Int[id].NumThLanes.ToString());
                //Segment Data
                //float SegLengthMiles = Convert.ToDouble(Seg[id - 1].Length) / 5280;
                if (Project.Mode != ModeType.SignalOnly)
                {
                    xtw.WriteElementString("LinkLength", Seg[id - 1].LengthFt.ToString());
                    xtw.WriteElementString("NumberOfLanes_SEG", Seg[id - 1].NumLanes.ToString());
                    xtw.WriteElementString("PostedSpeed", Seg[id - 1].PostSpeedMPH.ToString());
                    xtw.WriteElementString("FreeFlowSpeed", Seg[id - 1].Results.FreeFlowSpeedMPH.ToString());
                    if (Seg[id - 1].MedType == MedianType.None)
                        xtw.WriteElementString("MedianType", "None");
                    else if (Seg[id - 1].MedType == MedianType.Nonrestrictive)
                        xtw.WriteElementString("MedianType", "Non-Restrictive");
                    else if (Seg[id - 1].MedType == MedianType.Restrictive)
                        xtw.WriteElementString("MedianType", "Restrictive");

                    if (Seg[id - 1].OnStreetParkingExists == false)
                        xtw.WriteElementString("OnStreetParkingYN", "No");
                    else  //true
                        xtw.WriteElementString("OnStreetParkingYN", "Yes");

                    if (Seg[id - 1].ParkingActivity == ParkingActivityLevel.Low)
                        xtw.WriteElementString("ParkingActivity", "Low");
                    else if (Seg[id - 1].ParkingActivity == ParkingActivityLevel.Medium)
                        xtw.WriteElementString("ParkingActivity", "Medium");
                    else if (Seg[id - 1].ParkingActivity == ParkingActivityLevel.High)
                        xtw.WriteElementString("ParkingActivity", "High");
                    else if (Seg[id - 1].ParkingActivity == ParkingActivityLevel.NotApplicable)
                        xtw.WriteElementString("ParkingActivity", "N/A");
                }
                else
                {
                    xtw.WriteElementString("LinkLength", "N/A");
                    xtw.WriteElementString("NumberOfLanes_SEG", "N/A");
                    xtw.WriteElementString("FreeFlowSpeed", "N/A");
                    xtw.WriteElementString("MedianType", "N/A");
                }

                xtw.WriteElementString("AADT", Seg[id - 1].AADT.ToString());
                xtw.WriteElementString("DDHV", Seg[id - 1].DDHV.ToString("0"));
                //Calculated Values
                //xtw.WriteElementString("ThruMvmtFlowRate", Int[id].ThruVol.ToString("0"));
                //xtw.WriteElementString("AdjSatFlowRate", (Int[id].AdjSatFlow * Int[id].NumThLanes).ToString("0"));
                //xtw.WriteElementString("Cap", Int[id].Capacity.ToString("0"));
                
                    if (Seg[id - 1].OutsideLaneWidth == OutLaneWidth.Narrow)
                        xtw.WriteElementString("OutsideLnWidth", "Narrow");
                    else if (Seg[id - 1].OutsideLaneWidth == OutLaneWidth.Typical)
                        xtw.WriteElementString("OutsideLnWidth", "Typical");
                    else if (Seg[id - 1].OutsideLaneWidth == OutLaneWidth.Wide)
                        xtw.WriteElementString("OutsideLnWidth", "Wide");
                    //else if (Seg[id - 1].OutsideLaneWidth == OutLaneWidth.Custom)
                    //    xtw.WriteElementString("OutsideLnWidth", Seg[id - 1].NumOutsideLaneWidth.ToString());

                    //if (Seg[id - 1].PaveCond == PavementCondition.Desirable)
                    //    xtw.WriteElementString("PavementCondition", "Desirable");
                    //else if (Seg[id - 1].PaveCond == PavementCondition.Typical)
                    //    xtw.WriteElementString("PavementCondition", "Typical");
                    //else if (Seg[id - 1].PaveCond == PavementCondition.Undesirable)
                    //    xtw.WriteElementString("PavementCondition", "Undesirable");

                    //if (Seg[id - 1].BikeLaneExists == false)
                    //    xtw.WriteElementString("BikeLnYN", "No");
                    //else  //true
                    //    xtw.WriteElementString("BikeLnYN", "Yes");
                                        
                    //if (Seg[id - 1].SidewalkExists == false)
                    //    xtw.WriteElementString("SidewalkYN", "No");
                    //else  //true
                    //    xtw.WriteElementString("SidewalkYN", "Yes");
                

                //only write results to file if results are currently displayed
                if (writeResults == true)
                {
                    //write <MOEGROUP> information
                    //xtw.WriteStartElement("MOEGROUP");
                    if (Project.Mode != ModeType.SignalOnly)
                    {
                        xtw.WriteElementString("VCRatio", Int[id].Results.vcRatio.ToString("0.000"));
                        xtw.WriteElementString("ControlDelay", Int[id].Results.ControlDelay.ToString("0.00"));
                        xtw.WriteElementString("IntLOS", Int[id].Results.LOS);
                        //if (Int[id].Approaches[0].LaneGroups[0].AnalysisResults.QueStorageRatio <= 1.0)
                        //    xtw.WriteElementString("QueueStorageRatio", Int[id].Results.QueStorageRatio.ToString("0.00"));
                        //else
                        //    xtw.WriteElementString("QueueStorageRatio", "#");
                        xtw.WriteElementString("BaseFreeFlowSpeed", Seg[id - 1].Results.BaseFreeFlowSpeed.ToString("0.00"));
                        //xtw.WriteElementString("SectAverageTravelSpeed", Seg[id - 1].Results.AverageSpeed.ToString("0.00"));
                        xtw.WriteElementString("SectLOS", Seg[id - 1].Results.LOS);
                        
                    }
                    else  //signal-only analysis
                    {
                        xtw.WriteElementString("VCRatio", Int[2].Results.vcRatio.ToString("0.000"));
                        xtw.WriteElementString("ControlDelay", Int[2].Results.ControlDelay.ToString("0.00"));
                        xtw.WriteElementString("IntLOS", Int[2].Results.LOS);
                        //if (Int[2].Results.QueStorageRatio <= 1.0)
                        //    xtw.WriteElementString("QueueStorageRatio", Int[2].Results.QueStorageRatio.ToString("0.00"));
                        //else
                        //    xtw.WriteElementString("QueueStorageRatio", "#");
                        xtw.WriteElementString("BaseFreeFlowSpeed", "N/A");
                        xtw.WriteElementString("SectAverageTravelSpeed", "N/A");
                        xtw.WriteElementString("SectLOS", "N/A");
                    }
                    //xtw.WriteEndElement();  //close the <MOEGROUP> tag
                }

                xtw.WriteEndElement();  //close the <LANEGROUP> tag
                
                xtw.WriteEndElement();  //close the <APPROACH> tag
                xtw.WriteEndElement();  //close the <INTERSECTION> tag
            }
            xtw.WriteEndElement();  //close the <INTERSECTIONS> tag

            //only write service volume tables to file if results are currently displayed
            bool WriteServiceVols = false;  //temp
            if (WriteServiceVols == true) //(writeResults == true)
            {
                xtw.WriteStartElement("LOSTABLES");
                xtw.WriteAttributeString("Mode", "Auto");

                for (int lanes = 1; lanes <= 5; lanes++)      //2,4,6,8,*
                {
                    xtw.WriteStartElement("CROSSSECTION");
                    xtw.WriteAttributeString("Lanes", (lanes * 2).ToString());

                    for (int LOS = 1; LOS <= 5; LOS++)      //LOS A-E
                    {
                        xtw.WriteStartElement("SERVICEVOL");
                        xtw.WriteAttributeString("LOS", LOS.ToString());

                        if (SerVol.PkDirVol[lanes, LOS] > 0)
                        {
                            xtw.WriteElementString("PeakDirection", SerVol.PkDirVol[lanes, LOS].ToString());
                            if (Project.Period != StudyPeriod.PeakHour)     // Dir Pk Hr Demand study period
                            {
                                xtw.WriteElementString("BothDirections", SerVol.BothDirVol[lanes, LOS].ToString());
                                xtw.WriteElementString("AADT", SerVol.AADT[lanes, LOS].ToString());
                            }
                            else
                            {
                                xtw.WriteElementString("BothDirections", "N/A");
                                xtw.WriteElementString("AADT", "N/A");
                            }
                        }
                        else if (SerVol.PkDirVol[lanes, LOS] == -2)
                        {
                            xtw.WriteElementString("PeakDirection", "**");
                            if (Project.Period != StudyPeriod.PeakHour)     // Dir Pk Hr Demand study period
                            {
                                xtw.WriteElementString("BothDirections", "**");
                                xtw.WriteElementString("AADT", "**");
                            }
                            else
                            {
                                xtw.WriteElementString("BothDirections", "N/A");
                                xtw.WriteElementString("AADT", "N/A");
                            }
                        }
                        else if (SerVol.PkDirVol[lanes, LOS] == -3)
                        {
                            xtw.WriteElementString("PeakDirection", "***");
                            if (Project.Period != StudyPeriod.PeakHour)     // Dir Pk Hr Demand study period
                            {
                                xtw.WriteElementString("BothDirections", "***");
                                xtw.WriteElementString("AADT", "***");
                            }
                            else
                            {
                                xtw.WriteElementString("BothDirections", "N/A");
                                xtw.WriteElementString("AADT", "N/A");
                            }
                        }
                        xtw.WriteEndElement();  //close the <SERVICEVOL> tag
                    }
                    xtw.WriteEndElement();  //close the <CROSSSECTION> tag
                }
                xtw.WriteEndElement();  //close the <LOSTABLES> tag                
            }

            xtw.WriteEndElement();  //close the <ARTERIAL> tag
            xtw.WriteEndDocument(); //close the <TMML> tag
            xtw.Close();

        }
    }
}
