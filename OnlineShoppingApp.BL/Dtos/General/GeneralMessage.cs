using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingApp.BL.Dtos;

    public class GeneralMessage
    {
    public string Message { get; set; }

        public GeneralMessage(string message) 
        { 
          Message = message;

        }
    }

