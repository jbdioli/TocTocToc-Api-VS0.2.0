using System;
using System.ComponentModel;
using System.Globalization;
using TocTocToc.Models.Dto;
using TocTocToc.Models.View;
using TocTocToc.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TocTocToc.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BudgetPopup : Xamarin.CommunityToolkit.UI.Views.Popup<BudgetDtoModel>
    {
        private static BudgetDtoModel _ePayBudgetDto = new();
        private readonly BudgetViewModel _ePayBudgetView = new();
        private DateTime _dateNow;
        private TimeSpan _timeNow;
        private DateTime _dateTimeRef;


        public BudgetPopup(BudgetDtoModel ePayBudget)
        {
            InitializeComponent();

            BindingContext = _ePayBudgetView;

            InitValues(ePayBudget);
        }


        private void InitValues(BudgetDtoModel ePayBudget)
        {

            _dateNow = DateTime.Now;
            _timeNow = _dateNow.TimeOfDay;
            _dateTimeRef = new DateTime(_dateNow.Year, _dateNow.Month, _dateNow.Day, _dateNow.Hour, _dateNow.Minute, 0);

            if (ePayBudget != null && ePayBudget.Budget != 0)
            {
                _ePayBudgetDto = ePayBudget;
                CopyModel.EPayBudgetDtoToView(_ePayBudgetDto, _ePayBudgetView);
            }
            else
            {
                _ePayBudgetView.StartDate = _dateNow;
                _ePayBudgetView.EndDate = _dateNow;
                _ePayBudgetView.Duration = "1";
                _ePayBudgetView.Budget = _ePayBudgetView.BudgetMini.ToString();
            }

            XNameSliderBudget.Value = double.Parse(_ePayBudgetView.Budget);
            XNameTimeStart.Time = _timeNow;
            XNameStartDate.MinimumDate = _dateNow;
            XNameEndDate.MinimumDate = _dateNow;
        }



        private void OnBudgetEntry(object sender, EventArgs e)
        {

            var entry = (Entry)sender;
            var budget = !string.IsNullOrEmpty(entry.Text) ? int.Parse(entry.Text) : _ePayBudgetView.BudgetMini;
            if (string.IsNullOrEmpty(entry.Text)) _ePayBudgetView.Budget = _ePayBudgetView.BudgetMini.ToString();
            if (budget > _ePayBudgetView.BudgetMaxi)
            {
                _ePayBudgetView.Budget = _ePayBudgetView.BudgetMaxi.ToString();
            }

            XNameSliderBudget.Value = budget;
        }


        private void OnIsBudgetEntry(object sender, EventArgs e)
        {
            _ePayBudgetView.IsBudgetEntry = !_ePayBudgetView.IsBudgetEntry;
        }


        private void OnSliderBudget(object sender, ValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            var budget = Math.Round(slider.Value);
            if (budget == 0) // To correct the error if slider.Minimum = 1;
            {
                budget = _ePayBudgetView.BudgetMini;
                XNameSliderBudget.Value = _ePayBudgetView.BudgetMini;
            }
            _ePayBudgetView.Budget = budget.ToString(CultureInfo.InvariantCulture);
        }

        private void OnPlusDay(object sender, EventArgs e)
        {
            var duration = 1;
            if (!string.IsNullOrEmpty(_ePayBudgetView.Duration))
                duration = int.Parse(_ePayBudgetView.Duration);
            var day = duration;
            ++day;
            _ePayBudgetView.Duration = day.ToString();
        }

        private void OnMinusDay(object sender, EventArgs e)
        {
            var duration = 1;
            if (!string.IsNullOrEmpty(_ePayBudgetView.Duration))
                duration = int.Parse(_ePayBudgetView.Duration);
            if (duration <= 0) return;

            var day = duration;
            if (day != 1)
                --day;
            _ePayBudgetView.Duration = day.ToString();
        }

        private void OnStartDate(object sender, DateChangedEventArgs e)
        {
            var datePicker = (DatePicker)sender;
            var startDate = datePicker.Date;

            var duration = !string.IsNullOrEmpty(_ePayBudgetView.Duration) ? int.Parse(_ePayBudgetView.Duration) : 0;

            _ePayBudgetView.StartDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0);
            var time = startDate.TimeOfDay;

            XNameEndDate.MinimumDate = startDate;
            XNameEndDate.Date = RecalculateDateFromDuration(startDate, duration);
            XNameTimeStart.Time = time;
        }


        private void OnEndDate(object sender, DateChangedEventArgs e)
        {

            var datePicker = (DatePicker)sender;
            var endDate = datePicker.Date;

            var duration = RecalculateDurationFromDate(_ePayBudgetView.StartDate, endDate);

            _ePayBudgetView.EndDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 0);
            _ePayBudgetView.Duration = duration.ToString();

        }


        private void OnTimeStart(object sender, PropertyChangedEventArgs e)
        {
            var timePicker = (TimePicker)sender;
            var time = timePicker.Time;
            var startDate = _ePayBudgetView.StartDate.Date;

            var selectedDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, time.Hours, time.Minutes, 0);

            _ePayBudgetView.StartDate = selectedDateTime;
            if (selectedDateTime.Year == 1) return;
            _ePayBudgetView.IsWrongTime = selectedDateTime < _dateTimeRef;
        }


        private void OnDuration(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            var duration = !string.IsNullOrEmpty(entry.Text) ? int.Parse(entry.Text) : 1;
            if (string.IsNullOrEmpty(entry.Text) || int.Parse(entry.Text) == 0)
            {
                _ePayBudgetView.Duration = "1";
                duration = 1;
            }
            XNameEndDate.Date = RecalculateDateFromDuration(_ePayBudgetView.StartDate, duration);
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


        protected override BudgetDtoModel GetLightDismissResult()
        {
            CopyModel.EPayBudgetViewToDto(_ePayBudgetView, _ePayBudgetDto);
            _ePayBudgetDto.IsEPayBudget = true;
            return _ePayBudgetDto;
        }



    }
}