﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyFastFood.DTO
{
    class Bill
    {

        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status,int discount = 0)
        {
            this.ID = id;
            this.DateCheckIN = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
            this.Discount = discount;

        }
        public Bill(DataRow row)
        {

            this.ID = (int)row["id"];
            this.DateCheckIN = (DateTime?)row["dateCheckIn"];
            var dateCheckOutTemp = row["dateCheckOut"];
            if (dateCheckOutTemp.ToString() !="")
            this.DateCheckOut = (DateTime?)dateCheckOutTemp;
            this.Status = (int)row["status"];
            if(row["discount"].ToString() != "")
            this.Discount = (int)row["discount"];
        }
        private int discount;
        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        private int status;
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        private DateTime? dateCheckOut;
        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }
        private DateTime? dateCheckIN;
        public DateTime? DateCheckIN
        {
            get { return dateCheckIN; }
            set { dateCheckIN = value; }
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        
    }
}
