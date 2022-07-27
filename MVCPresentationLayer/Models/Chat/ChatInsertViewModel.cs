using System;

namespace MVCPresentationLayer.Models.Chat
{
    public class ChatInsertViewModel
    {
        public int RemetenteID { get; set; }
        public Domain.Pessoa Remetente { get; set; }
        public int DestinatarioID { get; set; }
        public Domain.Pessoa Destinatario { get; set; }
        public DateTime Data { get; set; }
        public string Corpo { get; set; }

    }
}
