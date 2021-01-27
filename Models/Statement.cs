using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Statement
    {
        [Key]
        public int StatementID { get; set; }        
        [Display(Name ="Total")]       
        public string DoctorID { get; set; }
        public string UserID { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }
        public string ProcedureName { get; set; }
        public double Total { get; set; }
        public double DueAmount { get; set; }

        public double paymAmoount { get; set; }

        public string refNumber { get; set; }



        public string GeneratePassword()
        {
            //I started with creating three string variables.
            //This one tells you how many characters the string will contain.
            int PasswordLength = 8;

            //This one, is empty for now - but will ultimately hold the finished randomly generated password
            string NewPassword = "";

            //This one tells you which characters are allowed in this new password
            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

            //Then working with an array...

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);

            string IDString = "";
            string temp = "";

            //utilize the "random" class
            Random rand = new Random();

            //and lastly - loop through the generation process...
            for (int i = 0; i < (PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }

            return NewPassword;
        }




    }
}