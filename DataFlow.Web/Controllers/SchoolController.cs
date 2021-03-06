﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DataFlow.Common.DAL;
using DataFlow.EdFi.Models.Resources;
using DataFlow.Web.Helpers;
using DataFlow.Web.Models;
using DataFlow.Web.Services;

namespace DataFlow.Web.Controllers
{
    public class SchoolController : BaseController
    {
        private readonly DataFlowDbContext dataFlowDbContext;
        private readonly EdFiService edFiService;

        public SchoolController(DataFlowDbContext dataFlowDbContext, EdFiService edFiService, IBaseServices baseService) : base(baseService)
        {
            this.dataFlowDbContext = dataFlowDbContext;
            this.edFiService = edFiService;
        }

        public ActionResult Index()
        {
            var schools = edFiService.GetSchools(0, 50);
            var uniqueDistrictIds = schools
                .Where(x => x.localEducationAgencyReference != null)
                .Select(x => x.localEducationAgencyReference.id)
                .Distinct()
                .ToList();

            var districts = new List<LocalEducationAgency>();

            uniqueDistrictIds.ForEach(districtId =>
            {
                var district = edFiService.GetLocalEducationAgencyById(districtId);
                districts.Add(district);
            });

            var schoolGrid = new List<SchoolViewModel.Grid>();
            schools.ForEach(x =>
            {
                var school = new SchoolViewModel.Grid()
                {
                    Id = x.id,
                    Name = x.nameOfInstitution,
                    Abbreviation = x.shortNameOfInstitution,
                    District = districts.FirstOrDefault(d=>d.id == x.localEducationAgencyReference.id)?.nameOfInstitution ?? string.Empty
                };
                schoolGrid.Add(school);
            });

            schoolGrid = schoolGrid.OrderBy(x => x.District).ThenBy(x => x.Name).ToList();

            return View(schoolGrid);
        }

        public ActionResult Details(string id)
        {
            var school = edFiService.GetSchool(id);

            var vm = new SchoolViewModel.Detail
            {
                Id = school.Id,
                Name = school.NameOfInstitution,
                Students = edFiService.GetStudentsBySchoolId(id),
                Staves = edFiService.GetStaffBySchoolId(id),
                Sections = edFiService.GetSectionsBySchoolId(id),
                Assessments = edFiService.GetAssessmentsBySchoolId(id)
            };

            return View(vm);
        }

        public ActionResult AssessmentDetails(string id)
        {
            var assessment = edFiService.GetAssessmentById(id);
            var studentAssessments = edFiService.GetStudentAssessmentsByAssessmentId(id);

            var vm = new SchoolViewModel.AssessmentDetail()
            {
                Assessment = assessment,
                StudentAssessments = studentAssessments
            };

            return View(vm);
        }

        public ActionResult Add()
        {
            var vm = new SchoolViewModel.AddOrUpdate
            {
                GradeLevels = GetGradeLevels(new List<SchoolGradeLevel>())
            };

            ViewBag.Districts = new SelectList(GetDistrictList, "Value", "Text");

            return View(vm);
        }

        public ActionResult Edit(string id)
        {
            var school = edFiService.GetSchool(id);

            var vm = new SchoolViewModel.AddOrUpdate
            {
                School = school,
                MailingAddress = school.Addresses.FirstOrDefault(x => x.AddressType == "Mailing") ?? school.Addresses.FirstOrDefault(),
                GradeLevels = GetGradeLevels(school.GradeLevels)
            };

            ViewBag.Districts = new SelectList(GetDistrictList, "Value", "Text");

            return View(vm);
        }

