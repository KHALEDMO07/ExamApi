﻿namespace Exam.Models
{


    public class Jwt
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int DurationInDays { get; set; }
    }
}