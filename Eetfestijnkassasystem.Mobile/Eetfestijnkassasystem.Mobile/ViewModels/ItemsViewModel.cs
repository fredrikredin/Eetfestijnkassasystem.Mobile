using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Eetfestijnkassasystem.Mobile.Models;
using Eetfestijnkassasystem.Mobile.Views;
using Eetfestijnkassasystem.Shared.Interface;
using Eetfestijnkassasystem.Shared.DTO;

namespace Eetfestijnkassasystem.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);
        }

        //async Task ExecuteLoadItemsCommand()
        //{
        //    IsBusy = true;

        //    try
        //    {
        //        Items.Clear();
        //        var items = await DataStore.GetItemsAsync(true);
        //        foreach (var item in items)
        //        {
        //            Items.Add(item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex);
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                IRepository<Order> orderRepository = DependencyService.Get<IRepository<Order>>();

                // order 1
                //Order order1 = new Order() { CustomerName = "Linake", Comment = "Extra napkins", Seating = "Table 7", };
                //order1.MenuItems.Add(new Shared.DTO.MenuItem() { Name = "Cheese burger", Cost = 6.0 });
                //order1.MenuItems.Add(new Shared.DTO.MenuItem() { Name = "Coke", Cost = 3.0 });
                //order1.MenuItems.Add(new Shared.DTO.MenuItem() { Name = "Sparkling water", Cost = 2.0 });
                //order1 = await orderRepository.AddAsync(order1);

                //// order 2
                //Order order2 = new Order("Linake");
                //order2.MenuItems.Add(order1.MenuItems[0]);
                //order2.MenuItems.Add(new Shared.DTO.MenuItem("cheese  burger") { Cost = 99 });
                //order2.MenuItems.Add(new Shared.DTO.MenuItem("sparkling WATER ") { Cost = 999 });
                //order2 = await orderRepository.AddAsync(order2);

                //Order order2 = await orderRepository.GetAsync(2);
                //Shared.DTO.MenuItem waterSparkling = order2.MenuItems.FirstOrDefault(o => o.Name == "Sparkling water");

                //// order 3
                //Order order3 = new Order("3 sparkling water") { Seating = "Bar 2", Comment = "No ice" };
                //order3.MenuItems.Add(waterSparkling);
                //order3.MenuItems.Add(waterSparkling);
                //order3.MenuItems.Add(waterSparkling);
                //order3 = await orderRepository.AddAsync(order3);

                //
                var allOrders = await orderRepository.GetAllAsync();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
    }
}