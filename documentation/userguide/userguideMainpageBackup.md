# HCM-CALC Urban Streets Open Source
## User Guide

[//]: # (This syntax works like a comment, and won't appear in any output.)

### Input Data Structure

#### Classes
* **[Project](#project-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsprojectdatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsprojectdatahtml)**
  * **[Arterial](#arterial-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsarterialdatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsarterialdatahtml)** 
    * **[Segment](#segment-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetssegmentdatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetssegmentdatahtml)** 
      * **[Link](#link-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetslinkdatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetslinkdatahtml)** 
        * **[Access Point](#access-point-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsaccesspointdatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsaccesspointdatahtml)** 
      * **[Intersection](#intersection-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsintersectiondatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsintersectiondatahtml)** 
        * **[Signal Cycle](#signal-cycle-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetssignalcycledatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetssignalcycledatahtml)** 
          * **[Timer](#timer-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetstimerdatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetstimerdatahtml)** 
        * **[Approach](#approach-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsapproachdatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsapproachdatahtml)** 
          * **[Lane Group](#lane-group-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetslanegroupdatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetslanegroupdatahtml)** 
            * **[Lane](#lane-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetslanedatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetslanedatahtml)**
            * **[Signal Phase](#signal-phase-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetssignalphasedatahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetssignalphasedatahtml)**

#### Enumerations
* **[Analysis Mode](#analysis-mode-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsanalysismodehtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsanalysismodehtml)**
* **[Area Type](#area-type-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsareatypehtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsareatypehtml)**
* **[Direction Type](#direction-type-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsdirectiontypehtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsdirectiontypehtml)**
* **[FFS Calculation Method](#ffs-calculation-method-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsffscalcmethodhtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsffscalcmethodhtml)**
* **[Lane Movements Allowed](#lane-movements-allowed-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetslanemovementsallowedhtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetslanemovementsallowedhtml)**
* **[LOS C Criteria](#los-c-criteria-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetslosccriteriahtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetslosccriteriahtml)**
* **[Median Type](#median-type-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsmediantypehtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsmediantypehtml)**
* **[Mode Type](#mode-type-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsmodetypehtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsmodetypehtml)**
* **[Segment Direction](#segment-direction-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsmovementdatasegmentdirectionhtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsmovementdatasegmentdirectionhtml)**
* **[Movement Direction](#movement-direction-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsmovementdirectionhtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsmovementdirectionhtml)**
* **[Movement Direction With Mid Block](#movement-direction-with-mid-block-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsmovementdirectionwithmidblockhtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsmovementdirectionwithmidblockhtml)**
* **[Nema Movement Numbers](#nema-movement-numbers-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsnemamovementnumbershtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsnemamovementnumbershtml)**
* **[Outer Lane Width](#outer-lane-width-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsoutlanewidthhtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsoutlanewidthhtml)**
* **[Parking Activity Level](#parking-activity-level-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsparkingactivitylevelhtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsparkingactivitylevelhtml)**
* **[Phase Color](#phase-color-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsphasecolorhtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsphasecolorhtml)**
* **[Phasing Type](#phasing-type-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsphasingtypehtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsphasingtypehtml)**
* **[Sequence Type](#sequence-type-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetssequencetypehtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetssequencetypehtml)**
* **[Signal Control Type](#signal-control-type-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetssigcontroltypehtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetssigcontroltypehtml)**
* **[Study Period](#study-period-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsstudyperiodhtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsstudyperiodhtml)**
* **[Travel Direction](#travel-direction-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetstraveldirectionhtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetstraveldirectionhtml)**
* **[Unsignalized Control Type](#unsignalized-control-type-apihttpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsunsignalcontroltypehtml-httpsswash17githubiohcmcalcurbanstreetsapihcmcalcurbanstreetsunsignalcontroltypehtml)**

## Details
### **Class Details**

#### Project ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html"))
[Agency](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_Agency):  
[Analysis Mode](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_AnalMode):  
[Analysis Date](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_AnalysisDate):  
[Analyst Name](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_AnalystName):  
[Analysis Mode Direction](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_DirectionAnalysisMode):  
[FFS Calculation Method](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_FFSCalc):  
[File Name](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_FileName):  
[File Path](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_FilePath):  
[LOS C Criteria](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_FileName):  
[Mode Type](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_Mode):  
[Period](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_Period):  
[User Notes](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ProjectData.html#HCMCalc_UrbanStreets_ProjectData_UserNotes):

#### Arterial ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html"))
[Analysis Travel Direction](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_AnalysisTravelDir):  
[Area](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_Area):  
[Arterial Name](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_ArtName):  
[Classification](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_Classification):  
[D Factor](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_Dfactor):  
[From (Origin)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_From):  
[K Factor](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_Kfactor):  
[Length (mi)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_LengthMiles):  
[Max Service Volume](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_MaxSerVol):  
[Over Capacity](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_OverCapacity):  
[Results](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_Results):  
[Segments](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_Segments):  
[Test Service Vol](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_TestSerVol):  
[To (Destination)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_To):  
[Total Intersections](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_TotalInts):  
[Total Segments](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ArterialData.html#HCMCalc_UrbanStreets_ArterialData_TotalSegs):  

#### Segment ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SegmentData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SegmentData.html"))
[ID](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SegmentData.html#HCMCalc_UrbanStreets_SegmentData_Id):  
[Intersection](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SegmentData.html#HCMCalc_UrbanStreets_SegmentData_Intersection):  
[Link](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SegmentData.html#HCMCalc_UrbanStreets_SegmentData_Link):  
[Results](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SegmentData.html#HCMCalc_UrbanStreets_SegmentData_Results):  

#### Link ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html"))
[AADT (vph)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_AADT):  
[Access Points](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_AccessPoints):  
[Adjusted DDHV](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_AdjDDHV):  
[DDHV](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_DDHV):  
[Free Flow Speed (mph)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_FreeFlowSpeedMPH):  
[ID](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_Id):  
[Length (ft)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_LengthFt):  
[Median Type](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_MedType):  
[Mid Block Entering Volume (vph)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_MidblockEnteringVolVehPerHr):  
[Mid Block Exiting Volume (vph)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_MidblockExitingVolVehPerHr):  
[Number of Access Points](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_NumAccessPts):  
[Number of Lanes](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_NumLanes):  
[Number of Through Lanes](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_NumThruLanes):  
[On Street Parking Exists](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_OnStreetParkingExists):  
[Outside Lane Width](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_OutsideLaneWidth):  
[Parking Activity](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_ParkingActivity):  
[Grade (%)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_PctGrade):  
[Percent On Street Parking (%)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_PctOnStreetParking):  
[Posted Speed (mph)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_PostSpeedMPH):  
[Proportion Curb Right Side](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_PropCurbRightSide):  
[Proportion Restricted Median](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_PropRestrictMedian):  
[Results](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_Results):  
[Travel Direction](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LinkData.html#HCMCalc_UrbanStreets_LinkData_TravelDirection):  

#### Access Point ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html"))
[Arrival Flow Rate](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html#HCMCalc_UrbanStreets_AccessPointData_ArrivalFlowRate):  
[Block Time](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html#HCMCalc_UrbanStreets_AccessPointData_BlockTime):  
[Deceleration Rate (ft/s^2)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html#HCMCalc_UrbanStreets_AccessPointData_DecelRate):  
[Lanes](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html#HCMCalc_UrbanStreets_AccessPointData_Volume):  
[Location (% of link)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html#HCMCalc_UrbanStreets_AccessPointData_Location):  
[Portion of Time Blocked](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html#HCMCalc_UrbanStreets_AccessPointData_PortionTimeBlocked):  
[Probability Inside Lane is Blocked](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html#HCMCalc_UrbanStreets_AccessPointData_ProbInsideLaneBlocked):  
[Right Turn Equivalency](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html#HCMCalc_UrbanStreets_AccessPointData_RightTurnEquivalency):  
[Through Delay](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html#HCMCalc_UrbanStreets_AccessPointData_ThruDelay):  
[Volume](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AccessPointData.html#HCMCalc_UrbanStreets_AccessPointData_Volume):  

#### Intersection ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html"))
[Approaches](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_Approaches):  
[Cross Street Name](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_CrossStreetName):  
[Cross Street Width (ft)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_CrossStreetWidth):  
[Demand Volume (vph)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_DemandVolumeVehPerHr):  
[Detector Length](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_DetectorLength):  
[Id](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_Id):  
[Label](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_Label):  
[Lane Grade HV Area Factors](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_LaneGradeHVAreaFactors):  
[Results](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_Results):  
[Signal](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_Signal):  
[Speed Limit](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.IntersectionData.html#HCMCalc_UrbanStreets_IntersectionData_SpeedLimit):  

#### Signal Cycle ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalCycleData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalCycleData.html"))
[Control Type](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalCycleData.html#HCMCalc_UrbanStreets_SignalCycleData_ControlType):  
[Cycle Length (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalCycleData.html#HCMCalc_UrbanStreets_SignalCycleData_CycleLengthSec):  
[Offset (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalCycleData.html#HCMCalc_UrbanStreets_SignalCycleData_OffsetSec):  
[Reference Phase ID](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalCycleData.html#HCMCalc_UrbanStreets_SignalCycleData_ReferencePhaseID):  
[Timers](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalCycleData.html#HCMCalc_UrbanStreets_SignalCycleData_Timers):  

#### Signal Phase ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html"))
[All Red (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_AllRedSec):  
[Dallas Phasing Mode](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_DallasPhasingMode):  
[Dual Entry Mode](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_DualEntryMode):  
[End Use (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_EndUseSec):  
[GapOutMode](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_GapOutMode):  
[gC](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_gC):  
[Effective Green Time (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_GreenEffectiveSec):  
[Intergreen Time (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_IntergreenTimeSec):  
[MinGreenIntervalSec](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_MinGreenIntervalSec):  
[Move Direction](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_MoveDir):  
[NEMA Phase ID](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_NemaPhaseId):  
[Phasing](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_Phasing):  
[Recall Max Mode](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_RecallMaxMode):  
[Recall Min Mode](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_RecallMinMode):  
[Sequence](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_Sequence):  
[Splits](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_Splits):  
[Start Up Lost Time (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_StartUpLostTimeSec):  
[Timer](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_Timer):  
[Yellow (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SignalPhaseData.html#HCMCalc_UrbanStreets_SignalPhaseData_YellowSec):  

#### Timer ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html"))
[Associated Lane Groups](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_AssociatedLaneGroups):  
[Critical Green Queue Clear Time (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_CriticalGreenQueueClearTime):  
[Duration (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_Duration):  
[End Time (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_EndTime):  
[Green Extension](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_GreenExtension):  
[Intergreen](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_Intergreen):  
[Is Permitted Timer](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_IsPermittedTimer):  
[Maximum Allowable Headway](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_MaxAllowHdwy):  
[Maximum Green](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_MaxGreen):  
[Maximum Queue Clear Time](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_MaxQueueClearTime):  
[Move Direction](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_MoveDir):  
[Movement Saturation Flow Rate](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_MvmtSatFlow):  
[NEMA Movement ID](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_NemaMvmtID):  
[Pct. Lefts Inside Lane (%)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_PctLeftsInsideLane):  
[Pct. Rights Outside Lane (%)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_PctRightsOutsideLane):  
[Permitted Effective Green](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_PermEffGreen):  
[Permitted EU_LT](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_PermEU_LT):  
[Permitted L1_LT](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_PermL1_LT):  
[Permitted Queue Serve Time (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_PermQueServeTime):  
[Permitted Serve Time (s)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_PermServeTime):  
[Phase Number](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_PhaseNumber):  
[Prob. Max Out](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_ProbMaxOut):  
[Prob. Of Phase Call](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_ProbOfPhaseCall):  
[Queue Serve Time Before Block](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_QueServeTimeBeforeBlk):  
[Queue Serve Time](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_QueueServeTime):  
[Results](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_Results):  
[Start Time (S)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_StartTime):  
[Time To First Block](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TimerData.html#HCMCalc_UrbanStreets_TimerData_TimeToFirstBlk):  

#### Approach ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html"))
[Demand Stops](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_DemandStops):  
[Demand Volume (vph)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_DemandVolume):  
[Direction](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_Dir):  
[ID](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_Id):  
[Label](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_Label):  
[Lane Groups](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_LaneGroups):  
[Left Turn Bay Exists](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_LeftTurnBayExists):  
[Percent Grade](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_PctGrade "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_PctGrade"): The incline of the approach, entered as a percentage (+/- 0-100).  
[Pct. Left Turns (%)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_PctLeftTurns):  
[Pct. Right Turns (%)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_PctRightTurns):  
[Results](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_Results):  
[Right Turn Bay Exists](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ApproachData.html#HCMCalc_UrbanStreets_ApproachData_RightTurnBayExists):  

#### Lane Group ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html"))
[Analysis Flow Rate](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_AnalysisFlowRate):  
[Analysis Results](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_AnalysisResults):  
[Arrival Type](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_ArvType):  
[Base Saturation Flow](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_BaseSatFlow):  
[Demand Volume (vph)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_DemandVolumeVehPerHr):  
[Discharge Volume (vph)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_DischargeVolume):  
[ID](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_Id):  
[Initial Queue](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_InitialQue):  
[Label](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_Label):  
[Lane Filter Factor](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_LaneFilterFactor):  
[Lanes](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_Lanes):  
[NEMA Movement ID](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_NemaMvmtID):  
[Number Lanes](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_NumLanes):  
[Number Left Lanes](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_NumLeftLanes):  
[Pct. Heavy Vehicles](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_PctHeavyVehicles):  
[Pct. Left Turns](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_PctLeftTurns):  
[Pct. Right Turns](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_PctRightTurns):  
[Pct. Turns Exc. Lane](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_PctTurnsExcLane):  
[Peak Hour Factor](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_PeakHourFactor):  
[Platoon Ratio](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_PlatoonRatio):  
[Portion On Green](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_PortionOnGreen):  
[Portion On Green Of Shared Lane](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_PortionOnGreenOfSharedLane):  
[Signal Phase](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_SignalPhase):  
[Travel Direction](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_TravelDir):  
[Turn Bay Left Length (ft)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_TurnBayLeftLengthFeet):  
[Turn Bay Right Length (ft)](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_TurnBayRightLengthFeet):  
[Type](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneGroupData.html#HCMCalc_UrbanStreets_LaneGroupData_Type):  

#### Lane ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneData.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneData.html"))
[Allowed Movements](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneData.html#HCMCalc_UrbanStreets_LaneData_AllowedMovements):  
[ID](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneData.html#HCMCalc_UrbanStreets_LaneData_Id):  

### **Enumeration Details**
Enumerations values are listed in order of index rather than alphabetical. Int values are displayed when they are defined.

#### Analysis Mode ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AnalysisMode.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AnalysisMode.html"))
The type of analysis mode for an arterial. "Operations" uses full calculation such as platoon dispersion while "Planning" uses simple calculations.

| Enum        | Int Value   |
| ----------- | ----------- |
| Operations |  |
| Planning |  |

#### Area Type ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AreaType.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.AreaType.html"))
The type of area for an arterial.

| Enum        | Int Value   |
| ----------- | ----------- |
| LargeUrbanized | 0 |
| OtherUrbanized | 1 |
| Transitioning | 2 |
| RuralDeveloped | 3 |


#### Direction Type ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.DirectionType.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.DirectionType.html"))
The type of analysis direction for a project.

| Enum        | Int Value   |
| ----------- | ----------- |
| PeakDirection | |
| BothDirections | |

#### FFS Calculation Method ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.FFSCalcMethod.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.FFSCalcMethod.html"))
The type of FFS calculation method to use for a project.

| Enum        | Int Value   |
| ----------- | ----------- |
| HCM2010 | |
| PostSpeed | |

#### Lane Movements Allowed ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneMovementsAllowed.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LaneMovementsAllowed.html"))
Allowed movement types (left-only, thru-and-left-turn-bay, etc.),  for a lane.

| Enum        | Int Value   |
| ----------- | ----------- |
| LeftOnly |  |
| ThruOnly |  |
| RightOnly |  |
| ThruLeftShared |  |
| ThruRightShared |  |
| ThruAndLeftTurnBay |  |
| ThruAndRightTurnBay |  |
| LeftRightShared |  |
| All |  |

#### LOS C Criteria ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LOSCcriteria.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.LOSCcriteria.html"))
The methodology to use for determining the criteria for LOS C for a project.

| Enum        | Int Value   |
| ----------- | ----------- |
| HCM2000 |  |
| HCM2010 |  |
| FDOT2ClassLength |  |
| FDOT2ClassSpeed |  |

#### Median Type ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.MedianType.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.MedianType.html"))
The type of median for a segment.

| Enum        | Int Value   |
| ----------- | ----------- |
| None |  |
| Nonrestrictive |  |
| Restrictive |  |

#### Mode Type ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ModeType.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ModeType.html"))
The type of mode for a project.

| Enum        | Int Value   |
| ----------- | ----------- |
| AutoOnly |  |
| Multimodal |  |
| SignalOnly |  |

#### Segment Direction ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.MovementData.SegmentDirection.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.MovementData.SegmentDirection.html"))
The analysis direction to study for an arterial.

| Enum        | Int Value   |
| ----------- | ----------- |
| EBNB | 0 |
| WBSB | 1 |

#### Movement Direction ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.MovementDirection.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.MovementDirection.html"))
The type of movement (L, T, R) used for multiple applications.

| Enum        | Int Value   |
| ----------- | ----------- |
| Left | 0 |
| Through | 1 |
| Right | 2 |

#### Movement Direction With Mid Block ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.MovementDirectionWithMidBlock.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.MovementDirectionWithMidBlock.html"))
The type of movement (L, T, R, MB) used for multiple applications, but also including mid-block.

| Enum        | Int Value   |
| ----------- | ----------- |
| Left | 0 |
| Through | 1 |
| Right | 2 |
| MidBlock | 3 |

#### Nema Movement Numbers ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.NemaMovementNumbers.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.NemaMovementNumbers.html"))
NEMA movement numbers for each approach plus movement combination.

| Enum        | Int Value   |
| ----------- | ----------- |
| EBLeft | 5 |
| EBThru | 2 |
| EBRight | 12 |
| WBLeft | 1 |
| WBThru | 6 |
| WBRight | 16 |
| NBLeft | 3 |
| NBThru | 8 |
| NBRight | 18 |
| SBLeft | 7 |
| SBThru | 4 |
| SBRight | 14 |

#### Outer Lane Width ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.OutLaneWidth.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.OutLaneWidth.html"))
The type of outer lane width design.

| Enum        | Int Value   |
| ----------- | ----------- |
| Narrow |  |
| Typical |  |
| Wide |  |
| Custom |  |

#### Parking Activity Level ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ParkingActivityLevel.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.ParkingActivityLevel.html"))
The level of parking activity.

| Enum        | Int Value   |
| ----------- | ----------- |
| NotApplicable |  |
| Low |  |
| Medium |  |
| High |  |


#### Phase Color ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.PhaseColor.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.PhaseColor.html"))
The color for a phase.

| Enum        | Int Value   |
| ----------- | ----------- |
| Green |  |
| Yellow |  |
| AllRed |  |

#### Phasing Type ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.PhasingType.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.PhasingType.html"))
The type of phasing for an intersection's signal cycle.

| Enum        | Int Value   |
| ----------- | ----------- |
| None |  |
| Protected |  |
| Permitted |  |
| ProtPerm |  |
| Split |  |
| ThruAndRight |  |

#### Sequence Type ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SequenceType.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SequenceType.html"))
The type of sequence for a left-turn movement.

| Enum        | Int Value   |
| ----------- | ----------- |
| Lead |  |
| Lag |  |
| None |  |

#### Signal Control Type ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SigControlType.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.SigControlType.html"))
The type of signal control for an intersection.

| Enum        | Int Value   |
| ----------- | ----------- |
| Pretimed | 0 |
| CoordinatedActuated | 1 |
| FullyActuated | 2 |

#### Study Period ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.StudyPeriod.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.StudyPeriod.html"))
The study period for a project.

| Enum        | Int Value   |
| ----------- | ----------- |
| StandardK |  |
| Kother |  |
| PeakHour |  |

#### Travel Direction ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TravelDirection.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.TravelDirection.html"))
The travel direction for multiple objects, organized clockwise starting with North.

| Enum        | Int Value   |
| ----------- | ----------- |
| Northbound | 0 |
| Eastbound | 1 |
| Southbound | 2 |
| Westbound | 3 |

#### Unsignalized Control Type ([API](https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.UnsignalControlType.html "https://swash17.github.io/HCMCalc_UrbanStreets/api/HCMCalc_UrbanStreets.UnsignalControlType.html"))
The type of control for an unsignalized intersection.

| Enum        | Int Value   |
| ----------- | ----------- |
| TwoWayStop |  |
| AllWayStop |  |