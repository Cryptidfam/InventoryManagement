using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using InventoryManagement;
using Newtonsoft.Json;

public static class UserDataManager
{
    private static readonly string FilePath = AppDomain.CurrentDomain.BaseDirectory + "users.json";


    static UserDataManager()
    {
        //LoadUserData();
        // Users.CollectionChanged += Users_CollectionChanged; // NullReferenceException: Object reference not set to an instance of an object.


    }

    //private static void Users_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    //{
    //    SaveUserData();
    //}

    //public static void LoadUserData()
    //{
    //    if (!File.Exists(FilePath)) // For some reason, file is never created 
    //    {
    //        Users = new ObservableCollection<User>(); // even though it steps into here
    //        return;
    //    }

    //    var json = File.ReadAllText(FilePath);
    //    Users = JsonConvert.DeserializeObject<ObservableCollection<User>>(json) ?? new ObservableCollection<User>();
    //}

    //public static void SaveUserData()
    //{
    //    var json = JsonConvert.SerializeObject(Users, Formatting.Indented);
    //    File.WriteAllText(FilePath, json);
    //}

    public static bool ValidateUser(string username, string password, ObservableCollection<User> Users)
    {
        string passwordHash = ComputeHash(password);
        return Users.Any(user => user.Username == username && user.PasswordHash == passwordHash);
    }

    public static bool RegisterUser(string username, string password, ObservableCollection<User> Users)
    {
        if (Users == null) return true;
        // From LoadUserData, it jumps here
        if (Users.Any(user => user.Username == username)) // System.ArgumentNullException: 'Value cannot be null.
        {
            return false; // Username already exists
        }

        Users.Add(new User
        {
            Username = username,
            PasswordHash = ComputeHash(password) 
        });

        //SaveUserData(); // New user created
        return true;
    }

    private static string ComputeHash(string input)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
