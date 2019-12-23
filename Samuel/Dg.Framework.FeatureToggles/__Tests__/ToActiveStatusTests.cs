using System.Collections.Generic;
using NUnit.Framework;
using static Dg.Framework.FeatureToggles.Transformations;

namespace Dg.Framework.FeatureToggles
{
    [TestFixture]
    public class ToActiveStatusTests
    {
        [Test]
        public void FeatureToggleIsInactive_UserIsNotActivated_ReturnsInactive() =>
            Assert.That(
                () => ToActiveStatusForUser(new FeatureToggle("Id", false, new HashSet<int>()), 1), 
                Is.EqualTo(new UserActiveStatus("Id", 1, false)));

        [Test]
        public void FeatureToggleIsInactive_UserIsActivated_ReturnsInactive() =>
            Assert.That(
                () => ToActiveStatusForUser(new FeatureToggle("Id", false, new HashSet<int> { 1 }), 1), 
                Is.EqualTo(new UserActiveStatus("Id", 1, false)));

        [Test]
        public void FeatureToggleIsActive_UserIsNotActivated_ReturnsInactive() => 
            Assert.That(
                () => ToActiveStatusForUser(new FeatureToggle("Id", true, new HashSet<int>()), 1),
                Is.EqualTo(new UserActiveStatus("Id", 1, false)));

        [Test]
        public void FeatureToggleIsActive_UserIsActivated_ReturnsActive() => 
            Assert.That(
                () => ToActiveStatusForUser(new FeatureToggle("Id", true, new HashSet<int> { 1 }), 1),
                Is.EqualTo(new UserActiveStatus("Id", 1, true)));

        
    }
}