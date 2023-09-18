using Newtonsoft.Json;
using ResumeManagement.Client.Models;
using System.Net.Http.Headers;
using System.Text;

namespace ResumeManagement.Client;

public partial class EmployeeDetailPage : ContentPage
{
    private EmployeesListViewModel employees;
    FileUpload imageUpload { get; set; }
    private string SelectedImageBase64 { get; set; }

    public EmployeeDetailPage(EmployeesListViewModel employees)
    {
        InitializeComponent();
        this.employees = employees;
        BindingContext = new EmployeeViewModel();
        Title = "Add Employee";
        SaveButton.IsVisible = true;
        UpdateButton.IsVisible = false;

      
        imageUpload = new FileUpload();
    }
    public EmployeeDetailPage(EmployeesListViewModel employees, EmployeeViewModel employee)
    {
        InitializeComponent();
        this.employees = employees;
        BindingContext = employee;
        Title = "Edit Employee";
        SaveButton.IsVisible = false;
        UpdateButton.IsVisible = true;

        (BindingContext as EmployeeViewModel)?.NotifyPropertyChanged("ImageUrl");

        imageUpload = new FileUpload();

    }




    private async void OnCounterClicked(object sender, EventArgs e)
    {
        var img = await imageUpload.OpenMediaPickerAsync();
        var imageFile = await imageUpload.Upload(img);
        SelectedImageBase64 = imageFile?.ByteBase64; // Store the base64-encoded image data
        Upload_Image.Source = ImageSource.FromStream(() => imageUpload.ByteArrayToStream(imageUpload.StringToByteBase64(imageFile.ByteBase64)));

    }

    async void SaveButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (BindingContext is EmployeeViewModel employeeViewModel)
            {
                
                employeeViewModel.ImageUrl = SelectedImageBase64;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5194" : "http://localhost:5194");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsonEmployee = JsonConvert.SerializeObject(employeeViewModel);
                StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/employees", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Employee saved successfully.", "OK");
                    await Navigation.PopAsync(animated: true);
                }
                else
                {
                    await DisplayAlert("Error", "Failed to save employee.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }


    async void UpdateButton_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (BindingContext is EmployeeViewModel employeeViewModel)
            {
      
                employeeViewModel.ImageUrl = SelectedImageBase64;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5194" : "http://localhost:5194");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string jsonEmployee = JsonConvert.SerializeObject(employeeViewModel);
                StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"api/employees/{employeeViewModel.EmployeeId}", content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Employee updated successfully.", "OK");
                    await Navigation.PopAsync(animated: true);
                }
                else
                {
                    await DisplayAlert("Error", "Failed to update employee.", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }
    }

   

}