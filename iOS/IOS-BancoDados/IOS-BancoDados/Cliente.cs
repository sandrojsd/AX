using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using SQLite;

namespace IOS_BancoDados
{
    public class Cliente
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }

        public String Nome { get; set; }
    }
}