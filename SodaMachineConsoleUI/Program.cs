using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SodaMachineLibrary.DataAccess;
using SodaMachineLibrary.Logic;
using System;

namespace SodaMachineConsoleUI
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        private static ISodaMachineLogic _sodaMachine;
        private static string userId;

        static void Main(string[] args)
        {
            RegisterServies();

            _sodaMachine = _serviceProvider.GetService<ISodaMachineLogic>();
            userId = new Guid().ToString();

            string userSelection = "";

            Console.WriteLine("Welcome to our Soda Machine.");

            do
            {
                userSelection = ShowMenu();

                switch (userSelection)
                {
                    case "1": // Show Soda Price
                        ShowSodaPrice();
                        break;
                    case "2": // List Soda Options
                        ListSodaOptions();
                        break;
                    case "3": // Show Amount Deposited
                        ShowAmountDeposited();
                        break;
                    case "4": // Deposit Money
                        DepositMoney();
                        break;
                    case "5": // Cancel Transaction

                        break;
                    case "6": // Request Soda

                        break;
                    case "9": // Close Machine
                        // Don't do anything - allow this to go to while, which will EXIT the loop
                        break;
                    default:
                        // Don't do anything - allow this to go to while, which will RESTART the loop
                        break;
                }
                Console.Clear();
            } while (userSelection != "9");

            Console.WriteLine("Thanks, have a nice day.");
            Console.WriteLine("Press return to quit...");

            Console.ReadLine();

        }

        private static void DepositMoney()
        {
            // Get what to deposit
            Console.WriteLine("How much would you like to add to machine: ");
            string amountText = Console.ReadLine();

            bool isValidAmount = decimal.TryParse(amountText, out decimal amountAdded);

            // Deposit that amount
            _sodaMachine.MoneyInserted(userId, amountAdded);
        }

        private static void ShowAmountDeposited()
        {
            var amountDposited = _sodaMachine.GetMoneyInsertedTotal(userId);

            Console.Clear();
            Console.WriteLine($"You have deposited { String.Format("{0:C}", amountDposited) }");
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        private static void ListSodaOptions()
        {
            var sodas = _sodaMachine.ListTypesOfSoda();

            Console.Clear();
            Console.WriteLine("The soda options are:");
            sodas.ForEach(x => Console.WriteLine(x.Name));
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        private static void ShowSodaPrice()
        {
            var sodaPrice = _sodaMachine.GetSodaPrice();
            Console.Clear();
            Console.WriteLine($"The price for soda is { String.Format("{0:C}", sodaPrice) }");
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.ReadLine();
        }

        private static string ShowMenu()
        {
            Console.WriteLine("Please make a selection from the following options:");
            Console.WriteLine("1: Show Soda Price");
            Console.WriteLine("2: List Soda Options");
            Console.WriteLine("3: Show Amount Deposited");
            Console.WriteLine("4: Deposit Money");
            Console.WriteLine("5: Cancel Transaction");
            Console.WriteLine("6: Request Soda");
            Console.WriteLine("9: Close Machine");

            return Console.ReadLine();
        }

        private static void RegisterServies()
        {
            var collection = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();


            collection.AddSingleton(config);
            collection.AddTransient<IDataAccess, TextFileDataAccess>();
            collection.AddTransient<ISodaMachineLogic, SodaMachineLogic>();

            _serviceProvider = collection.BuildServiceProvider();
        }
    }
}
