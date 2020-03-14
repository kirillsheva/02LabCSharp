using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using _01LabShevchenko.Models;
using _01LabShevchenko.Tools;
using _01LabShevchenko.Tools.Exceptions;
using _01LabShevchenko.ViewModel;

namespace _01LabShevchenko
{
    internal class ZodiacViewModel : BaseViewModel, ILoaderOwner
    {
        #region Fields
        private string _name;
        private string _surname;
        private string _info;
        private string _secondaryInfo;
        private string _emailAddress;
        private DateTime _date = DateTime.Today;
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;

        #region Commands

        private RelayCommand<object> _submitCommand;

        #endregion

        #endregion

        
        #region Properties
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string MainInfo
        {
            get => _info;
            set
            {
                _info = value;
                OnPropertyChanged();
            }
        }
        public string SecondaryInfo
        {
            get => _secondaryInfo;
            set
            {
                _secondaryInfo = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                 OnPropertyChanged();
            }
        }

        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand<object> SubmitCommand
        {
            get
            {
                return _submitCommand ??= new RelayCommand<object>(
                    ProcessingInf, o=>CanExecuteCommand());
            }
        }
        #endregion

        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(Name) && 
                   !string.IsNullOrWhiteSpace(Surname) && 
                   !string.IsNullOrWhiteSpace(EmailAddress);
        }

        private async void ProcessingInf(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
                {
                    try
                    {
                        Thread.Sleep(1000);
                        Person person = new Person(Name,Surname,EmailAddress,Date);
                        MainInfo = "Name: " + Name + "   Surname: " + Surname + "\nEmail: " + EmailAddress + "\nBirthday date: " + Date.ToShortDateString();
                        SecondaryInfo = "Is Adult: " + person.IsAdult + "     Is Birthday: " + person.IsBirthday + "\nSun Sign: " + person.WesternZodiac + "\nChinese Sign: " + person.ChineseZodiac;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                });
            LoaderManager.Instance.HideLoader();
        }

        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get => _isControlEnabled;
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }
        internal ZodiacViewModel()
        {
            LoaderManager.Instance.Initialize(this);
        }
    }
}
