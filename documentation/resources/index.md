Introduction to HCM Urban Streets Open Source
==============

 Welcome to the Highway Capacity Manual (HCM) Urban Streets Open Source project. This project consists of software code, in a GitHub repository, and supporting documentation for those who want to use an alternative to closed-source/proprietary implementations of computational engines for the HCM urban streets analysis methodology. Additionally, the source code and/or compiled binaries for this project can be used without paying license fees. 
 
 This effort was initiated by Dr. Scott Washburn, who has been involved with the [Highway Capacity and Quality of Service Committee (HCQSC)](https://www.hcqstrb.org/) since 2000. 
 
 It should be noted that the HCQSC does not officially endorse any specific software implementation of the HCM analysis methodology calculations. While the committee has assisted a variety of software developers over the years with explanations and interpretations of HCM analysis methodology content, there is no software product that is considered an official software product of the committee.

# What

It is a computational engine for the urban streets analysis methdology of the 6th edition of the HCM. The HCM documents this methodology in the following chapters:

* 16: Urban Street Facilities
* 17: Urban Street Reliability and ATDM
* 18: Urban Street Segments
* 29: Urban Street Facilities Supplemental
* 30: Urban Street Segments Supplemental

These chapters also reference the analysis methodologies for interupted-flow point facilities; for example, Chapter 19--Signalized Intersections.

# Why

## Background

Prior to the HCM 2000 (4th edition), the analysis methodologies were generally simple enough that they could easily be performed by hand with the supplied worksheets, or at most, a simple spreadsheet implementation. 

The HCM 2000 introduced an analysis methodology for analzying freeway facilities, which allowed an extended section of freeway with varying contiguous segment types (basic, on-/off-ramp, weaving) to be analyzed together as a cohesive freeway facility unit. For undersaturated traffic conditions, this new methodology utilized the current segment methodologies, but added a mechanism for analyzing an extended time period (e.g., a 3-h peak period). The calculations were still straightforward, but analyzing several hours, in 15-min increments, required many repeated applications of the methodology, which was cumbersome to do manually. The most significant addition to this methodology for the HCM 2000 was the introduction of a analysis methodology for oversaturated traffic conditions. The oversaturated analysis methodology was considerably more complex than the undersaturated methdology and virtually infeasible to perform manually. A supporting computational engine, in the form of VBA code in an Excel workbook, known as FREEVAL was introduced at the same time as the release of the HCM 2000. This tool has undergone further development over the years, including conversion to a Java programming language implementation. However, it is not an open-source product.

A joint committee meeting was held between the HCQSC and the [Traffic Signal Systems Committee (TSSC)](http://www.trb.org/AHB25/AHB25.aspx) in July 2005 (Las Vegas, NV). At this meeting there was considerable discussion about how to update the signalized intersection analysis methodology of the HCM to accommodate actuated signal control instead of just pretimed control. Consequently, a significant overhaul to the signalized intersection analysis methodology was performed, based largely on actuated signal control analysis methods from the academic literature. Furthermore, the urban streets analysis methodology was significantly overhauled, based on the results from [NCHRP Project 3-79 (Bonneson et al., 2008)](#References). These updated methods were included in the HCM 2010 (5th edition), along with a supporting computational engine, in the form of VBA code in an Excel workbook. This tool, and the underlying VBA code, were made available to software developers to assist them with their own implementations of the methodology, but it was never intended to be used in the public domain and formal support for it has never been available.

Each update to the HCM (latest is HCM 2016, sixth edition) brings more complexity to the analysis methodologies. At this point, it is either completely infeasible or totally impractical to use the HCM analysis methodologies without software. Unfortunately, the necessary reliance on software, especially closed-source software, makes it very challenging to verify if the calculations implemented in the software are correct. This challenge is further compounded by the fact that the HCQSC does not maintain a comprehensive set of verified test data sets (input values with corresponding output values) that can be used to independently verify software accuracy. The challenge this presents to the HCQSC and the segment of the transportation engineering profession that uses the HCM, is summarized well by [(Roess and Prassas, 2014)](#References). The following text is excerpted from Section 11.5 of their book, which is titled "**The Software Is the Manual!**":

> "The Committee has steadfastly avoided resolving this conundrum. Because few users ever read the manual, or even consult it, its implementation is virtually fully controlled by the software that implements it. The Committee,  however, does not review, vouch for, or endorse software that is developed privately. While the Committee enjoys a good relationship with (several software developers), it does not exercise direct control over these products or their development.

> With the 2010 HCM, this issue was significantly heightened. For two critical methodologies (signalized intersections and freeway facilities), the documentation of the methodology was programming code--referred to as a "computational engine." These engines were devoid of dramatic and easy-to-use input and output formats, but detailed the actual workings of the model. Neither methodology could be explained sufficiently in English to allow software developers to write implementing code. In fact, the freeway facility methodology was developed as a software package. Because of this, two critical methodologies of the 2010 HCM have become virtual "black boxes." The ongoing issues for the committee are twofold:  
> * How do we describe complex methodologies to the majority of users in a way that they can comprehend?  
> * How do we guarantee that any given piece of implementing software is adequately and accurately replicating the various HCM methodologies?

> The first is an issue of presentation and explanation. Improvement is needed. It raises, however, a background, but equally important issue: How complex should HCM methodologies be? The issue of software verification is also complex. The first question is: Is this the Committee's job, or is it beyond their scope? So far, the answer has been that it is beyond the Committee's purview. However, if indeed the typical user is virtually dependent upon implementing software, the Committee is, in effect, losing any practical control over the use of its product by avoiding the issue.

> Until recently, one approach was to develop a set of sample problems that Committee members could agree were correct--i.e., proper implementations of the HCM. Then, software developers could compare the output of their programs to the sample problems to demonstrate faithful implementation. The issue of how many problems are needed to cover a wide enough range of potential cases was left to judgment.

> The 2010 HCM models for signalized intersections, urban street segments, urban street facilities and freeway facilities, however, presented new challenges. Sample problems could not be created without having software. The price of complexity is the ability to reasonably do any significant number of examples (other than the most simple) by hand. If we need software to develop the sample problems, we can't use the sample problems to check on the accuracy of software. Comparing the output of one software package to another without the ability to verify the accuracy of either is a losing proposition.

> In the end, the authors do not see any way to continue avoiding this problem. The Committee simply must undertake to examine and verify at least one basic computational engine that properly implements each HCM methodology. Software developers need to have access to the code for these engines, and/or access to its use to help the developer verify their own software. If this forces the Committee to re-examine the issue of complexity--and how much is justified, that would also be a good outcome."


Furthermore, this issue has significant implications for society at large, because hundreds of millions, if not billions, of dollars of transportation infrasture investment decisions are made annually in part on the results of software implements of the HCM.

## Potential Solution

An unintended consquence of closed-source software implementations of complex analysis methodologies is the 'black box' syndrome, which can lead to a high incidence of misuse and abuse of the analysis methods. A potential solution to this is to promote greater transparency of the software tools--the underlying code implementation of the algorithms and the associated documentation. One way to do this is through open-source analysis methodology calculations code and reference data sets.

[NASA](https://www.nasa.gov/) recently conducted an assessment of its software practices with a particular focus on the implications of closed-source versus open-source to accomplishing the objectives of its Science Mission Directorate [(National Academies of Sciences, Engineering, and Medicine, 2018)](#References). A few excerpts from this report that are germane to this discussion are included here:

> "Modern science is ever more driven by computations and simulations... At the same time, scientific work requires data processing, presentation, and analysis through broadly available proprietary and community software. Implicitly or explicitly, software is central to science. Scientific discovery, understanding, validation, and interpretation are all enhanced by access to the source code of the software used by scientists."

> "Poorly documented software and associated data files, even if shared publicly, will likely result in an inability to replicate research."

> "Recommendation: NASA Science Mission Directorate should explicitly recognize the scientific value of open source software and incentivize its development and support, with the goal that open source science software becomes routine scientific practice."

> "Recommendation: NASA Science Mission Directorate should consider a variety of policy options depending on discipline and software type and transition to greater openness over time."

# Potential Benefits

Open-source computational engines for the HCM analysis methodologies may result in the following benefits:

* Help facilitate understanding of methods by HCQSC members, college faculty and students, and practitioners.
* Leverage participation by the larger HCM community to reduce reliance on HCQSC insitutional knowlege and 'democratize' the determination of truth.
* Leverage participation by the larger HCM community to help maintain it, improve it, and keep it relevant.
* Help commercial software developers 'get it right' (they could also use code directly as their computational engine and package it with custom user interface components).
* Help promote digital proficiency--a mandatory skill for the transportation engineering professional in the 21st century.

# Further Information

If you have inquiries about this project that are not answered in this documentation, or want to request revisions/additions to this documentation, or want to learn how to become a contributor to this project, contact:

Dr. Scott S. Washburn  
Email: swash@ce.ufl.edu  
Web: https://faculty.eng.ufl.edu/scott-washburn


# References
Bonneson, J. A., M. P. Pratt, and M. A. Vandehey. (2008) *Predicting the Performance of Automobile Traffic on Urban Streets: Final Report*. National Cooperative Highway Research Program Project 03-79. Texas Transportation Institute, Texas A&M University, College Station, Jan. 2008.

National Academies of Sciences, Engineering, and Medicine. (2018) Open Source Software Policy Options for NASA Earth and Space Sciences. Washington, DC: The National Academies Press. https://doi.org/10.17226/25217.

Roess, Roger P. and Prassas, Elena S. (2014) *The Highway Capacity Manual: A Conceptual and Research History* Springer International Publishing, Switzerland. https://doi.org/10.1007/978-3-319-05786-6