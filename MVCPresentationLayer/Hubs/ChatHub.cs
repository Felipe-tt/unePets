using DataInfrastructure;
using Domain;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string idDoador, string message)
        {
            string teste = idDoador;
            int idRemetente = int.Parse(this.Context.User.Claims.ToList()[2].Value);
            string user = this.Context.User.Claims.ToList()[0].Value;

            Mensagem m = new Mensagem();
            m.Corpo = message;
            m.DestinatarioID = int.Parse(idDoador);
            m.RemetenteID = idRemetente;
            m.Data = System.DateTime.Now;

            using (UnePetsDBContext db = new UnePetsDBContext())
            {
                db.Mensagens.Add(m);
                await db.SaveChangesAsync();
            }

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
//A classe ChatHub herda da classe SignalR Hub. A classe Hub gerencia conexões, grupos e sistemas de mensagens.
//O método SendMessage pode ser chamado por um cliente conectado para enviar uma mensagem a todos os clientes. 
//O código cliente do JavaScript que chama o método é mostrado posteriormente no tutorial. SignalR O código
//é assíncrono para fornecer escalabilidade máxima.