using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ResumeManagement.Client;

public partial class EmployeesPage : ContentPage
{
    private string selectedImageBase64;
    public EmployeesPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        EmployeesListViewModel viewModel = new();
        viewModel.Clear();
        LoadCustomer();
    }

    private void LoadCustomer()
    {
        EmployeesListViewModel viewModel = new();
        try
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5194" : "http://localhost:5194")
            };
            InfoLabel.Text = $"BaseAddress : {client.BaseAddress}";
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/employees").Result;
            response.EnsureSuccessStatusCode();

            IEnumerable<EmployeeViewModel> customersFromService = response.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewModel>>().Result;

            foreach (EmployeeViewModel c in customersFromService.OrderBy(customer => customer.EmployeeName))
            {
                viewModel.Add(c);
            }
            InfoLabel.Text += $"\n{viewModel.Count} customers Loaded";
        }
        catch (Exception ex)
        {

            ErrorLabel.Text = ex.Message + "\n useing sample data instead";
            ErrorLabel.IsVisible = true;

            viewModel.AddSampleDataIfEmpty();
        }
        BindingContext = viewModel;
    }


    async void Customer_Tapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is not EmployeeViewModel c) return;
        await Navigation.PushAsync(new EmployeeDetailPage(BindingContext as EmployeesListViewModel, c));

    }


    async void Customers_Refreshing(object sender, EventArgs e)
    {
        if (sender is not ListView listView) return;
        listView.IsRefreshing = true;
        await Task.Delay(1500);
        listView.IsRefreshing = false;
    }
    async void Customer_Deleted(object sender, EventArgs e)
    {

        MenuItem menuItem = sender as MenuItem;
        if (menuItem.BindingContext is not EmployeeViewModel customer) return;

        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5194" : "http://localhost:5194");

            HttpResponseMessage response = await client.DeleteAsync($"api/employees/{customer.EmployeeId}");

            if (response.IsSuccessStatusCode)
            {
               
                (BindingContext as EmployeesListViewModel)?.Remove(customer);
            }
            else
            {
            
                await DisplayAlert("Error", "Failed to delete customer.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

    

    async void Add_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EmployeeDetailPage(BindingContext as EmployeesListViewModel));
    }
}
