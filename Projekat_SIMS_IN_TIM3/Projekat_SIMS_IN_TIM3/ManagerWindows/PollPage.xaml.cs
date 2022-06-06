﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Projekat_SIMS_IN_TIM3.Commands;
using Projekat_SIMS_IN_TIM3.Controller;
using Projekat_SIMS_IN_TIM3.Model;

namespace Projekat_SIMS_IN_TIM3.ManagerWindows
{
    /// <summary>
    /// Interaction logic for PollPage.xaml
    /// </summary>
    public partial class PollPage : Page
    {
        public DoctorController doctorController = new DoctorController();
        public ObservableCollection<Doctor> Doctors { get; set; } = new ObservableCollection<Doctor>();
        public List<HospitalGrade> HospitalGrades { get; set; } = new List<HospitalGrade>();
        public SeriesCollection DoctorAverageGrades { get; set; }
        public SeriesCollection HospitalGradesCount { get; set; }

        public HospitalGradeController hospitalGradeController;

        public DoctorGradeController doctorGradeController;
        public RelayCommand HospitalGradeChange { get; set; }
        public string[] DoctorLabels { get; set; } = new[] { "Knowledge", "Helpfulness", "Punctuality", "Pleasantness" };
        public string[] HospitalLabels { get; set; } = new[] { "Room", "Staff", "Hospitality", "Location", "Cleanliness" };

        public PollPage()
        {
            var app = Application.Current as App;
            this.hospitalGradeController = app.hospitalGradeController;
            this.doctorGradeController = app.doctorGradeController;
            HospitalGradeChange = new RelayCommand(HospitalGradeChanged);
            InitializeComponent();
            DoctorAverageGrades = new SeriesCollection
            {
            };
            HospitalGradesCount = new SeriesCollection
            {
            };
            this.HospitalGrades = this.hospitalGradeController.GetAll();
            this.Doctors = new ObservableCollection<Doctor>(this.doctorController.GetAll());
            float sum = 0;
            List<HospitalGrade> allHospitalGrades = this.hospitalGradeController.GetAll();
            int i;
            for (i = 0; i < allHospitalGrades.Count; i++)
            {
                sum += allHospitalGrades[i].StaffGrade+ allHospitalGrades[i].CleanlinessGrade+ allHospitalGrades[i].HospitalityGrade+ allHospitalGrades[i].LocationGrade+ allHospitalGrades[i].RoomGrade;
            }

            this.TotalAverageHospital.Content = Math.Round(sum / i / 5,1);
            this.DataContext = this;
        }

        private void Show_Stats_Click(object sender, RoutedEventArgs e)
        {
            Doctor doctor = (Doctor)((Button)e.Source).DataContext;
            List<DoctorGrade> DoctorGrades = this.doctorGradeController.GetAllByDoctorId(doctor.User.Id);
            DoctorGrade averageDoctorGrade = new DoctorGrade(doctor.User.Id, 0, 0, 0, 0);
            int i;
            for (i = 0; i < DoctorGrades.Count; i++)
            {
                averageDoctorGrade.knowledgeGrade += DoctorGrades[i].knowledgeGrade;
                averageDoctorGrade.helpfulnessGrade += DoctorGrades[i].helpfulnessGrade;
                averageDoctorGrade.punctualityGrade += DoctorGrades[i].punctualityGrade;
                averageDoctorGrade.pleasantnessGrade += DoctorGrades[i].pleasantnessGrade;
            }

            if (i == 0)
            {
                MessageBox.Show("Doctor doesn't have any grades!");
                return;
            }
            float knowledgeGrade = averageDoctorGrade.knowledgeGrade /= i;
            float helpfulnessGrade = averageDoctorGrade.helpfulnessGrade /= i;
            float punctualityGrade = averageDoctorGrade.punctualityGrade /= i;
            float pleasantnessGrade = averageDoctorGrade.pleasantnessGrade /= i;
            var add = new ColumnSeries
            {
                Title = doctor.User.Name,
                Values = new ChartValues<ObservableValue>
                {
                    new ObservableValue(knowledgeGrade),
                    new ObservableValue(helpfulnessGrade),
                    new ObservableValue(punctualityGrade),
                    new ObservableValue(pleasantnessGrade)
                }
            };
            this.DoctorGradeChart.Series.Add(add);
            this.TotalAverageDoctor.Content = (knowledgeGrade + helpfulnessGrade +
                                               punctualityGrade +
                                               pleasantnessGrade) / 4;
        }

        private void Clear_Doctor_Chart_Click(object sender, RoutedEventArgs e)
        {
            this.DoctorGradeChart.Series.Clear();
        }

        private void HospitalGradeChanged(object parameter)
        {
            this.PieHospitalGrades.Series.Clear();
            int selectedGrade = Int32.Parse((string)parameter);
            HospitalGrade grades = new HospitalGrade(0, 0, 0, 0, 0);
            foreach (var grade in HospitalGrades)
            {
                if (grade.RoomGrade == selectedGrade)
                {
                    grades.RoomGrade++;
                }
                if (grade.StaffGrade == selectedGrade)
                {
                    grades.StaffGrade++;
                }
                if (grade.HospitalityGrade == selectedGrade)
                {
                    grades.HospitalityGrade++;
                }
                if (grade.LocationGrade == selectedGrade)
                {
                    grades.LocationGrade++;
                }
                if (grade.CleanlinessGrade == selectedGrade)
                {
                    grades.CleanlinessGrade++;
                }
            }
            var toAdd = new SeriesCollection
            {
                new PieSeries
                {
                    Title= "Room Grade",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(grades.RoomGrade)},
                    DataLabels = true
                },
                new PieSeries
                {
                Title= "Staff Grade",
                Values = new ChartValues<ObservableValue> {new ObservableValue(grades.StaffGrade) },
                DataLabels = true
                },
                new PieSeries
                {
                    Title= "Hospitality Grade",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(grades.HospitalityGrade) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title= "Location Grade",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(grades.LocationGrade) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title= "Cleanliness Grade",
                    Values = new ChartValues<ObservableValue> {new ObservableValue(grades.CleanlinessGrade) },
                    DataLabels = true
                }
            };
            foreach (var ps in toAdd)
            {
                this.PieHospitalGrades.Series.Add(ps);
            }
        }
    }
}