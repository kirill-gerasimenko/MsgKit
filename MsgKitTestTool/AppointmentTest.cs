﻿using MsgKit;
using MsgKit.Enums;
using System;

namespace MsgKitTestTool
{
    public class AppointmentTest
    {
        public void Run()
        {
            //using (var email = new Email(
            //        new Sender("peterpan@neverland.com", "Peter Pan"),
            //        new Representing("tinkerbell@neverland.com", "Tinkerbell"),
            //        "Hello Neverland subject"))
            //{

            //    email.Recipients.AddTo("captainhook@neverland.com", "Captain Hook");
            //    email.Recipients.AddCc("crocodile@neverland.com", "The evil ticking crocodile");
            //    email.Subject = "This is the subject";
            //    email.BodyText = "Hello Neverland text";
            //    email.BodyHtml = "<html><head></head><body><b>Hello Neverland html</b></body></html>";
            //    email.Importance = MessageImportance.IMPORTANCE_HIGH;
            //    email.IconIndex = MessageIconIndex.ReadMail;
            //    email.Attachments.Add(@"d:\crocodile.jpg");
            //    email.Save(@"c:\test.msg");

            //    // Show the message
            //    System.Diagnostics.Process.Start(@"c:\test.msg");
            //}
            
            using (var appointment = new Appointment(
                new Sender("peterpan@neverland.com", "Peter Pan"),
                new Representing("tinkerbell@neverland.com", "Tinkerbell"),
                "Hello Neverland subject")) 
            {
                appointment.Recipients.AddTo("captainhook@neverland.com", "Captain Hook");
                appointment.Recipients.AddCc("crocodile@neverland.com", "The evil ticking crocodile");
                appointment.Subject = "This is the subject";
                appointment.Location = "Neverland";
                appointment.MeetingStart = DateTime.Now.Date;
                appointment.MeetingEnd = DateTime.Now.Date.AddDays(1).Date;
                appointment.AllDay = true;
                appointment.BodyRtf = @"{\rtf1\ansi\deff0{\colortbl;\red0\green0\blue0;\red255\green0\blue0;}This line is the default color\line\cf2This line is red\line\cf1This line is the default color}";
                appointment.BodyRtfCompressed = true;
                appointment.BodyText = "Hello Neverland text";
                appointment.BodyHtml = "<html><head></head><body><b>Hello Neverland html</b></body></html>";
                appointment.SentOn = DateTime.UtcNow;
                appointment.Importance = MessageImportance.IMPORTANCE_NORMAL;
                appointment.IconIndex = MessageIconIndex.UnsentMail;
                appointment.Attachments.Add(@"d:\crocodile.jpg");
                appointment.Save(@"c:\test.msg");

                // Show the appointment
                System.Diagnostics.Process.Start(@"c:\test.msg");
            }

        }
    }
}
