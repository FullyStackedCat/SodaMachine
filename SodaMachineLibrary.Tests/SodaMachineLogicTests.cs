using SodaMachineLibrary.Logic;
using SodaMachineLibrary.Models;
using SodaMachineLibrary.Tests.Mocks;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SodaMachineLibrary.Tests
{
    public class SodaMachineLogicTests
    {
        [Fact]
        public void AddtoCoinInventory_ShouldWork()
        {
            MockDataAccess da = new MockDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);
            List<CoinModel> coins = new List<CoinModel>
            {
                new CoinModel{ Name = "Quarter", Amount = 0.25M},
                new CoinModel{ Name = "Quarter", Amount = 0.25M},
                new CoinModel{ Name = "Dime", Amount = 0.10M}

            };

            logic.AddToCoinInventory(coins);

            int expected = 6;
            int actual = da.CoinInventory.Where(x => x.Name == "Quarter").Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddToSodaInventory_ShouldWork()
        {
            MockDataAccess da = new MockDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            List<SodaModel> sodas = new List<SodaModel>
            {
                new SodaModel{ Name = "Coke", SlotOccupied = "1"},
                new SodaModel{ Name = "Coke", SlotOccupied = "1"}
            };

            logic.AddSodaToInventory(sodas);


            int expected = 7;
            int actual = da.SodaInventory.Where(x => x.Name == "Coke").Count();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EmptyMoneyFromMachine_ShouldWork()
        {
            MockDataAccess da = new MockDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            var (_, expected, _) = da.MachineInfo;
            decimal actual = logic.EmptyMoneyFromMachine();

            Assert.Equal(expected, actual);

            expected = 0;
            (_, actual, _) = da.MachineInfo;

            Assert.Equal(expected, actual);

        }

        [Fact]
        public void GetCoinInventory_ShouldWork()
        {
            MockDataAccess da = new MockDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            var coins = logic.GetCoinInventory();

            var expected = da.CoinInventory.Where(x => x.Name == "Quarter").Count();
            int actual = coins.Where(x => x.Name == "Quarter").Count();


            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetCurrentIncome_ShouldWork()
        {
            MockDataAccess da = new MockDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            var (_, expected, _) = da.MachineInfo;
            var actual = logic.GetCurrentIncome();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetMoneyInsertedTotal_ShouldWork()
        {
            MockDataAccess da = new MockDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);

            decimal expected = 0.65M;
            da.UserCredit.Add("test", expected);

            decimal actual = logic.GetMoneyInsertedTotal("test");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSodaInventory_ShouldWork()
        {
            MockDataAccess da = new MockDataAccess();
            SodaMachineLogic logic = new SodaMachineLogic(da);


            int actual = logic.GetSodaInventory().Count();
            int expected = 8;

            Assert.Equal(expected, actual);
        }
    }
}
