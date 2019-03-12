using System;
using System.Threading.Tasks;
using Calendar.Controllers;
using Calendar.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calendar.Tests
{

    [TestClass]
    public class CalendarController_Create
    {
        private CalendarController controller;

        [TestInitialize]
        public void Initialize()
        {
            controller = new CalendarController();
        }

        [TestMethod]
        public async Task StartDateAfterEnd()
        {
            //Setup
            EventViewModel event1 = new EventViewModel("", new DateTime(2019, 3, 10), new DateTime(2019, 3, 1));

            //Test
            var x = await controller.Create(event1);

            //Assert
            Assert.AreEqual(controller.ModelState.IsValid, false);
        }

    }

}