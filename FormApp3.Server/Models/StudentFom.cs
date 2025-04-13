using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FormApp3.Server.Models
{
    public class StudentForm
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //autoincrement
        public int Id { get; set; }
        public required string Nume { get; set; }
        public required string Prenume { get; set; }
        public required string Facultate { get; set; }

        [MinLength(100)]
        public required string Motivare { get; set; }
    }
}
