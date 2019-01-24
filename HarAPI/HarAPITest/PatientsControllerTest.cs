using HarAPI.Controllers;
using HarAPI.Models;
using HarAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace HarAPITest
{
    [TestClass]
    public class PatientsControllerTest
    {
        readonly Mock<IPatientManager> manager;
        readonly List<Patient> patients;
        public PatientsControllerTest()
        {

            // Set up Prerequisites   
            manager = new Mock<IPatientManager>();
            patients = new List<Patient>();
            patients.Add(new Patient
            {
                Id = 1,
                PatientXML = @"<?xml version='1.0' encoding='utf - 16'?><Patient xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><Id>0</Id><Forenames>Siva</Forenames><Surname>Nomula</Surname><DateofBirth>1982-12-20T00:00:00</DateofBirth><Telephonenumbers><Type>Home</Type><Number>123456789</Number></Telephonenumbers><Telephonenumbers><Type>Mobile</Type><Number>987654321</Number></Telephonenumbers><Telephonenumbers><Type>Work</Type><Number>678912345</Number></Telephonenumbers></Patient>"
            });
            patients.Add(new Patient
            {
                Id = 2,
                PatientXML = @"<?xml version='1.0' encoding='utf - 16'?><Patient xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><Id>0</Id><Forenames>Siva Rama Kumar</Forenames><Surname>Nomula</Surname><DateofBirth>1985-12-08T00:00:00</DateofBirth><Telephonenumbers><Type>Home</Type><Number>123456789</Number></Telephonenumbers><Telephonenumbers><Type>Mobile</Type><Number>987654321</Number></Telephonenumbers><Telephonenumbers><Type>Work</Type><Number>678912345</Number></Telephonenumbers></Patient>"
            });
            manager.Setup(p => p.Get(1)).Returns(patients[0]);
            manager.Setup(p => p.GetAll()).Returns(patients);
            manager.Setup(p => p.Add(patients[1])).Returns(patients[1].Id);

        }
        [TestMethod]
        public void PatientGetTest()
        {   

            var patientModel = new PatientModel()
            {
                Forenames = "Siva",
                Surname = "Nomula",
                Gender = "M",
                DateofBirth = DateTime.Parse("20-12-1982"),
                Id = 1,
                Telephonenumbers = new List<PhoneNumber>() {
                    new PhoneNumber { PhoneType = PhoneType.Home, Number = "123456789" },
                    new PhoneNumber { PhoneType = PhoneType.Mobile, Number = "987654321" },
                    new PhoneNumber { PhoneType = PhoneType.Work, Number = "678912345" },
                }
            };
            var controller = new PatientsController(manager.Object);
            IActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkObjectResult;

            Assert.IsTrue(((PatientModel)contentResult.Value).Id.Equals(1));
            Assert.AreEqual("Siva", patientModel.Forenames);
        }
        [TestMethod]
        public void PatientAddTest()
        {   

            var patientModel = new PatientModel()
            {
                Forenames = "Siva",
                Surname = "Nomula",
                Gender = "M",
                DateofBirth = DateTime.Parse("20-12-1982"),
                Id = 1,
                Telephonenumbers = new List<PhoneNumber>() {
                    new PhoneNumber { PhoneType = PhoneType.Home, Number = "123456789" },
                    new PhoneNumber { PhoneType = PhoneType.Mobile, Number = "987654321" },
                    new PhoneNumber { PhoneType = PhoneType.Work, Number = "678912345" },
                }
            };
            var controller = new PatientsController(manager.Object);
            IActionResult actionResult = controller.Post(patientModel);
            var contentResult = actionResult as CreatedAtRouteResult;

            Assert.AreEqual(((PatientModel)contentResult.Value).Forenames,patientModel.Forenames);            
        }
        [TestMethod]
        public void PatientAddTestNull()
        {   

            var patientModel = new PatientModel()
            {
                Forenames = "Siva",
                Surname = "Nomula",
                Gender = "M",
                DateofBirth = DateTime.Parse("20-12-1982"),
                Id = 1,
                Telephonenumbers = new List<PhoneNumber>() {
                    new PhoneNumber { PhoneType = PhoneType.Home, Number = "123456789" },
                    new PhoneNumber { PhoneType = PhoneType.Mobile, Number = "987654321" },
                    new PhoneNumber { PhoneType = PhoneType.Work, Number = "678912345" },
                }
            };
            var controller = new PatientsController(manager.Object);
            IActionResult actionResult = controller.Post(null);
            var contentResult = actionResult as BadRequestObjectResult;

            Assert.AreEqual(contentResult.Value, "Patient is null.");            
        }
        [TestMethod]
        public void PatientAddTestValidationFailure()
        {   

            var patientModel = new PatientModel()
            {
                Forenames = "Si",
                Surname = "Nomula",
                Gender = "M",
                DateofBirth = DateTime.Parse("20-12-1982"),
                Id = 1,
                Telephonenumbers = new List<PhoneNumber>() {
                    new PhoneNumber { PhoneType = PhoneType.Home, Number = "123456789" },
                    new PhoneNumber { PhoneType = PhoneType.Mobile, Number = "987654321" },
                    new PhoneNumber { PhoneType = PhoneType.Work, Number = "678912345" },
                }
            };
            var controller = new PatientsController(manager.Object);
            controller.ModelState.AddModelError("Forenames", "Fornames should be minimum 3 characters");
            IActionResult actionResult = controller.Post(patientModel);
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestObjectResult));                    
        }
    }
}
