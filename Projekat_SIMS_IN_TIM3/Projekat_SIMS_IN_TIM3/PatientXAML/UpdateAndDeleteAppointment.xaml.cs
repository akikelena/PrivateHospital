﻿using Projekat_SIMS_IN_TIM3.Model;
using Projekat_SIMS_IN_TIM3.Controller;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace Projekat_SIMS_IN_TIM3.PatientXAML
{
    /// <summary>
    /// Interaction logic for UpdateAndDeleteAppointment.xaml
    /// </summary>
   
    public partial class UpdateAndDeleteAppointment : Window
    {

        public App application { get; set; }
        public List<Appointment> appointments { get; set; } = new List<Appointment>();
        public Appointment selectedAppointment { get; set; }
       /* public AppText appText { get; set; }
        public List<AppText> appTexts {get; set;} = new List<AppText>();*/
        public UpdateAndDeleteAppointment()
        {
            InitializeComponent();
            this.DataContext = this;

            application = Application.Current as App;
            appointments = application.appointmentController.GetAll();
            

            var appsNew = application.appointmentController.GetAll();
            
         
            DataBinding1.ItemsSource = appsNew;
        }

        public void Show(object sender, RoutedEventArgs e)
        {
            var appsNew = application.appointmentController.GetAll();
            DataBinding1.ItemsSource = appsNew;
        }

        public void Delete(object sender, RoutedEventArgs e)
        {
            application.appointmentController.DeleteAppointment(selectedAppointment.Id);
            MessageBox.Show("Successfully deleted appointment");

          
            startTime1.Value = default(DateTime);
        }

        public void Select(object sender, RoutedEventArgs e)
        {
            selectedAppointment = (Appointment)DataBinding1.SelectedItem;
            /*tb1.Text = selectedPatient.User.Name;
            tb2.Text = selectedPatient.User.Surname;
            tb3.Text = selectedPatient.User.Username;
            tb4.Text = selectedPatient.User.Jmbg;
            tb5.Text = selectedPatient.User.Password;
            tb6.Text = selectedPatient.User.Email;
            tb7.Text = selectedPatient.User.Address;
            tb8.Text = selectedPatient.User.Phone;
            dataofbirth1.SelectedDate = selectedPatient.User.DateOfBirth;*/

             
                
        }

        public void Update(object sender, RoutedEventArgs e)
        {

            DateTime startTime = (DateTime)startTime1.Value;

            TimeSpan diff = startTime.Subtract(DateTime.Now);

            int appointmentId = selectedAppointment.Id;
            Appointment currApp = application.appointmentController.GetById(appointmentId);

            DateTime initialDate = currApp.StartTime;

            TimeSpan initialDiff = startTime.Subtract(initialDate);

            if (diff.TotalDays < 1 || initialDiff.TotalDays > 4 || initialDiff.TotalMilliseconds < 0 || diff.TotalMilliseconds < 0)
            {
                if (initialDiff.TotalMilliseconds < 0 || diff.TotalMilliseconds < 0)
                {
                    MessageBox.Show("Cannot update backwards");

                }
                else
                {
                    if (diff.TotalDays < 1 && initialDiff.TotalDays <= 4)
                    {
                        MessageBox.Show("Too late to update appointment");
                    }
                    else if (initialDiff.TotalDays > 4 && diff.TotalDays >= 1)
                    {
                        MessageBox.Show("New date too far from initial date");
                    }
                    else
                    {
                        MessageBox.Show("Too late to update and new date too far from initial date");
                    }
                }
            }
            else
            {

                application.appointmentController.DeleteAppointment(appointmentId);
                var newAppointment = new Appointment(appointmentId, (DateTime)startTime1.Value, selectedAppointment.DurationInMinutes,
                    selectedAppointment.Type, selectedAppointment.DoctorId, selectedAppointment.PatientId);


                application.appointmentController.CreateAppointment(newAppointment);
                MessageBox.Show("Successfully updated appointment ");
            }
        }
    }

    
}
