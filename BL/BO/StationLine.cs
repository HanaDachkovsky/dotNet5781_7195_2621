﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BO
{
    public class StationLine
    {
        public int Id { get; set; }////proparty?
        public int Code { get; set; }
        public int LastStation { get; set; }
        public DateTime ArrivalTimes { get; set; }
        //מזהה קו, מספר קו, תחנת סיום. לבונוס - זמני הגעה לתחנה

    }
}