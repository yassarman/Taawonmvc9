using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaawonMVC.Applications;
using TaawonMVC.Applications.DTO;
using TaawonMVC.Buildings;
using TaawonMVC.Buildings.DTO;
using TaawonMVC.BuildingType;
using TaawonMVC.BuildingUnits;
using TaawonMVC.BuildingUnits.DTO;
using TaawonMVC.BuildingUses;
using TaawonMVC.InterventionType;
using TaawonMVC.Neighborhood;
using TaawonMVC.PropertyOwnership;
using TaawonMVC.RestorationType;
using TaawonMVC.Web.Models.Applications;

namespace TaawonMVC.Web.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly IApplicationsAppService _applicationsAppService;
        private readonly IBuildingsAppService _buildingsAppService;
        private readonly IBuildingUnitsAppService _buildingUnitsAppService;
        private readonly IPropertyOwnershipAppService _propertyOwnershipAppService;
        private readonly IInterventionTypeAppService _interventionTypeAppService;
        private readonly IRestorationTypeAppService _restorationTypeAppService;
        private readonly INeighborhoodAppService _neighborhoodAppService;
        private readonly IBuildingTypeAppService _buildingTypeAppService;
        private readonly IBuildingUsesAppService _buildingUsesAppService;

        public ApplicationsController(IApplicationsAppService applicationsAppServic,
            IBuildingsAppService buildingsAppService, 
            IBuildingUnitsAppService buildingUnitsAppService, 
            IPropertyOwnershipAppService propertyOwnershipAppService, 
            IInterventionTypeAppService interventionTypeAppService, 
            IRestorationTypeAppService restorationTypeAppService,
            INeighborhoodAppService neighborhoodAppService,
            IBuildingTypeAppService buildingTypeAppService,
            IBuildingUsesAppService buildingUsesAppService)
        {
            _applicationsAppService = applicationsAppServic;
            _buildingsAppService = buildingsAppService;
            _buildingUnitsAppService = buildingUnitsAppService;
            _propertyOwnershipAppService = propertyOwnershipAppService;
            _interventionTypeAppService = interventionTypeAppService;
            _restorationTypeAppService = restorationTypeAppService;
            _neighborhoodAppService = neighborhoodAppService;
            _buildingTypeAppService = buildingTypeAppService;
            _buildingUsesAppService = buildingUsesAppService;



        }
        // GET: Applications
        public ActionResult Index()
        {
            var applications = _applicationsAppService.getAllApplications();
            var applicationsViewModel = new ApplicationsViewModel()
            {
             Applications = applications
            };
            return View("Applications", applicationsViewModel);
        }

        public ActionResult ApplicationForm()
        {
            // get the list of building uses
            var buildingUses = _buildingUsesAppService.getAllBuildingUses();
            //get the list of buildingTypes
            var buildingTypes = _buildingTypeAppService.getAllBuildingtype().ToList();
            // get the list of neighborhoods
            var neighborhoods = _neighborhoodAppService.GetAllNeighborhood().ToList();
            // get all of buildings 
            var buildings = _buildingsAppService.getAllBuildings();
            // get all of restoration types 
            var restorationTypes = _restorationTypeAppService.getAllResorationTypes();
            // get all of intervention types 
            var interventionTypes = _interventionTypeAppService.getAllInterventionTypes();
            // get all applications 
            var applications = _applicationsAppService.getAllApplications().ToList();
            // get all property ownerships 
            var propertyOwnerships = _propertyOwnershipAppService.getAllPropertyOwnerships();
            // populate yes no drop down list 
            var yesOrNo = new List<string>
            {
                "True",
                "False"
            };
            var fullNameList = new List<string>();
            foreach(var application in applications)
            {
                if (!String.IsNullOrWhiteSpace(application.fullName))
                {
                    fullNameList.Add(application.fullName);
                }
            }
            var fullNameArray = fullNameList.Distinct().ToArray();
            var applicationsViewModel = new ApplicationsViewModel()
            {
                fullNameArray = fullNameArray,
                buildingOutput = new GetBuildingsOutput(),
                PropertyOwnerShips= propertyOwnerships,
                YesOrNo= new SelectList(yesOrNo),
                InterventionTypes= interventionTypes,
                RestorationTypes = restorationTypes ,
                Applications = applications,
                Buildings = buildings,
                Building = new CreateBuildingsInput(),
                Neighborhoods = neighborhoods,
                BuildingTypes = buildingTypes,
                BuildingUses = buildingUses 



            };

            return View("ApplicationForm", applicationsViewModel);
        }

        public ActionResult PopulateApplicationForm(int buildingId)
        { 
            
            
            //instantiate object GetBuidlingsInput to get the building entity with given id 
            var getBuildingInput = new GetBuidlingsInput()
            {
              Id= buildingId
            };
            // retrieve the building with givin id 
            var building = _buildingsAppService.getBuildingsById(getBuildingInput);
            var buildingUnits = _buildingUnitsAppService.getAllBuildingUnits().ToList();
            var BuildingUnits = from BU in buildingUnits where BU.BuildingId == buildingId select BU;
            // declare viewmodel object to pass data to view 
            var applicationViewModel = new ApplicationsViewModel()
            {
                 buildingOutput = building
                
            };

             return Json(applicationViewModel, JsonRequestBehavior.AllowGet);
          //  return View("ApplicationForm", applicationViewModel);
        }

        public ActionResult PopulateDropDownListBuildingUnits(int buildingId)
        {
            var buildingUnitsApp = _buildingUnitsAppService.getAllBuildingUnits();
            var buildingUnits = (from BU in buildingUnitsApp where BU.BuildingId == buildingId select BU).ToList();
            List<SelectListItem> buildingUnitsList = new List<SelectListItem>();
            
              foreach (var buildingUnit in buildingUnits)
                 {
                     buildingUnitsList.Add(new SelectListItem { Text =buildingUnit.ResidentName, Value = buildingUnit.Id.ToString() });
                 }
        
            //var ListOfBuildingUnits = new List<string>();
            //foreach (var BuildingUnit in BuildingUnits)
            //{
            //    ListOfBuildingUnits.Add(BuildingUnit.ResidentName);
            //}
            //var applicationViewModelPL = new ApplicationsViewModel()
            //{
            //  // BuildingUnitList = ListOfBuildingUnits
            //   //  BuildingUnits = buildingUnits
            //      BuildingUnitList= buildingUnitsList,
            //    BuildingUnit = new GetBuildingUnitsOutput()
            //};
                return Json(buildingUnitsList, JsonRequestBehavior.AllowGet);
           // return View("ApplicationForm", applicationViewModelPL);
            
        }

        public ActionResult CreateApplication(CreateApplicationsInput model )
        {
            var application = new CreateApplicationsInput();
            application.fullName = model.fullName;
             application.isThereFundingOrPreviousRestoration = model.isThereFundingOrPreviousRestoration;
            var YourRadioButton = Request["RB1"];

            return null;

        }
    }
}