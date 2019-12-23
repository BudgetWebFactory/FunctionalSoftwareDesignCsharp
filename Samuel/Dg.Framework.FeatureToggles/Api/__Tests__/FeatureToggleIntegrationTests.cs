using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NUnit.Framework;

namespace Dg.Framework.FeatureToggles.Api
{
    [TestFixture]
    public class FeatureToggleIntegrationTests
    {
        const int enabledUserId = 1;
        const int disabledUserId = 2;
        const string disabledFeatureToggleId = "TestDisabled";
        const string enabledFeatureToggleId = "TestEnabled";
        const string nonExistentFeatureToggleId = "TestNonExistent";

        [Test]
        public void Get_NonExistentFeatureToggleId_ResponseCode404()
        {
            var result = new FeatureToggleApiController(LoadFeatureToggles).Get(nonExistentFeatureToggleId, enabledUserId);

            Assert.That(result, Is.AssignableTo<IStatusCodeActionResult>());
            Assert.That(((IStatusCodeActionResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.NotFound));
        }

        [Test]
        public void Get_ExistentFeatureToggleId_ResponseCode200()
        {
            var result = new FeatureToggleApiController(LoadFeatureToggles).Get(enabledFeatureToggleId, enabledUserId);

            Assert.That(result, Is.AssignableTo<IStatusCodeActionResult>());
            Assert.That(((IStatusCodeActionResult)result).StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
        }

        [Test]
        public void Get_ExistentFeatureToggleId_FeatureToggleIdMatchesRequest()
        {
            var result = new FeatureToggleApiController(LoadFeatureToggles).Get(enabledFeatureToggleId, enabledUserId);

            Assert.That(result, Is.AssignableTo<ObjectResult>());
            var objectResult = (ObjectResult)result;
            Assert.That(objectResult.Value, Is.AssignableTo<FeatureToggleResponse>());
            var featureToggleResponse = (FeatureToggleResponse)objectResult.Value;
            Assert.That(featureToggleResponse.Id, Is.EqualTo(enabledFeatureToggleId));
        }

        [Test]
        public void Get_UserIsDisabled_FlagInResponseIsFalse()
        {
            var result = new FeatureToggleApiController(LoadFeatureToggles).Get(enabledFeatureToggleId, disabledUserId);

            Assert.That(result, Is.AssignableTo<ObjectResult>());
            var objectResult = (ObjectResult)result;
            Assert.That(objectResult.Value, Is.AssignableTo<FeatureToggleResponse>());
            var featureToggleResponse = (FeatureToggleResponse)objectResult.Value;
            Assert.That(featureToggleResponse.IsActive, Is.False);
        }

        [Test]
        public void Get_UserIsEnabled_FlagInResponseIsTrue()
        {
            var result = new FeatureToggleApiController(LoadFeatureToggles).Get(enabledFeatureToggleId, enabledUserId);

            Assert.That(result, Is.AssignableTo<ObjectResult>());
            var objectResult = (ObjectResult)result;
            Assert.That(objectResult.Value, Is.AssignableTo<FeatureToggleResponse>());
            var featureToggleResponse = (FeatureToggleResponse)objectResult.Value;
            Assert.That(featureToggleResponse.IsActive, Is.True);
        }

        [Test]
        public void Get_FeatureToggleIsDisabledAndUserIsEnabled_FlagInResponseIsFalse()
        {
            var result = new FeatureToggleApiController(LoadFeatureToggles).Get(disabledFeatureToggleId, enabledUserId);

            Assert.That(result, Is.AssignableTo<ObjectResult>());
            var objectResult = (ObjectResult)result;
            Assert.That(objectResult.Value, Is.AssignableTo<FeatureToggleResponse>());
            var featureToggleResponse = (FeatureToggleResponse)objectResult.Value;
            Assert.That(featureToggleResponse.IsActive, Is.False);
        }

        [Test]
        public void Get_FeatureToggleIsDisabledAndUserIsDisabled_FlagInResponseIsFalse()
        {
            var result = new FeatureToggleApiController(LoadFeatureToggles).Get(disabledFeatureToggleId, disabledUserId);

            Assert.That(result, Is.AssignableTo<ObjectResult>());
            var objectResult = (ObjectResult)result;
            Assert.That(objectResult.Value, Is.AssignableTo<FeatureToggleResponse>());
            var featureToggleResponse = (FeatureToggleResponse)objectResult.Value;
            Assert.That(featureToggleResponse.IsActive, Is.False);
        }

        private static IList<FeatureToggle> LoadFeatureToggles() =>
            new List<FeatureToggle> 
            {
                new FeatureToggle(enabledFeatureToggleId, true, new HashSet<int> { enabledUserId }),
                new FeatureToggle(disabledFeatureToggleId, false, new HashSet<int> { enabledUserId })
            };
    }
}