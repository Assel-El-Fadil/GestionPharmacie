using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionPharmacie
{
    public class Utilisateur
    {
        public int UtilisateurId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string NomUtilisateur { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public string Adresse { get; set; }
        public string Ville { get; set; }
        public string Telephone { get; set; }
        public string Cin { get; set; }

        public Utilisateur(int UtilisateurID)
        {
                       UtilisateurId = UtilisateurID;
        }

        public Utilisateur(int utilisateurId, string nom, string prenom, string nomUtilisateur,
                           string email, string motDePasse, string adresse,
                           string ville, string telephone, string cin)
        {
            UtilisateurId = utilisateurId;
            Nom = nom;
            Prenom = prenom;
            NomUtilisateur = nomUtilisateur;
            Email = email;
            MotDePasse = motDePasse;
            Adresse = adresse;
            Ville = ville;
            Telephone = telephone;
            Cin = cin;
        }
    }
}
