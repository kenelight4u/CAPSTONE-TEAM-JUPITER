using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JupiterCapstone.DTO.InputModels
{
    public class CardIM
    {
        public string Id { get; set; }


        public string CardHolderName { get; set; }


        public string CardNumber { get; set; }

        public DateTime ExpiryDate { get; set; }


        public int CVV { get; set; }


        public string UserId { get; set; }
    }
}
