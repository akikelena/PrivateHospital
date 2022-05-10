﻿using Projekat_SIMS_IN_TIM3.Controller;
using Projekat_SIMS_IN_TIM3.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
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

namespace Projekat_SIMS_IN_TIM3.ManagerWindows
{
    /// <summary>
    /// Interaction logic for RoomWindow.xaml
    /// </summary>
    public partial class RoomWindow : Window
    {
        RoomController roomController = new RoomController();

        AppointmentController appointmentController = new AppointmentController();
        public static ObservableCollection<Room> Rooms { get; set; }
        public RoomWindow()
        {
            InitializeComponent();
            this.roomController.UpdateDisabledFields();
            this.DataContext = this;
            Rooms = new ObservableCollection<Room>(roomController.GetAll());
            
        }

        private void Add_Room_Click(object sender, RoutedEventArgs e)
        {
            var addRoom = new AddRoom();
            addRoom.Show();
        }

        private void Edit_Room_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)((Button)e.Source).DataContext;
            int id = room.Id;
            var change = new EditRoomWindow(id);
            change.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)((Button)e.Source).DataContext;
            if(room.Id == 0)
            {
                MessageBox.Show("Default storage room cant be deleted!");
                return;
            }
            Rooms.Remove(room);
            if (EquipmentWindow.Equipment_All!=null)
            {
                foreach (var equipment in EquipmentWindow.Equipment_All.Where(x => x.RoomId == room.Id))
                {
                    equipment.RoomName = this.roomController.GetById(0).Name;
                }
            }
            this.roomController.DeleteById(room.Id);
        }

        private void Basic_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)((Button)e.Source).DataContext;
            var basic = new BasicRenovationWindow(room);
            basic.Show();
        }
    }
}
