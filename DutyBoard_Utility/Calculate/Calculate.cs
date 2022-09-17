using DutyBoard_Models;
using DutyBoard_Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
namespace DutyBoard_Utility.Calculate
{
    public class Calculate
    {
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        private readonly IEnumerable<Roster> _allRoster;
        private readonly IEnumerable<Employee> _allEmployees;
        private readonly IEnumerable<Holiday> _allHolidays;
        private readonly IEnumerable<Workday> _allWorkdays;
        private LinkList<Mapping> _dailyDuties;

        public Calculate(
            IEnumerable<Roster> allRoster,
            IEnumerable<Employee> allEmployees,
            IEnumerable<Holiday> allHolidays,
            IEnumerable<Workday> allWorkdays
            )
        {
            _allRoster = allRoster;
            _allEmployees = allEmployees;
            _allHolidays = allHolidays;
            _allWorkdays = allWorkdays;
        }
        public LinkList<Mapping> StartCalculate()
        {
            Init();
            CalcHandRoster();
            CalcAutoRoster();
            return _dailyDuties;
        }
        public LinkList<Mapping> StartCalculate(DateTime start, DateTime finish)
        {
            Start = start;
            Finish = finish;
            Init();
            CalcHandRoster();
            CalcAutoRoster();
            return _dailyDuties;
        }

        private IEnumerable<int> GetHoliDayEmployees(DateTime currDate, int roster)
        {
            var item = _allRoster.First(x => x.RosterId == roster);
            var finishDate = currDate.AddHours(item.DurationOfDuty);

            return _allHolidays.Where(x => x.DateStart <= finishDate && x.DateFinish >= currDate).Select(x => x.EmployeeId);
        }

        private void ResetCountDay(DateTime date)
        {
            foreach (var emp in _allEmployees)
            {
                emp.CountDuty = 0;
            }
            foreach (var item in _dailyDuties)
            {
                if (item.DateStart.GetWeekOfYear() == date.GetWeekOfYear() && item.EmployeeId != 0)
                    _allEmployees.First(x => x.EmployeeId == item.EmployeeId).CountDuty++;
            }
        }

        private void Init()
        {
            _dailyDuties = new LinkList<Mapping>();
            var currDate = Start;
            while (currDate <= Finish)
            {
                var currRoster = _allRoster.Where(x => x.DaysOfWeekId == currDate.GetDayOfWeek());
                foreach (var item in currRoster)
                {
                    _dailyDuties.Add(new Mapping(item.RosterId, currDate + item.StartTime));
                }
                currDate = currDate.AddDays(1);
            }
        }

        private void CalcHandRoster()
        {
            var workdaysWithAlways = _allWorkdays.Where(x => x.IsAlways);
            var workdaysWithDay = _allWorkdays.Where(x => !x.IsAlways);

            foreach (var item in _dailyDuties)
            {
                var holiDayEmployees = GetHoliDayEmployees(item.DateStart, item.RosterId);
                if (workdaysWithAlways.Any(x => x.RosterId == item.RosterId &&
                    !holiDayEmployees.Any(h => h == workdaysWithAlways.FirstOrDefault(w => w.RosterId == item.RosterId).EmployeeId)))
                {
                    item.EmployeeId = GetEmployeeHand(workdaysWithAlways.Where(x => x.RosterId == item.RosterId));
                    _allEmployees.First(x => x.EmployeeId == item.EmployeeId).CountDuty++;
                }

                if (workdaysWithDay.Any(x => x.RosterId == item.RosterId && x.StartDateWork == item.DateStart))
                {
                    item.EmployeeId = GetEmployeeHand(workdaysWithDay.Where(x => x.RosterId == item.RosterId && x.StartDateWork == item.DateStart));
                    _allEmployees.First(x => x.EmployeeId == item.EmployeeId).CountDuty++;
                }
            }
        }

        private void CalcAutoRoster()
        {
            var currNode = _dailyDuties.Head;
            if (_dailyDuties.Head == null) return;
            while (currNode != null)
            {
                if (currNode.Data.DateStart.GetDayOfWeek() == 1)
                    ResetCountDay(currNode.Data.DateStart);
                if (currNode.Data.EmployeeId == 0)
                {
                    currNode.Data.EmployeeId = GetFreeEmployee(currNode);
                    if (currNode.Data.EmployeeId != 0)
                        _allEmployees.FirstOrDefault(x => x.EmployeeId == currNode.Data.EmployeeId).CountDuty++;
                }
                currNode = currNode.Next;
            }
        }

        private int GetFreeEmployee(Node<Mapping> node)
        {
            var holiDayEmployees = GetHoliDayEmployees(node.Data.DateStart, node.Data.RosterId);
            var workEmployees = holiDayEmployees.Any() ? _allEmployees.Where(x => !(holiDayEmployees.Any(e => e == x.EmployeeId))) : _allEmployees;

            if (workEmployees.Any())
            {
                var count = 0;
                var allFreeEmployees = workEmployees;
                if (node.Previous != null && node.Next != null)
                {
                    count = workEmployees.Count(x => x.EmployeeId != node.Previous.Data.EmployeeId && x.EmployeeId != node.Next.Data.EmployeeId);
                    if (count > 0)
                        allFreeEmployees = workEmployees.Where(x => x.EmployeeId != node.Previous.Data.EmployeeId && x.EmployeeId != node.Next.Data.EmployeeId);
                }
                else if (node.Next != null)
                {
                    count = workEmployees.Count(x => x.EmployeeId != node.Next.Data.EmployeeId);
                    if (count > 0)
                        allFreeEmployees = workEmployees.Where(x => x.EmployeeId != node.Next.Data.EmployeeId);
                }
                else if (node.Previous != null)
                {
                    count = workEmployees.Count(x => x.EmployeeId != node.Previous.Data.EmployeeId);
                    if (count > 0)
                        allFreeEmployees = workEmployees.Where(x => x.EmployeeId != node.Previous.Data.EmployeeId);
                }

                var minCountDuty = allFreeEmployees.Min(x => x.CountDuty);
                return allFreeEmployees.Where(x => x.CountDuty == minCountDuty).Random().EmployeeId;
            }
            else
                return -1;
        }

        private int GetEmployeeHand(IEnumerable<Workday> workdays)
        {
            var employees = (
                from item in workdays 
                where _allEmployees.Any(x => x.EmployeeId == item.EmployeeId) 
                select _allEmployees.FirstOrDefault(x => x.EmployeeId == item.EmployeeId)
                ).ToList();
            var minCountDay = employees.Min(x => x.CountDuty);
            return employees.Where(x => x.CountDuty == minCountDay).Random().EmployeeId;
        }
    }
}
