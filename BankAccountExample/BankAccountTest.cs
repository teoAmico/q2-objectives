namespace DemoTestApp
{
    [TestClass]
    public class BankAccountTest
    {
        [TestInitialize]
        public void SetUp() { 
            //init mocks if any here
        }

        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bin", beginningBalance);

            account.Debit(debitAmount);
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            double beginningBalance = 11.99;
            double creditAmount = 4.55;
            double expected = 16.54;
            BankAccount account = new BankAccount("Mr. Jhon", beginningBalance);
            account.Credit(creditAmount);
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not credit correctly");
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Debit_WhenTheAmountToDebitIsMoreThenBalance_returnsExceptions()
        {
            double beginningBalance = 11.99;
            double debitAmount = 79.00;
            BankAccount account = new BankAccount("Mr. Jhon", beginningBalance);
            
            account.Debit(debitAmount);

        }
    }
}