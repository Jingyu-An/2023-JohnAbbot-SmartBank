using Day07AzureDb;

namespace SmartBankTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Customer_Customer_Test()
        {
            Customer customer = new Customer()
            {
                Full_name = "Test",
                Email = "Test@Test.com",
                Password = "Password",
                Phone_number = "1234567890",
                Address = "1234 Montreal",
                Created_at = DateTime.Now,
                Account_type = "Customer"
            };

            Assert.AreEqual("Test", customer.Full_name);
        }

        [TestMethod]
        public void Create_User_Test() 
        { 
            Users users = new Users()
            {
                Full_name = "Test",
                Email = "Test@Test.com",
                Password = "Password",
                Phone_number = "1234567890",
                Address = "1234 Montreal",
                Created_at = DateTime.Now,
                Account_type = "Customer"
            };

            string expected = "Test";

            Assert.AreEqual("Test", users.Full_name);
        }

        [TestMethod]
        public void Create_Account_Test()
        {
            Account account = new Account()
            {
                Customer_id = 1,
                User_id = 1,
            };

            string expected = "Smart Bank in Montreal";

            Assert.AreEqual(expected, account.Bank_branch_address);
        }

        [TestMethod]
        public void Create_Operation_Test()
        {
            Operation op1 = new Operation();
            Operation op2 = new Operation();

            int expected = 2;

            Assert.AreEqual(expected, op2.Transaction_id);
        }

        [TestMethod]
        public void Email_Validation_Test()
        {
            Users user = new Users();
            Customer customer = new Customer();
            string expected = "Please insert a valid email address.";

            try
            {
                user.Email = "Test";
                customer.Email = "Test";
            }
            catch (FormatException e)
            {
                string result = e.Message;
                Assert.AreEqual(expected, result);
            }

        }

        [TestMethod]
        public void PhoneNumber_Validation_Test()
        {
            Users user = new Users();
            Customer customer = new Customer();
            string expected = "Please insert 10 numbers, no dashes please.";

            try
            {
                user.Phone_number = "12345";
                customer.Phone_number = "32323232";
            }
            catch (FormatException e)
            {
                string result = e.Message;
                Assert.AreEqual(expected, result);
            }
            
        }

        [TestMethod]
        public void Password_Validation_Test()
        {
            Users user = new Users();
            Customer customer = new Customer();
            string expected = "Please insert a password between 5 to 10 characters long";
            try
            {
                user.Password = "12345";
                customer.Password = "12312332323232";
            }
            catch (FormatException e)
            {
                string result = e.Message;
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void FullName_Format_Validation_Test()
        {
            Users user = new Users();
            Customer customer = new Customer();
            string expectedFormat = "Invalid inputs, please insert letters only.";
            try
            {
                user.Full_name = "12345";
                customer.Full_name = "a";
            }
            catch (FormatException e)
            {
                string result = e.Message;
                Assert.AreEqual(expectedFormat, result);
            }

        }

        [TestMethod]
        public void FullName_Length_Validation_Test()
        {
            Users user = new Users();
            Customer customer = new Customer();
            string expectedLength = "Minimum and/or maximum character length exceeded(2-30).";
            try
            {
                user.Full_name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                customer.Full_name = "a";
            }
            catch (FormatException e)
            {
                string result = e.Message;
                Assert.AreEqual(expectedLength, result);
            }

        }

        [TestMethod]
        public void Address_Validation_Test()
        {
            Users user = new Users();
            Customer customer = new Customer();
            string expected = "Please start with street number and ";
            
            try
            {
                user.Address = "12345";
                customer.Address = "a";
            }
            catch (FormatException e)
            {
                string result = e.Message;
                Assert.AreEqual(expected, result);
            }
        }

    }
}