using System.Collections.Generic;
using NUnit.Framework;
using static Dg.Framework.FeatureToggles.BusinessRules;

namespace Dg.Framework.FeatureToggles
{
    [TestFixture]
    public class IsActiveForUserTests
    {
        [Test]
        public void FeatureToggleIsInactive_UserIsNotActivated_ReturnsFalse() =>
            Assert.That(
                () => IsActiveForUser(new FeatureToggle("Id", false, new HashSet<int>()), 1), 
                Is.False);

        [Test]
        public void FeatureToggleIsInactive_UserIsActivated_ReturnsFalse() =>
            Assert.That(
                () => IsActiveForUser(new FeatureToggle("Id", false, new HashSet<int> { 1 }), 1), 
                Is.False);

        [Test]
        public void FeatureToggleIsActive_UserIsNotActivated_ReturnsFalse() => 
            Assert.That(
                () => IsActiveForUser(new FeatureToggle("Id", true, new HashSet<int>()), 1),
                Is.False);

        [Test]
        public void FeatureToggleIsActive_UserIsActivated_ReturnsTrue() => 
            Assert.That(
                () => IsActiveForUser(new FeatureToggle("Id", true, new HashSet<int> { 1 }), 1),
                Is.True);

        
    }
}