        public ActionResult Delete(string id)
        {
            var response = edFiService.DeleteSchool(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SchoolViewModel.AddOrUpdate vm)
        {
            if (!GetSelectedGradeLevels(vm).Any())
            {
                ModelState.AddModelError("NoGradesSelected", "Please select the grade levels for this school.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Districts = new SelectList(GetDistrictList, "Value", "Text");
                return View(vm);
            }

            SaveSchool(vm);
            LogService.Info(LogTemplates.InfoCrud("School", vm.School.NameOfInstitution,
                vm.School.SchoolId, LogTemplates.EntityAction.Added));

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SchoolViewModel.AddOrUpdate vm)
        {
            if (!GetSelectedGradeLevels(vm).Any())
            {
                ModelState.AddModelError("NoGradesSelected", "Please select the grade levels for this school.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Districts = new SelectList(GetDistrictList, "Value", "Text");
                return View(vm);
            }

            SaveSchool(vm);
            LogService.Info(LogTemplates.InfoCrud("School", vm.School.NameOfInstitution,
                vm.School.SchoolId, LogTemplates.EntityAction.Modified));

            return RedirectToAction("Index");
        }

        private void SaveSchool(SchoolViewModel.AddOrUpdate vm)
        {
            var selectedGradeLevels = GetSelectedGradeLevels(vm);

            var school = new EdFi.Models.Resources.School
            {
                SchoolId = vm.School.SchoolId,
                NameOfInstitution = vm.School.NameOfInstitution,
                ShortNameOfInstitution = vm.School.ShortNameOfInstitution,
                WebSite = vm.School.WebSite,
                StateOrganizationId = Convert.ToString(vm.School.SchoolId),
                LocalEducationAgencyReference = new LocalEducationAgencyReference()
                {
                    localEducationAgencyId = vm.School.LocalEducationAgencyReference.localEducationAgencyId
                },
                Addresses = new List<EducationOrganizationAddress>()
                {
                    new EducationOrganizationAddress()
                    {
                        AddressType = "Mailing",
                        StreetNumberName = vm.MailingAddress.StreetNumberName,
                        City = vm.MailingAddress.City,
                        StateAbbreviationType = vm.MailingAddress.StateAbbreviationType,
                        PostalCode = vm.MailingAddress.PostalCode
                    }
                },
                IdentificationCodes = new List<EducationOrganizationIdentificationCode>()
                {
                    new EducationOrganizationIdentificationCode()
                    {
                        educationOrganizationIdentificationSystemDescriptor = "School",
                        identificationCode = Convert.ToString(vm.School.SchoolId)
                    }
                },
                EducationOrganizationCategories = new List<EducationOrganizationCategory>()
                {
                    new EducationOrganizationCategory()
                    {
                        type = "School"
                    }
                },
                GradeLevels = new List<SchoolGradeLevel>(selectedGradeLevels)
            };

            edFiService.CreateSchool(school);
        }

        private List<SchoolGradeLevel> GetSelectedGradeLevels(SchoolViewModel.AddOrUpdate vm)
        {
            return vm.GradeLevels
                .Where(x => x.Checked)
                .Select(x => new SchoolGradeLevel()
                {
                    GradeLevelDescriptor = x.Text.Replace("_", " ") //In the view, for the labels to work we replace spaces with underscores
                })
                .ToList();
        }

        private List<CheckBox> GetGradeLevels(List<SchoolGradeLevel> selectedGrades)
        {
            var gradeLevelCheckBoxes = dataFlowDbContext.EdfiDictionary
                .Where(x => x.GroupSet == "levelDescriptors")
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new CheckBox() {Text = x.OriginalValue})
                .ToList();

            if (selectedGrades.Any())
            {
                selectedGrades.ForEach(grade =>
                {
                    var gradeCheckBox = gradeLevelCheckBoxes.FirstOrDefault(x => x.Text == grade.GradeLevelDescriptor);
                    if (gradeCheckBox != null)
                    {
                        gradeCheckBox.Checked = true;
                    }
                });
            }

            return gradeLevelCheckBoxes;
        }

        private List<SelectListItem> GetDistrictList
        {
            get
            {
                var entityList = new List<SelectListItem>();
                entityList.Add(new SelectListItem() { Text = "Select District", Value = string.Empty });
                entityList.AddRange(edFiService.GetLocalEducationAgencies(0, 100).Select(x =>
                    new SelectListItem()
                    {
                        Text = x.nameOfInstitution,
                        Value = x.localEducationAgencyId.ToString()
                    }));

                return entityList;
            }
        }
    }
}