using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace OOO_SportProduct.Classes
{
    internal class Helper
    {
         public static OOO_SportProducts.Model.DBsport403Entities dBSportEntities { get; set; } //доступное свойство связи с бд

        //Доступное свойство пользователя системы
        public static OOO_SportProducts.Model.User user { get; set; }
    }
}
