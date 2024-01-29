using System;
using System.ComponentModel;
using System.Globalization;
using TocTocToc.Models.Dto;
using TocTocToc.Models.Model;
using TocTocToc.Models.View;
using TocTocToc.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetPopup : Xamarin.CommunityToolkit.UI.Views.Popup
    {
        private static BudgetDtoModel _ePayBudgetDto = new();
        private readonly BudgetModel _ePayBudget = new();
        private DateTime _dateNow;
        private TimeSpan _timeNow;
        private DateTime _dateTimeRef;


        public BudgetPopup(BudgetDtoModel ePayBudget)
        {
            InitializeComponent();

            InitValues(ePayBudget);
        }


        private void InitValues(BudgetDtoModel ePayBudget)
        {
            
            if (ePayBudget != null && ePayBudget.Budget != 0)
            {
                _ePayBudgetDto = ePayBudget;
                _ePayBudgetDto.EndDate = _ePayBudgetDto.StartDate.AddDays(ePayBudget.Duration);
                _dateNow = ePayBudget.StartDate;
                CopyModel.EPayBudgetDtoToModel(_ePayBudgetDto, _ePayBudget);

            }
            else
            {
                _dateNow = DateTime.Now;
                _timeNow = _dateNow.TimeOfDay;
                _ePayBudget.StartDate = _dateNow;
                _ePayBudget.EndDate = _dateNow;
                _ePayBudget.Duration = "1";
                _ePayBudget.Budget = _ePayBudget.BudgetMini.ToString();
                _ePayBudget.StartTime = _timeNow;
                //XNameStartDate.MinimumDate = _dateNow;
                //XNameEndDate.MinimumDate = _dateNow;
                //XNameTimeStart.Time = _timeNow;
            }

            //XNameSliderBudget.Value = double.Parse(_ePayBudget.Budget);

            _dateTimeRef = new DateTime(_dateNow.Year, _dateNow.Month, _dateNow.Day, _dateNow.Hour, _dateNow.Minute, 0);
            BindingContext = _ePayBudget;
        }



        private void OnBudgetEntry(object sender, EventArgs e)
        {

            var entry = (Entry)sender;
            var budget = !string.IsNullOrEmpty(entry.Text) ? int.Parse(entry.Text) : _ePayBudget.BudgetMini;
            if (string.IsNullOrEmpty(entry.Text)) _ePayBudget.Budget = _ePayBudget.BudgetMini.ToString();
            if (budget > _ePayBudget.BudgetMaxi)
            {
                _ePayBudget.Budget = _ePayBudget.BudgetMaxi.ToString();
            }

            XNameSliderBudget.Value = budget;
        }


        private void OnIsBudgetEntry(object sender, EventArgs e)
        {
            _ePayBudget.IsBudgetEntry = !_ePayBudget.IsBudgetEntry;
        }


        private void OnSliderBudget(object sender, ValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            var budget = Math.Round(slider.Value);
            if (budget == 0) // To correct the error if slider.Minimum = 1;
            {
                budget = _ePayBudget.BudgetMini;
                XNameSliderBudget.Value = _ePayBudget.BudgetMini;
            }
            _ePayBudget.Budget = budget.ToString(CultureInfo.InvariantCulture);
        }

        private void OnPlusDay(object sender, EventArgs e)
        {
            var duration = 1;
            if (!string.IsNullOrEmpty(_ePayBudget.Duration))
                duration = int.Parse(_ePayBudget.Duration);
            var day = duration;
            ++day;
            _ePayBudget.Duration = day.ToString();
        }

        private void OnMinusDay(object sender, EventArgs e)
        {
            var duration = 1;
            if (!string.IsNullOrEmpty(_ePayBudget.Duration))
                duration = int.Parse(_ePayBudget.Duration);
            if (duration <= 0) return;

            var day = duration;
            if (day != 1)
                --day;
            _ePayBudget.Duration = day.ToString();
        }

        private void OnStartDate(object sender, DateChangedEventArgs e)
        {
            var datePicker = (DatePicker)sender;
            var startDate = datePicker.Date;

            var duration = !string.IsNullOrEmpty(_ePayBudget.Duration) ? int.Parse(_ePayBudget.Duration) : 0;

            _ePayBudget.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0);
            var time = startDate.TimeOfDay;

            XNameEndDate.MinimumDate = startDate;
            XNameEndDate.Date = RecalculateDateFromDuration(startDate, duration);
            XNameTimeStart.Time = time;
        }


        private void OnEndDate(object sender, DateChangedEventArgs e)
        {

            var datePicker = (DatePicker)sender;
            var endDate = datePicker.Date;

            var duration = RecalculateDurationFromDate(_ePayBudget.StartDate, endDate);

            _ePayBudget.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 0);
            _ePayBudget.Duration = duration.ToString();

        }


        private void OnTimeStart(object sender, PropertyChangedEventArgs e)
        {
            var timePicker = (TimePicker)sender;
            var time = timePicker.Time;
            var startDate = _ePayBudget.StartDate.Date;

            var selectedDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, time.Hours, time.Minutes, 0);

            _ePayBudget.StartDate = selectedDateTime;
            if (selectedDateTime.Year == 1) return;
            _ePayBudget.IsWrongTime = selectedDateTime < _dateTimeRef;
        }


        private void OnDuration(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            var duration = !string.IsNullOrEmpty(entry.Text) ? int.Parse(entry.Text) : 1;
            if (string.IsNullOrEmpty(entry.Text) || int.Parse(entry.Text) == 0)
            {
                _ePayBudget.Duration = "1";
                duration = 1;
            }
            XNameEndDate.Date = RecalculateDateFromDuration(_ePayBudget.StartDate, duration);
        }


        private static DateTime RecalculateDateFromDuration(DateTime date, int duration)
        {
            var dateUpdated = date.AddDays((double)duration);
            return dateUpdated;

        }

        private static int RecalculateDurationFromDate(DateTime startDate, DateTime endDate)
        {
            if (!(endDate.Date >= startDate.Date))
                throw new Exception("[ Error : EndDate can't be lower that startDate ]");

            var durationUpdated = (endDate.Date - startDate.Date).Days;

            return durationUpdated;
        }


        protected override object GetLightDismissResult()
        {
            CopyModel.EPayBudgetModelToDto(_ePayBudget, _ePayBudgetDto);
            _ePayBudgetDto.IsEPayBudget = true;
            return _ePayBudgetDto;
        }



    }
}