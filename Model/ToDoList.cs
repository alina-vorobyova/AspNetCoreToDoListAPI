﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoListAPI.Model
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public ICollection<ToDoItem> ToDoItem { get; set; }

    }
}
