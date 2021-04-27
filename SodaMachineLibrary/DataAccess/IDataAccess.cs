using SodaMachineLibrary.Models;
using System.Collections.Generic;

namespace SodaMachineLibrary.DataAccess
{
    public interface IDataAccess
    {
        // Gets list of soda types
        List<SodaModel> SodaInventory_GetTypes();
        bool SodaInventory_CheckIfSodaInStock(SodaModel soda);
        SodaModel SodaInventory_GetSoda(SodaModel soda, decimal amount);
        void SodaInventory_AddSodas(List<SodaModel> sodas);
        List<SodaModel> SodaInventory_GetAll();

        // Money inserted
        void UserCredit_Insert(string userId, decimal amount);
        void UserCredit_Clear(string userId);
        decimal UserCredit_Total(string userId);
        void UserCredit_Deposit(string userId);

        // Machine info
        decimal MachineInfo_SodaPrice();
        decimal MachineInfo_EmptyCash();
        decimal MachineInfo_CashOnHand();
        decimal MachineInfo_TotalIncome();


        // Get coin inventory
        List<CoinModel> CoinInventory_WithdrawCoins(decimal coinValue, int quantity);
        List<CoinModel> CoinInventory_GetAll();
        void CoinInventory_AddCoins(List<CoinModel> coins);


    }
}
