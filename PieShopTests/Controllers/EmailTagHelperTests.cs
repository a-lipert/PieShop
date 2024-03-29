﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using PieShop.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PieShopTests.Controllers
{
    public class EmailTagHelperTests
    {
        [Fact]
        public void Generates_Email_Link()
        {
            //Arrange

            var emailTagHelper = new EmailTagHelper()
            {
                Address = "test@pieshop.com",
                Content = "Email"
            };

            var tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(), string.Empty);

            var content = new Mock<TagHelperContent>();

            var tagHelperOutput = new TagHelperOutput("a", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(content.Object));

            //Act

            emailTagHelper.Process(tagHelperContext, tagHelperOutput);

            //Assert

            Assert.Equal("Email", tagHelperOutput.Content.GetContent());
            Assert.Equal("a", tagHelperOutput.TagName);
            Assert.Equal("mailto:test@pieshop.com", tagHelperOutput.Attributes[0].Value);
        }
    }
}
