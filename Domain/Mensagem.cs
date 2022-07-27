using System;

namespace Domain
{
    public class Mensagem
    {
        public int ID { get; set; }
        public int RemetenteID { get; set; }
        public Pessoa Remetente { get; set; }

        public int DestinatarioID { get; set; }
        public Pessoa Destinatario { get; set; }

        public DateTime Data { get; set; }
        public string Corpo { get; set; }


    }
}
