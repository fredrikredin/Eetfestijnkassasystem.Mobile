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
                IRepository<OrderDto> orderRepository = DependencyService.Get<IRepository<OrderDto>>();

                // order 1
                OrderDto order1 = new OrderDto() { CustomerName = "Linake", Comment = "Extra napkins", Seating = "Table 7", };
                order1.MenuItems.Add(new MenuItemDto() { Name = "Cheese burger", Cost = 6.0 });
                order1.MenuItems.Add(new MenuItemDto() { Name = "Coke", Cost = 3.0 });
                order1.MenuItems.Add(new MenuItemDto() { Name = "Sparkling water", Cost = 2.0 });
                order1 = await orderRepository.AddAsync(order1);

                //OrderDto order1 = await orderRepository.GetByIdAsync(1);

                MenuItemDto cheeseBurger = order1.MenuItems[0];
                MenuItemDto sparklingWater = order1.MenuItems[2];

                // order 2
                OrderDto order2 = new OrderDto("Linake");
                order2.MenuItems.Add(cheeseBurger);
                order2.MenuItems.Add(new MenuItemDto("vegan burger") { Cost = 99 });
                order2.MenuItems.Add(new MenuItemDto("sparkling WATER ") { Cost = 999 });
                order2 = await orderRepository.AddAsync(order2);

                // order 3
                OrderDto order3 = new OrderDto("3 sparkling water") { Seating = "Bar 2", Comment = "No ice" };
                order3.MenuItems.Add(sparklingWater);
                order3.MenuItems.Add(sparklingWater);
                order3.MenuItems.Add(sparklingWater);
                order3 = await orderRepository.AddAsync(order3);

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