﻿using System.Diagnostics.CodeAnalysis;

namespace ZbW.Testing.Dms.Client.Views
{
    using System.Windows;

    using ZbW.Testing.Dms.Client.ViewModels;

    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    [ExcludeFromCodeCoverage]
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
        }
    }
}