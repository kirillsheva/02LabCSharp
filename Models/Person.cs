using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using _01LabShevchenko.Tools.Exceptions;
using _01LabShevchenko.ViewModel;


namespace _01LabShevchenko.Models
{
    internal class Person : BaseViewModel
    {
        #region Fields
        private readonly EmailAddressAttribute _emailAddressAttribute = new EmailAddressAttribute();
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _date;
        private readonly string _westernzodiac;
        private readonly string _chinesezodiac;
        private readonly bool _isAdult;
        private readonly bool _isBirthday;



        #endregion

        #region Properties

        protected internal string Name
        {
            get => _name;
            private set
            {
                _name = value;
                OnPropertyChanged();
            }

        }
        protected internal string Surname
        {
            get => _surname;
            private set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        protected internal string EmailAddress
        {
            get => _email;
            private set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        private DateTime BirthdayDate => _date;

        protected internal bool IsAdult
        {
            get => _isAdult;
            set => throw new NotImplementedException();
        }

        protected internal bool IsBirthday
        {
            get => _isBirthday;
            set => throw new NotImplementedException();
        }

        protected internal string WesternZodiac
        {
            get => _westernzodiac;
            set => throw new NotImplementedException();
        }

        protected internal string ChineseZodiac
        {
            get => _chinesezodiac;
            set => throw new NotImplementedException();
        }

        #endregion

        #region Constructors

        protected internal Person(string name, string surname, string email, DateTime date)
        {
            _name = name;
            _surname = surname;
            if (_emailAddressAttribute.IsValid(email))
            {
                _email = email;
            }
            else {
                throw new IncorrectEmailException("Incorrect email address!");
            }
            _date = date;
            _westernzodiac = SunSign();
            _chinesezodiac = ChineseSign();
            _isAdult = CalculateAge() >= 18;
            _isBirthday = (BirthdayDate.Day == DateTime.Today.Day && BirthdayDate.Month == DateTime.Today.Month);
       
        }

        protected internal Person(string name, string surname, string email) : this(name, surname, email,DateTime.Today )
        {

        }
        protected internal Person(string name, string surname, DateTime date) : this(name, surname, "", date)
        {

        }
        #endregion

        #region Methods

        private string ChineseSign()
        {
            return CalcChineseZodiac();
        }

        private string SunSign()
        {
            return CalcWesternZodiac();
        }


        #endregion

        #region PrivateMethods

        private int CalculateAge()
        {

            var currentDate = DateTime.Today;
            var fullYears = currentDate.Year - BirthdayDate.Year;
            if (BirthdayDate.Month > currentDate.Month)
            {
                --fullYears;
            }
            else if (BirthdayDate.Month == currentDate.Month)
            {
                if (BirthdayDate.Day > currentDate.Day)
                {
                    --fullYears;
                }
            }
            if (BirthdayDate.Month == currentDate.Month && BirthdayDate.Day == currentDate.Day)
            {
                MessageBox.Show("Mister, You are older today, than yesterday! Congrats,you are " + fullYears + " years old!!!");

            }
            if (fullYears < 0)
            {
                throw new TooOldException("Wait a bit, were you even born? Try again.");
            }
            if (fullYears > 135)
            {
                throw new BirthdayInFutureException("You have picked the wrong date... Try again. ");
            }
            return fullYears;
        }

        private string CalcWesternZodiac()
        {
            var date = Convert.ToDateTime(BirthdayDate);
            return date.Month switch
            {
                1 when date.Day >= 21 => "Aquarius",
                2 when date.Day <= 20 => "Aquarius",
                2 when date.Day >= 21 => "Pisces",
                3 when date.Day <= 20 => "Pisces",
                3 when date.Day >= 21 => "Aries",
                4 when date.Day <= 20 => "Aries",
                4 when date.Day >= 21 => "Taurus",
                5 when date.Day <= 20 => "Taurus",
                5 when date.Day >= 21 => "Gemini",
                6 when date.Day <= 21 => "Gemini",
                6 when date.Day >= 22 => "Cancer",
                7 when date.Day <= 22 => "Cancer",
                7 when date.Day >= 23 => "Leo",
                8 when date.Day <= 23 => "Leo",
                8 when date.Day >= 24 => "Virgo",
                9 when date.Day <= 23 => "Virgo",
                9 when date.Day >= 24 => "Libra",
                10 when date.Day <= 22 => "Libra",
                10 when date.Day >= 23 => "Scorpio",
                11 when date.Day <= 22 => "Scorpio",
                11 when date.Day >= 23 => "Sagittarius",
                12 when date.Day <= 21 => "Sagittarius",
                12 when date.Day >= 22 => "Capricorn",
                1 when date.Day <= 20 => "Capricorn",
                _ => ""
            };
        }

        private string CalcChineseZodiac()
        {
            var date = Convert.ToDateTime(BirthdayDate).Year % 12 + 1;
            return date switch
            {
                1 => "Monkey",
                2 => "Rooster",
                3 => "Dog",
                4 => "Pig",
                5 => "Rat",
                6 => "Ox",
                7 => "Tiger",
                8 => "Rabbit",
                9 => "Dragon",
                10 => "Snake",
                11 => "Horse",
                12 => "Goat",
                _ => "Secret animal 0_0"
            };
        }

        #endregion
    }
}
