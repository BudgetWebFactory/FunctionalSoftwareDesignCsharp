using System.Net;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;
using static Dg.Framework.Web.WebHelpers;

namespace Dg.Framework.Web
{
    [TestFixture]
    public class ValidateModelStateTests
    {
        private static ModelStateDictionary emptyModelState = new ModelStateDictionary();

        private static ModelStateDictionary CreateNonMeptyModelState() 
        {
            var dictionary = new ModelStateDictionary();
            dictionary.AddModelError("Key", "Message");
            return dictionary;
        }

        private static ModelStateDictionary nonEmptyModelState = CreateNonMeptyModelState();

        [Test]
        public void HasNoValidationErrors_OkTrue() =>
            Assert.That(() => ValidateModelState(emptyModelState).Match(onOk: v => v, onErr: _ => false), Is.True);

        [Test]
        public void HasValidationErrors_ErrorBadRequest() =>
            Assert.That(() => ValidateModelState(nonEmptyModelState).Match(onOk: _ => HttpStatusCode.OK, onErr: e => e), Is.EqualTo(HttpStatusCode.BadRequest));
    }
}