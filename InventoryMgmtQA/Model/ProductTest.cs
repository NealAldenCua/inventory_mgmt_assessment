using InventoryMgmt.Model;
using System.ComponentModel.DataAnnotations;

// guide: https://learn.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2022

namespace InventoryMgmtQA.Model
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void TestAddProduct()
        {
            // create a new product with compliant attribute values
            Product product = new()
            {
                Name = "TestProduct",
                QuantityInStock = 1,
                Price = 1.0M
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // the product must be valid since all attributes values validated correctly
            Assert.IsTrue(isProductValid);
        }
        [TestMethod]
        public void TestAddProductPriceNegative()
        {
            Product product = new()
            {
                Name = "TestProduct",
                QuantityInStock = 1,
                Price = -1.0M // test for negative price
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            // the product must NOT be valid since the Price attribute has invalid value
            Assert.IsFalse(isProductValid);
        }

        // add more test cases here

        [TestMethod]
        public void TestAddProductNameMissing()
        {
            Product product = new()
            {
                Name = null, // Name is missing
                QuantityInStock = 1,
                Price = 1.0M
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            Assert.IsFalse(isProductValid);
        }

        [TestMethod]
        public void TestAddProductQuantityNegative()
        {
            Product product = new()
            {
                Name = "TestProduct",
                QuantityInStock = -1, // Negative quantity
                Price = 1.0M
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            Assert.IsFalse(isProductValid);
        }

        [TestMethod]
        public void TestAddProductQuantityZero()
        {
            Product product = new()
            {
                Name = "TestProduct",
                QuantityInStock = 0, // Zero quantity
                Price = 1.0M
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            Assert.IsTrue(isProductValid);
        }

        [TestMethod]
        public void TestAddProductPriceZero()
        {
            Product product = new()
            {
                Name = "TestProduct",
                QuantityInStock = 1,
                Price = 0.0M // Zero price
            };

            var results = new List<ValidationResult>();
            var context = new ValidationContext(product, null, null);
            bool isProductValid = Validator.TryValidateObject(product, context, results, true);

            Assert.IsTrue(isProductValid); // Assuming zero price is allowed
        }

        [TestMethod]
        public void TestAddProductQuantityWithSpecialCharacters()
        {
            // This case simulates input validation for numeric fields that might be entered as strings.
            string quantityInput = "1A"; // Simulate user input with special character

            int parsedQuantity;
            bool isParsed = int.TryParse(quantityInput, out parsedQuantity);

            Assert.IsFalse(isParsed);
        }

        [TestMethod]
        public void TestAddProductPriceWithSpecialCharacters()
        {
            // Simulate entering price as a string with special characters and trying to parse it
            string priceInput = "1.0$"; // Simulate user input with special character

            decimal parsedPrice;
            bool isParsed = decimal.TryParse(priceInput, out parsedPrice);

            Assert.IsFalse(isParsed);
        }
    }
}