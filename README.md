# Navisworks Clash Data Exporter & Power BI Reporting

The ultimate goal of these tools and workflow is to improve MEP BIM coordination communication with project teams.  The Navisworks add-in provides a means to automate clash metrics progress reporting,  visualize clash data, and make model data meaningful and accessible for various project stakeholders. This workflow requires Autodesk Navisworks Manage, Microsoft Power BI, and Navisworks API to access the desired Clash Detective data for exporting.  The tools and workflow are designed to have Navisworks Manage coupled with Microsoft Power BI to deliver multiple interactive visual reports in Power BI.  However, this respository focuses on the Navisworks API add-in.  The included Power BI template is only for reference and testing.

## Getting Started
Environment setup regarding application development logistics.

* IDE:
  * Visual Studio 2019
  
* Framework:
  * .NET Framework 4.7.2

* Language:
  * C#

* Output Type:
  * Dynamic-link Library (DLL)

* Additional Library Packages Implemented: </br>
  * Navisworks API </br>
    - Autodesk.Navisworks.Api
    - Autodesk.Navisworks.Automation
    - Autodesk.Navisworks.Clash
    - navisworks.gui.roamer
    - AdWindows
  * Microsoft.Office.Interop.Excel

## Application Development
Application features and specs for Navisworks Manage add-in

* Software Required
  - Navisworks Manage 2019
  - Microsoft Power BI
  
* Navisworks Model Preparation & Clash Detective Setup Requirements
  - Models appended need to be solids (not polymesh)
  - Consistent model file naming convention
  - Defined Focus Zones
  - Active Grid System must be set to Grid System that includes boundaries of Floor Plan used in Reports
  - Setup rules for clash tests to eliminate false positives
  - Cannot Compact Clashes
  - Always Update "All Clash"

* User Interface
  - User to specify focus zone for data being exported
  - User to specify project disciplines (ability to save project discipline list and be loaded for future exports)
  - User to specify repository location for data output (Excel file type) to be saved

## Application Structure
Overall workflow from Navisworks Manage to Power BI
> [Navisworks Workflow Video](https://youtu.be/ksFMhAtj59k)
<p align="center">
  <img src="https://user-images.githubusercontent.com/44215479/75582505-91cff600-5a20-11ea-92b7-44ee63b8cf38.png" width="1000">
</p>

1. The Clash Data Exporter add-in has two modules: </br>
  - Clash Test - Exports specific Clash Detective data and writes in Excel spreadsheet
  - Total Objects by Discipline - Exports total objects in open model by discipline for producing metrics in Excel spreadsheet</br>
<p align="center">
  <img src="https://user-images.githubusercontent.com/44215479/75580936-70b9d600-5a1d-11ea-82eb-315ec8ec78f4.png" width="300">
</p>

2. User prompted to specify focus zone associated with exported data and project disciplines 
<p align="center">
  <img src="https://user-images.githubusercontent.com/44215479/75582807-3c481900-5a21-11ea-8609-8b1173f09d06.png" width="600">
</p>

3.  Power BI Reports</br>
    - Projects are created using a template: Project_Name-Clash_Matrics_Template_V2.pbit </br>
      - Create new project using template: **File --> Import --> Power BI template**
    - Select exported Clash Data location generated from the add-in for ClashData and TotalObjects parameters
      <p align="center">
          <img src="https://user-images.githubusercontent.com/44215479/75591045-98b43400-5a33-11ea-87d6-74a46c66b4a7.png" width="600">
      </p>
    - Videos links to examples of the Power BI report functionality
       > - [Project Summary Report Example](https://youtu.be/XugV3iUmORw)
       > - [Overall Progress Report Example](https://youtu.be/sr67IXhXou0)
       > - [Discipline Clash Progress Report Example](https://youtu.be/axuEieqbO3Q)
       > - [Discipline Clash Details Report Example](https://youtu.be/DGS9jlB2jxk)
       > - [Clash Map Report Example](https://youtu.be/XgPejZVefOU)
       
## Navisworks API Implementation
Below highlights specific API features implemented to access and export specific Clash Detective Data
<p align="center">
  <img src="https://user-images.githubusercontent.com/44215479/75583929-7ca89680-5a23-11ea-99a4-8d49d07a47e4.png" width="600">
</p>

##### Clash Test Module
 - How API is mapped to data in Clash Detective UI for OPEN clashes
<p align="center">
  <img src="https://user-images.githubusercontent.com/44215479/75581657-cf338400-5a1e-11ea-8786-9a9ef484ad9a.png" width="600">
</p>  

 - How API is mapped to data in Clash Detective UI for CLOSED clashes
<p align="center">
  <img src="https://user-images.githubusercontent.com/44215479/75582011-86c89600-5a1f-11ea-9988-308d1b967523.png" width="600">
</p> 

##### Total Objects By Discipline Module
- How API is mapped to model files in Selection Tree UI
<p align="center">
  <img src="https://user-images.githubusercontent.com/44215479/75584908-b8446000-5a25-11ea-99ed-18e675b9782d.png" width="600">
</p>

##### Output Excel Spreadsheet Examples
- Export from Clash Test module
<p align="center">
  <img src="https://user-images.githubusercontent.com/44215479/75585249-77991680-5a26-11ea-9e63-43ed83651cb1.png" width="1000">
</p>

- Export from Total Objects by Discipline module
<p align="center">
  <img src="https://user-images.githubusercontent.com/44215479/75585302-9ac3c600-5a26-11ea-950d-f41adfa4aa90.png" width="400">
</p>

## Installing and Running Application
1. Clone or download project. </br>
2. Open ClashData.sln in Visual Studio 2019. </br>
3. Ensure that the library packages stated in Getting Started are installed and referenced. </br>
4. The application can then be run in debug mode. </br>
5. Go to Debug/Release location and copy the files and folder below. </br>
   <p align = "center">
      <img src="https://user-images.githubusercontent.com/44215479/75586032-50434900-5a28-11ea-9cb2-8e3d00008d17.png" width = "200">
   </p>
6. Create a ClashData folder in **Local_Drive:\...\Autodesk\Navisworks Manage 2019\Plugins** </br>
7. Paste copied files and folders to ClashData folder </br>
8. Open Navisworks Manage 2019 to execute and test add-in.

## References for Further Learning
- Tools and Workflow described here are based on AU 2019 Presentation: [Visualizing Clash Metrics in Navisworks with Power BI - Carlo Caparas](https://www.autodesk.com/autodesk-university/class/Its-All-Data-Visualizing-Clash-Metrics-Navisworks-and-Power-BI-2019)
- [Customizing Autodesk® Navisworks® 2013 with the .NET API - Simon Bee](https://www.autodesk.com/autodesk-university/class/Customizing-AutodeskR-NavisworksR-2013-NET-API-2012)
- [Navisworks .NET API 2013 new feature – Clash 1 - Xiaodong Liang](https://adndevblog.typepad.com/aec/2012/05/navisworks-net-api-2013-new-feature-clash-1.html)
- [Navisworks .NET API 2013 new feature – Clash 2 - Xiaodong Liang](https://adndevblog.typepad.com/aec/2012/05/navisworks-net-api-2013-new-feature-clash-2.html)
- [API Docs - Guilherme Talarico](https://apidocs.co/apps/navisworks/2018/87317537-2911-4c08-b492-6496c82b3ed1.htm#)
- [Power BI Documentation - Microsoft Corporation](https://docs.microsoft.com/en-us/power-bi/#pivot=home&panel=home-all)

