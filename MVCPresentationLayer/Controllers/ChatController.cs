using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCPresentationLayer.Models.Chat;
using Service;
using Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCPresentationLayer.Controllers
{
    public class ChatController : Controller
    {
        private IChatService _chatService;
        private IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public ChatController(IChatService chatService, IMapper mapper, IHttpContextAccessor context)
        {
            this._chatService = chatService;
            this._mapper = mapper;
            this._accessor = context;
        }

        public async Task<IActionResult> Index()
        {
            DataResponse<Mensagem> response = await this._chatService.GetAll();//GetAll
            if (!response.Sucesso)
            {
                ViewBag.Errors = response.Mensagem;
            }
            List<ChatQueryViewModel> chats =
                _mapper.Map<List<ChatQueryViewModel>>(response.Data);

            return View(chats);
        }
    }
}
