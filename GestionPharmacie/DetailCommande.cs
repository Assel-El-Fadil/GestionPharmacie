using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPharmacie
{
    public class DetailCommande
    {
        private static readonly string connectionString = "data source = DESKTOP-DDPB0HH\\GI2_1;initial catalog=AppPharmacie;" +
            "integrated security=True;TrustServerCertificate=True";

        public int MedicamentID { get; set; }
        public string NomMedicament { get; set; }
        public decimal Quantite { get; set; }
        public decimal PrixUnitaire { get; set; }
        public decimal SousTotal { get; set; }
    }
}
