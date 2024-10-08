﻿using MediMax.Data.Dao;
using MediMax.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace MediMax.Data.Models
{
    public class Notification
    {
        [Key]
        public int id { get; set; }
        public string titulo { get; set; }
        public string descricao { get; set; }
        public string horario { get; set; }
        public string tipo { get; set; }
        public int Treatment_id { get; set; }
        public int remedio_id { get; set; }
    }
}
