﻿using Projekat_SIMS_IN_TIM3.ManagerWindows;
using Projekat_SIMS_IN_TIM3.Model;
using Projekat_SIMS_IN_TIM3.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_SIMS_IN_TIM3.Service
{
    public class RoomService
    {

        public int getMaxId()
        {
            return this.roomRepository.next_id();
        }
        public Room GetById(int id)
        {
            return this.roomRepository.GetById(id);
        }

        public List<Room> GetAll()
        {
            return this.roomRepository.GetAll();
        }

        public (bool,bool) Create(Room room)
        {
            var list = this.roomRepository.GetAll();
            foreach(var existingRoom in list)
            {
                if(existingRoom.Name == room.Name)
                {
                    return (false,false);
                }
            }
            return (this.roomRepository.Create(room),true);
        }

        public bool Update(Room room)
        {
            var list = this.roomRepository.GetAll();
            foreach (var existingRoom in list)
            {
                if (existingRoom.Name == room.Name)
                {
                    return false;
                }
            }
            return this.roomRepository.Update(room);    
        }

        public bool DeleteById(int id)
        {
            return this.roomRepository.DeleteById(id);
        }

        public List<RenovationTerm> BasicRenovation(int roomId, DateTime start, DateTime end,int duration)
        {
            var dates = new List<DateTime>();

            for (var dt = start; dt <= end; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }
            var appointments = this.appointmentRepository.GetAll();
            for (int i = 0; i < dates.Count;i++)
            {
                foreach(var appointment in appointments)
                {
                    if(dates[i] == appointment.StartTime && roomId == appointment.RoomNumber)
                    {
                        dates.RemoveAt(i);
                    }
                }
            }
            List<RenovationTerm> retVal = new List<RenovationTerm>();
            duration--;//first day is already included so we subrtact that day from total amount of days
            int renovationId=0;
            for (int i = 0; i < dates.Count-duration; i++)
            {
                if (dates[i].AddDays(duration) == dates[i + duration])
                {
                    retVal.Add(new RenovationTerm(renovationId++,dates[i].ToShortDateString(),dates[i+duration].ToShortDateString()));
                }
            }
            return retVal;
        }

        public bool ScheduleRenovation(int roomId, string start, string end)
        {
            return this.roomRepository.ScheduleRenovation(roomId, start, end);
        }

        public bool Split(int id)
        {
            return this.roomRepository.Split(id);
        }

        public bool Merge(int firstId, int secondId)
        {
            return this.roomRepository.Merge(firstId, secondId);
        }

        public Room GetByName(string name)
        {
            var list = this.roomRepository.GetAll();
            foreach(var item in list)
            {
                if(item.Name == name)
                {
                    return item;
                }
            }
            Debug.WriteLine("Room not found!");
            return null;
        }

        public RoomRepository roomRepository = new RoomRepository();
        public AppointmentRepository appointmentRepository = new AppointmentRepository();
        public DoctorRepository doctorRepository = new DoctorRepository();
    }
}
